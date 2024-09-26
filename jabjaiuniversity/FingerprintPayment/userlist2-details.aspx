<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="userlist2-details.aspx.cs" Inherits="FingerprintPayment.userlist2_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js"></script>

    <script type="text/javascript">

        function changetab(index) {

            var bodypage = document.getElementsByClassName("bodypage");

            if (index == "1")
                bodypage[0].style.paddingBottom = "1100px";
            if (index == "2")
                bodypage[0].style.paddingBottom = "2300px";
            if (index == "3")
                bodypage[0].style.paddingBottom = "1750px";
            if (index == "4")
                bodypage[0].style.paddingBottom = "900px";

        }



    </script>

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

        .smol3 {
            font-size: 70%;
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
    <div class="full-card box-content bodypage" style="margin-top: 10px; padding-top: 10px; padding-bottom: 1100px;">
        <div class="row">
            <div class="col-sm-12 col-md-12 user-details">
                <div class="user-image" style="padding-bottom: 20px;">
                    <asp:Image ID="profileimage" runat="server" />
                </div>
                <div class="user-info-block">
                    <div class="user-heading hid">
                        <h3>Karan Singh Sisodia</h3>
                        <span class="help-block">Chandigarh, IN</span>
                    </div>
                    <ul class="navigation">
                        <li class="active">
                            <a data-toggle="tab" href="#information" onclick="changetab(1)">
                                <span class="fa fa-graduation-cap" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#app" onclick="changetab(4)">
                                <span class="glyphicon glyphicon-book" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101170") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#family" onclick="changetab(2)">
                                <span class="fa fa-user" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>"></span>
                            </a>
                        </li>
                        <li>
                            <a data-toggle="tab" href="#health" onclick="changetab(3)">
                                <span class="fa fa-heart" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %>"></span>
                            </a>
                        </li>


                    </ul>
                    <div class="user-body" style="border-top: 0; padding-top: 0; padding-right: 0px; margin-top: 0; border-left: 10px; margin-left: 30px; padding-left: 0;">
                        <div class="tab-content">

                            <div id="information" class="tab-pane active" style="border-top: 0; padding-top: 0; margin-top: 0;">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h1>
                                <div class="col-xs-12">
                                    <div class="col-xs-6">
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox65" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101051") %></label>
                                                <div class="col-lg-8 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="txtStudentNumber" runat="server" CssClass='form-control' class="input--mid" ReadOnly></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox12" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox21" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox64" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox6" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox60" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox8" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox14" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="StudentTumbon" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="StudentProvince" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox13" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                        <!--70-->
                                    </div>
                                    <div class="col-xs-6">



                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group row student " style="">
                                                    <div class="col-md-12 col-sm-12" style="">
                                                        <label class="col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                                        <div class="col-xs-8 control-input">
                                                            <asp:TextBox ID="TextBox16" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student " style="">
                                                    <div class="col-md-12 col-sm-12" style="">
                                                        <label class="col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                                        <div class="col-xs-8 control-input">
                                                            <asp:TextBox ID="TextBox61" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox7" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox22" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox5" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox15" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox63" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group row student" style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox3" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student" style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox10" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student" style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="StudentAumpher" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student" style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox20" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student " style="">
                                            <div class="col-md-12 col-sm-12" style="">
                                                <label class="col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                <div class="col-xs-8 control-input">
                                                    <asp:TextBox ID="TextBox45" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
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
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famTitle" runat="server" CssClass="width100 form-control" ReadOnly="True">
                                                            
                                                            
                                                                </asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famName" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famLast" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famIdCard" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famCeerChad" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famSunChad" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famReligion" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famReletive" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
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
                                                                <asp:TextBox ID="famHomenum" runat="server" CssClass='form-control famHome' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famSoy" runat="server" CssClass='form-control famSoy' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famMuu" runat="server" CssClass='form-control famMuu' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famRoad" runat="server" CssClass='form-control famRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famProvince" runat="server" CssClass='form-control famRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famAumpher" runat="server" CssClass='form-control famRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famTumbon" runat="server" CssClass='form-control famRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPost" runat="server" CssClass='form-control famPost' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap;">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> 1</label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPhone1" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap;">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> 2</label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPhone2" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap;">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> 3</label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famPhone3" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="famEmail" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <hr />
                                            </div>
                                            <div class="col-xs-12 center">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></label>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherTitle" runat="server" CssClass='form-control famRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherName" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherLast" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherIdCard" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherCeerChad" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherSunChad" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherReligion" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
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
                                                                <asp:TextBox ID="fatherHome" runat="server" CssClass='form-control fatherHome' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherSoy" runat="server" CssClass='form-control fatherSoy' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherMuu" runat="server" CssClass='form-control fatherMuu' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherRoad" runat="server" CssClass='form-control fatherRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherProvinceStatus2">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherProvince" runat="server" CssClass='form-control fatherProvince2' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherAumpherStatus2">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherAumpher" runat="server" CssClass='form-control fatherAumpher2' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student fatherTumbonStatus2">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherTumbon" runat="server" CssClass='form-control fatherTumbon2' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherPost" runat="server" CssClass='form-control fatherPost' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap;">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="fatherPhone" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <hr />
                                            </div>
                                            <div class="col-xs-12 center">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></label>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="col-xs-6">
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherTitle" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherName" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>
                                                            </label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherLast" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherIdCard" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherCeerChad" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherSunChad" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap; font-size: 90%">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherReligion" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
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
                                                                <asp:TextBox ID="motherHome" runat="server" CssClass='form-control motherHome' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherSoy" runat="server" CssClass='form-control motherSoy' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherMuu" runat="server" CssClass='form-control motherMuu' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherRoad" runat="server" CssClass='form-control motherRoad' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row student motherProvinceStatus2">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherProvince" runat="server" CssClass='form-control motherProvince2' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherAumpherStatus2">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherAumpher" runat="server" CssClass='form-control motherAumpher2' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student motherTumbonStatus2">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherTumbon" runat="server" CssClass='form-control motherTumbon2' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherPost" runat="server" CssClass='form-control motherPost' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row student">
                                                        <div class="col-md-12 col-sm-12">
                                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext" style="overflow: hidden; white-space: nowrap;">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                                <asp:TextBox ID="motherPhone" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>


                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>

                            </div>
                            <div id="health" class="tab-pane">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></h1>

                                <div class="contentBox">
                                    <div class="col-xs-12" style="padding: 0px; width: 105%; margin-left: -2%;">
                                        <div class="col-xs-12" style="width: 800px; margin-left: 6%;">
                                            <canvas id="myChart" width="800" height="450"></canvas>


                                        </div>
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

                                            .centertext {
                                                text-align: center;
                                            }

                                            .tdtr {
                                                border: 1px solid #000000;
                                                text-align: center;
                                                padding: 0px;
                                                width: 35px;
                                            }

                                            .noborder {
                                                border: 0px;
                                                background-color: #eee;
                                                width: 100%;
                                            }

                                            .eee {
                                                background-color: #eee;
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
                                                border: 1px solid #eee;
                                                background-color: #eee;
                                                width: 100%;
                                            }

                                            .smol {
                                                font-size: 90%;
                                                padding: 0px;
                                                padding-right: 0px;
                                                padding-left: 0px;
                                                width: 100%;
                                                border: none;
                                            }

                                            .eee {
                                                background-color: #eee;
                                            }

                                            .smol2 {
                                                font-size: 90%;
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

                                                var smol = document.getElementsByClassName("age");
                                                var weight = document.getElementsByClassName("bmi1");
                                                var height = document.getElementsByClassName("bmi2");

                                                var array = [];
                                                if (smol[0].value != "") array[0] = smol[0].value;
                                                if (smol[1].value != "") array[1] = smol[1].value;
                                                if (smol[2].value != "") array[2] = smol[2].value;
                                                if (smol[3].value != "") array[3] = smol[3].value;
                                                if (smol[4].value != "") array[4] = smol[4].value;
                                                if (smol[5].value != "") array[5] = smol[5].value;
                                                if (smol[6].value != "") array[6] = smol[6].value;
                                                if (smol[7].value != "") array[7] = smol[7].value;
                                                if (smol[8].value != "") array[8] = smol[8].value;
                                                if (smol[9].value != "") array[9] = smol[9].value;
                                                if (smol[10].value != "") array[10] = smol[10].value;
                                                if (smol[11].value != "") array[11] = smol[11].value;
                                                if (smol[12].value != "") array[12] = smol[12].value;
                                                if (smol[13].value != "") array[13] = smol[13].value;
                                                if (smol[14].value != "") array[14] = smol[14].value;
                                                if (smol[15].value != "") array[15] = smol[15].value;
                                                if (smol[16].value != "") array[16] = smol[16].value;
                                                if (smol[17].value != "") array[17] = smol[17].value;
                                                if (smol[18].value != "") array[18] = smol[18].value;
                                                if (smol[19].value != "") array[19] = smol[19].value;
                                                if (smol[20].value != "") array[20] = smol[20].value;
                                                if (smol[21].value != "") array[21] = smol[21].value;
                                                if (smol[22].value != "") array[22] = smol[22].value;
                                                if (smol[23].value != "") array[23] = smol[23].value;

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


                                                var options = {
                                                    type: 'line',
                                                    data: {
                                                        labels: array,
                                                        datasets: [
                                                            {
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
                                                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111033") %>',
                                                                data: [30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,],
                                                                borderWidth: 1,
                                                                pointStyle: 'dash',
                                                                backgroundColor: 'rgba(255,255,0,0.3)'
                                                            },
                                                            {
                                                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111034") %>',
                                                                data: [35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35],
                                                                borderWidth: 1,
                                                                pointStyle: 'dash',
                                                                backgroundColor: 'rgba(255,122,0,0.3)'
                                                            },
                                                            {
                                                                label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111035") %>',
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
                                                        }
                                                    }
                                                }

                                                var ctx = document.getElementById('myChart').getContext('2d');
                                                new Chart(ctx, options);

                                            }
                                            window.onload = start;

                                        </script>
                                        <div class="col-xs-12 hid">hidden<asp:TextBox ID="txthidden" runat="server" CssClass="centertext txtcheck" class="input--mid"></asp:TextBox></div>
                                        <table class="table2">
                                            <tr class="tdtr">
                                                <th class="tdtr headerCell" colspan="25"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101211") %> 
        <i class="fa fa-question-circle" style="font-size: 18px;" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131177") %>" data-placement="top" data-toggle="tooltip"></i>
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
                                                <td class="tdtr " style="font-size: 80%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101214") %></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age11" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age12" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age13" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age14" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age21" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age22" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age23" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age24" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age31" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age32" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age33" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee">
                                                    <asp:Label ID="age34" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age41" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age42" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age43" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age44" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age51" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age52" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age53" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del4">
                                                    <asp:Label ID="age54" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del6">
                                                    <asp:Label ID="age61" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del6">
                                                    <asp:Label ID="age62" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del6">
                                                    <asp:Label ID="age63" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>
                                                <td class="tdtr eee del6">
                                                    <asp:Label ID="age64" runat="server" CssClass="smol disable2 centertext noborder"></asp:Label></td>

                                            </tr>
                                            <tr>
                                                <td class="tdtr"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101215") %></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight11" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight12" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight13" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight14" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight21" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight22" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight23" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight24" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight31" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight32" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight33" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="weight34" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight41" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight42" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight43" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight44" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight51" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight52" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight53" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="weight54" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="weight61" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="weight62" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="weight63" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="weight64" runat="server" CssClass="noborder centertext bmi1 smol3" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="tdtr"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101216") %></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height11" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height12" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height13" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height14" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height21" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height22" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height23" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height24" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height31" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height32" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height33" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee">
                                                    <asp:TextBox ID="height34" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height41" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height42" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height43" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height44" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height51" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height52" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height53" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del4">
                                                    <asp:TextBox ID="height54" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="height61" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="height62" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="height63" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                                <td class="tdtr eee del6">
                                                    <asp:TextBox ID="height64" runat="server" CssClass="noborder centertext bmi2 smol3" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-xs-12 hid">
                                        <h5>hidden</h5>
                                    </div>
                                    <div class="column70">
                                        <div class="form-group row student">
                                            <div class="col-xs-12 hidden">

                                                <asp:TextBox ID="graph11" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph12" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph13" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph14" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph21" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph22" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph23" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph24" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph31" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph32" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph33" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph34" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph41" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph42" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph43" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph44" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph51" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph52" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph53" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph54" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                               
                                                    <asp:TextBox ID="graph61" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                                
                                                    <asp:TextBox ID="graph62" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                               
                                                    <asp:TextBox ID="graph63" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>                                               
                                                    <asp:TextBox ID="graph64" runat="server" CssClass="age disable2 centertext noborder"></asp:TextBox></td>
                                            </div>
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131178") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox53" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label lefttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103115") %>
                                                </label>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131179") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox54" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label lefttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131180") %>
                                                </label>
                                            </div>
                                        </div>



                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox62" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101219") %></label>
                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101220") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox55" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131225") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox56" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101222") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox57" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101223") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox58" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101224") %></label>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                                                    <asp:TextBox ID="TextBox59" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>


                                        <!--70-->
                                    </div>


                                </div>
                            </div>

                            <div id="app" class="tab-pane">
                                <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101170") %></h1>
                                <div class="contentBox">
                                    <div class="">







                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %></label>
                                                <div class="col-xs-4 control-input">
                                                    <asp:TextBox ID="oldSchoolName" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %></label>
                                                <div class="col-xs-4 control-input">
                                                    <asp:TextBox ID="oldSchoolTumbon" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %></label>
                                                <div class="col-xs-4 control-input">
                                                    <asp:TextBox ID="oldSchoolAumpher" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                <div class="col-xs-4 control-input">
                                                    <asp:TextBox ID="oldSchoolProvince" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %></label>
                                                <div class="col-xs-4 control-input">
                                                    <asp:TextBox ID="oldSchoolGraduated" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103097") %></label>
                                                <div class="col-xs-4 control-input">
                                                    <asp:TextBox ID="oldSchoolGPA" runat="server" CssClass='form-control' class="input--mid" ReadOnly="True"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>




                                        <!--70-->
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
