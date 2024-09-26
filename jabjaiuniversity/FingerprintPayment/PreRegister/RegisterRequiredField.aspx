<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RegisterRequiredField.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterRequiredField" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Permission/asset/toggle.css" rel="stylesheet">
    <style>
        .nav-pills .nav-item .nav-link {
            border-radius: 10px;
        }

            .nav-pills .nav-item .nav-link span {
                white-space: pre;
                font-size: 10px;
            }

        .el-switch .el-switch-style {
            height: 1.6em;
            left: 0;
            background: #ff514b;
            -webkit-border-radius: 0.8em;
            border-radius: 0.8em;
            display: inline-block;
            position: relative;
            top: 0;
            -webkit-transition: all 0.3s ease-in-out;
            transition: all 0.3s ease-in-out;
            width: 3em;
            cursor: pointer;
        }

        .table > tbody > tr > td {
            padding: 6px 8px;
        }

        /* Progress Style */
        .loader {
            border: 8px solid #f3f3f3;
            border-radius: 50%;
            border-top: 8px solid #3498db;
            width: 60px;
            height: 60px;
            -webkit-animation: spin 1.5s linear infinite; /* Safari */
            animation: spin 1.5s linear infinite;
        }

        /* Safari */
        /* Progress Style */
        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103180") %> / Set up online student admissions information
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card stdProfile">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">settings</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103180") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="panel with-nav-tabs panel-default">
                        <div class="panel-heading">
                            <ul class="nav nav-pills nav-pills-rose nav-justified" role="tablist" id="requiredFieldTabs">
                                <li class="nav-item active"><a class="nav-link active show" href="#tab1" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %><br>
                                    <span>(Personal Information)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab2" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %><br>
                                    <span>(Permanent Address)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab3" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %><br>
                                    <span>(Current Address)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab4" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %><br>
                                    <span>(Father Information)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab5" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %><br>
                                    <span>(Mother Information)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab6" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %><br>
                                    <span>(Parent Information)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab7" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132857") %><br>
                                    <span>(Educational Information)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab8" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %><br>
                                    <span>(Health Information)</span></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab9" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103095") %><br>
                                    <span>(Document Information)</span></a></li>
                            </ul>
                        </div>
                        <div class="panel-body" style="padding: 0px;">
                            <div class="tab-content">
                                <div class="tab-pane fade in active show" id="tab1">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab2">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab3">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab4">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab5">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab6">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab7">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab8">
                                    <div class="content1"></div>
                                </div>
                                <div class="tab-pane fade" id="tab9">
                                    <div class="content1"></div>
                                </div>
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

    <script>

        var flagTab = [0, 0, 0, 0, 0, 0, 0, 0, 0];
        var urlTab = [['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx'],
        ['RegisterRequiredFieldForm.aspx']];

        $(function () {

            $.ajaxSetup({
                statusCode: {
                    500: function () {
                        window.location.replace("/Default.aspx");
                    }
                }
            });

            if (jQuery().dataTable) {
                $.fn.dataTable.ext.errMode = 'none';
            }

            // initial first tab content
            flagTab[0] = 1;
            $('#tab1').prepend('<div class="loader"></div>');
            $.each(urlTab[0], function (index, url) {
                $.get(url, { "v": "form01" },
                    function (data) {
                        $('#tab1 .content' + (index + 1)).html(data);

                        $('#tab1 .loader').remove();
                    }
                );
            });

            // tab event click
            $('#requiredFieldTabs li:not(.dropdown) a').click(function (e) {

                // find index tabs is active
                var tabIndex = parseInt($(this).attr('href').replace('#tab', '')) - 1;
                if (flagTab[tabIndex] == 0) {

                    // already first click
                    flagTab[tabIndex] = 1;

                    $('#tab' + (tabIndex + 1)).prepend('<div class="loader"></div>');
                    $.each(urlTab[tabIndex], function (index, url) {
                        $.get(url, { "v": "form" + ('00' + (tabIndex + 1)).slice(-2), "d": "<%=DateTime.Now.Ticks%>" },
                            function (data) {
                                $('#tab' + (tabIndex + 1) + ' .content' + (index + 1)).html(data).resize();

                                $('#tab' + (tabIndex + 1) + ' .loader').remove();
                            }
                        );
                    });
                }
                else {
                    // force resize
                    $.each(urlTab[tabIndex], function (index, url) {
                        $('#tab' + (tabIndex + 1) + ' .content' + (index + 1)).resize();
                    });
                }
            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });

        });

        $(document).on('click', '.switch-button', function () {
            $('div[data-vfiid=' + $(this).attr('data-vfiid') + '] p').css('background-color', this.checked ? '#4caf50' : '#ff514b').html(this.checked ? '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132956") %><br/>(Required)' : '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132957") %><br/>(Not Required)');
        });

        var requiredField = {
            SaveRequiredField: function (data) {

                $("#modalWaitDialog").modal('show');

                $.ajax({
                    type: "POST",
                    url: "RegisterRequiredField.aspx/SaveRequiredField",
                    data: JSON.stringify({ requiredFieldDatas: data }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: requiredField.OnSuccessSave,
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                        $("#modalWaitDialog").modal('hide');
                    }
                });
            },
            OnSuccessSave: function (response) {
                var r = JSON.parse(response.d);

                var title = "";
                var body = "";

                if (r.success) {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00895") %>';
                }
                else {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132449") %> [' + r.message + ']';
                }

                $("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            ForceReloadActivedTab: function (tabIndex) {
                $('#tab' + tabIndex).prepend('<div class="loader"></div>');
                $.each(urlTab[tabIndex - 1], function (index, url) {
                    $.get(url, { "v": "form" + ('00' + tabIndex).slice(-2) },
                        function (data) {
                            $('#tab' + tabIndex + ' .content' + (index + 1)).html(data).resize();

                            $('#tab' + tabIndex + ' .loader').remove();
                        }
                    );
                });
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
