<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_EQ.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Qusetion.Index_EQ" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

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



        .btn-outline-secondary {
            color: #6c757d;
            background-color: transparent;
            background-image: none;
            border-color: #6c757d
        }

            .btn-outline-secondary:hover {
                color: #fff;
                background-color: #6c757d;
                border-color: #6c757d
            }

            .btn-outline-secondary.focus, .btn-outline-secondary:focus {
                box-shadow: 0 0 0 .2rem rgba(108,117,125,.5)
            }

            .btn-outline-secondary.disabled, .btn-outline-secondary:disabled {
                color: #6c757d;
                background-color: transparent
            }

            .btn-outline-secondary:not(:disabled):not(.disabled).active, .btn-outline-secondary:not(:disabled):not(.disabled):active, .show > .btn-outline-secondary.dropdown-toggle {
                color: #fff;
                background-color: #6c757d;
                border-color: #6c757d
            }

                .btn-outline-secondary:not(:disabled):not(.disabled).active:focus, .btn-outline-secondary:not(:disabled):not(.disabled):active:focus, .show > .btn-outline-secondary.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .2rem rgba(108,117,125,.5)
                }



        .btn-outline-success {
            color: #28a745;
            background-color: transparent;
            background-image: none;
            border-color: #28a745
        }

            .btn-outline-success:hover {
                color: #fff;
                background-color: #28a745;
                border-color: #28a745
            }

            .btn-outline-success.focus, .btn-outline-success:focus {
                box-shadow: 0 0 0 .2rem rgba(40,167,69,.5)
            }

            .btn-outline-success.disabled, .btn-outline-success:disabled {
                color: #28a745;
                background-color: transparent
            }

            .btn-outline-success:not(:disabled):not(.disabled).active, .btn-outline-success:not(:disabled):not(.disabled):active, .show > .btn-outline-success.dropdown-toggle {
                color: #fff;
                background-color: #28a745;
                border-color: #28a745
            }

                .btn-outline-success:not(:disabled):not(.disabled).active:focus, .btn-outline-success:not(:disabled):not(.disabled):active:focus, .show > .btn-outline-success.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .2rem rgba(40,167,69,.5)
                }


        .btn-outline-danger {
            color: #dc3545;
            background-color: transparent;
            background-image: none;
            border-color: #dc3545
        }

            .btn-outline-danger:hover {
                color: #fff;
                background-color: #dc3545;
                border-color: #dc3545
            }

            .btn-outline-danger.focus, .btn-outline-danger:focus {
                box-shadow: 0 0 0 .2rem rgba(220,53,69,.5)
            }

            .btn-outline-danger.disabled, .btn-outline-danger:disabled {
                color: #dc3545;
                background-color: transparent
            }

            .btn-outline-danger:not(:disabled):not(.disabled).active, .btn-outline-danger:not(:disabled):not(.disabled):active, .show > .btn-outline-danger.dropdown-toggle {
                color: #fff;
                background-color: #dc3545;
                border-color: #dc3545
            }

                .btn-outline-danger:not(:disabled):not(.disabled).active:focus, .btn-outline-danger:not(:disabled):not(.disabled):active:focus, .show > .btn-outline-danger.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .2rem rgba(220,53,69,.5)
                }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card box-content">
        <div class="studentList">
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                    <div class="col-md-7">
                        <select id="sltYear" name="sltYear[]" class="form-control">
                            <%--<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>--%>
                            <asp:Literal ID="ltrYear" runat="server" />
                        </select>
                    </div>
                </div>
                <div class="col-md-6">
                    <label class="col-md-5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                    <div class="col-md-7">
                        <select id="sltTerm" name="sltTerm[]" class="form-control">
                            <asp:Literal ID="ltrTerm" runat="server" />
                        </select>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                    <div class="col-md-7">
                        <select id="sltLevel" name="sltLevel[]" class="form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            <asp:Literal ID="ltrLevel" runat="server" />
                        </select>
                    </div>
                </div>
                <div class="col-md-6">
                    <label class="col-md-5">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                    <div class="col-md-7">
                        <select id="sltClass" name="sltClass[]" class="form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-6">
                    <label class="col-md-5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                    <div class="col-md-7">
                        <input id="iptStudentName" name="iptStudentName" type="text" class="form-control" style="width: 100%;" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>" />
                    </div>
                </div>
                <div class="col-md-6" style="display: none;">
                </div>
            </div>
            <div class="row">
                <br />
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <button id="btnSearch" class="btn btn-info btn-round col-md-2" style="font-size: 24px; float: none;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                    </button>
                </div>
            </div>
            <div class="row">
                <br />
            </div>
            <table id="tableData" class="table table-bordered table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>
                        <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                        <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                        <th style="width: 14%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                        <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                        <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                        <th style="width: 18%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                        <th style="width: 18%" class="text-center">แบบ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132984") %></th>
                        <th style="display: none;"></th>
                    </tr>
                </thead>

                <tfoot>
                    <tr>
                        <th colspan="9">
                            <div class="row">
                                <div class="col-md-4 text-left" style="padding-left: 4%">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                    <select id="sltPageSize">
                                        <option selected="selected" value="20">20</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                    </select>
                                </div>
                                <div class="col-md-4 text-center">
                                    <a id="aPrevious" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                    <select id="sltPageIndex">

                                    </select>
                                    <a id="aNext" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></a>
                                </div>
                                <div class="col-md-4 text-right" style="padding-right: 2%">
                                    <span id="spnPageInfo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132005") %></span>
                                </div>
                            </div>
                        </th>
                    </tr>
                </tfoot>


            </table>


        </div>
    </div>

    <!-- Modal -->
    <div id="modalWaitDialog" class="modal fade" tabindex="-1" role="dialog" data-keyboard="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00106") %></h3>
                </div>
                <div class="modal-body">
                    <div style="text-align: center;">
                        <img src="../Assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center;">
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>

    <script type="text/javascript" language="javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {

            $(".studentList #iptStudentName").autocomplete({
                source: function (request, response) {
                    var param = { keyword: $('.studentList #iptStudentName').val() };
                    $.ajax({
                        url: "Index_EQ.aspx/GetStudentName",
                        data: JSON.stringify(param),
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(textStatus);
                        }
                    });
                }
            });

            $('.studentList #tableData #sltPageSize').change(function () {
                studentList.PageSize = parseInt($(".studentList #tableData #sltPageSize").children('option:selected').val());
                studentList.PageIndex = 0;
                studentList.ReloadListData();
                return false;
            });

            $('.studentList #tableData #sltPageIndex').change(function () {
                studentList.PageIndex = parseInt($('.studentList #tableData #sltPageIndex').children('option:selected').val());
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

            studentList.LoadListData();
        });


        $(".studentList #btnSearch").click(function () {
            studentList.PageIndex = 0;
            studentList.ReloadListData();
            return false;
        });


        $("#sltYear").change(function () {
            LoadTerm($(this).val(), '#sltTerm');
        });

        $("#sltLevel").change(function () {
            $("#modalWaitDialog").modal('show');
            LoadTermSubLevel2($(this).val(), '#sltClass');
        });


        var studentList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            dt: null,
            LoadListData: function () {
                studentList.dt = $(".studentList #tableData").DataTable({
                    processing: true,
                    serverSide: true,
                    stateSave: true,
                    info: false,
                    searching: false,
                    paging: false,
                    ajax: {
                        type: "POST",
                        url: "Index_EQ.aspx/LoadStudent",
                        contentType: "application/json; charset=utf-8",
                        data: function (d) {
                            d.year = $(".studentList #sltYear").children("option:selected").val();
                            d.term = $(".studentList #sltTerm").children("option:selected").val();
                            d.level = $(".studentList #sltLevel").children("option:selected").val();
                            d.className = $(".studentList #sltClass").children("option:selected").val();
                            d.stdName = $(".studentList #iptStudentName").val();
                            d.page = studentList.PageIndex;
                            d.length = studentList.PageSize;

                            return JSON.stringify(d, function (key, value) {
                                return (value === undefined) ? "" : value
                            });
                        },
                        dataSrc: function (json) {
                            var jsond = $.parseJSON(json.d);
                            console.log(jsond);
                            return jsond.data;
                        },
                        beforeSend: function () {
                            $("#modalWaitDialog").modal('show');
                        },
                        complete: function () {
                            $("#modalWaitDialog").modal('hide');
                        }
                    },
                    columns: [
                        { "data": "no", "orderable": false },
                        { "data": "No", "orderable": true },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "ClassName", "orderable": true },
                        { "data": "Status", "orderable": true },
                        { "data": "sID", "orderable": false },
                        { "data": "TermID", "orderable": false }
                    ],
                    order: [
                        [1, "asc"]
                    ],
                    columnDefs: [
                        { className: "vertical-align-middle text-center", targets: [0, 8] },
                        { className: "text-center", targets: [1, 2, 3, 4, 5, 6, 7] },
                        {
                            render: function (data, type, row, draw) {
                                var check = row.ValueEQ;

                                if (check != "0") {
                                    return '<button type="button" class="sucEQ btn btn-outline-success" style="width: 40%;"> EQ </button>';
                                } else {
                                    return '<button type="button" class="fEQ btn btn-outline-secondary" style="width: 40%;"> EQ </button>';
                                }
                            }, targets: [8]
                        },
                        { className: "hidden", targets: [9], "visible": false }
                    ],
                    drawCallback: function (setting) {
                        var json = $.parseJSON(setting.json.d);
                        studentList.PageCount = json.pageCount;

                        var options = '';
                        for (var i = 0; i < json.pageCount; i++) {
                            options += '<option ' + (studentList.PageIndex == i ? 'selected="selected"' : '') + ' value="' + i + '">' + (i + 1) + '</option>';
                        }
                        $('.studentList #tableData #sltPageIndex').html(options);

                        $('.studentList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (studentList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + studentList.PageCount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                    }
                });
                studentList.dt.on('draw.dt', function () {
                    studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
                    });
                    studentList.dt.column(8, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sid = studentList.dt.cells({ row: i, column: 8 }).data()[0];
                        var tid = studentList.dt.cells({ row: i, column: 9 }).data()[0];
                        $(cell).find(".fEQ").button().removeClass("ui-widget").attr("Onclick", 'fcEQ(' + sid + ')');
                        $(cell).find(".sucEQ").button().removeClass("ui-widget").attr("Onclick", 'successEQ(' + sid + ')');
                    });
                });
            },
            ReloadListData: function () {
                studentList.dt.draw();
            }
        }


        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "Index_EQ.aspx/LoadTerm",
                    data: "{ yearID: " + yearID + " }",
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
                        }
                    },
                    beforeSend: function () {
                        $("#modalWaitDialog").modal('show');
                    },
                    complete: function () {
                        $("#modalWaitDialog").modal('hide');
                    },
                    fail: function (response) {
                        console.log(response);
                    },
                    error: function (response) {
                        console.log(response);
                    }
                });
            }
        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "Index_EQ.aspx/LoadTermSubLevel2",
                    data: '{ subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;
                        $(objResult).empty();
                        if (subLevel2.length > 0) {
                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                            $(subLevel2).each(function () {
                                options += '<option value="' + this.id + '">' + this.name + '</option>';
                            });
                            $(objResult).html(options);
                        }
                    },
                    beforeSend: function () {
                        $("#modalWaitDialog").modal('show');
                    },
                    complete: function () {
                        $("#modalWaitDialog").modal('hide');
                    },
                    fail: function (response) {
                        console.log(response.d)
                    },
                    error: function (response) {
                        console.log(response.d)
                    }
                });
            }
        }

        function fcEQ(sID) {
            var term = $(".studentList #sltTerm").children("option:selected").val();

            var link = "Question_EQ.aspx?sid=" + sID + "&term=" + term;
            window.location = link;
        }

        function successEQ(sID) {
            var term = $(".studentList #sltTerm").children("option:selected").val();

            var link = "Summary_EQ.aspx?sid=" + sID + "&term=" + term;
            window.open(link, '_blank');
        }

        $('#myform').on('submit', function (ev) {
            $('#myModal').modal('show');
        });



    </script>



</asp:Content>

