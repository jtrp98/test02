<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="NewsReportView.aspx.cs" Inherits="FingerprintPayment.Message.NewsReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-form.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .material-form .form-control {
            padding: 0px 0px 0px 5px;
            margin-top: 0px;
        }

        .files-uploaded-list {
            margin: 5px 0px 5px 0px;
        }

        .files-uploaded {
            position: relative;
            text-align: center;
            padding: 8px;
            border: 1px solid gray;
            border-radius: 5px;
            transition: 0.2s;
            display: inline-block;
            width: 100%;
        }

            .files-uploaded img {
                width: 65px;
                height: 65px;
                float: left;
            }

            .files-uploaded p.file-name {
                float: left;
                margin-left: 20px;
                overflow-wrap: break-word;
                text-align: left;
                width: 85%;
            }

            .files-uploaded .file-download, .files-uploaded .file-view {
                float: right;
                margin-top: 2px;
                padding: 0px;
            }

                .files-uploaded .file-download .material-icons, .files-uploaded .file-view .material-icons {
                    font-size: x-large;
                }

            .files-uploaded .file-size {
                font-size: 12px;
                color: #aaa;
            }

        .user-table tr th, .user-list table tr th {
            font-weight: bold !important;
        }

        .user-table tr th, .user-table tr td {
            text-align: center;
        }

            .user-table tr td:nth-child(4) {
                text-align: left;
            }

        .form-check .form-check-label label {
            padding-right: 15px;
            color: black;
        }

        .input-data {
            margin: 7px 0px 5px 5px;
        }

            .input-data .material-icons {
                vertical-align: sub;
            }

        .select-data {
            margin: 15px 0px 5px 7px;
        }

        /*Chart Style*/
        .ct-chart .ct-series-a .ct-slice-pie, .ct-chart .ct-series-a .ct-slice-donut-solid, .ct-chart .ct-series-a .ct-area {
            fill: #4caf50;
        }

        .ct-chart .ct-series-b .ct-slice-pie, .ct-chart .ct-series-b .ct-slice-donut-solid, .ct-chart .ct-series-b .ct-area {
            fill: #ff6f6a;
        }

        .ct-chart .ct-series-c .ct-slice-pie, .ct-chart .ct-series-c .ct-slice-donut-solid, .ct-chart .ct-series-c .ct-area {
            fill: #ff9800;
        }

        .ct-label {
            stroke: black;
            font-size: 16px;
        }

            .ct-label p {
                /*stroke: black;*/
                color: black;
                font-size: 17px;
                /*font-weight: bold;*/
            }

        h4.chart-topic {
            font-weight: bold;
        }

        .ct-legend {
            position: relative;
            z-index: 10;
            list-style: none;
            text-align: center;
            padding: 0px;
        }

            .ct-legend li {
                position: relative;
                padding-left: 23px;
                margin-right: 10px;
                margin-bottom: 3px;
                cursor: pointer;
                display: inline-block;
            }

            .ct-legend .ct-series-0:before {
                background-color: #4caf50;
                border-color: #4caf50;
            }

            .ct-legend .ct-series-1:before {
                background-color: #ff6f6a;
                border-color: #ff6f6a;
            }

            .ct-legend .ct-series-2:before {
                background-color: #ff9800;
                border-color: #ff9800;
            }

            .ct-legend li:before {
                width: 12px;
                height: 12px;
                position: absolute;
                left: 5px;
                content: '';
                border: 3px solid transparent;
                border-radius: 6px;
                top: 5px;
            }

        .user-list-search .form-control {
            padding: 0px 35px 0px 0px !important;
            margin-top: 0px;
        }

        .user-list-search .form-control-feedback {
            opacity: 1;
        }

        .user-list-search .col-md-10 .col-md-6 .col-form-label {
            padding: 17px 10px 0 0;
        }

        @media (min-width: 320px) and (max-width: 767px) {
            .div-bag {
                width: fit-content;
                float: left;
                text-align: left;
            }
        }

        @media (min-width: 1200px) {
            .modal-xl {
                max-width: 950px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01561") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401054") %>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card material-form">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">insert_drive_file</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401054") %></h4>
                </div>
                <div class="card-body">
                    <div>
                        <form id="frmNewsForm" class="form-padding">
                            <div class="row">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401045") %> : </span></label>
                                    <p class="input-data"><%=_NewsInfo.Title %></p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 mr-auto ml-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401046") %> : </span></label>
                                    </div>
                                    <p class="select-data"><%=_NewsInfo.MessageType %></p>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401047") %> : </span></label>
                                    <p class="select-data"><%=_NewsInfo.SendType %></p>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401055") %> : </span></label>
                                    <p class="select-data"><%=_NewsInfo.AcceptType %></p>
                                </div>
                            </div>
                            <div class="row date-time" style="display: none;">
                                <div class="col-md-4 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132454") %> : </span></label>
                                    <div class="form-group div-datepicker">
                                        <p class="input-data"><i class="material-icons">today</i> <%=_NewsInfo.SendDate %> &nbsp;&nbsp;<i class="material-icons">schedule</i> <%=_NewsInfo.SendTime %></p>
                                    </div>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                    <div class=""></div>
                                    <div class=""></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 mr-auto ml-auto checkbox-radios">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401048") %> : </span></label>
                                    </div>
                                    <p class="select-data"><%=_NewsInfo.GroupType %></p>
                                </div>
                                <div class="col-md-4 ml-auto mr-auto send-group">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401049") %> : </span></label>
                                    </div>
                                    <p class="select-data"><%=_NewsInfo.Group %></p>
                                </div>
                                <div class="col-md-4 ml-auto mr-auto send-individual" style="display: none;">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> : </span></label>
                                    </div>
                                    <p class="select-data"><%=_NewsInfo.GroupIndividual %></p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> : </span></label>
                                    </div>
                                    <p class="input-data" style="height: auto; min-height: 100px;"><%=_NewsInfo.Message %></p>
                                </div>
                            </div>
                            <asp:Literal ID="ltrFileList" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div class="row">
        <div class="col-md-12">
            <div class="card material-form">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">insert_drive_file</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401057") %></h4>
                </div>
                <div class="card-body">
                    <div>
                        <div class="row">
                            <div class="col-sm-6 text-center">
                                <h4 class="chart-topic"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401058") %></h4>
                                <div class="ct-chart read-status text-center"></div>
                                <div>
                                </div>
                            </div>
                            <div class="col-sm-6 text-center">
                                <h4 class="chart-topic"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401059") %></h4>
                                <div class="ct-chart message-reply text-center"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <br />
                            </div>
                        </div>
                        <div class="row user-list-search">
                            <div class="col-md-10 ml-auto mr-auto d-flex">
                                <div class="col-md-6 d-flex">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %> : </span></label>
                                    <div class="form-group" style="width: 90%;">
                                        <input id="iptSearch" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401063") %>">
                                        <span class="form-control-feedback">
                                            <i class="material-icons">search</i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6 text-right">
                                </div>
                            </div>
                        </div>
                        <div class="row send-individual-list">
                            <div class="col-md-10 ml-auto mr-auto">
                                <table id="tblUserList" class="table user-table">
                                    <thead>
                                        <tr>
                                            <th scope="col" width="7%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th scope="col" width="13%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                            <th scope="col" width="13%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></th>
                                            <th scope="col" width="37%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                            <th scope="col" width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                            <th scope="col" width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                            <th scope="col" width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401059") %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Literal ID="ltrUserList" runat="server"></asp:Literal>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <!-- Chartist JS -->
    <%--<script src="/Content/Material/assets/js/plugins/chartist.min.js"></script>--%>
    <script src="/Content/Material/assets/js/plugins/chartist.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="/Content/Material/assets/js/plugins/chartist-plugin-legend.js"></script>

    <script>

        $(document).ready(function () {

            $('body').tooltip({
                container: '.wrapper',
                selector: '[data-toggle="tooltip"]'
            });

            $(document).on('click', '.files-uploaded .file-download', function () {

                $('.tooltip').tooltip('hide');

                var link = document.createElement('a');
                link.href = $(this).data('url');
                link.click();
                link.remove();

                return false;
            });

            $(document).on('click', '.files-uploaded .file-view', function () {

                $('.tooltip').tooltip('hide');

                var link = document.createElement('a');
                link.target = '_blank';
                link.href = $(this).data('url');
                link.click();
                link.remove();

                return false;
            });

            $("#iptSearch").on("input", function () {
                var value = $(this).val().toLowerCase();
                $("#tblUserList > tbody > tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });

            // Modal Section
            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function (e) {
                if ($("#modalWaitDialog").data('bs.modal')?._isShown) {
                    $("#modalWaitDialog").modal('hide');
                }
            });

            var sum = function (a, b) { return a + b };

            <%=ReadStatusChart%>

            <%=MessageReplyChart%>

        });

    </script>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
