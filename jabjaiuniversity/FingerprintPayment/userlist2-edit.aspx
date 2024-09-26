<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="userlist2-edit.aspx.cs" Inherits="FingerprintPayment.userlist2_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js"></script>

    <script type="text/javascript">
        function changeFinger() {
            $userid = getUrlParameter("id");
            $.ajax("/App_Logic/deleteDataJSON.ashx?mode=delfinger&userid=" + $userid + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
                $('input[id*=btnDelFinger]').addClass("disabled");
            });
        }
        function ShowPopUP() {
            var name = $("input[id*=stdnameTh]").val() + " " + $("input[id*=stdlastTh]").val();
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#modalpopupdatamac .modal-footer").removeClass("hidden");
        }

        $(function () {
            $(".datepicker").datepicker();
            if ($("select[id*=ddlstatus]").val() !== "0") {
                $(".student_status").show();
                $("#labelstatus").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("select[id*=ddlstatus] option:selected").text())
            } else {
                $(".student_status").hide();
            }

            $("select[id*=ddlstatus]").change(function () {
                if ($("select[id*=ddlstatus]").val() !== "0") {
                    $(".student_status").show();
                    $("#labelstatus").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("select[id*=ddlstatus] option:selected").text())
                } else {
                    $(".student_status").hide();
                }
            });

            $("#btnroomchange").click(function () {
                if ($(".roomchange").css("display") === "none") {
                    $(".roomchange").show();
                } else {
                    $(".roomchange").hide();
                }
            });

            $("#ctl00_MainContent_Button1").click(function (e) {
                $('body').mLoading();
                
                $('#ctl00_MainContent_moveInDate').val($('#ctl00_MainContent_MID').val());
                $('#ctl00_MainContent_txtday').val($('#ctl00_MainContent_txtday2').val());
                $('#ctl00_MainContent_txtdayroomchange').val($('#ctl00_MainContent_txtdayroomchange2').val());


                var btnSave = document.getElementById('<%= Button1.ClientID %>').name;
                if ($("select[id*=ddlstatus]").val() !== "0") {
                    if ($("#<%= txtday.ClientID %>").val() === "") {
                        e.preventDefault();
                        $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131205") %>");
                        $("#modalpopupdatamac .modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                        $(".modal-footer .btn.btn-primary").hide();
                        $("#modalpopupdatamac").modal("show");
                        $('body').mLoading('hide');
                        return false;
                    }
                }
                if ($(".roomchange").css("display") !== "none") {
                    if ($('#<%= ddlSubLV2.ClientID %> option:selected').val() === $("#<%= ddlroomchange.ClientID %> option:selected").val() || $("#<%= ddlroomchange.ClientID %> option:selected").val() === "") {
                        e.preventDefault();
                        $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131206") %>");
                        $("#modalpopupdata-content .modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                        $(".modal-footer .btn.btn-primary").hide();
                        $("#modalpopupdatamac").modal("show");
                        return false;
                    }
                    else if ($("#<%= txtdayroomchange.ClientID %>").val() === "") {
                        e.preventDefault();
                        $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131207") %>");
                        $("#modalpopupdata-content .modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                        $(".modal-footer .btn.btn-primary").hide();
                        $("#modalpopupdatamac").modal("show");
                        return false; s
                    }
                } else {
                    $('#<%= ddlroomchange.ClientID %>').val("");
                }
                javascript: __doPostBack(btnSave, "");
            });
        });
    </script>
    <style type="text/css">
        body {
            background: #EAEAEA;
        }

        .user-details {
            position: relative;
            padding: 0;
        }

            .user-details .user-image {
                position: relative;
                z-index: 1;
                width: 100%;
                text-align: center;
            }

        .user-image img {
            clear: both;
            margin: auto;
            position: relative;
        }

        .pad0 {
            padding-left: 0px;
            padding-right: 0px;
        }

        .user-details .user-info-block {
            width: 100%;
            position: absolute;
            top: 100px;
            background: rgb(255, 255, 255);
            z-index: 0;
            /*padding-top: 35px;*/
        }

        .user-info-block .user-heading {
            width: 100%;
            text-align: center;
            margin: 10px 0 0;
        }

        .user-info-block .navigation {
            float: left;
            width: 100%;
            margin: 0;
            padding: 0;
            list-style: none;
            border-bottom: 1px solid #428BCA;
            border-top: 1px solid #428BCA;
        }

        .navigation li {
            float: left;
            margin: 0;
            padding: 0;
        }

            .navigation li a {
                padding: 20px 30px;
                float: left;
            }

            .navigation li.active a {
                background: #428BCA;
                color: #fff;
            }

        .user-info-block .user-body {
            float: left;
            padding: 5%;
            width: 90%;
        }

        .user-body .tab-content > div {
            float: left;
            width: 100%;
        }

        .user-body .tab-content h4 {
            width: 100%;
            margin: 10px 0;
            color: #333;
        }

        .hid {
            visibility: hidden;
        }

        .width100 {
            margin: 0 auto;
            width: 100%;
        }

        .contentBox {
            margin: 0 auto;
            width: 120%;
        }

            .contentBox .column70 {
                float: left;
                margin: 0;
                width: 55%;
            }

            .contentBox .column50 {
                float: left;
                margin: 0;
                width: 45%;
            }

        .circle-cropper {
            background-repeat: no-repeat;
            background-position: 50%;
            border-radius: 50%;
            width: 100px;
            height: 100px;
        }

        .righttext {
            position: relative;
            text-align: right;
        }

        .contentBox .column30 {
            float: left;
            margin: 0;
            width: 45%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="full-card box-content" style="margin-top: 10px; padding-top: 10px; padding-bottom: 2850px;">
        <div class="row">
            <div class="col-sm-12 col-md-12 user-details">
                <div class="col-sm-12 col-md-12 user-details">
                    <div class="col-sm-5 col-md-5">
                        <div class="user-image">
                            <asp:Image ID="profileimage" runat="server" Width="180" Height="180" />
                        </div>
                    </div>
                    <div class="col-sm-1 pad0 col-md-1 righttext">
                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131208") %></h3>
                    </div>
                    <div class="col-sm-4 col-md-4">
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" />
                    </div>
                </div>
                <div class="row--space">
                </div>
                <div class="user-info-block">
                    <div class="user-heading hid">
                        <h3>Karan Singh Sisodia</h3>
                        <span class="help-block">Chandigarh, IN</span>
                    </div>
                    <ul class="navigation">
                        <li class="active">
                            <a data-toggle="tab" href="#information" <%--onclick="changetab(1)"--%>>
                                <span class="fa fa-graduation-cap" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#app" <%--onclick="changetab(4)"--%>>
                                <span class="glyphicon glyphicon-book" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101170") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#family" <%--onclick="changetab(2)"--%>>
                                <span class="fa fa-user" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#health" <%--onclick="changetab(3)"--%>>
                                <span class="fa fa-heart" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>"></span>
                            </a>
                        </li>

                        <li>
                            <a data-toggle="tab" href="#confirm" <%--onclick="changetab(5)"--%>>
                                <span class="glyphicon glyphicon-ok" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121034") %>"></span>
                            </a>
                        </li>
                    </ul>
                    <div class="user-body" style="border-top: 0; padding-top: 0; margin-top: 0; border-left: 10px; margin-left: 30px; padding-left: 0;">
                        <div class="tab-content">
                            <div id="information" class="tab-pane active" style="border-top: 0; padding-top: 0; margin-top: 0;">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h1>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="contentBox">
                                            <div class="column70">
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdid" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="MID" runat="server" CssClass='form-control datepicker' class="input--mid" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input hidden">
                                                            <asp:TextBox ID="moveInDate" runat="server" CssClass='form-control datepicker' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101051") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="txtStudentNumber" runat="server" CssClass='form-control' class="input--mid" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlSubLV" runat="server" class="input--short" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubLV_Change">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                        <asp:HiddenField ID="hiddenfathertitle" runat="server" />
                                                        <asp:HiddenField ID="hiddenmothertitle" runat="server" />
                                                        <asp:HiddenField ID="hiddenfamtitle" runat="server" />
                                                        <asp:HiddenField ID="hiddenstdtitle" runat="server" />

                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="stdtitle" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                            <br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdnameTh" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>





                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                            <br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdnameEn" runat="server" CssClass='form-control' class="input--mid" onkeypress="return onlyAlphabets(event,this); " MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                            <br />
                                                            <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>)</sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdnameOther" runat="server" CssClass='form-control' class="input--mid" onkeypress="return onlyAlphabets(event,this); " MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>
                                                            <br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdnickname" runat="server" CssClass='form-control' class="input--mid" onkeypress="return onlyAlphabets(event,this);" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdphone" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 13%;">
                                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="width100 form-control">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                                <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="margin-left: -15px; padding-left: -15px; width: 25%;">
                                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="width100 form-control">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
                                                            <asp:DropDownList ID="ddlAge" runat="server" CssClass="width100 form-control" Style="margin-left: -15px; padding-left: -15px;">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdRace" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdReligion" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdSon" runat="server" CssClass='form-control' class="input--mid" MaxLength="3" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131209") %>"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>



                                                <h3 style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103066") %></h3>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdHome" runat="server" CssClass='form-control' class="input--mid" MaxLength="500"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdMuu" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlProvince" CssClass="form-control input--mid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlAumper" runat="server" CssClass='form-control' class="input--mid" AutoPostBack="True" OnSelectedIndexChanged="ddlAumper_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="sStudentHousePhone" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103071") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stayWithName" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103073") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stayWithEmail" runat="server" CssClass='form-control' class="input--mid" MaxLength="100"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>





                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %></label>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                            <asp:DropDownList ID="HomeType" runat="server" CssClass="width100 form-control">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103075") %>" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101159") %>" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %>" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %>" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101162") %>" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101163") %>" Value="5"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>

                                                <script type="text/javascript" language="javascript">


                                                    $(document).ready(function () {

                                                        var availableTags = [];


                                                        //$.get("/app_logic/studentlist.ashx", function (Result) {
                                                        //    $.each(Result, function (index) {
                                                        //        availableTags.push(Result[index].fullName);
                                                        //    });
                                                        //});

                                                        //$("#ctl00_MainContent_friendSID").autocomplete({
                                                        //    source: availableTags
                                                        //});


                                                    });


                                                </script>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103071") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="txtfriendName" runat="server" class="form-control inputname" Width="100%" MaxLength="50">                    
                                                        </asp:TextBox>

                                                    </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="visibility: hidden;">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103076") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="Textbox1" runat="server" class="form-control inputname" Width="100%">                    
                                                        </asp:TextBox>

                                                    </div>
                                                    </div>
                                                </div>

                                                <h3 style="text-align: center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103079") %></h3>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="houseRegistrationNumber" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="houseRegistrationSoy" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="houseRegistrationProvince" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="houseRegistrationProvince_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="houseRegistrationTumbon" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="houseRegistrationPhone" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="bornFromProvince" runat="server" CssClass="width100 form-control pageprovince" AutoPostBack="True" OnSelectedIndexChanged="bornFromProvince_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="bornFromTumbon" runat="server" CssClass="form-control pagetumbon"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>


                                                <!--70-->
                                            </div>
                                            <div class="column30">
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlstatus" CssClass="form-control input--mid" runat="server">
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %>" Value="0" Selected="True" />
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %>" Value="1" />
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>" Value="2" />
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %>" Value="3" />
                                                                <%--<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101030") %>" Value="4" />--%>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %>" Value="5" />
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %>" Value="6" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="optionCourse" runat="server" CssClass="width100 form-control">
                                                                <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103009") %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student student_status" style="margin-left: -15%; padding-left: -15%; display: none;">
                                                    <div class="col-md-12 col-sm-12 " style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext" id="labelstatus">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102097") %></label>
                                                         <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="txtday2" runat="server" CssClass="form-control datepicker" ReadOnly="true"/>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input hidden">
                                                            <asp:TextBox ID="txtday" runat="server" CssClass="form-control datepicker" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student student_status" style="margin-left: -15%; padding-left: -15%; display: none;">
                                                    <div class="col-md-12 col-sm-12 " style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox TextMode="MultiLine" ID="txtNote" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        &nbsp;
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlSubLV2" runat="server" class="input--short" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student roomchange" style="margin-left: -15%; padding-left: -15%; display: none;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131210") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlroomchange" runat="server" class="input--short" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student roomchange" style="margin-left: -15%; padding-left: -15%; display: none;">
                                                    <div class="col-md-12 col-sm-12 " style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111042") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="txtdayroomchange2" runat="server" CssClass="form-control datepicker" ReadOnly="true"/>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input hidden">
                                                            <asp:TextBox ID="txtdayroomchange" runat="server" CssClass="form-control datepicker" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="stdGender" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                            <br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdlastTh" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                            <br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdlastEn" runat="server" CssClass='form-control' class="input--mid" onkeypress="return onlyAlphabets(event,this);" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                            <br />
                                                            <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>)</sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdlastOther" runat="server" CssClass='form-control' class="input--mid" onkeypress="return onlyAlphabets(event,this);" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %>
                                                            <br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="sNickNameEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="text-align: right">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131181") %><br />
                                                            <sub style="font-size: 100%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131211") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdIdnumber" runat="server" CssClass='form-control' class="input--mid" MaxLength="13"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdEmail" runat="server" CssClass='form-control' class="input--mid" MaxLength="80"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdNation" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101111") %></label>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                            <asp:DropDownList ID="nSonTotal" runat="server" CssClass="width100 form-control pageson2">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103060") %>" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext" style="text-align: right">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131212") %><br />
                                                            <sub style="font-size: 100%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103063") %></sub></label>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 50%;">
                                                            <asp:DropDownList ID="nRelativeStudyHere" runat="server" CssClass="width100 form-control pageson3">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103064") %>" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %>" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>" Value="10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>

                                                <h3 class="hid">hidden</h3>

                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="sStudentHomeRegisterCode" runat="server" CssClass='form-control' class="input--mid" MaxLength="11"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdSoy" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdRoad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlTumbon" CssClass="form-control input--mid" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stdPost" runat="server" CssClass='form-control' class="input--mid" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103070") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="stayWithTitle" runat="server" CssClass="width100 form-control titlecheck">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103069") %><br />
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103072") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stayWithLast" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131214") %><br />
                                                            <sub style="font-size: 100%">/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="stayWithEmergencyCall" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103072") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="txtfriendLastname" runat="server" class="form-control inputname" Width="100%" MaxLength="50">                    
                                                        </asp:TextBox>

                                                    </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104034") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103077") %></sub></label></label>
                                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                        <asp:TextBox ID="txtfriendTel" runat="server" class="form-control inputname" Width="100%" MaxLength="50">                    
                                                        </asp:TextBox>

                                                    </div>
                                                    </div>
                                                </div>
                                                <h3 class="hid">hidden</h3>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="houseRegistrationMuu" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="houseRegistrationRoad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="houseRegistrationAumpher" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="houseRegistrationAumpher_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="houseRegistrationPost" runat="server" CssClass='form-control' class="input--mid" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103081") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="bornFrom" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student" style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pad0 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103080") %><br>
                                                            <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></sub></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="bornFromAumpher" runat="server" CssClass="form-control pageaumpher" AutoPostBack="True" OnSelectedIndexChanged="bornFromAumpher_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <asp:Literal runat="server" ID="ltrfinger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></asp:Literal>
                                                        </label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:Literal runat="server" ID="ltrpassword"></asp:Literal>
                                                            <asp:Button OnClientClick="ShowPopUP();return false;" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %>" runat="server"
                                                                CssClass="btn btn-info" data-toggle="modal"
                                                                data-target="#modalpopupdatamac" ID="btnDelFinger"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlProvince" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlAumper" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlSubLV" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <script>
                                function momCopyAddress() {
                                    var famHomenum = $('#ctl00_MainContent_famHomenum').val();

                                    var famSoy = $('#ctl00_MainContent_famSoy').val();

                                    var famMuu = $('#ctl00_MainContent_famMuu').val();

                                    var famRoad = $('#ctl00_MainContent_famRoad').val();

                                    var famProvince = $('#ctl00_MainContent_famProvince').val();

                                    var famaumpher = $('#ctl00_MainContent_famaumpher').val();

                                    var famTumbon = $('#ctl00_MainContent_famTumbon').val();

                                    var famPost = $('#ctl00_MainContent_famPost').val();

                                    $('#ctl00_MainContent_motherHome').val(famHomenum);
                                    $('#ctl00_MainContent_motherSoy').val(famSoy);
                                    $('#ctl00_MainContent_motherMuu').val(famMuu);
                                    $('#ctl00_MainContent_motherRoad').val(famRoad);
                                    $('#ctl00_MainContent_motherProvince').val(famProvince);
                                    $('#ctl00_MainContent_motherAumpher').val(famaumpher);
                                    $('#ctl00_MainContent_motherTumbon').val(famTumbon);
                                    $('#ctl00_MainContent_motherPost').val(famPost);
                                }

                                function dadCopyAddress() {
                                    var famHomenum = $('#ctl00_MainContent_famHomenum').val();

                                    var famSoy = $('#ctl00_MainContent_famSoy').val();

                                    var famMuu = $('#ctl00_MainContent_famMuu').val();

                                    var famRoad = $('#ctl00_MainContent_famRoad').val();

                                    var famProvince = $('#ctl00_MainContent_famProvince').val();

                                    var famaumpher = $('#ctl00_MainContent_famaumpher').val();

                                    var famTumbon = $('#ctl00_MainContent_famTumbon').val();

                                    var famPost = $('#ctl00_MainContent_famPost').val();

                                    $('#ctl00_MainContent_fatherHome').val(famHomenum);
                                    $('#ctl00_MainContent_fatherSoy').val(famSoy);
                                    $('#ctl00_MainContent_fatherMuu').val(famMuu);
                                    $('#ctl00_MainContent_fatherRoad').val(famRoad);
                                    $('#ctl00_MainContent_fatherProvince').val(famProvince);
                                    $('#ctl00_MainContent_fatherAumpher').val(famaumpher);
                                    $('#ctl00_MainContent_fatherTumbon').val(famTumbon);
                                    $('#ctl00_MainContent_fatherPost').val(famPost);
                                }

                            </script>
                            <div id="family" class="tab-pane">
                                <div class="step2">
                                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></h1>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-xs-12 center">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></label>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 pad0 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="famTitle" runat="server" CssClass="width100 form-control famTitle">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 pad0 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famName" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 pad0 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famLast" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 pad0 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFamilyNameEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 pad0 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFamilyLastEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                                <asp:DropDownList ID="dFamilyBirthDayDD" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input pad0" style="margin-left: 3px;">
                                                                <asp:DropDownList ID="dFamilyBirthDayMM" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 3px;">
                                                                <asp:DropDownList ID="dFamilyBirthDayYY" runat="server" CssClass="width100 form-control" Style="">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 pad0 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famIdCard" runat="server" CssClass='form-control' class="input--mid" MaxLength="13"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 pad0 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famCeerChad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 pad0 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famSunChad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 pad0 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famReligion" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 pad0 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></label>
                                                            <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                                                                <asp:RadioButtonList ID="RadioButtonList1" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>&nbsp;</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext" style="text-align: center">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101197") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input" style="">
                                                                <asp:DropDownList ID="nFamilyRequestStudyMoney" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103100") %>" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101199") %>" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101200") %>" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-xs-4 control-label righttext pad0">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                            <div class="col-xs-8 control-input">
                                                                <asp:DropDownList ID="sFamilyGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> " Value="4 "></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>" Value="6"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-xs-4 control-label righttext pad0">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %></label>
                                                            <div class="col-xs-8 control-input">
                                                                <asp:DropDownList ID="familyStatus" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101202") %>" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101203") %>" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101204") %>" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101205") %>" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %>" Value="4 "></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %>" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101207") %>" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101208") %>" Value="7"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famHomenum" runat="server" CssClass='form-control famHome' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famSoy" runat="server" CssClass='form-control famSoy' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famMuu" runat="server" CssClass='form-control famMuu' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famRoad" runat="server" CssClass='form-control famRoad' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="famProvince" AutoPostBack="true" OnSelectedIndexChanged="famProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control famProvince">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="famaumpher" runat="server" CssClass='form-control famAumpher' OnSelectedIndexChanged="famaumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="famTumbon" runat="server" CssClass='form-control famTumbon' class="input--mid"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPost" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFamilyJob" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFamilyWorkPlace" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                               <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                                <asp:TextBox ID="nFamilyIncome" runat="server" CssClass='form-control famPost' class="input--mid" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131215") %>" MaxLength="15"></asp:TextBox>
                                                            </div>
                                                            <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPhone1" runat="server" CssClass='form-control' class="input--mid" MaxLength="15"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPhone2" runat="server" CssClass='form-control' class="input--mid" MaxLength="15"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPhone3" runat="server" CssClass='form-control' class="input--mid" MaxLength="15"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <hr />
                                            </div>
                                            <div class="col-xs-3 center">
                                                <label></label>
                                            </div>
                                            <div class="col-xs-6 center">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></label>
                                            </div>
                                            <div class="col-xs-3 center">
                                                <button class="btn btn-info" style="margin: 5px 0;"
                                                    data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131182") %>  <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131183") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131184") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131185") %>  <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131186") %>" data-html="true" data-placement="right" data-toggle="tooltip"
                                                    onclick="dadCopyAddress();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131187") %></button>    
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="fatherTitle" runat="server" CssClass="width100 form-control fatherTitle">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherName" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherLast" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherNameEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherLastEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                                <asp:DropDownList ID="dFatherBirthDayDD" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input pad0" style="margin-left: 3px;">
                                                                <asp:DropDownList ID="dFatherBirthDayMM" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 3px;">
                                                                <asp:DropDownList ID="dFatherBirthDayYY" runat="server" CssClass="width100 form-control" Style="">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherIdCard" runat="server" CssClass='form-control' class="input--mid" MaxLength="13"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherCeerChad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherSunChad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherReligion" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                            <div class="col-xs-8 control-input">
                                                                <asp:DropDownList ID="sFatherGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> " Value="4 "></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>" Value="6"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherHome" runat="server" CssClass='form-control fatherHome' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherSoy" runat="server" CssClass='form-control fatherSoy' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherMuu" runat="server" CssClass='form-control fatherMuu' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherRoad" runat="server" CssClass='form-control fatherRoad' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherProvinceStatus2 hidden">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass='form-control fatherProvince2 disable' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherAumpherStatus2 hidden">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="TextBox4" runat="server" CssClass='form-control fatherAumpher2 disable' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherTumbonStatus2 hidden">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="TextBox5" runat="server" CssClass='form-control fatherTumbon2 disable' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherProvinceStatus">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="fatherProvince" AutoPostBack="true" OnSelectedIndexChanged="fatherProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control fatherProvince">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherAumpherStatus">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="fatherAumpher" runat="server" CssClass='form-control fatherAumpher' OnSelectedIndexChanged="fatheraumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherTumbonStatus">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="fatherTumbon" runat="server" CssClass='form-control fatherTumbon' class="input--mid"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherPost" runat="server" CssClass='form-control fatherPost' class="input--mid" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherJob" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherWorkPlace" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                               <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                                <asp:TextBox ID="nFatherIncome" runat="server" CssClass='form-control famPost' class="input--mid" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131215") %>" MaxLength="15"></asp:TextBox>
                                                            </div>
                                                            <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherPhone" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherPhone2" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sFatherPhone3" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <hr />
                                            </div>
                                            <div class="col-xs-3 center">
                                                <label></label>
                                            </div>
                                            <div class="col-xs-6 center">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></label>
                                            </div>
                                            <div class="col-xs-3 center">
                                                <button class="btn btn-info" style="margin: 5px 0;" 
                                                    data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131182") %>  <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131183") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131184") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131185") %>  <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131186") %>" data-html="true" data-placement="right" data-toggle="tooltip"
                                                    onclick="momCopyAddress();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131187") %></button>
                                            </div>

                                            <div class="col-xs-12">
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="motherTitle" runat="server" CssClass="width100 form-control motherTitle">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherName" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherLast" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherNameEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                                <br />
                                                                <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherLastEN" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 pad0 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 15px;">
                                                                <asp:DropDownList ID="dMotherBirthDayDD" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input pad0" style="margin-left: 3px;">
                                                                <asp:DropDownList ID="dMotherBirthDayMM" runat="server" CssClass="width100 form-control">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input pad0" style="margin-left: 3px;">
                                                                <asp:DropDownList ID="dMotherBirthDayYY" runat="server" CssClass="width100 form-control" Style="">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherIdCard" runat="server" CssClass='form-control' class="input--mid" MaxLength="13"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherCeerChad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherSunChad" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherReligion" runat="server" CssClass='form-control' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                            <div class="col-xs-8 control-input">
                                                                <asp:DropDownList ID="sMotherGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %>" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %>" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %>" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102059") %> " Value="4 "></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %>" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02176") %>" Value="6"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherHome" runat="server" CssClass='form-control motherHome' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherSoy" runat="server" CssClass='form-control motherSoy' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherMuu" runat="server" CssClass='form-control motherMuu' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherRoad" runat="server" CssClass='form-control motherRoad' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherProvinceStatus2 hidden">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="TextBox11" runat="server" CssClass='form-control motherProvince2 disable' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherAumpherStatus2 hidden">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="TextBox14" runat="server" CssClass='form-control motherAumpher2 disable' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherTumbonStatus2 hidden">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="TextBox18" runat="server" CssClass='form-control motherTumbon2 disable' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherProvinceStatus">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="motherProvince" AutoPostBack="true" OnSelectedIndexChanged="motherProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control motherProvince">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherAumpherStatus">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="motherAumpher" runat="server" CssClass='form-control motherAumpher' OnSelectedIndexChanged="motheraumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherTumbonStatus">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:DropDownList ID="motherTumbon" runat="server" CssClass='form-control motherTumbon' class="input--mid"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherPost" runat="server" CssClass='form-control motherPost' class="input--mid" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherJob" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherWorkPlace" runat="server" CssClass='form-control famPost' class="input--mid" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                               <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104044") %></label>
                                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                                <asp:TextBox ID="nMotherIncome" runat="server" CssClass='form-control famPost' class="input--mid" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131215") %>" MaxLength="17"></asp:TextBox>
                                                            </div>
                                                            <label class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherPhone" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherPhone2" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 pad0 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %><br />
                                                                <sub>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131217") %>)</sub></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="sMotherPhone3" runat="server" CssClass='form-control' class="input--mid" MaxLength="20"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="famProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="famaumpher" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="motherProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="motherAumpher" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="fatherProvince" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="fatherAumpher" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div id="health" class="tab-pane">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></h1>

                                <div class="contentBox">

                                    <div class="col-xs-12" style="width: 800px; margin-left: 6%;">
                                        <canvas id="myChart" width="800" height="450"></canvas>
                                    </div>
