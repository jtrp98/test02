<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true"
    CodeBehind="historylogin.aspx.cs" Inherits="FingerprintPayment.Report.historylogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />

    <style type="text/css">
        .table > tbody > tr > td.vertical-align-middle {
            vertical-align: middle;
        }

        .table > thead > tr > th.vertical-align-middle {
            vertical-align: middle;
            padding-right: 10px;
        }

        .modal-body {
            font-size: 26px;
        }

        .dropdown-menu {
            font-size: 22px;
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

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary {
            width: 110px;
            height: 44px;
        }

        #modalSortStudentNo input[type=checkbox], #modalSortStudentNo input[type=radio],
        #modalManageStudentTitle input[type=checkbox], #modalManageStudentTitle input[type=radio] {
            -ms-transform: scale(1.3);
            -moz-transform: scale(1.3);
            -webkit-transform: scale(1.3);
            -o-transform: scale(1.3);
            transform: scale(1.3);
            padding: 10px;
            cursor: pointer;
        }

        .modal {
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

        .modal-content {
            /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
            width: inherit;
            height: inherit;
            /* To center horizontally */
            margin: 0 auto;
        }

        .none-finger {
            background-color: #fd7a7a;
            color: white;
        }

        #tableData tbody tr.none-finger.even:hover, #tableData tbody tr.none-finger.odd:hover {
            background-color: #fd7a7a;
            color: white;
        }

        .bootbox-confirm .modal-title {
            font-size: 36px;
        }

        .bootbox-confirm .bootbox-body {
            line-height: 1;
        }

        .bootbox-confirm .modal-footer {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
        <div class="container-fluid">
            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701048") %> </h3>
            <div class="row" id="search">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header card-header-info card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">search</i>
                            </div>
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701049") %> </h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-3 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                        <div class="col-md-9">
                                            <select class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>" name="sltYear" id="sltYear" style="width: 100%">
                                                <asp:Literal ID="ltrYear" runat="server" />
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-3 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                        <div class="col-md-9">
                                            <select class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %>" name="sltTerm" id="sltTerm" style="width: 100%">
                                                <asp:Literal ID="ltrTerm" runat="server" />
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-3 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                                        <div class="col-md-9">
                                            <select class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>" name="sltLevel" id="sltLevel" style="width: 100%">
                                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %></option>
                                                <asp:Literal ID="ltrLevel" runat="server" />
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <label class="col-md-3 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                        <div class="col-md-9">
                                            <select class="selectpicker" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>" name="sltClass" id="sltClass" style="width: 100%">
                                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %></option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="row">
                                        <label class="col-md-5 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204079") %></label>
                                        <div class="col-md-5">
                                            <%--<input type="text" name="student_id" id="student_id" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204079") %>">--%>
                                            <select id="student_id" class="selectpicker" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204079") %>">
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3 ml-3">
                                <div class="col-sm-12">
                                    <label class="check_container">
                                        <input type="radio" checked="checked" class="student_option" name="student_option" value="0">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>
                                        <span class="checkmark"></span>
                                    </label>
                                    <label class="check_container">
                                        <input type="radio" name="student_option" class="student_option" value="1">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701055") %>
                                        <span class="checkmark"></span>
                                    </label>
                                    <label class="check_container">
                                        <input type="radio" name="student_option" class="student_option" value="2">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701056") %>
                                        <span class="checkmark"></span>
                                    </label>
                                </div>
                            </div>
                            <div class="text-center m-3">
                                <div class="btn btn-info device_search"><i class="material-icons">search</i> Search </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="exam_list">
                <div class="col-md-12">
                    <div class="card card_table">
                        <div class="card-header card-header-success card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">list</i>
                            </div>
                            <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701057") %> </h4>
                        </div>
                        <div class="card-body">
                            <div class="toolbar">
                            </div>
                            <div class="material-datatables">
                                <table id="device_table" class="table table-striped table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701061") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701062") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701063") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701064") %></th>
                                            <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701065") %></th>
                                            <th class="text-center">
                                                <%--    <div class="btn btn-success text-center" onclick="exportExcel()">
                                                    Export File
                                                </div>--%>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalWaitDialog" class="modal fade" tabindex="-1" role="dialog" data-keyboard="false"
        data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00106") %></h3>
                </div>
                <div class="modal-body">
                    <div style="text-align: center;">
                        <img src="/assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/5.5.2/bootbox.min.js"></script>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>

    <link rel="stylesheet" href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />

    <link href="../../Content/jquery-confirm.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-confirm.js"></script>

    <%--<script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>--%>

    <script type="text/javascript">

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $("#modalWaitDialog").modal('show');
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            $(yearData).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        $("#modalWaitDialog").modal('hide');
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

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $("#modalWaitDialog").modal('show');
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %></option>';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        $("#modalWaitDialog").modal('hide');
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

            $.ajaxSetup({
                statusCode: {
                    500: function () {
                        $("#modalWaitDialog").modal('hide');
                        //window.location.replace("/Default.aspx");
                    }
                },
                beforeSend: function () {
                    //var millisecondsToWait = 500;
                    //setTimeout(function () {
                    //    // Whatever you want to do after the wait
                    //    $("#modalWaitDialog").modal('show');
                    //}, millisecondsToWait);
                },
                complete: function () {
                    var millisecondsToWait = 500;
                    setTimeout(function () {
                        // Whatever you want to do after the wait
                        $("#modalWaitDialog").modal('hide');
                    }, millisecondsToWait);
                },
                success: function () {
                    $("#modalWaitDialog").modal('hide');
                }
            });

            if (jQuery().dataTable) {
                $.fn.dataTable.ext.errMode = 'none';
            }

            // Searching, Pagination event 
            $('.studentList #btnSearch').click(function () {

                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $("#sltClass ,#sltTerm").change(function () {
                getStudentList();
            });

            $(".device_search").click(function () {
                var param = {
                    nTermSubLevel2: $('#sltClass').val(), TermID: $('#sltTerm').val(),
                    searchType: $('[name=student_option]:checked').val(), StudentName: $('#student_id').val()
                };

                if (param.nTermSubLevel2 == "") {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %> !');
                    return;
                }

                $("#modalWaitDialog").modal('show');
                $.ajax({
                    url: "historylogin.aspx/LoadHistoryLogin",
                    data: JSON.stringify({ search: param }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        //console.table(data.d);
                        $("#device_table tbody tr").remove();
                        let _html = "";
                        $.each(data.d, function (e, s) {
                            let LoginData = s.loginData;
                            let rowspan = 1;
                            if (LoginData.length == 0) {
                                _html += `<tr>
                                            <td>`+ (e + 1) + `. </td>
                                            <td>`+ s.sStudentID + `</td>
                                            <td>`+ s.sName + `</td>
                                            <td>`+ s.sLastname + `</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td colspan=2></td>
                                        </tr>`;
                            } else {
                                $.each(LoginData, function (e1, s1) {
                                    if (e1 == 0) {
                                        _html += `<tr>
                                            <td rowspan=` + LoginData.length + `>` + (e + 1) + `. </td>
                                            <td rowspan=` + LoginData.length + `>` + s.sStudentID + `</td>
                                            <td rowspan=` + LoginData.length + `>` + s.sName + `</td>
                                            <td rowspan=` + LoginData.length + `>` + s.sLastname + `</td>
                                            <td>`+ s1.System + `</td>
                                            <td>`+ (s1.Fd_MBBrand ?? "") + `</td>
                                            <td>`+ s1.Imei + `</td>
                                            <td>`+ s1.Fd_Version + `</td>
                                            <td colspan=2>`+ s1.Fd_LoginDate + `</td>
                                        </tr>`;
                                    } else {
                                        _html += `<tr>
                                            <td>`+ s1.System + `</td>
                                            <td>`+ (s1.Fd_MBBrand ?? "") + `</td>
                                            <td>`+ s1.Imei + `</td>
                                            <td>`+ s1.Fd_Version + `</td>
                                            <td colspan=2>`+ s1.Fd_LoginDate + `</td>
                                        </tr>`;
                                    }
                                })
                            }
                        });

                        $("#device_table tbody").html(_html);
                        //$("#modalWaitDialog").modal('hide');
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //$("#modalWaitDialog").modal('hide');
                        console.log(textStatus);
                    }
                });
            });

            $('.studentList #tableData #sltPageSize').change(function () {

                studentList.PageSize = parseInt($(".studentList #tableData #sltPageSize").children("option:selected").val());
                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $('.studentList #tableData #sltPageIndex').change(function () {

                studentList.PageIndex = parseInt($(".studentList #tableData #sltPageIndex").children("option:selected").val());

                studentList.ReloadListData();

                return false;
            });

            $('.studentList #tableData #aPrevious').click(function () {

                if (studentList.PageIndex > 0) {
                    studentList.PageIndex--;
                    studentList.ReloadListData();
                }

                return false;
            });

            $('.studentList #tableData #aNext').click(function () {

                if (studentList.PageIndex < (studentList.PageCount - 1)) {
                    studentList.PageIndex++;
                    studentList.ReloadListData();
                }

                return false;
            });

            // Search
            $("#sltYear").change(function () {

                LoadTerm($(this).val(), '#sltTerm');

            });

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });


            function getStudentList() {
                var param = { keyword: $('#sltClass').val(), termID: $('#sltTerm').val() };
                $.ajax({
                    url: "historylogin.aspx/GetStudentName",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        let select = $("#student_id");
                        var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204079") %></option>';
                        $.each(data.d, function (key, value) {
                            options += '<option value=' + value.UserID + '>' + value.StudentCode + ' ' + value.StudentName + '</option>';
                        });
                        select.empty();
                        select.append(options);
                        select.selectpicker('refresh');
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(textStatus);
                    }
                });
            }
            //$.ui.autocomplete.prototype._renderItem = function (ul, item) {
            //    var t = String(item.value).replace(
            //        new RegExp(this.term, "gi"),
            //        "<strong>$&</strong>");
            //    return $("<li></li>")
            //        .data("item.autocomplete", item)
            //        .append("<a>" + t + "</a>")
            //        .appendTo(ul);
            //};

            //$(".studentList #iptStudentName").autocomplete({
            //    source: function (request, response) {
            //        var param = { keyword: $('.studentList #iptStudentName').val(), termID: $('.studentList #sltTerm').val() };
            //        $.ajax({
            //            url: "/StudentInfo/StudentList.aspx/GetStudentName",
            //            data: JSON.stringify(param),
            //            dataType: "json",
            //            type: "POST",
            //            contentType: "application/json; charset=utf-8",
            //            dataFilter: function (data) { return data; },
            //            success: function (data) {
            //                response($.map(data.d, function (item) {
            //                    return {
            //                        value: item
            //                    }
            //                }))
            //            },
            //            error: function (XMLHttpRequest, textStatus, errorThrown) {
            //                console.log(textStatus);
            //            }
            //        });
            //    },
            //    select: function (event, ui) {
            //        // ui.item
            //        // ui.item.value
            //    },
            //    minLength: 1
            //});

            // Initial control

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
