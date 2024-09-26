<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="employees-edit.aspx.cs"
    Inherits="FingerprintPayment.Employees.employees_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .user-details .user-info-block {
            width: 100%;
            position: absolute;
            top: 55px;
            background: rgb(255, 255, 255);
            z-index: 0;
            padding-top: 35px;
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

        .upload2 {
            display: inline-block;
            background-color: white;
            border: 1px solid white;
            font-size: 20px;
            padding: 4px;
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

        .oneline {
            white-space: nowrap;
        }

        .circle-cropper {
            background-repeat: no-repeat;
            background-position: 50%;
            border-radius: 50%;
            width: 100px;
            height: 100px;
        }

        html, body {
            width: 100%;
            margin: 0px;
            padding: 0px;
            overflow-x: hidden;
        }

        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }

        .contentBox .column30 {
            float: left;
            margin: 0;
            width: 45%;
        }
    </style>
    <script type="text/javascript">
        function readURL(input) {
            //if (input.files && input.files[0]) {
            //    var reader = new FileReader();
            //    reader.onload = function (e) {
            //        $('#profileimage').attr('src', e.target.result);
            //    }

            //    reader.readAsDataURL(input.files[0]);
            //}
        }
        //$("#filePhoto").change(function () {
        //    readURL(this);
        //});
        //$('input[id*=btnDelFinger]').click(function () {
        //    showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('id')); return false;
        //});

        $(function () {
            $("form").submit(function (e) {
                if ($("input[id*=empPhone]").val() === "") {
                    e.preventDefault()
                }
            });
        })

        function changeFinger() {
            $userid = getUrlParameter("id");
            $.ajax("/App_Logic/deleteDataJSON.ashx?mode=delfinger&userid=" + $userid + "&type=1", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
                $('input[id*=btnDelFinger]').addClass("disabled");
            });
        }
        function ShowPopUP() {
            var name = $("input[id*=empName]").val() + " " + $("input[id*=empLastname]").val();
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class=" full-card box-content" style="margin-top: 10px; padding-top: 10px; padding-bottom: 830px;">
        <div class="row">
            <div class="col-sm-12 col-md-12 user-details">
                <div class="user-image">
                    <img id="profileimage" alt="" width="180" height="180" runat="server" />
                </div>

                <div class="user-info-block">
                    <div class="user-heading hid">
                        <h3>Karan Singh Sisodia</h3>
                        <span class="help-block">Chandigarh, IN</span>
                    </div>
                    <ul class="navigation">
                        <li class="active">
                            <a data-toggle="tab" href="#information">
                                <span class="fa fa-user" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121036") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#confirm">
                                <span class="glyphicon glyphicon-ok" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121034") %>"></span>
                            </a>
                        </li>
                    </ul>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="user-body" style="border-top: 0; padding-top: 0; margin-top: 0; border-left: 10px; margin-left: 30px; padding-left: 0;">
                                <div class="tab-content">
                                    <div id="information" class="tab-pane active" style="border-top: 0; padding-top: 0; margin-top: 0;">
                                        <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121036") %></h1>
                                        <div class="contentBox">
                                            <div class="column70">
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext oneline">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121043") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="empTitle" runat="server" CssClass="width100 form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlJob" runat="server" CssClass="width100 form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="width100 form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empLastname" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empIdCard" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empHomenumber" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empSoy" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlprovince" runat="server" CssClass="width100 form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddltumbon" runat="server" CssClass='form-control' class="input--mid"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empPhone" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--70-->
                                            </div>
                                            <div class="column30">
                                                <div class="form-group row student hid " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            hidden</label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:RadioButtonList ID="radiogender" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>&nbsp;" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105032") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddluser_type" CssClass="form-control" runat="server">
                                                                <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121028") %></asp:ListItem>
                                                                <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121029") %></asp:ListItem>
                                                                <asp:ListItem Value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121030") %></asp:ListItem>
                                                                <asp:ListItem Value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121031") %></asp:ListItem>
                                                                <asp:ListItem Value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121032") %></asp:ListItem>
                                                                <asp:ListItem Value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121033") %></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlcUserType" runat="server" CssClass="width100 form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
                                                            <asp:DropDownList ID="ddlDate" runat="server" CssClass="width100 form-control">
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
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input" style="margin-left: -15px; padding-left: -15px; width: 31%;">
                                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="width100 form-control">
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
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                            <asp:DropDownList ID="ddlAge" runat="server" CssClass="width100 form-control" Style="margin-left: -15px; padding-left: -15px;">
                                                                <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empMuu" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empRoad" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:DropDownList ID="ddlaumper" runat="server" CssClass='form-control' class="input--mid" AutoPostBack="True" OnSelectedIndexChanged="ddlaumper_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empPost" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                            <asp:TextBox ID="empEmail" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                                    <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
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
                                    </div>
                                    <div id="confirm" class="tab-pane">
                                        <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121034") %></h1>
                                        <div class="row mini--space__top center">
                                            &nbsp;
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-label righttext">
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
                                                    <ul class="list-inline ">
                                                        <li>
                                                            <asp:Button ID="Button1" class="btn btn-success global-btn pull-right"
                                                                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" ValidationGroup="add" />
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-input">
                                                    <ul class="list-inline ">
                                                        <li>
                                                            <asp:Button ID="Button2" class="btn btn-danger global-btn"
                                                                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" ValidationGroup="add" />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlaumper" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlprovince" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="Button1" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                    <label class="btn btn-danger" onclick='$("#modalpopupdatamac").modal("hide");' style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
