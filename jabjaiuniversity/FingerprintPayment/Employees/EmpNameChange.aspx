<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpNameChange.aspx.cs" Inherits="FingerprintPayment.Employees.EmpNameChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            List Content
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empNameChangeForm" style="padding: 15px;">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-6 mb-6">
                            <label for="iptOldFirstName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121023") %></label>
                            <input type="text" class="form-control" id="iptOldFirstName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121023") %>" required>
                        </div>
                        <div class="col-md-6 mb-6">
                            <label for="iptOldLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121024") %></label>
                            <input type="text" class="form-control" id="iptOldLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121024") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-6 mb-6">
                            <label for="iptNewFirstName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121018") %></label>
                            <input type="text" class="form-control" id="iptNewFirstName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121018") %>" required>
                        </div>
                        <div class="col-md-6 mb-6">
                            <label for="iptNewLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121019") %></label>
                            <input type="text" class="form-control" id="iptNewLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121019") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-6 mb-6">
                            <label for="iptChabgePlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121083") %></label>
                            <input type="text" class="form-control" id="iptChabgePlace"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121083") %>" required>
                        </div>
                        <div class="col-md-6 mb-6">
                            <label for="divChangeDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121025") %></label>
                            <div class='input-group date' id='divChangeDate'>
                                <input type='text' class="form-control"
                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121025") %>" required />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row text-right">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-default" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var empNameChangeForm = {
                    GetItem: function (empID, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpNameChange.aspx/GetItem",
                            data: '{empID: ' + empID + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empNameChangeForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $("#iptOldFirstName").val($(this).find("F1").text());
                                $("#iptOldLastName").val($(this).find("F2").text());
                                $("#iptNewFirstName").val($(this).find("F3").text());
                                $("#iptNewLastName").val($(this).find("F4").text());
                                $("#divChangeDate > :input").val($(this).find("F5").text());
                                $("#iptChabgePlace").val($(this).find("F6").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpNameChange.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empNameChangeForm.OnSuccessSave,
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
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                    // Close modal
                                    empNameChangeForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        empLeaveList.ReloadListData2();
                                    });

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "EmpNameChange.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    }
                }

                $(document).ready(function () {

                    $(".empNameChangeForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".empNameChangeForm #cancel").bind({
                        click: function () {

                            // Close modal
                            empNameChangeForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });
                    $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').on('click', function () {
                        $('#modalNotifyConfirmSave').modal('hide');

                        $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalWaitDialog").modal('show');

                        // Save command
                        var data = new Array();
                        data[0] = "0";
                        data[1] = $("#iptOldFirstName").val();
                        data[2] = $("#iptOldLastName").val();
                        data[3] = $("#iptNewFirstName").val();
                        data[4] = $("#iptNewLastName").val();
                        data[5] = $("#divChangeDate > :input").val();
                        data[6] = $("#iptChabgePlace").val();

                        empNameChangeForm.SaveItem(data);

                    });

                    // Initial data


                    $('#divChangeDate').datetimepicker({
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th'
                    });


                    // Load info command
                    empNameChangeForm.GetItem(<%=Request.QueryString["eid"]%>, <%=Request.QueryString["id"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
