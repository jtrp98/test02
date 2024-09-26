<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="VisitHouse.aspx.cs" Inherits="FingerprintPayment.VisitHouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />

    <link rel="stylesheet" href="/Styles/style-upload.css" />

    <link rel="stylesheet" href="/Styles/style-visithouse.css" />

    <style type="text/css">
        .modal-content {
            width: 60%;
            max-width: inherit;
            height: inherit;
            margin: 0 auto;
            pointer-events: all;
        }

            .modal-content .modal-title {
                font-size: 30px;
            }

        .learningCenterList #tableData .btn-group a {
            padding: 5px;
        }
    </style>
    <style type="text/css">
        .table > tbody > tr > td.vertical-align-middle {
            vertical-align: middle;
        }

        .table > thead > tr > th.vertical-align-middle {
            vertical-align: middle;
            padding-right: 10px;
        }

        .ui-menu {
            list-style: none;
            padding: 2px;
            margin: 0;
            display: block;
            float: left;
        }

            .ui-menu .ui-menu {
                margin-top: -3px;
            }

            .ui-menu .ui-menu-item {
                margin: 0;
                padding: 0;
                zoom: 1;
                float: left;
                clear: left;
                width: 100%;
            }

                .ui-menu .ui-menu-item a {
                    text-decoration: none;
                    display: block;
                    padding: .2em .4em;
                    line-height: 1.5;
                    zoom: 1;
                    cursor: pointer;
                }

                    .ui-menu .ui-menu-item a strong {
                        color: orange;
                    }

                    .ui-menu .ui-menu-item a.ui-state-hover,
                    .ui-menu .ui-menu-item a.ui-state-active {
                        font-weight: normal;
                        margin: -1px;
                    }

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary, .btn.btn-info, .btn.btn-warning {
            width: 110px;
            height: 44px;
        }

        .btn.btn-warning {
            width: 140px;
            height: 44px;
        }

        .vertical-alignment-helper {
            display: table;
            height: 100%;
            width: 100%;
        }

        .vertical-align-center {
            /* To center vertically */
            display: table-cell;
            vertical-align: middle;
        }

        #tableData tbody tr.none-finger.even:hover, #tableData tbody tr.none-finger.odd:hover {
            background-color: #fd7a7a;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-body">
                <div class="fade in" id="divContent"></div>
            </div>
        </div>
    </div>

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js"></script>
    <script type='text/javascript' src="/Scripts/prototype.js"></script>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <!-- DatetimePicker -->
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.js"></script>

    <script type='text/javascript' src="/scripts/upload-function.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>

    <script type='text/javascript'>

        var modalForm = {
            showForm: function (formPage, title, action, callShow, year, vid) {

                $.get(formPage, { "v": action, "year": year, "vid": vid },
                    function (data) {
                        $('#modalShowForm').find('.modal-body').html(data);
                    }
                );
                $('#modalShowForm').find('.modal-title').text(title);

                if (callShow) {
                    $('#modalShowForm').modal('show');
                }

            },
            hideForm: function () {

                $('#modalShowForm').modal('hide');
                $('#modalShowForm').find('.modal-body').html('');

            }
        }

        var manageContent = {
            redirect: function (url, para) {
                $('#divContent').html('<div class="loader"></div>');
                $.get(url, para,
                    function (data) {
                        $('#divContent').html(data);
                    }
                );
            }
        }

        gpsVariable = {
            Latitude: null,
            Longitude: null
        }

        $(function () {

            // initial first tab content
            manageContent.redirect('VisitHouseData.aspx?v=list', { "id": 0 });

            $('#modalShowForm').on('show.bs.modal', function (e) {

                $formName = $(e.relatedTarget).attr('form-name');
                $formAction = $(e.relatedTarget).attr('form-action');
                $formTitle = $(e.relatedTarget).attr('form-title');
                $year = $(e.relatedTarget).attr('year');
                if (typeof $year === 'undefined') {
                    $year = 0;
                }
                $vid = $(e.relatedTarget).attr('vid');
                if (typeof $vid === 'undefined') {
                    $vid = 0;
                }

                modalForm.showForm($formName, $formTitle, $formAction, false, $year, $vid);

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
