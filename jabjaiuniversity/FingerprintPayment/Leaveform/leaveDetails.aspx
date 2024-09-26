<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="leaveDetails.aspx.cs" Inherits="FingerprintPayment.leaveDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
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

        .noborder {
            border-style: none;
            text-decoration: none;
            text-shadow: none !important;
            box-shadow: inset 0px 0px 0px 0px white;
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



        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .cover {
            color: yellow;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }

        .hid {
            visibility: hidden;
        }

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

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

        .btn-red {
            background: red; /* use your color here */
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

        label {
            font-size: 16px;
        }

        .form-control {
            border-bottom: 0px solid #FFF !important;
            background-image: none !important;
            padding-top: 2px;
        }

        table th {
            color: black !important;
        }
    </style>

    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
            padding: 5px;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301032") %> > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>
            </p>
        </div>
    </div>

    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTEmployees" ServicePath="AutoCompleteService.asmx"
        EnableCaching="true" FirstRowSelected="true" CompletionListCssClass="completionList"
        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-body ">
                        <div class=" row ">
                            <div class="col-md-12">
                                <div class="full-card box-content ">
                                    <div class=" row ">
                                        <div class="col-sm-12 text-center">
                                            <h3>
                                                <label style="font-size: 20px !important"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102245") %></label></h3>
                                        </div>
                                        <div class="col-md-12 col-sm-12 hid">
                                            <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                                                <label>
                                                    hidden</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                        </div>
                                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
                                            สถานศึกษา</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="labelSchool" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                        </div>
                                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="labelDate" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105069") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="txtheader" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102238") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="labelName" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtJob" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtType" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105073") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtReason" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105075") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtStartDate" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtEndDate" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102250") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtTimeLeave" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105077") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtTotalleave" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>

                                    </div>


                                    <div class="row hid">
                                        <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                                            <label>
                                                hidden</label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102252") %></label>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtHomenum" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtRoad" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtTumbon" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtAumpher" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtProvince" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="txtPhone" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row hid">
                                        <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                                            <label>
                                                hidden</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102253") %></label>
                                    </div>

                                    <table style="width: 100%">
                                        <tr>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102256") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102257") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="sickLeave" runat="server" CssClass='form-control noborder' Style="text-align: center;"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="bLeave" runat="server" CssClass='form-control noborder' Style="text-align: center;"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="gsLeave" runat="server" CssClass='form-control noborder' Style="text-align: center;"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="studyLeave" runat="server" CssClass='form-control noborder' Style="text-align: center;"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="etcLeave" runat="server" CssClass='form-control noborder' Style="text-align: center;"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="totalLeave" runat="server" CssClass='form-control noborder' Style="text-align: center;"></asp:TextBox></td>
                                        </tr>
                                    </table>



                                    <div class="row hid">
                                        <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                                            <label>
                                                hidden</label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-body ">
                        <div class=" row ">
                            <div class="col-md-12">
                                <div class="full-card box-content" style="margin-top: 30px;">
                                    <div class=" row ">
                                        <div class="col-sm-12 text-center">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102258") %></label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                        </div>
                                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="admin1Date" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102259") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="admin1Name" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                            <asp:Label ID="admin1Job" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102260") %></label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:Label ID="admin1Comment" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="card ">
                <div class="card-body ">
                    <div class=" row ">
                        <div class="col-md-12">
                            <div class="full-card box-content" style="margin-top: 30px;">
                                <div class=" row ">
                                    <div class="col-sm-12 text-center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102261") %></label>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    </div>
                                    <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="admin2Date" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102259") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="admin2Name" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="admin2Job" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102260") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                        <asp:Label ID="admin2Comment" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>


                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="card ">
                <div class="card-body ">
                    <div class=" row ">
                        <div class="col-md-12">
                            <div class="full-card box-content" style="margin-top: 30px;">
                                <div class=" row ">
                                    <div class="col-sm-12 text-center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102262") %></label>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    </div>
                                    <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="admin3Date" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102259") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="admin3Name" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="admin3Job" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102260") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                        <asp:Label ID="admin3Comment" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div class="card ">
                <div class="card-body ">
                    <div class=" row ">
                        <div class="col-md-12">
                            <div class="full-card box-content" style="margin-top: 30px;">

                                <div class=" row ">
                                    <div class="col-sm-12 text-center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102263") %></label>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    </div>
                                    <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="registerDate" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105071") %></label>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
                                        <asp:Label ID="registerName" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                        <asp:Label ID="registerJobHead" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                                    </div>
                                </div>




                                <div class=" row hid">
                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            hidden</label>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <asp:TextBox ID="TextBox26" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102260") %></label>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                        <asp:RadioButton ID="registerCommentAccept" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %>" GroupName="Radio8" />
                                    </div>
                                </div>

                                <div class="row">
                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
                                    </label>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
                                        <asp:RadioButton ID="registerCommentReject2" runat="server" Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %>" GroupName="Radio8" />
                                    </div>
                                </div>



                                <div class=" row hid">
                                    <div class="row">
                                        <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                            hidden</label>

                                    </div>
                                </div>
                                <div class="row ">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="Button1" class="btn btn-primary global-btn" Style="width: 30%;"
                                            runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102265") %>" />
                                        <asp:Button ID="Button2" class="btn btn-warning global-btn" Style="width: 10%;"
                                            runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
  
 

    </form>


</asp:Content>
