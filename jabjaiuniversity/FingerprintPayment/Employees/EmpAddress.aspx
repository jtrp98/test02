<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpAddress.aspx.cs" Inherits="FingerprintPayment.Employees.EmpAddress" %>

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
            <div class="empAddressForm">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-12 mb-12">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %></p>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3">
                            <label for="iptNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                            <input type="text" class="form-control" id="iptNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" required>
                        </div>
                        <div class="col-md-1 mb-1">
                            <label for="iptVillageNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %></label>
                            <input type="text" class="form-control" id="iptVillageNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptVillage"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %></label>
                            <input type="text" class="form-control" id="iptVillage"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptAlley"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                            <input type="text" class="form-control" id="iptAlley"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>">
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4">
                            <label for="iptBuilding"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %></label>
                            <input type="text" class="form-control" id="iptBuilding"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                            <input type="text" class="form-control" id="iptRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="sltDistrict"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                            <select id="sltDistrict" name="sltDistrict[]" class="form-control"
                                required>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4">
                            <label for="sltAmphur"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %></label>
                            <select id="sltAmphur" name="sltAmphur[]" class="form-control"
                                required>
                            </select>
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="sltProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                            <select id="sltProvince" name="sltProvince[]" class="form-control"
                                required>
                            </select>
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                            <input type="text" class="form-control" id="iptPostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4">
                            <label for="iptPhoneNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %></label>
                            <input type="text" class="form-control" id="iptPhoneNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                        </div>
                        <div class="col-md-4 mb-4">
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-12 mb-12">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102029") %></p>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3">
                            <label for="iptNo2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
                            <input type="text" class="form-control" id="iptNo2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" required>
                        </div>
                        <div class="col-md-1 mb-1">
                            <label for="iptVillageNo2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %></label>
                            <input type="text" class="form-control" id="iptVillageNo2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptVillage2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %></label>
                            <input type="text" class="form-control" id="iptVillage2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptAlley2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                            <input type="text" class="form-control" id="iptAlley2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>">
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4">
                            <label for="iptBuilding2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %></label>
                            <input type="text" class="form-control" id="iptBuilding2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptRoad2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                            <input type="text" class="form-control" id="iptRoad2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="sltDistrict2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                            <select id="sltDistrict2" name="sltDistrict2[]" class="form-control"
                                required>
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4">
                            <label for="sltAmphur2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %></label>
                            <select id="sltAmphur2" name="sltAmphur2[]" class="form-control"
                                required>
                            </select>
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="sltProvince2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                            <select id="sltProvince2" name="sltProvince2[]" class="form-control"
                                required>
                            </select>
                        </div>
                        <div class="col-md-4 mb-4">
                            <label for="iptPostalCode2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                            <input type="text" class="form-control" id="iptPostalCode2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" required>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-4 mb-4">
                            <label for="iptPhoneNumber2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %></label>
                            <input type="text" class="form-control" id="iptPhoneNumber2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %>">
                        </div>
                        <div class="col-md-4 mb-4">
                        </div>
                        <div class="col-md-4 mb-4">
                        </div>
                    </div>
                    <div class="row text-right">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-default">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var empAddressForm = {
                    GetItem: function (empID) {
                        $.ajax({
                            type: "POST",
                            url: "EmpAddress.aspx/GetItem",
                            data: '{empID: ' + empID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empAddressForm.OnSuccessGet,
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

                                $("#iptNo").val($(this).find("F1").text());
                                $("#iptVillageNo").val($(this).find("F2").text());
                                $("#iptVillage").val($(this).find("F3").text());
                                $("#iptAlley").val($(this).find("F4").text());
                                $("#iptBuilding").val($(this).find("F5").text());
                                $("#iptRoad").val($(this).find("F6").text());
                                $("#sltDistrict").val($(this).find("F7").text());
                                $("#sltAmphur").val($(this).find("F8").text());
                                $("#sltProvince").val($(this).find("F9").text());
                                $("#iptPostalCode").val($(this).find("F10").text());
                                $("#iptPhoneNumber").val($(this).find("F11").text());

                                $("#iptNo2").val($(this).find("F12").text());
                                $("#iptVillageNo2").val($(this).find("F13").text());
                                $("#iptVillage2").val($(this).find("F14").text());
                                $("#iptAlley2").val($(this).find("F15").text());
                                $("#iptBuilding2").val($(this).find("F16").text());
                                $("#iptRoad2").val($(this).find("F17").text());
                                $("#sltDistrict2").val($(this).find("F18").text());
                                $("#sltAmphur2").val($(this).find("F19").text());
                                $("#sltProvince2").val($(this).find("F20").text());
                                $("#iptPostalCode2").val($(this).find("F21").text());
                                $("#iptPhoneNumber2").val($(this).find("F22").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpAddress.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empAddressForm.OnSuccessSave,
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
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "EmpAddress.aspx/ClearSessionID",
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

                    $(".empAddressForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".empAddressForm #cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            empAddressForm.ClearSession(function () {
                                window.location.replace("EmployeeList.aspx");
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

                        $("#modalWaitDialog").modal('show');

                        // Save command
                        var data = new Array();
                        data[0] = "0";
                        data[1] = $("#iptNo").val();
                        data[2] = $("#iptVillageNo").val();
                        data[3] = $("#iptVillage").val();
                        data[4] = $("#iptAlley").val();
                        data[5] = $("#iptBuilding").val();
                        data[6] = $("#iptRoad").val();
                        data[7] = $("#sltDistrict").children("option:selected").val();
                        data[8] = $("#sltAmphur").children("option:selected").val();
                        data[9] = $("#sltProvince").children("option:selected").val();
                        data[10] = $("#iptPostalCode").val();
                        data[11] = $("#iptPhoneNumber").val();

                        data[12] = $("#iptNo2").val();
                        data[13] = $("#iptVillageNo2").val();
                        data[14] = $("#iptVillage2").val();
                        data[15] = $("#iptAlley2").val();
                        data[16] = $("#iptBuilding2").val();
                        data[17] = $("#iptRoad2").val();
                        data[18] = $("#sltDistrict2").children("option:selected").val();
                        data[19] = $("#sltAmphur2").children("option:selected").val();
                        data[20] = $("#sltProvince2").children("option:selected").val();
                        data[21] = $("#iptPostalCode2").val();
                        data[22] = $("#iptPhoneNumber2").val();

                        empAddressForm.SaveItem(data);

                    });

                    // Initial data
                    initFunction.setDropdown('#sltDistrict', 'District', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>');
                    initFunction.setDropdown('#sltAmphur', 'Amphur', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103068") %></option>');
                    initFunction.setDropdown('#sltProvince', 'Province', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>');
                    initFunction.setDropdown('#sltDistrict2', 'District', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>');
                    initFunction.setDropdown('#sltAmphur2', 'Amphur', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103068") %></option>');
                    initFunction.setDropdown('#sltProvince2', 'Province', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>');


                    // Load info command
                    empAddressForm.GetItem(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
