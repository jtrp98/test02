<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leavePrintTeacher.aspx.cs" Inherits="FingerprintPayment.leavePrintTeacher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>
    <%--<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>

    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />


    <%--<script src="/bootstrap SB2/dist/js/sb-admin-2.js" type="text/javascript"></script>--%>

    <style>
        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }

        .lefttext {
            position: relative;
            text-align: left;
            white-space: nowrap;
        }

        .noborder {
            border-style: none;
            text-decoration: none;
            text-shadow: none !important;
            box-shadow: inset 0px 0px 0px 0px white;
        }

        .centerunderline {
            text-align: center;
        }

        .hid {
            visibility: hidden;
        }

        body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 12pt "Tahoma";
        }

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }

        .page {
            width: 210mm;
            min-height: 297mm;
            padding: 10mm;
            margin: 10mm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            /*padding: 0.5cm;*/
            border: 5px;
            height: 257mm;
            outline: 2cm;
        }

        .control-label {
            margin-top: 1.3px;
            padding: 0px
        }

        @page {
            size: A4;
            margin: 4mm;
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
            }

            .page {
                margin: 0;
            }
        }

        .example-print {
            display: none;
        }

        @media print {
            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }
        }
    </style>
    <title>Fingerprint Payment System</title>



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.approveType1').hide();
            $('.approveType2').hide();
            $('.approveType2_2').hide();
            $('.approveType2_3').hide();

            if ($('#txtName2').val() != "") {
                $('.approveType1').show();
            }
            if ($('#txtname3').val() != "") {
                $('.approveType2').show();
            }
            if ($('#txtApproveNameType2_1').val() != "") {
                $('.approveType2_2').show();
            }
            if ($('#txtApproveNameType3_1').val() != "") {
                $('.approveType2_3').show();
            }
        });
    </script>


</head>

