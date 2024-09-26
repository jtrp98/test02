<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="schoolsetting.aspx.cs" Inherits="FingerprintPayment.schoolsetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap-toggle.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-toggle.js" type="text/javascript"></script>
    <style type="text/css">
        .table > tbody > tr > td {
            border-top: none;
        }

        .table > tbody > tr.sub > td {
            padding: 0px 8px 4px 8px;
        }

        .table > tbody > tr.border {
            border-bottom: 1px solid #ddd;
        }

        .modal-content {
            /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
            width: inherit;
            height: inherit;
            /* To center horizontally */
            margin: 0 auto;
        }

        .modal-footer {
            border-top: none;
        }

        /* Toggle Switch Style */
        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .toggle.btn {
            width: 100px !important;
            height: 14px !important;
        }

        .toggle-on {
            padding: 0px;
        }

        .toggle-off {
            padding: 0px 10px 0px 0px;
        }

        #modalpopup-data-submit, #modalpopup-data-cancel {
            width: 90px;
        }

        .table tbody tr td input {
            text-align: center;
        }

        .table tbody tr td select {
            text-align-last: center;
        }

        .error {
            color: red;
            font-size: 20px;
        }

        #behavior_absence_half_point {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.3); /* IE */
            -moz-transform: scale(1.3); /* FF */
            -webkit-transform: scale(1.3); /* Safari and Chrome */
            -o-transform: scale(1.3); /* Opera */
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .behavior_absence_half_point input {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.3); /* IE */
            -moz-transform: scale(1.3); /* FF */
            -webkit-transform: scale(1.3); /* Safari and Chrome */
            -o-transform: scale(1.3); /* Opera */
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .checkbox-inline input[type="checkbox"] {
            position: relative;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('.behavior_show_minus_sign').bootstrapToggle();

            $("#btnSubmit").click(function () {

                var data = {
                    "bScanOut": ($("#behavior_show_minus_sign").prop("checked") ? true : false)
                };

                var data1 = {
                    "ClassNameDisable": ($("#ClassNameDisable").prop("checked") ? false : true)
                };

                PageMethods.SaveData(data,data1,
                    function (result) {
                        console.log(result);
                        alert("บันทึกข้อมูลเรียบร้อยแล้่ว");
                    },
                    function (result) {
                        alert(result._meassage);
                    }
                );
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="detail-card box-content companyedit-container" style="width: 100%;">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-md-12 center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801002") %></label>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302016") %></label>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <asp:Label ID="score" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2"></div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302017") %></label>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <asp:Label ID="type" runat="server"></asp:Label>
            </div>
        </div>--%>

        <table class="table" style="border: 1px solid #ccc;">
            <thead class="bg-primary">
                <tr>
                    <th scope="col" style="text-align: center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                    <th scope="col" colspan="3" style="text-align: center;"></th>
                </tr>
            </thead>
            <tbody>
                <tr class="border">
                    <td style="text-align: center; width: 10%;">1.</td>
                    <td style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801003") %></td>
                    <td style="width: 0%; text-align: center;">
                        <asp:Label ID="score" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center; width: 30%;">
                        <% 
                            string bScanOut = "";
                            if (Model.bScanOut == true) bScanOut = "checked";
                        %>
                        <input id="behavior_show_minus_sign" class="behavior_show_minus_sign" type="checkbox" <%= bScanOut %> data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801005") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801046") %>" />
                    </td>
                </tr>
                <tr class="border">
                    <td style="text-align: center; width: 10%;">2.</td>
                    <td style="width: 60%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801004") %></td>
                    <td style="width: 0%; text-align: center;">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: center; width: 30%;">
                        <% 
                            string bClassNameDisable = "";
                            if (ModelCompany.ClassNameDisable == false) bClassNameDisable = "checked";
                        %>
                        <input id="ClassNameDisable" class="behavior_show_minus_sign" type="checkbox" <%= bClassNameDisable %> data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801005") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801046") %>" />
                    </td>
                </tr>
            </tbody>
        </table>

        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 center">
                <a class="btn btn-info btnpermission" id="btnSubmit" style="cursor: pointer;">
                    <span class="glyphicon glyphicon-pencil" style="vertical-align: middle; padding: 5px;"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
