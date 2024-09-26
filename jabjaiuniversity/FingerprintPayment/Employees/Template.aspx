<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="FingerprintPayment.Employees.Template" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">

            <script type="text/javascript">

                var TemplateList = {
                    LoadListData: function (eid) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102049") %>
                        var dt = $(".TemplateList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": true,
                            "stateSave": true,
                            "ajax": { "url": "Ashx/LoadEmpEducationList.ashx", "type": "GET", "data": { "eid": eid }},
                            "columns": [
                                { "data": "StudyYear", "orderable": true },
                                { "data": "GraduationYear", "orderable": true },
                                { "data": "GraduationDate", "orderable": true },
                                { "data": "Level", "orderable": true },
                                { "data": "Qualification", "orderable": true },
                                { "data": "Major", "orderable": true },
                                { "data": "MinorSubject", "orderable": true },
                                { "data": "Institution", "orderable": true },
                                { "data": "Bursary", "orderable": true },
                                { "data": "BursaryType", "orderable": true },
                                { "data": "Country", "orderable": true },
                                { "data": "FundingAgencies", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[0, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 9, 10] },
                                { "targets": [13], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    '<button type="button" class="btn btn-primary btn-xs"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121013") %></button>' +
                                                '<button type="button" class="btn btn-primary btn-xs dropdown-toggle" data-toggle="dropdown">' +
                                                    '<span class="caret"></span>' +
                                                    '<span class="sr-only">Toggle Dropdown</span>' +
                                                '</button>' +
                                                '<ul class="dropdown-menu" role="menu">' +
                                                    '<li><a href="#"><i class="fa fa-search"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></a></li>' +
                                                    '<li class="divider"></li>' +
                                                    '<li><a href="#"><i class="fa fa-edit"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a></li>' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a></li>' +
                                                '</ul>' +
                                            '</div>';
                                    },
                                    "targets": 12
                                }
                            ]
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            dt.column(12, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 7 }).data()[0];
                                $(cell).find(".fa-search").parent().attr("href", "EmpEducation.aspx?v=view&eid=" + eid + "&id=" + id);
                                $(cell).find(".fa-edit").parent().attr("href", "EmpEducation.aspx?v=modify&eid=" + eid + "&id=" + id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("tableNo", 1);
                            });
                        });
                    },
                    LoadListData2: function (eid) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102065") %>
                        var dt = $(".TemplateList #tableData2").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": true,
                            "stateSave": true,
                            "ajax": { "url": "Ashx/LoadEmpFameList.ashx", "type": "GET", "data": { "eid": eid }},
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "Type", "orderable": true },
                                { "data": "Department", "orderable": true },
                                { "data": "Year", "orderable": true },
                                { "data": "UpdateDate", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "id", "orderable": false }
                            ],
                            "order": [[5, "desc"]],
                            "columnDefs": [
                                { className: "text-center", "targets": [0, 3, 4, 5] },
                                { "targets": [6], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                                    '<button type="button" class="btn btn-primary btn-xs"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121013") %></button>' +
                                                '<button type="button" class="btn btn-primary btn-xs dropdown-toggle" data-toggle="dropdown">' +
                                                    '<span class="caret"></span>' +
                                                    '<span class="sr-only">Toggle Dropdown</span>' +
                                                '</button>' +
                                                '<ul class="dropdown-menu" role="menu">' +
                                                    '<li><a href="#"><i class="fa fa-search"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></a></li>' +
                                                    '<li class="divider"></li>' +
                                                    '<li><a href="#"><i class="fa fa-edit"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a></li>' +
                                                    '<li><a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="Are you sure you want to remove this item?"><i class="fa fa-remove"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a></li>' +
                                                '</ul>' +
                                            '</div>';
                                    },
                                    "targets": 5
                                }
                            ]
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            var info = dt.page.info();
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (info.page * info.length) + (i + 1) + '.';
                            });
                            dt.column(5, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var id = dt.cells({ row: i, column: 6 }).data()[0];
                                $(cell).find(".fa-search").parent().attr("href", "EmpEducation.aspx?v=view&eid=" + eid + "&id=" + id);
                                $(cell).find(".fa-edit").parent().attr("href", "EmpEducation.aspx?v=modify&eid=" + eid + "&id=" + id);
                                $(cell).find(".fa-remove").parent().attr("id", id);
                                $(cell).find(".fa-remove").parent().attr("tableNo", 2);
                            });
                        });
                    },
                    RemoveItem: function (eid, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/RemoveItem",
                            data: '{eid: ' + eid + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: TemplateList.OnSuccessRemove,
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
                    RemoveItem2: function (eid, id) {
                        $.ajax({
                            type: "POST",
                            url: "EmpEducation.aspx/RemoveItem2",
                            data: '{eid: ' + eid + ', id: ' + id + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: TemplateList.OnSuccessRemove,
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
                    OnSuccessRemove: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                                ReloadListData();
                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121006") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    }
                }

                $(document).ready(function () {

                    // Datatable Section
                    TemplateList.LoadListData(<%=Request.QueryString["eid"]%>);
                    TemplateList.LoadListData2(<%=Request.QueryString["eid"]%>);

                    // Modal Section
                    $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                        $title = $(e.relatedTarget).attr('data-title');
                        $(this).find('.modal-title').text($title);
                        $message = $(e.relatedTarget).attr('data-message');
                        $(this).find('.modal-body p').text($message);
                        $id = $(e.relatedTarget).attr('id');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('id', $id);
                        $tableNo = $(e.relatedTarget).attr('tableNo');
                        $(this).find('.modal-footer #modalConfirmRemove').attr('tableNo', $tableNo);
                    });
                    $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                        $('#modalNotifyConfirmRemove').modal('hide');

                        $("#modalWaitDialog").modal('show');

                        // Remove command
                        if($(this).attr('tableNo') == 1){
                            TemplateList.RemoveItem(<%=Request.QueryString["eid"]%>, $(this).attr('id'));
                        }
                        else{
                            TemplateList.RemoveItem2(<%=Request.QueryString["eid"]%>, $(this).attr('id'));
                        }

                    });

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">

            <script type="text/javascript">

                var TemplateForm = {
                    GetItem: function (empID) {
                        $.ajax({
                            type: "POST",
                            url: "Template.aspx/GetItem",
                            data: '{empID: ' + empID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: TemplateForm.OnSuccessGet,
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

                            // Initial value


                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);


                                $("#iptTemplate1").val($(this).find("F1").text());
                                $("#iptTemplate2").val($(this).find("F2").text());
                            });

                        }
                    },
                    SaveItem: function (type, type2, type3) {
                        $.ajax({
                            type: "POST",
                            url: "TemplateForm.aspx/SaveItem",
                            data: '{type: \'' + type + '\', type2: \'' + type2 + '\', type3: \'' + type3 + '\'}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: TemplateForm.OnSuccessSave,
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

                                TemplateForm.ClearForm();

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                    // Close modal

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %> [{0}]';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default:

                                var ar = response.d.split("-");
                                if (ar[0] == "warning") {
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %> [{0}]'.format(ar[1]);
                                }

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearForm: function (response) {
                        //$('#frm').bootstrapValidator('resetForm', true);

                        TemplateForm.ClearValue();
                    },
                    ClearValue: function (response) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInfo.aspx/ClearValue",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {

                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

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

                    $("#save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $("#cancel").bind({
                        click: function () {

                            // Close modal

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

                        TemplateForm.SaveItem(Encode($("input[name='rdoType']:checked").val()), Encode($("#iptTemplate1").val()), Encode($("#iptTemplate2").val()));

                    });

                    // Load info command
                    TemplateForm.GetItem(<%=Request.QueryString["eid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
