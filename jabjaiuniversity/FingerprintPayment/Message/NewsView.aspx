<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="NewsView.aspx.cs" Inherits="FingerprintPayment.Message.NewsView" %>

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

        #frmNewsForm .material-form .form-group .form-control {
            padding: 0px 35px 0px 0px;
            margin-top: 0px;
        }

        #frmNewsForm .form-control-feedback {
            opacity: 1;
        }

        #frmNewsForm .row .col-md-10 .col-md-6 .col-form-label {
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
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01561") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card material-form">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">email</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %></h4>
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
                                    <p class="input-data" style="height: auto; min-height: 100px; white-space: pre-wrap;"><%=_NewsInfo.Message %></p>
                                </div>
                            </div>
                            <asp:Literal ID="ltrFileList" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-md-10 ml-auto mr-auto d-flex">
                                    <div class="col-md-6 d-flex">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %> : </span></label>
                                        <div class="form-group" style="width: 90%;">
                                            <input id="iptSearch" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132468") %>">
                                            <span class="form-control-feedback">
                                                <i class="material-icons">search</i>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-6 text-right">
                                        <button id="btnBackNewsList" class="btn btn-default">
                                            <span class="btn-label">
                                                <i class="material-icons">arrow_back</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                        </button>
                                        <button id="btnEditNews" class="btn btn-info">
                                            <span class="btn-label">
                                                <i class="material-icons">edit</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>
                                        </button>
                                        <button id="btnRemoveNews" class="btn btn-danger">
                                            <span class="btn-label">
                                                <i class="material-icons">delete</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="row send-individual-list">
                                <div class="col-md-10 ml-auto mr-auto mt-3">
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

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
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

            $("#btnBackNewsList").bind({
                click: function () {

                    // Redirect to group list
                    window.location.replace("NewsList.aspx");

                    return false;
                }
            });

            $("#btnEditNews").bind({
                click: function () {

                    // Redirect to group list
                    window.location.replace("NewsEditForm.aspx?nid=<%=_NewsInfo.NewsID %>");

                    return false;
                }
            });

            $("#btnRemoveNews").bind({
                click: function () {

                    $.ajax({
                        type: "POST",
                        url: "NewsView.aspx/RemoveNews",
                        data: JSON.stringify({ smsID: '<%=_NewsInfo.NewsID %>' }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            //console.log(result.d);
                            var r = JSON.parse(result.d);
                            //console.log(r);
                            var title = "";
                            var body = "";

                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02746") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                    // Redirect to student list
                                    window.location.replace("NewsList.aspx");
                                });
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132467") %> [' + r.message + ']';
                            }

                            $("#modalWaitDialog").modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                            $("#modalNotifyOnlyClose").modal('show');
                        },
                        beforeSend: function () {
                            // Display upload-progress
                        },
                        error: function (response) {
                            alert(response.responseText);
                        }
                    });

                    return false;
                }
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

        });

    </script>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