<body>


    <form id="form1" runat="server">
        <div class="book">
            <div class="page printableArea">
                <div class="subpage">


                    <div class="col-xs-12" style="padding: 0px;">
                        <div class="col-xs-2">
                            <img id="schoolpicture" runat="server" alt="" class="avatar img-responsive centertext" style="margin: 0 auto; display: block; width: 120px;" />
                        </div>
                        <div class="col-xs-10" style="padding: 0px; padding-top: 15px;">
                            <div class="col-xs-12 lefttext">
                                <asp:Label ID="p1header1" CssClass="bigtxt2" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-12 lefttext">
                                <asp:Label ID="p1header2" CssClass="" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-12 lefttext">
                                <asp:Label ID="p1header3" CssClass="" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />

                    <div class="col-sm-12" style="text-align: center">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105067") %></label>
                    </div>
                    <br />
                    <div class="form-group row ">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            </div>
                            <label class="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105068") %></label>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" style="padding: 0px">
                                <asp:Label ID="labelSchool" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12 col-sm-12">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            </div>
                            <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2  righttext noborder">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 " style="padding: 0px">
                                <asp:Label ID="labelDate" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105069") %> :</label>
                            <div class="col-xs-4 ">
                                <asp:TextBox ID="txtHeader" runat="server" CssClass='form-control noborder' class="input--mid"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105070") %> :</label>
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass='form-control noborder' class="input--mid"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105071") %> :</label>
                            <div class="col-xs-4">
                                <asp:Label ID="txtName" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %> :</label>
                            <div class="col-xs-4 ">
                                <asp:Label ID="txtJob" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105072") %> :</label>
                            <div class="col-xs-4" style="padding-right: 0px">
                                <asp:Label ID="txtSchool" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105074") %> :</label>
                            <div class="col-xs-4 ">
                                <asp:Label ID="txtType" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105073") %> :</label>
                            <div class="col-xs-10">
                                <asp:Label ID="txtReason" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105075") %> :</label>
                            <div class="col-xs-4">
                                <asp:Label ID="dStart" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %> :</label>
                            <div class="col-xs-4 ">
                                <asp:Label ID="dEnd" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %> :</label>
                            <div class="col-xs-4">
                                <asp:Label ID="typeLeave" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105077") %> :</label>
                            <div class="col-xs-4 ">
                                <asp:Label ID="sumLeave" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <div class="col-xs-1">
                                <asp:Label ID="Label5" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
                            </div>
                            <label class="col-xs-7 control-label">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105078") %></label>
                        </div>
                    </div>

                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label>
                            <div class="col-xs-2" style="padding-right: 0px">
                                <asp:Label ID="txtHomenumber" runat="server" CssClass='form-control noborder' class="input--mid">-</asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label>
                            <div class="col-xs-2 " style="padding-right: 0px">
                                <asp:Label ID="txtRoad" runat="server" CssClass='form-control noborder' class="input--mid">-</asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> :</label>
                            <div class="col-xs-2 " style="padding-right: 0px">
                                <asp:Label ID="txtsubDistrict" runat="server" CssClass='form-control noborder' class="input--mid">-</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row ">
                        <div class="col-md-12 col-sm-12">
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> :</label>
                            <div class="col-xs-2" style="padding-right: 0px">
                                <asp:Label ID="txtDistrict" runat="server" CssClass='form-control noborder' class="input--mid">-</asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label>
                            <div class="col-xs-2 " style="padding-right: 0px">
                                <asp:Label ID="txtProvince" runat="server" CssClass='form-control noborder' class="input--mid">-</asp:Label>
                            </div>
                            <label class="col-xs-2 control-label righttext">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %> :</label>
                            <div class="col-xs-2 " style="padding-right: 0px">
                                <asp:Label ID="txtPhone" runat="server" CssClass='form-control noborder' class="input--mid">-</asp:Label>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="approveType1">

                        <div class=" form-group row">
                            <div class="col-sm-12" style="margin-bottom: 10px;">
                                <div class="col-xs-6">
                                    <p class="text-center" style="margin: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></p>
                                </div>

                                <div class="col-xs-6">
                                    <asp:TextBox ID="txtApproveComment1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class=" form-group row">
                            <div class="col-sm-12" style="margin-bottom: 10px;">
                                <div class="col-xs-6">
                                    <asp:TextBox ID="txtName2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                                </div>

                                <div class="col-xs-6">
                                    <asp:TextBox ID="txtApproveNameType1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-12" style="margin-bottom: 10px;">
                                <div class="col-xs-6">
                                    <asp:TextBox ID="txtJob2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                                </div>

                                <div class="col-xs-6">

                                    <asp:TextBox ID="txtApprovePositionType1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-12" style="margin-bottom: 10px;">
                                <div class="col-xs-6">
                                </div>

                                <div class="col-xs-6">
                                    <asp:TextBox ID="txtApproveDateType1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="approveType2">
                        <div class="form-group row">
                            <div class="col-xs-12" style="text-align: center">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-12">
                                <asp:TextBox ID="txtname3" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-12">
                                <asp:TextBox ID="txtJob3" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="approveType2_2">
                        <div class="form-group row">
                            <div class="col-xs-6" style="text-align: center">
                                <asp:TextBox ID="txtApproveCommentType2_1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-6" style="text-align: center">
                                <asp:TextBox ID="txtApproveCommentType2_2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtApproveNameType2_1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtApproveNameType2_2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtApproveDateType2_1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-6">
                                <asp:TextBox ID="txtApproveDateType2_2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="approveType2_3">
                        <div class="form-group row">
                            <div class="col-xs-4" style="text-align: center">
                                <asp:TextBox ID="txtApproveCommentType3_1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-4" style="text-align: center">
                                <asp:TextBox ID="txtApproveCommentType3_2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-4" style="text-align: center">
                                <asp:TextBox ID="txtApproveCommentType3_3" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtApproveNameType3_1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtApproveNameType3_2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtApproveNameType3_3" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtApproveDateType3_1" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtApproveDateType3_2" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtApproveDateType3_3" runat="server" CssClass='form-control noborder' class="input--mid" Style="text-align: center;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <br />
                    <div class="form-group row hidden ">
                        <div class="col-md-12 col-sm-12">
                            <asp:TextBox ID="StatsLeave" runat="server" CssClass='form-control noborder' Style="font-weight: bold;"></asp:TextBox>
                        </div>
                    </div>

                    <style>
                        table, th, td {
                            border: 1px solid black;
                            border-collapse: collapse;
                            padding: 5px;
                            text-align: center;
                        }
                    </style>

                    <table style="width: 100%" class="hidden">
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
                    <br />
                    <br />



                    <div class="form-group row example-screen">
                        <div class="col-md-12 col-sm-12">
                            <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 ">
                            </div>
                            <div>
                                <input value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102268") %>" onclick=" window.print() " type="button" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </form>
</body>
</html>
