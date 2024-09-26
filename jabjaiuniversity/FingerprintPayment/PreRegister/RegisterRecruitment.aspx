<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RegisterRecruitment.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterRecruitment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/style-emp.css?v=<%=DateTime.Now.Ticks%>" />

    <!-- bootstrap-toggle -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" />

    <style>
        .bootstrap-filestyle .badge {
            color: #f0ad4e;
            background-color: #fff;
            border-radius: 10px;
            min-width: 10px;
            padding: 3px 5px;
            font-weight: bold;
            margin-left: 5px;
        }

        .bootstrap-filestyle.input-group {
            width: auto !important;
        }

        #modalShowForm .modal-content {
            width: 700px;
        }

        .backup-plans.hide {
            display: none;
        }

        .bootstrap-select.show-tick .dropdown-menu .selected span.check-mark {
            top: 10px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="panel with-nav-tabs panel-default" style="border: 0px solid transparent;">
            <div class="panel-body">
                <div class="" id="divContent"></div>
            </div>
        </div>
    </div>

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <!-- bootstrap-toggle -->
    <script type='text/javascript' src="/assets/plugins/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

    <!-- filestyle -->
    <script type='text/javascript' src="assets/js/bootstrap-filestyle.min.js"></script>

    <script type='text/javascript'>

        var modalForm = {
            showForm: function (formPage, title, action, callShow, rid) {
                $.get(formPage, { "v": action, "rid": rid },
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

        function activateBootstrapSelect(selector) {
            if ($(selector).length != 0) {
                $(selector).selectpicker();
            }
        }

        $(function () {

            // initial first tab content
            manageContent.redirect('RegisterRecruitmentData.aspx?v=list', { "id": 0 });

            $('#modalShowForm').off().on('show.bs.modal', function (e) {

                $formName = $(e.relatedTarget).attr('form-name');
                $formAction = $(e.relatedTarget).attr('form-action');
                $formTitle = $(e.relatedTarget).attr('form-title');
                $rid = $(e.relatedTarget).attr('rid');
                if (typeof $rid === 'undefined') {
                    $rid = 0;
                }

                modalForm.showForm($formName, $formTitle, $formAction, false, $rid);

            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
