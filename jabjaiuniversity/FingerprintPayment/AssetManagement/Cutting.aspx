<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="Cutting.aspx.cs" Inherits="FingerprintPayment.AssetManagement.Cutting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <link rel="stylesheet" href="/styles/style-asset.css" />

    <style type="text/css">
        .modal-content {
            max-width: inherit;
            height: inherit;
            margin: 0 auto;
            pointer-events: all;
        }

        .cuttingList #tableData .btn-group a {
            padding: 5px;
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
    
    <script type='text/javascript' src="/AssetManagement/Scripts/init-function.js"></script>
    
    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>
    
    <script type='text/javascript'>

        var modalForm = {
            showForm: function (formPage, title, action, callShow, year, id) {

                $.get(formPage, { "v": action, "year": year, "id": id },
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

        $(function () {

            // initial first tab content
            $('#divContent').html('<div class="loader"></div>');
            $.get('CuttingData.aspx?v=list', { "id": 0 },
                function (data) {
                    $('#divContent').html(data);
                }
            );

            $('#modalShowForm').on('show.bs.modal', function (e) {

                $formName = $(e.relatedTarget).attr('form-name');
                $formAction = $(e.relatedTarget).attr('form-action');
                $formTitle = $(e.relatedTarget).attr('form-title');
                $year = $(e.relatedTarget).attr('year');
                if (typeof $year === 'undefined') {
                    $year = 0;
                }
                $id = $(e.relatedTarget).attr('id');
                if (typeof $id === 'undefined') {
                    $id = 0;
                }

                modalForm.showForm($formName, $formTitle, $formAction, false, $year, $id);

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>

