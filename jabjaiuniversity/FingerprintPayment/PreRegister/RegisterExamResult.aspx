<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterExamResult.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterExamResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
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


        @media (min-width: 320px) and (max-width: 767px) {
            .complete-form h5 {
                text-align: center;
            }

            .print-form h5 {
                text-align: center;
            }
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
                            <span style="margin-top: 28px; font-size: 1.7em; color: #000; font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132816") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <%=registerYearBE %></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center" style="padding-top: 25px;">
                            <button id="btnResultCheck" class="btn btn-lg col-md-3" style="background-color: #f5f5f5; border: 1px solid #606060; color: #000; padding: 5px 10px; border-radius: 7px;">
                                <div class="row">
                                    <div class="col-md-3" style="padding: 0px 0px 0px 10px;">
                                        <span class="btn-label">
                                            <i class="material-icons" style="font-size: 4em; color: #707070; margin-top: 0px; margin-bottom: 0px;">announcement</i>
                                        </span>
                                    </div>
                                    <div class="col-md-9 text-left print-form" style="padding: 10px 10px 0px 20px;">
                                        <h5><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132816") %></h5>
                                        <h6></h6>
                                    </div>
                                </div>
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11 ml-auto mr-auto" style="padding-top: 25px;">
                            <asp:Literal ID="ltrDescription" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>

        $(document).ready(function () {

            $("#btnResultCheck").bind({
                click: function () {

                    window.location.href = "RegisterExamResultCheck.aspx";

                    return false;
                }
            });

        });
    </script>
</asp:Content>
