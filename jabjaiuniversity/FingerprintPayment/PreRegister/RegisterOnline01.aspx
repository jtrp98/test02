<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline01.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
    <link href="/assets/plugins/summernote/0.8.12/summernote-lite.css" rel="stylesheet" />

    <style>
        .table.regulation > tbody > tr > td {
            padding: 0px 8px;
            vertical-align: middle;
            border-color: #ddd;
        }

            .table.regulation > tbody > tr > td > i {
                vertical-align: middle;
            }

        .table.regulation > tbody > tr {
            border-bottom: 1px solid #ddd;
        }

        .complete-form h5 {
            margin-bottom: 0px;
            font-size: 1.5rem;
            font-family: THSarabunNew;
        }

        .complete-form h6 {
            margin-bottom: 0px;
        }

        .print-form h5 {
            margin-bottom: 0px;
            font-size: 1.5rem;
            font-family: THSarabunNew;
            color: #000;
        }

        .print-form h6 {
            margin-bottom: 0px;
        }

        .note-editor.note-frame {
            border: 0px solid #a9a9a9;
            font-weight: normal;
        }

        @media (min-width: 320px) and (max-width: 767px) {
            .complete-form h5 {
                text-align: center;
            }

            .print-form h5 {
                text-align: center;
            }
        }

        #divExplanationData {
            line-height: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12 text-center" style="padding-top: 25px;">
                            <span style="margin-top: 28px; font-size: 1.7em; color: #000; font-weight: bold; display: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132826") %> <%=registerYearBE %></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center" style="padding-top: 25px;">
                            <button id="btnNext" class="btn btn-lg btn-success col-md-4 col-sm-9" style="border: 1px solid #fff; padding: 5px 10px; border-radius: 7px;" <%=registerEnable %>>
                                <div class="row">
                                    <div class="col-md-3" style="padding: 0px 0px 0px 10px;">
                                        <span class="btn-label">
                                            <i class="material-icons" style="font-size: 4em; margin-top: 0px; margin-bottom: 0px;">edit</i>
                                        </span>
                                    </div>
                                    <div class="col-md-9 text-left complete-form" style="padding: 10px 10px 0px 15px;">
                                        <h5><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103175") %></h5>
                                        <h6></h6>
                                    </div>
                                </div>
                            </button>
                            <button id="btnPrint" class="btn btn-lg col-md-4 col-sm-9" style="background-color: #f5f5f5; border: 1px solid #606060; color: #000; padding: 5px 10px; border-radius: 7px;" <%=printEnable %>>
                                <div class="row">
                                    <div class="col-md-3" style="padding: 0px 0px 0px 10px;">
                                        <span class="btn-label">
                                            <i class="material-icons" style="font-size: 4em; color: #707070; margin-top: 0px; margin-bottom: 0px;">print</i>
                                        </span>
                                    </div>
                                    <div class="col-md-9 text-left print-form" style="padding: 10px 10px 0px 20px;">
                                        <h5><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103034") %></h5>
                                        <h6></h6>
                                    </div>
                                </div>
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <div id="divExplanationData" class="col-md-11 ml-auto mr-auto" style="padding-top: 25px; font-weight: normal;">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11 ml-auto mr-auto" style="padding-top: 25px;">
                            <div class="card card-plain">
                                <div class="card-header card-header-icon card-header-rose">
                                    <h4 class="card-title mt-0" style="font-size: 1.5rem; color: #000; font-family: THSarabunNew;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103176") %></h4>
                                </div>
                                <div class="card-body" style="color: #000;">
                                    <div class="table-responsive">
                                        <table class="table regulation">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%;">#</th>
                                                    <th style="width: 80%;" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></th>
                                                    <th style="width: 10%;"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Literal ID="ltrDocument" runat="server" />
                                            </tbody>
                                        </table>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage() {

            preRegister.Page01Saved = true;

            // Save data to localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                ls.setItem('preRegister', preRegister);
            } else {
                // No web storage Support.
            }

            ez.activePageComplete(1);

            window.location.href = "RegisterOnline02.aspx";

        }

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            $("#btnNext").bind({
                click: function () {

                    SaveDataToLocalStorage();

                    return false;
                }
            });

            $("#btnPrint").bind({
                click: function () {

                    window.location.href = "RegisterSearchID.aspx";

                    return false;
                }
            });

            $('#divExplanationData').html(`<%=ExplanationData%>`);

        });
    </script>
</asp:Content>
