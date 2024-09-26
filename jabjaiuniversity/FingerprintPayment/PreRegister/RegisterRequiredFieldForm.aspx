<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterRequiredFieldForm.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterRequiredFieldForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    </style>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="Form01Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-01" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132958") %><br />
                                        (List of Personal Information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField01" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave01" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel01" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave01").bind({
                        click: function () {

                            var data = [];
                            $('.table-01 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel01").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(1);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form02Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-02" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132959") %><br />
                                        (List of Permanent Address)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField02" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave02" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel02" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave02").bind({
                        click: function () {

                            var data = [];
                            $('.table-02 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel02").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(2);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form03Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-03" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132960") %><br />
                                        (List of Current Address)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField03" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave03" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel03" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave03").bind({
                        click: function () {

                            var data = [];
                            $('.table-03 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel03").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(3);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form04Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-04" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132961") %><br />
                                        (List of Father information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField04" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave04" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel04" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave04").bind({
                        click: function () {

                            var data = [];
                            $('.table-04 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel04").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(4);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form05Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-05" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132962") %><br />
                                        (List of Mother information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField05" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave05" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel05" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave05").bind({
                        click: function () {

                            var data = [];
                            $('.table-05 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel05").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(5);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form06Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-06" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132963") %><br />
                                        (List of Parent information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField06" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave06" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel06" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave06").bind({
                        click: function () {

                            var data = [];
                            $('.table-06 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel06").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(6);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form07Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-07" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132964") %><br />
                                        (List of Educational information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField07" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave07" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel07" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave07").bind({
                        click: function () {

                            var data = [];
                            $('.table-07 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel07").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(7);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form08Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-08" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132965") %><br />
                                        (List of health information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField08" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave08" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel08" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave08").bind({
                        click: function () {

                            var data = [];
                            $('.table-08 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel08").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(8);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
        <asp:View ID="Form09Content" runat="server">
            <div style="padding: 15px 45px;">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-no-bordered table-09" cellspacing="0" width="100%" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br />
                                        No.</th>
                                    <th style="width: 61%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103123") %><br />
                                        (List of Document information)</th>
                                    <th style="width: 20%" class="disabled-sorting text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102148") %><br />
                                        (usage status)</th>
                                    <th style="width: 10%" class="disabled-sorting text-center"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltrRequiredField09" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center" style="padding: 25px 0px 20px 0px;">
                        <button id="btnSave09" class="btn btn-success">
                            <span class="btn-label">
                                <i class="material-icons">save</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                        </button>
                        <button id="btnCancel09" class="btn btn-danger">
                            <span class="btn-label">
                                <i class="material-icons">close</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                        </button>
                    </div>
                </div>
            </div>
            <script type='text/javascript'>
                $(function () {

                    $("#btnSave09").bind({
                        click: function () {

                            var data = [];
                            $('.table-09 .switch-button').each(function (i) {
                                data.push({ vfiId: $(this).data('vfiid'), categoryId: $(this).data('cid'), status: $(this).prop('checked') });
                            });
                            requiredField.SaveRequiredField(data);

                            return false;
                        }
                    });

                    $("#btnCancel09").bind({
                        click: function () {

                            requiredField.ForceReloadActivedTab(9);

                            return false;
                        }
                    });

                });
            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
