<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="EmployeeNew.aspx.cs" Inherits="FingerprintPayment.Employees.EmployeeNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />

    <link rel="stylesheet" href="/styles/style-emp.css" />

    <link rel="stylesheet" href="/Styles/style-upload.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="empInfoForm" style="text-align: center;">
            <div id="divProfileImage" class="picture-frame" style="border: 1px solid; height: 200px; width: 170px; display: inline-block; margin-bottom: 15px;">
                <img class="img-photo" src="/Assets/images/avatar.png"
                    style="width: 100%; height: 100%;" />
                <div class="div-browse"><i class="glyphicon fa fa-camera"></i></div>
                <div class="progress" style="height: 5px; margin-right: 0px; margin-top: 0px; position: absolute; width: 100%; display: none;">
                    <div class="progress-bar" role="progressbar" style="width: 25%;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
                <div style='height: 0px; width: 0px; overflow: hidden;'>
                    <input type="file" value="upload" />
                </div>
            </div>
        </div>
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs" id="employeeTabs">
                    <li class="active"><a href="#tab1" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102009") %></a></li>
                </ul>
            </div>
            <div class="panel-body" style="padding: 0px;">
                <div class="tab-content">
                    <div class="tab-pane fade in active" id="tab1"></div>
                </div>
            </div>
        </div>
    </div>

    <% Response.WriteFile("modal-emp.inc"); %>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type='text/javascript' src="/Employees/Scripts/init-function.js"></script>
    <script type='text/javascript' src="/Employees/Scripts/com-function.js"></script>
    <script type='text/javascript' src="/Employees/Scripts/prototype.js"></script>

    <!-- DatetimePicker -->
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.js"></script>

    <script type='text/javascript' src="/scripts/upload-function.js"></script>

    <script type='text/javascript'>

        $(function () {

            // initial first tab content
            $('#tab1').html('<div class="loader"></div>');
            $.get('EmpInfo.aspx?v=form', { "eid": 0 },
                function (data) {
                    $('#tab1').html(data);
                }
            );

            // Upload function
            $(".empInfoForm #divProfileImage .div-browse").bind({
                click: function () {

                    $('.empInfoForm #divProfileImage input[type="file"]').trigger('click');

                    return false;
                }
            });

            $('.empInfoForm #divProfileImage input[type="file"]').change(function () {

                UploadFile('.empInfoForm #divProfileImage', 'file_emp_info_', 'tmp-emp');

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
