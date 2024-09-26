<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="employees-details.aspx.cs" Inherits="FingerprintPayment.Employees.employees_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <style>
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
    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#profileimage').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#filePhoto").change(function () {
            readURL(this);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
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
                    </ul>
                    <div class="user-body" style="border-top: 0; padding-top: 0; margin-top: 0; border-left: 10px; margin-left: 30px; padding-left: 0;">
                        <div class="tab-content">
                            <div id="information" class="tab-pane active" style="border-top: 0; padding-top: 0; margin-top: 0;">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121036") %></h1>
                                <div class="contentBox">
                                    <div class="column70">
                                        <div class="form-group row student hid">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext oneline">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121027") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">

                                                    <asp:fileupload id="FileUpload1" runat="server" cssclass="upload2" onchange="readURL(this)" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="empTitle" runat="server" cssclass="width100 form-control" enabled="False">
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121037") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121037") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121038") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121038") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121039") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121039") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121041") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121041") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121040") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121040") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00363") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121042") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121042") %>"></asp:ListItem>
                                                        <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                                                        <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                        <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                                        <asp:ListItem Text="Ms." Value="Ms."></asp:ListItem>
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddlJob" runat="server" cssclass="width100 form-control" enabled="false">
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddlDepartment" runat="server" cssclass="width100 form-control" enabled="false">
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empName" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empLastname" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empIdCard" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empHomenumber" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empSoy" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddlprovince" runat="server" cssclass="width100 form-control" enabled="False">
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddltumbon" runat="server" enabled="False" cssclass='form-control' class="input--mid" readonly="True"></asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empPhone" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
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
                                                    <asp:textbox id="TextBox2" runat="server" cssclass='form-control' class="input--mid"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:radiobuttonlist id="radiogender" cssclass="radioButtonList" runat="server" repeatdirection="Horizontal" enabled="False">
                                                        <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>&nbsp;" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>&nbsp;</asp:ListItem>
                                                    </asp:radiobuttonlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105032") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddluser_type" cssclass="form-control" runat="server">
                                                                <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121028") %></asp:ListItem>
                                                                <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121029") %></asp:ListItem>
                                                                <asp:ListItem Value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121030") %></asp:ListItem>
                                                                <asp:ListItem Value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121031") %></asp:ListItem>
                                                                <asp:ListItem Value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121032") %></asp:ListItem>
                                                                <asp:ListItem Value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121033") %></asp:ListItem>
                                                            </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddlcUserType" runat="server" cssclass="width100 form-control" enabled="False">
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
                                                    <asp:dropdownlist id="ddlDate" runat="server" cssclass="width100 form-control" enabled="False">
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
                                                    </asp:dropdownlist>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input" style="margin-left: -15px; padding-left: -15px; width: 31%;">
                                                    <asp:dropdownlist id="ddlMonth" runat="server" cssclass="width100 form-control" enabled="False">
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
                                                    </asp:dropdownlist>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                                    <asp:dropdownlist id="ddlAge" runat="server" cssclass="width100 form-control" style="margin-left: -15px; padding-left: -15px;" enabled="False">
                                                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                    </asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empMuu" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empRoad" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:dropdownlist id="ddlaumper" runat="server" cssclass='form-control' enabled="False" class="input--mid" readonly="True"></asp:dropdownlist>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empPost" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                                            <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:textbox id="empEmail" runat="server" cssclass='form-control' class="input--mid" readonly="True"></asp:textbox>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
