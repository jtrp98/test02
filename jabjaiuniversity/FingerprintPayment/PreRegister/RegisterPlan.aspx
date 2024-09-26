<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RegisterPlan.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/style-emp.css?v=<%=DateTime.Now.Ticks%>" />

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

    <script type='text/javascript'>

        var modalForm = {
            showForm: function (formPage, title, action, callShow, rid, lid) {
                $.get(formPage, { "v": action, "rid": rid, "lid": lid },
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
            manageContent.redirect('RegisterPlanData.aspx?v=list', { "id": 0 });

            $('#modalShowForm').off().on('show.bs.modal', function (e) {

                $formName = $(e.relatedTarget).attr('form-name');
                $formAction = $(e.relatedTarget).attr('form-action');
                $formTitle = $(e.relatedTarget).attr('form-title');
                $rid = $(e.relatedTarget).attr('rid');
                if (typeof $rid === 'undefined') {
                    $rid = 0;
                }
                $lid = $(e.relatedTarget).attr('lid');
                if (typeof $lid === 'undefined') {
                    $lid = 0;
                }

                modalForm.showForm($formName, $formTitle, $formAction, false, $rid, $lid);

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