<i class="fa fa-question-circle" style="font-size: 18px;" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101210") %>" data-placement="top" data-toggle="tooltip"></i>

                                    <div class="col-xs-12" style="padding: 0px; width: 105%; margin-left: -2%;">
                                        <style type="text/css">
                                            .table2 {
                                                border-collapse: collapse;
                                                width: 87%;
                                                font-size: 90%;
                                            }

                                            .headerCell {
                                                background-color: #337AB7;
                                                color: White;
                                                height: 65px;
                                                font-size: 25px;
                                                font-weight: bold;
                                            }

                                            .tdtr {
                                                border: 1px solid #000000;
                                                text-align: center;
                                                padding: 1px;
                                                width: 35px;
                                            }

                                            .noborder {
                                                border: 0px;
                                                background-color: white;
                                            }

                                            .disable {
                                                pointer-events: none;
                                                border: 1px solid #337AB7;
                                                background-color: #337AB7;
                                                color: White;
                                                font-size: 25px;
                                                font-weight: bold;
                                                width: 100%;
                                            }

                                            .disable2 {
                                                pointer-events: none;
                                            }

                                            .smol {
                                                font-size: 90%;
                                                padding: 0px;
                                                padding-right: 0px;
                                                padding-left: 0px;
                                                width: 100%;
                                                height: 37px;
                                                border: none;
                                                text-align: center;
                                            }

                                            .smol2 {
                                                font-size: 90%;
                                            }

                                            .eee {
                                                background-color: #eee;
                                            }

                                            .centertext {
                                                text-align: center;
                                            }
                                        </style>
                                        <script type="text/javascript" language="javascript">
                                            function start() {

                                                var txtcheck = document.getElementsByClassName("txtcheck");

                                                var del4 = document.getElementsByClassName("del4");
                                                var del6 = document.getElementsByClassName("del6");

                                                if (txtcheck[0].value == "4") {
                                                    for (var x = 0; x < del4.length; x++)
                                                        del4[x].classList.add('hidden');
                                                    for (var y = 0; y < del6.length; y++)
                                                        del6[y].classList.add('hidden');
                                                }
                                                if (txtcheck[0].value == "6") {
                                                    for (var z = 0; z < del6.length; z++)
                                                        del6[z].classList.add('hidden');
                                                }

                                                drawchart();

                                            }

                                            function drawchart() {

                                                

                                                var smol = $('.smol').text().split("ด.");
                                                var array = [];

                                                for (var i = 0; i < smol.length - 1; i++) {
                                                    if(smol[0]!="") array[i]=smol[i].replace(" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %> ","/");
                                                }
                                                //console.log(array); 
                                                var weight = document.getElementsByClassName("weight");
                                                var height = document.getElementsByClassName("height");

                                                var array2 = [];

                                                if (weight[0].value != "" && height[0] != "")
                                                    array2[0] = Number(weight[0].value) / ((Number(height[0].value) / 100) * (Number(height[0].value) / 100));
                                                else array2[0] = 0;
                                                if (weight[1].value != "" && height[1] != "")
                                                    array2[1] = Number(weight[1].value) / ((Number(height[1].value) / 100) * (Number(height[1].value) / 100));
                                                else array2[1] = 0;
                                                if (weight[2].value != "" && height[2] != "")
                                                    array2[2] = Number(weight[2].value) / ((Number(height[2].value) / 100) * (Number(height[2].value) / 100));
                                                else array2[2] = 0;
                                                if (weight[3].value != "" && height[3] != "")
                                                    array2[3] = Number(weight[3].value) / ((Number(height[3].value) / 100) * (Number(height[3].value) / 100));
                                                else array2[3] = 0;
                                                if (weight[4].value != "" && height[4] != "")
                                                    array2[4] = Number(weight[4].value) / ((Number(height[4].value) / 100) * (Number(height[4].value) / 100));
                                                else array2[4] = 0;
                                                if (weight[5].value != "" && height[5] != "")
                                                    array2[5] = Number(weight[5].value) / ((Number(height[5].value) / 100) * (Number(height[5].value) / 100));
                                                else array2[5] = 0;
                                                if (weight[6].value != "" && height[6] != "")
                                                    array2[6] = Number(weight[6].value) / ((Number(height[6].value) / 100) * (Number(height[6].value) / 100));
                                                else array2[6] = 0;
                                                if (weight[7].value != "" && height[7] != "")
                                                    array2[7] = Number(weight[7].value) / ((Number(height[7].value) / 100) * (Number(height[7].value) / 100));
                                                else array2[7] = 0;
                                                if (weight[8].value != "" && height[8] != "")
                                                    array2[8] = Number(weight[8].value) / ((Number(height[8].value) / 100) * (Number(height[8].value) / 100));
                                                else array2[8] = 0;
                                                if (weight[9].value != "" && height[9] != "")
                                                    array2[9] = Number(weight[9].value) / ((Number(height[9].value) / 100) * (Number(height[9].value) / 100));
                                                else array2[9] = 0;
                                                if (weight[10].value != "" && height[10] != "")
                                                    array2[10] = Number(weight[10].value) / ((Number(height[10].value) / 100) * (Number(height[10].value) / 100));
                                                else array2[10] = 0;
                                                if (weight[11].value != "" && height[11] != "")
                                                    array2[11] = Number(weight[11].value) / ((Number(height[11].value) / 100) * (Number(height[11].value) / 100));
                                                else array2[11] = 0;
                                                if (weight[12].value != "" && height[12] != "")
                                                    array2[12] = Number(weight[12].value) / ((Number(height[12].value) / 100) * (Number(height[12].value) / 100));
                                                else array2[12] = 0;
                                                if (weight[13].value != "" && height[13] != "")
                                                    array2[13] = Number(weight[13].value) / ((Number(height[13].value) / 100) * (Number(height[13].value) / 100));
                                                else array2[13] = 0;
                                                if (weight[14].value != "" && height[14] != "")
                                                    array2[14] = Number(weight[14].value) / ((Number(height[14].value) / 100) * (Number(height[14].value) / 100));
                                                else array2[14] = 0;
                                                if (weight[15].value != "" && height[15] != "")
                                                    array2[15] = Number(weight[15].value) / ((Number(height[15].value) / 100) * (Number(height[15].value) / 100));
                                                else array2[15] = 0;
                                                if (weight[16].value != "" && height[16] != "")
                                                    array2[16] = Number(weight[16].value) / ((Number(height[16].value) / 100) * (Number(height[16].value) / 100));
                                                else array2[16] = 0;
                                                if (weight[17].value != "" && height[17] != "")
                                                    array2[17] = Number(weight[17].value) / ((Number(height[17].value) / 100) * (Number(height[17].value) / 100));
                                                else array2[17] = 0;
                                                if (weight[18].value != "" && height[18] != "")
                                                    array2[18] = Number(weight[18].value) / ((Number(height[18].value) / 100) * (Number(height[18].value) / 100));
                                                else array2[18] = 0;
                                                if (weight[19].value != "" && height[19] != "")
                                                    array2[19] = Number(weight[19].value) / ((Number(height[19].value) / 100) * (Number(height[19].value) / 100));
                                                else array2[19] = 0;
                                                if (weight[20].value != "" && height[20] != "")
                                                    array2[20] = Number(weight[20].value) / ((Number(height[20].value) / 100) * (Number(height[20].value) / 100));
                                                else array2[20] = 0;
                                                if (weight[21].value != "" && height[21] != "")
                                                    array2[21] = Number(weight[21].value) / ((Number(height[21].value) / 100) * (Number(height[21].value) / 100));
                                                else array2[21] = 0;
                                                if (weight[22].value != "" && height[22] != "")
                                                    array2[22] = Number(weight[22].value) / ((Number(height[22].value) / 100) * (Number(height[22].value) / 100));
                                                else array2[22] = 0;
                                                if (weight[23].value != "" && height[23] != "")
                                                    array2[23] = Number(weight[23].value) / ((Number(height[23].value) / 100) * (Number(height[23].value) / 100));
                                                else array2[23] = 0;

                                                //console.log(array2);



                                                var chart = {
                                                    type: 'line',
                                                    data: {
                                                        labels: array,
                                                        datasets: [{
                                                            label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111032") %>',
                                                            data: [18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5, 18.5,],
                                                            fill: 'origin',
                                                            pointStyle: 'dash',
                                                            borderWidth: 1,
                                                            backgroundColor: 'rgba(192,192,192,0.3)'
                                                        },
                                                        {
                                                            label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>',
                                                            data: [25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25],
                                                            borderWidth: 1,
                                                            pointStyle: 'dash',
                                                            backgroundColor: 'rgba(0,255,0,0.3)'
                                                        },
                                                        {
                                                            label: 'อ้วนระดับ1',
                                                            data: [30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,],
                                                            borderWidth: 1,
                                                            pointStyle: 'dash',
                                                            backgroundColor: 'rgba(255,255,0,0.3)'
                                                        },
                                                        {
                                                            label: 'อ้วนระดับ2',
                                                            data: [35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35],
                                                            borderWidth: 1,
                                                            pointStyle: 'dash',
                                                            backgroundColor: 'rgba(255,122,0,0.3)'
                                                        },
                                                        {
                                                            label: 'อ้วนระดับ3',
                                                            data: [40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40],
                                                            borderWidth: 1,
                                                            pointStyle: 'dash',
                                                            backgroundColor: 'rgba(255,0,0,0.3)'
                                                        },
                                                        {
                                                            label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111031") %>',
                                                            data: array2,
                                                            borderWidth: 2,
                                                            pointStyle: 'circle',
                                                            backgroundColor: 'rgba(0,0,0,0.0)',
                                                            borderColor: '#c45850'
                                                        },


                                                        ]
                                                    },
                                                    options: {
                                                        elements: {
                                                            line: {
                                                                fill: '-1'
                                                            }
                                                        },
                                                        scales: {
                                                            yAxes: [{
                                                                scaleLabel: {
                                                                    display: true,
                                                                    labelString: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111036") %>'
                                                                }
                                                            }],
                                                            xAxes: [{
                                                                scaleLabel: {
                                                                    display: true,
                                                                    labelString: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %>'
                                                                }
                                                            }]
                                                        },
                                                        title: {
                                                            display: true,
                                                            text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111038") %>'
                                                        },
                                                        legend: {
                                                            onClick: null
                                                        },
                                                        animation: {
                                                            duration: 0 // general animation time
                                                        },
                                                        hover: {
                                                            animationDuration: 0 // duration of animations when hovering an item
                                                        },
                                                        responsiveAnimationDuration: 0 // animation duration after a resize
                                                    }
                                                }

                                                var ctx = document.getElementById('myChart').getContext('2d');
                                                new Chart(ctx, chart);

                                            }

                                            window.onload = start;
                                        </script>
                                        <div class="col-xs-12 hidden">hidden<asp:TextBox ID="txthidden" runat="server" CssClass="centertext txtcheck" class="input--mid"></asp:TextBox></div>
                                        <table class="table2">
                                            <tr class="tdtr">
                                                <th class="tdtr headerCell" colspan="25"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101211") %><br>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td class="tdtr headerCell"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101212") %><br>
                                                </td>
                                                <td class="tdtr headerCell" colspan="4">
                                                    <asp:TextBox ID="growClass1" runat="server" CssClass="noborder disable centertext" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell" colspan="4">
                                                    <asp:TextBox ID="growClass2" runat="server" CssClass="noborder disable centertext" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell" colspan="4">
                                                    <asp:TextBox ID="growClass3" runat="server" CssClass="noborder disable centertext" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4" colspan="4">
                                                    <asp:TextBox ID="growClass4" runat="server" CssClass="noborder disable centertext" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4" colspan="4">
                                                    <asp:TextBox ID="growClass5" runat="server" CssClass="noborder disable centertext" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del6" colspan="4">
                                                    <asp:TextBox ID="growClass6" runat="server" CssClass="noborder disable centertext" class="input--mid"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="tdtr headerCell"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth11" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth12" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth13" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth14" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth21" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth22" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth23" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth24" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth31" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth32" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth33" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell">
                                                    <asp:TextBox ID="growMonth34" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth41" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth42" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth43" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth44" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth51" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth52" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth53" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del4">
                                                    <asp:TextBox ID="growMonth54" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del6">
                                                    <asp:TextBox ID="growMonth61" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del6">
                                                    <asp:TextBox ID="growMonth62" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del6">
                                                    <asp:TextBox ID="growMonth63" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                                <td class="tdtr headerCell del6">
                                                    <asp:TextBox ID="growMonth64" runat="server" CssClass="noborder disable centertext smol2" class="input--mid"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="tdtr" style="font-size: 80%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101214") %></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age11" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age12" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age13" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age14" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age21" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age22" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age23" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age24" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age31" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age32" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age33" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr">
                                                    <asp:Label ID="age34" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age41" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age42" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age43" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age44" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age51" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age52" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age53" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del4">
                                                    <asp:Label ID="age54" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del6">
                                                    <asp:Label ID="age61" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del6">
                                                    <asp:Label ID="age62" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del6">
                                                    <asp:Label ID="age63" runat="server" CssClass="smol disable2"></asp:Label></td>
                                                <td class="tdtr del6">
                                                    <asp:Label ID="age64" runat="server" CssClass="smol disable2"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="tdtr"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101215") %></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight11" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight12" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight13" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight14" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight21" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight22" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight23" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight24" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight31" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight32" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight33" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="weight34" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight41" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight42" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight43" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight44" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight51" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight52" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight53" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="weight54" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="weight61" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="weight62" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="weight63" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="weight64" runat="server" CssClass="form-control weight" onkeypress='validate(event)'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="tdtr"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101216") %></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height11" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height12" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height13" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height14" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height21" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height22" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height23" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height24" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height31" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height32" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height33" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr">
                                                    <asp:TextBox ID="height34" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height41" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height42" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height43" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height44" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height51" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height52" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height53" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del4">
                                                    <asp:TextBox ID="height54" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="height61" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="height62" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="height63" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                                <td class="tdtr del6">
                                                    <asp:TextBox ID="height64" runat="server" CssClass="form-control height" onkeypress='validate(event)'></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-xs-12 hid">
                                        <h5>hidden</h5>
                                    </div>
                                    <div class="column70">
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:DropDownList ID="blood" runat="server" class="input--short" CssClass="form-control">
                                                        <asp:ListItem Enabled="true" Text="- <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131223") %> -" Value="" class="grey"></asp:ListItem>
                                                        <asp:ListItem Text="A" Value="A" class="grey"></asp:ListItem>
                                                        <asp:ListItem Text="B" Value="B" class="grey"></asp:ListItem>
                                                        <asp:ListItem Text="AB" Value="AB" class="grey"></asp:ListItem>
                                                        <asp:ListItem Text="O" Value="O" class="grey"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 pad0 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101219") %></label>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="sickFood" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="sickDrug" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="sickOther" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="sicknoemal" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="sickDanger" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <!--70-->
                                    </div>
                                </div>
                            </div>
                            <div id="app" class="tab-pane">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131218") %></h1>
                                <div class="">
                                    <div class="">
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %></label>
                                                <div class="col-xs-5 control-input">
                                                    <asp:TextBox ID="oldSchoolName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group row student motherProvinceStatus">
                                                            <div class="col-md-12 col-sm-12">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                                    <asp:DropDownList ID="oldSchoolProvince" AutoPostBack="true" OnSelectedIndexChanged="oldschoolProvince_SelectedIndexChanged" runat="server" CssClass="width100 form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row student motherAumpherStatus">
                                                            <div class="col-md-12 col-sm-12">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                                    <asp:DropDownList ID="oldSchoolAumpher" runat="server" CssClass='form-control' OnSelectedIndexChanged="oldschoolaumpher_SelectedIndexChanged" class="input--mid" AutoPostBack="True"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row student motherTumbonStatus">
                                                            <div class="col-md-12 col-sm-12">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                                                                    <asp:DropDownList ID="oldSchoolTumbon" runat="server" CssClass='form-control' class="input--mid"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="oldSchoolProvince" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="oldSchoolAumpher" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103097") %></label>
                                                <div class="col-xs-5 control-input">
                                                    <asp:TextBox ID="oldSchoolGPA" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                <div class="col-xs-5 control-input">
                                                    <asp:DropDownList ID="oldSchoolGraduated" runat="server" CssClass="form-control oGradu pagegradu">
                                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131188") %>" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131189") %>" Value="12"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131190") %>" Value="13"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131191") %>" Value="14"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131192") %>" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131193") %>" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131194") %>" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131195") %>" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131196") %>" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206406") %>" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131198") %>" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131199") %>" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131200") %>" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131201") %>" Value="15"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131202") %>" Value="16"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131203") %>" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131204") %>" Value="17"></asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %></label>
                                                <div class="col-xs-5 control-input">
                                                    <asp:TextBox ID="moveOutReason" runat="server" CssClass="form-control oMoveout" class="input--mid"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <!--70-->
                                    </div>
                                </div>
                            </div>
                            <div id="confirm" class="tab-pane">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121034") %></h1>
                                <div class="row mini--space__top center">
                                    &nbsp;
                                </div>
                                <div class="form-group row student">
                                    <div class="col-md-12 col-sm-12">
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                            <ul class="list-inline ">
                                                <li>
                                                    <asp:Button ID="Button1" class="btn btn-success global-btn"
                                                        runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" ValidationGroup="add" /></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
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
                    <label class="btn btn-danger" onclick='$("#modalpopupdatamac").modal("hide");' style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
