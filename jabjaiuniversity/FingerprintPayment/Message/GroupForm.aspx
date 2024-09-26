<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="GroupForm.aspx.cs" Inherits="FingerprintPayment.Message.GroupForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-form.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .material-form .btn.btn-default {
            width: 150px;
            background-color: white;
            color: black;
            border-color: #00acc1;
            border-style: solid;
            border-width: thin;
        }

        #tblPreSelectList tbody .form-check .form-check-sign .check, #tblSelectedList tbody .form-check .form-check-sign .check {
            z-index: 0;
        }

        /* Scroll Style */
        /* width */
        ::-webkit-scrollbar {
            width: 9px;
        }

        /* Track */
        ::-webkit-scrollbar-track {
            box-shadow: inset 0 0 2px #ccc;
            border-radius: 5px;
        }

        /* Handle */
        ::-webkit-scrollbar-thumb {
            background: #ccc;
            border-radius: 5px;
        }

            /* Handle on hover */
            ::-webkit-scrollbar-thumb:hover {
                background: #bbb;
            }

        .table-responsive {
            height: 505px;
            overflow-y: auto;
            overflow-x: hidden;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

            .table-responsive .table thead {
                position: sticky;
                top: 0;
                z-index: 1;
                background-color: white;
            }

            .table-responsive .table td, .table-responsive .table th {
                border: none;
                padding-top: 7px;
                padding-bottom: 7px;
                color: black;
            }

                .table-responsive .table td:nth-child(2), .table-responsive .table td:nth-child(3), .table-responsive .table td:nth-child(5) {
                    text-align: center;
                }

            .table-responsive .table .form-check {
                padding-left: inherit;
            }

            .table-responsive .table .form-group.bmd-form-group {
                margin: 0px;
                padding: 0px;
            }

            .table-responsive .table .col-form-label {
                padding: 8px 5px 0 0;
                font-size: 14px;
            }


            .table-responsive .table .form-control-feedback {
                opacity: 1;
                color: #00acc1;
            }

        .bootstrap-select.show-tick .dropdown-menu .selected span.check-mark {
            top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01561") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401066") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401074") %>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401074") %></h4>
                </div>
                <div class="card-body">
                    <div class="material-form">
                        <form id="groupForm" class="form-padding">
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401075") %></label>
                                <div class="col-sm-3">
                                    <input id="iptGroupName" name="iptGroupName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401075") %>" data-gid="<%=Request.QueryString["gid"]%>" />
                                </div>
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                                <div class="col-sm-3">
                                    <select id="sltType" class="selectpicker col-sm-12" data-size="2" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %>">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %></option>
                                    </select>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401076") %></label>
                                <div class="col-sm-3">
                                    <input id="iptGroupNameEn" name="iptGroupNameEn" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401076") %>" data-gid="<%=Request.QueryString["gid"]%>" />
                                </div>
                                <div class="col-sm-7"></div>
                            </div>
                            <div class="row student-only" style="display: none;">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                <div class="col-sm-3">
                                    <select id="sltLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                        <asp:Literal ID="ltrLevel" runat="server" />
                                    </select>
                                </div>
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                <div class="col-sm-3">
                                    <select id="sltClassroom" class="selectpicker col-sm-12" data-style="select-with-transition" multiple data-size="10" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    </select>
                                </div>
                                <div class="col-sm-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                                <div class="col-sm-3">
                                    <input id="iptName" name="iptName" type="text" class="form-control a" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401077") %>">
                                </div>
                                <div class="col-sm-1"></div>
                                <label class="col-sm-1 col-form-label text-left"></label>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 text-center" style="padding: 20px 0px 20px 0px;">
                                    <button id="btnSearch" class="btn btn-info">
                                        <span class="btn-label">
                                            <i class="material-icons">search</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <h4 class="card-title" style="margin-bottom: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401080") %></h4>
                                </div>
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-5">
                                    <h4 class="card-title" style="margin-bottom: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401081") %></h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="table-responsive">
                                        <table id="tblPreSelectList" class="table table-striped table-bordered">
                                            <thead class="text-primary">
                                                <tr>
                                                    <th style="width: 10%" class="text-center">
                                                        <div class="form-check">
                                                            <label class="form-check-label">
                                                                <input class="form-check-input pre-select-all" type="checkbox" value="">
                                                                <span class="form-check-sign">
                                                                    <span class="check"></span>
                                                                </span>
                                                            </label>
                                                        </div>
                                                    </th>
                                                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                    <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401082") %><br>
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %></th>
                                                    <th style="width: 40%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>
                                                    <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                </tr>
                                                <tr>
                                                    <th colspan="5" style="background-color: #f4f4f4">
                                                        <div class="row">
                                                            <label class="col-sm-2 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                                                            <div class="col-sm-10" style="padding-right: 25px;">
                                                                <div class="form-group">
                                                                    <input id="iptPreSelect" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401084") %>">
                                                                    <span class="form-control-feedback">
                                                                        <i class="material-icons">search</i>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--Render PreSelect List--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-sm-2 align-self-center">
                                    <div class="text-center">
                                        <button id="btnMove" class="btn btn-default">
                                            <span class="btn-label">
                                                <i class="material-icons">navigate_next</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401085") %>
                                        </button>
                                        <br />
                                        <button id="btnMoveBack" class="btn btn-default">
                                            <span class="btn-label">
                                                <i class="material-icons">navigate_before</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401086") %>
                                        </button>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="table-responsive">
                                        <table id="tblSelectedList" class="table table-striped table-bordered">
                                            <thead class="text-primary">
                                                <tr>
                                                    <th style="width: 10%" class="text-center">
                                                        <div class="form-check">
                                                            <label class="form-check-label">
                                                                <input class="form-check-input selected-all" type="checkbox" value="">
                                                                <span class="form-check-sign">
                                                                    <span class="check"></span>
                                                                </span>
                                                            </label>
                                                        </div>
                                                    </th>
                                                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                    <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401082") %><br>
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %></th>
                                                    <th style="width: 40%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>
                                                    <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                </tr>
                                                <tr>
                                                    <th colspan="5" style="background-color: #f4f4f4">
                                                        <div class="row">
                                                            <label class="col-sm-2 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                                                            <div class="col-sm-10" style="padding-right: 25px;">
                                                                <div class="form-group">
                                                                    <input id="iptSelected" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401084") %>">
                                                                    <span class="form-control-feedback">
                                                                        <i class="material-icons">search</i>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--Render Selected List--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 text-center" style="padding: 40px 0px 20px 0px;">
                                    <button id="btnSaveGroup" class="btn btn-success">
                                        <span class="btn-label">
                                            <i class="material-icons">save</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                    </button>
                                    <button id="btnCancelGroup" class="btn btn-danger">
                                        <span class="btn-label">
                                            <i class="material-icons">close</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                                    </button>
                                </div>
                            </div>
                        </form>
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
        var groupForm = {
            PreSelect: [],
            Selected: [],
            GetData: function (groupID) {
                if (groupID != '0' && groupID != '') {
                    $.ajax({
                        type: "POST",
                        url: "GroupForm.aspx/GetData",
                        data: '{groupID: \'' + groupID + '\'}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: groupForm.OnSuccessGetData,
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
            },
            OnSuccessGetData: function (response) {
                var r = JSON.parse(response.d);
                //console.log(r);
                if (r.success) {
                    $("#iptGroupName").val(r.groupData.groupName).prop("disabled", !!r.groupData.groupDefault);
                    $("#iptGroupNameEn").val(r.groupData.groupNameEn).prop("disabled", !!r.groupData.groupDefault);

                    groupForm.Selected = r.groupData.userData;

                    groupForm.RenderList(groupForm.Selected, '#tblSelectedList', 'selected-one');
                }
                else {
                    var title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    var body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111005") %> [' + r.message + ']';

                    $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                    $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                    $("#modalNotifyOnlyClose").modal('show');
                }
            },
            SaveGroup: function (data) {
                //console.log(data);
                $.ajax({
                    type: "POST",
                    url: "GroupForm.aspx/SaveGroup",
                    data: JSON.stringify({ groupData: data }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: groupForm.OnSuccessSave,
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

                    $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                        // Redirect to student list
                        window.location.replace("GroupList.aspx");
                    });
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
            RenderList: function (data, tableName, checkboxClass) {
                //console.log(data);

                var preSelectList = '';
                var type = '';
                var level = '';
                var room = '';
                var no = 1;
                $.each(data, function (i, obj) {
                    if (type != obj.type || level != obj.level || room != obj.room) {
                        if (obj.type == 'student') {
                            preSelectList += `<tr class="row-header">
                                                <td colspan="5" style="background-color: #d8d8d8">
                                                    <p class="" style="margin: 0px 0px 0px 18px;">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> `+ obj.level + `/` + obj.room + `
                                                    </p>
                                                </td>
                                              </tr>`;
                        }
                        else if (obj.type == 'employee') {
                            preSelectList += `<tr class="row-header">
                                                <td colspan="5" style="background-color: #d8d8d8">
                                                    <p class="" style="margin: 0px 0px 0px 18px;">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %>
                                                    </p>
                                                </td>
                                              </tr>`;
                        }
                        type = obj.type; level = obj.level; room = obj.room;
                    }

                    preSelectList += `<tr>
                                        <td>
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <input class="form-check-input `+ checkboxClass + `" type="checkbox" value="" data-id="` + obj.id + `">
                                                    <span class="form-check-sign">
                                                        <span class="check"></span>
                                                    </span>
                                                </label>
                                            </div>
                                        </td>
                                        <td>`+ no + `</td>
                                        <td>`+ obj.code + `</td>
                                        <td>`+ obj.title + obj.name + ' ' + obj.lastname + `</td>
                                        <td>`+ (obj.type == 'student' ? obj.level + '/' + obj.room : '') + `</td>
                                      </tr>`;

                    no++;
                });

                $(tableName + ' tbody').html(preSelectList);
            }
        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "GroupForm.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '<option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        $(document).ready(function () {

            // Validate rule for groupForm
            $("#groupForm").validate({
                rules: {
                    iptGroupName: "required",
                    iptGroupNameEn: "required"
                },
                messages: {
                    iptGroupName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptGroupNameEn: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "iptGroupName": 
                        case "iptGroupNameEn": error.insertAfter(element); break;
                        case "slt": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#sltType").change(function () {

                switch ($(this).val()) {
                    case '0':
                        $('.student-only').show();
                        $('#iptName').attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401077") %>");
                        break;
                    case '1':
                        $('.student-only').hide();
                        $('#iptName').attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %>");
                        break;
                }

            });

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClassroom');

            });

            $('#sltClassroom').on('change', function () {
                var thisObj = $(this);
                var isAllSelected = thisObj.find('option[value="0"]').prop('selected');
                var lastAllSelected = $(this).data('all');
                var selectedOptions = (thisObj.val()) ? thisObj.val() : [];
                var allOptionsLength = thisObj.find('option[value!="0"]').length;

                //console.log(selectedOptions);
                var selectedOptionsLength = selectedOptions.length;

                if (isAllSelected == lastAllSelected) {

                    if ($.inArray("0", selectedOptions) >= 0) {
                        selectedOptionsLength -= 1;
                    }

                    if (allOptionsLength <= selectedOptionsLength) {

                        thisObj.find('option[value="0"]').prop('selected', true).parent().selectpicker('refresh');
                        isAllSelected = true;
                    } else {
                        thisObj.find('option[value="0"]').prop('selected', false).parent().selectpicker('refresh');
                        isAllSelected = false;
                    }

                } else {
                    thisObj.find('option').prop('selected', isAllSelected).parent().selectpicker('refresh');
                }

                $(this).data('all', isAllSelected);
            }).trigger('change');

            $("#btnSearch").bind({
                click: function () {

                    var searchOption = {
                        "groupID": $('#iptGroupName').data('gid'),
                        "type": $("#sltType").val(),
                        "levelID": $("#sltLevel").val(),
                        "roomIDs": $("#sltClassroom").val(),
                        "name": $("#iptName").val()
                    }

                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "GroupForm.aspx/SearchData",
                        data: "{searchOption:" + JSON.stringify(searchOption) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var r = JSON.parse(response.d);

                            var title = "";
                            var body = "";

                            if (r.success) {
                                //console.log(r);

                                groupForm.PreSelect = r.data;

                                groupForm.RenderList(groupForm.PreSelect, '#tblPreSelectList', 'pre-select-one');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132450") %> [' + r.message + ']';

                                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                                $("#modalNotifyOnlyClose").modal('show');
                            }
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

                    return false;
                }
            });

            $("#iptPreSelect").on("input", function () {
                var value = $(this).val().toLowerCase();
                $("#tblPreSelectList > tbody > tr:not(.row-header)").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });

            $("#iptSelected").on("input", function () {
                var value = $(this).val().toLowerCase();
                $("#tblSelectedList > tbody > tr:not(.row-header)").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });

            $("#btnMove").bind({
                click: function () {

                    if ($('.pre-select-one').filter(":checked").length > 0) {

                        // find select array id only
                        var selectIds = $('.pre-select-one').filter(":checked").map(function (i, elm) { return $(elm).data('id') });

                        // find selected array id only
                        var selectedIds = $(groupForm.Selected).map(function (i, elm) { return elm.id });

                        // find select array object
                        var selectObj = groupForm.PreSelect.filter(v => $.inArray(v.id, selectIds) !== -1);

                        $(selectObj).each(function () {
                            if ($.inArray(this.id, selectedIds) == -1) {
                                groupForm.Selected.push(this);
                                selectedIds.push(this.id);
                            }
                        });

                        groupForm.RenderList(groupForm.Selected, '#tblSelectedList', 'selected-one');

                        $('#iptSelected').trigger("input");
                    }

                    return false;
                }
            });

            $("#btnMoveBack").bind({
                click: function () {

                    if ($('.selected-one').filter(":checked").length > 0) {

                        // find remove selected array id only
                        var removeSelectedIds = $('.selected-one').filter(":checked").map(function (i, elm) { return $(elm).data('id') });

                        groupForm.Selected = groupForm.Selected.filter(function (obj) {
                            return $.inArray(obj.id, removeSelectedIds) == -1;
                        });

                        groupForm.RenderList(groupForm.Selected, '#tblSelectedList', 'selected-one');

                        $('#iptSelected').trigger("input");
                    }

                    return false;
                }
            });

            $("#btnSaveGroup").bind({
                click: function () {

                    if ($('#groupForm').valid()) {

                        $('#modalNotifyConfirmSave').modal('show');

                        // Modal Section
                        $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                            $('#modalNotifyConfirmSave').modal('hide');

                            $("#modalWaitDialog").modal('show');

                            // Save command
                            var data = {
                                groupID: $('#iptGroupName').data('gid'),
                                groupName: $('#iptGroupName').val(),
                                groupNameEn: $('#iptGroupNameEn').val(),
                                users: $.map(groupForm.Selected, function (obj) { return obj.id; })
                            };

                            groupForm.SaveGroup(data);

                        });
                    }

                    return false;
                }
            });

            $("#btnCancelGroup").bind({
                click: function () {

                    // Redirect to group list
                    window.location.replace("GroupList.aspx");

                    return false;
                }
            });


            $('.pre-select-all').click(function () {
                $(".pre-select-one").prop('checked', $(this).prop('checked'));
            });

            $('#tblPreSelectList').on('change', 'tbody input.pre-select-one', function () {
                $(".pre-select-all").prop('checked', ($('.pre-select-one').length == $('.pre-select-one').filter(":checked").length));
            });

            $('.selected-all').click(function () {
                $(".selected-one").prop('checked', $(this).prop('checked'));
            });

            $('#tblSelectedList').on('change', 'tbody input.selected-one', function () {
                $(".selected-all").prop('checked', ($('.selected-one').length == $('.selected-one').filter(":checked").length));
            });


            // Modal Section
            $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function (e) {
                if ($("#modalWaitDialog").data('bs.modal')?._isShown) {
                    $("#modalWaitDialog").modal('hide');
                }
            });

            // Load info command
            groupForm.GetData('<%=Request.QueryString["gid"]%>');

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
