<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="BackupCardList.aspx.cs" Inherits="FingerprintPayment.Card.BackupCardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <!-- DataTables -->
    <%-- <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />--%>

    <style type="text/css">
        /*.table > tbody > tr > td.vertical-align-middle {
            vertical-align: middle;
        }

        .table > thead > tr > th.vertical-align-middle {
            vertical-align: middle;
            padding-right: 10px;
        }

        .modal-title {
            font-size: 36px;
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

        .btn.btn-success, .btn.btn-cancel, .btn.btn-danger, .btn.btn-primary, .btn.btn-default {
            width: 110px;
            height: 44px;
        }

        .modal {
        }

        .vertical-alignment-helper {
            display: table;
            height: 100%;
            width: 100%;
        }

        .vertical-align-center {*/
        /* To center vertically */
        /*display: table-cell;
            vertical-align: middle;
        }

        .modal-content {*/
        /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
        /*width: inherit;
            height: inherit;*/
        /* To center horizontally */
        /*margin: 0 auto;
        }

        .table-bordered > thead > tr > th,
        .table-bordered > tbody > tr > td {
            border-left-width: 0;
            border-right-width: 0;
        }*/


        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        table.dataTable tfoot tr:last-child th {
            border-top: 1px solid #000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106145") %>
            </p>
        </div>
    </div>

    <form id="aspnetForm" runat="server">
        <div class="row backupCardList">
            <div class="col-md-12">

                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">

                        <div class="row" style="padding-bottom: 10px;">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-1 text-right">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106146") %></label>
                                    </div>
                                    <div class="col-md-4">
                                        <input id="iptBackupCardName" name="iptBackupCardName" type="text" class="form-control" style="width: 100%;" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106147") %>" />
                                    </div>
                                    <div class="col-md-6">
                                        <button id="btnSearch" class="btn btn-info col-md-2" stylxe="float: none; height: 41px; padding: 4px 12px;">
                                            <span class="btn-label">
                                                <i class="material-icons">search</i>
                                            </span>
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                        </button>
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>


                <div class="card ">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                    </div>
                    <div class="card-body ">
                        <table id="tableData" class="table-hover dataTable" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                    <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106148") %></th>
                                    <th style="width: 13%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106149") %></th>
                                    <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106150") %></th>
                                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></th>
                                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                    <th style="width: 18%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                    <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106152") %></th>
                                    <th style="width: 10%" class="text-center">
                                        <a href="#" class="btn btn-success gvbutton" stylxe="font-size: 24px; text-decoration: blink; display: inline-block; width: 100%;" data-toggle="modal" data-target="#modalAddBackupCard" onclick="return AddBackupCard();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></a>
                                    </th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>

                           <%-- <tfoot class="d-none">
                                <tr>
                                    <th colspan="12">
                                        <div class="row">
                                            <div class="col-md-4 mb-4 text-left" style="padding-left: 4%;">
                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                                <select id="sltPageSize" class="selectpicker " data-style="select-with-transition">
                                                    <option selected="selected" value="20">20</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>
                                            </div>
                                            <div class="col-md-4 mb-4 text-center">
                                                <a id="aPrevious" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                                <select id="sltPageIndex" class="selectpicker " data-style="select-with-transition">
                                                </select>
                                                <a id="aNext" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></a>
                                            </div>
                                            <div class="col-md-4 mb-4 text-right" style="padding-right: 2%;">
                                                <span id="spnPageInfo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132005") %></span>
                                            </div>
                                        </div>
                                    </th>
                                </tr>
                            </tfoot>--%>
                        </table>
                    </div>
                </div>

            </div>
        </div>

        <%-- <div class="full-card box-content">
        <div class="backupCardList">

            <div class="row">
                <br />
            </div>

        </div>
    </div>--%>

        <div id="modalAddBackupCard" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true" style="margin: 0 auto; top: 10%;">
            <div class="modal-dialog global-modal">
                <div class="modal-content" style="width: 420px;">

                    <div class="modal-header text-center" style="padding: 0px 15px; top: 25%;">
                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106153") %></h3>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106154") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptCardName" name="iptCardName" class="form-control" type="text" tabindex="0" style="padding-left: 7px;" maxlength="250" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptBarCode" name="iptBarCode" class="form-control" type="text" style="padding-left: 7px;" maxlength="20" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106156") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptNFC" name="iptNFC" class="form-control" type="text" style="padding-left: 7px;" maxlength="20" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106157") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptInsurance" name="iptInsurance" class="form-control" type="text" style="padding-left: 7px;" maxlength="6" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button type="button" id="btnSaveAddBackupCard" class="btn btn-success global-btn">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                        <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.modal -->

        <div id="modalBorrowBackupCard" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true" style="margin: 0 auto; top: 10%;">
            <div class="modal-dialog global-modal">
                <div class="modal-content" style="width: 400px;">

                    <div class="modal-header text-center" style="padding: 0px 15px; top: 25%;">
                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106159") %></h3>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></span>
                                </div>
                                <div class="col-md-8 p-0">
                                    <select id="sltUserType2" name="sltUserType2"
                                        class="selectpicker col-md-12" data-style="select-with-transition">
                                        <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %></option>
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106161") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106162") %></option>
                                    </select>
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptUserName2" name="iptUserName2" class="form-control" type="text" style="padding-left: 7px; display: none;" />
                                    <%--for usertype=0,1--%>
                                    <input id="iptUserName3" name="iptUserName3" class="form-control" type="text" style="padding-left: 7px;" />
                                    <%--for usertype=2--%>
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106164") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptBalance2" name="iptBalance2" class="form-control" type="text" style="padding-left: 7px;" maxlength="7" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106157") %></span>
                                </div>
                                <div class="col-md-8 text-left">
                                    <span id="spnInsurance2">0.00</span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button type="button" id="btnSaveBorrowBackupCard" class="btn btn-success global-btn">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.modal -->

        <div id="modalReturnBackupCard" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true" style="margin: 0 auto; top: 10%;">
            <div class="modal-dialog global-modal">
                <div class="modal-content text-center" style="width: 400px;">

                    <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106166") %></h3>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptUserType" name="iptUserType" class="form-control" type="text" style="padding-left: 7px;" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptUserName" name="iptUserName" class="form-control" type="text" style="padding-left: 7px;" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106164") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptBalance" name="iptBalance" class="form-control" type="text" style="padding-left: 7px;" maxlength="6" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106157") %></span>
                                </div>
                                <div class="col-md-8 text-left">
                                    <span id="spnInsurance">0.00</span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106167") %></span>
                                </div>
                                <div class="col-md-8 text-left">
                                    <span id="spnNetTotal" style="font-weight: bold;">0.00</span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button type="button" id="btnSaveReturnBackupCard" class="btn btn-success global-btn">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                        <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.modal -->

        <div id="modalEditBackupCard" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
            aria-hidden="true" style="margin: 0 auto; top: 10%;">
            <div class="modal-dialog global-modal">
                <div class="modal-content" style="width: 420px;">

                    <div class="modal-header text-center" style="padding: 0px 15px; top: 25%;">
                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106168") %></h3>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106154") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptCardName2" name="iptCardName2" class="form-control" type="text" style="padding-left: 7px;" maxlength="250" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106155") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptBarCode2" name="iptBarCode2" class="form-control" type="text" style="padding-left: 7px;" maxlength="20" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106156") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptNFC2" name="iptNFC2" class="form-control" type="text" style="padding-left: 7px;" maxlength="20" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 10px;">
                                <div class="col-md-4">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106157") %></span>
                                </div>
                                <div class="col-md-8">
                                    <input id="iptInsurance2" name="iptInsurance2" class="form-control" type="text" style="padding-left: 7px;" maxlength="6" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display: block; text-align: center;">
                        <button type="button" id="btnSaveEditBackupCard" class="btn btn-success global-btn">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                        <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.modal -->

        <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <!-- DataTables -->
    <%--    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>--%>

    <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>

    <%--  <script language="javascript" type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>--%>

    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>



    <script type="text/javascript">


        var backupCardList = {
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            dt: null,
            LoadListData: function () {
                backupCardList.dt = $(".backupCardList #tableData").DataTable({
                    "processing": true,
                    //"serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": true,
                    "pageLength": 20,
                    "bLengthChange": false,
                    //"stateSave": true,
                    "ajax": {
                        "url": "BackupCardList.aspx/LoadBackupCard",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.searchName = $(".backupCardList #iptBackupCardName").val();
                            d.page = backupCardList.PageIndex;
                            d.length = backupCardList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            var jsond = $.parseJSON(json.d);
                            //console.log(jsond);
                            return jsond.data;
                        },
                        "beforeSend": function () {
                            // Handle the beforeSend event
                            ////$("#modalWaitDialog").modal('show');
                        },
                        "complete": function () {
                            // Handle the complete event
                            ////$("#modalWaitDialog").modal('hide');
                        }
                    },
                    "columns": [
                        { "data": "no", "orderable": false },//0
                        { "data": "CardName", "orderable": true },
                        { "data": "BarCode", "orderable": true },
                        { "data": "NFC", "orderable": true },
                        { "data": "Money", "orderable": true },
                        { "data": "UserType", "orderable": true },
                        { "data": "UserName", "orderable": true },
                        { "data": "BorrowingDate", "orderable": true },
                        {
                            "data": "action", "orderable": false, "mRender": function (data, type, row) {
                                var chid = row.chid;
                                var _btn = '';
                                if (!chid) {
                                    _btn = `<a href="#" data-toggle="modal" data-target="#modalBorrowBackupCard" onclick="BorrowBackupCard('${row.cid}','${row.insurance}')"><img class="bc-borrow" src="assets/img/card-borrow.svg" style="width: 24px; margin-right: 5px; margin-top: -7px;"></a>`;
                                    //console.log('borrow');
                                    //$(cell).find(".bc-return").parent().hide();
                                    //$(cell).find(".bc-borrow").parent().show();
                                    //$(cell).find(".bc-borrow").parent().attr("onClick", 'BorrowBackupCard(\'' + cid + '\', \'' + insurance + '\')');
                                }
                                else {
                                    _btn = `<a href="#" data-toggle="modal" data-target="#modalReturnBackupCard" onclick="ReturnBackupCard('${row.chid}')"><img class="bc-borrow" src="assets/img/card-return.svg" style="width: 24px; margin-right: 5px; margin-top: -7px;"></a>`;
                                    //console.log('return');
                                    //$(cell).find(".bc-borrow").parent().hide();
                                    //$(cell).find(".bc-return").parent().show();
                                    //$(cell).find(".bc-return").parent().attr("onClick", 'ReturnBackupCard(\'' + chid + '\')');
                                }

                                //$(cell).find(".bc-edit").parent().attr("onClick", 'EditBackupCard(\'' + cid + '\', \'' + cardName + '\', \'' + nfc + '\', \'' + barCode + '\', \'' + chid + '\', \'' + insurance + '\')');
                                //$(cell).find(".bc-remove").parent().attr("cid", cid);
                                //$(cell).find(".bc-history").parent().attr("href", "BackupCardHistory.aspx?cid=" + cid);

                                _btn +=
                                    `<a href="#" data-toggle="modal" data-target="#modalEditBackupCard"><i class="fa fa-edit bc-edit" onClick="EditBackupCard('${row.cid}','${row.CardName}','${row.NFC}','${row.BarCode}','${row.chid}','${row.insurance}')" style="padding-right: 5px; color: rgb(91, 192, 222);font-size: 18px;"></i></a>` +
                                    '<a href="BackupCardHistory.aspx?cid=' + row.cid + '"><i class="fa fa-history bc-history"  style="padding-right: 5px; color: black;font-size: 18px;"></i></a>';

                                if (row.IsRemove == true) {
                                    _btn += '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>" cid="' + row.cid + '"><i class="fa fa-remove bc-remove text-danger" style="padding-right: 5px;font-size: 18px;"></i></a>';
                                }
                                return _btn;
                                //return "<a href='Admin/Categories/Edit/" + data + "'>EDIT</a>";
                            } },//8
                        { "data": "cid", "orderable": false },
                        { "data": "chid", "orderable": false },
                        { "data": "insurance", "orderable": false }//11
                    ],
                    //"order": [[1, "desc"]],
                    "columnDefs": [
                        { className: "vertical-align-middle text-center", "targets": [0, 8] },
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                        { "targets": [9, 10, 11], "visible": false },
                        //{
                        //    "render": function (data, type, row) {
                        //        return '<a href="#" data-toggle="modal" data-target="#modalBorrowBackupCard"><img class="bc-borrow" src="assets/img/card-borrow.svg" style="width: 24px; margin-right: 5px; margin-top: -7px;"></a>' +
                        //            '<a href="#" data-toggle="modal" data-target="#modalReturnBackupCard"><img class="bc-return" src="assets/img/card-return.svg" style="width: 24px; margin-right: 5px; margin-top: -7px;"></a>' +
                        //            '<a href="#" data-toggle="modal" data-target="#modalEditBackupCard"><i class="fa fa-edit bc-edit" style="padding-right: 5px; color: rgb(91, 192, 222);font-size: 18px;"></i></a>' +
                        //            '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove bc-remove text-danger" style="padding-right: 5px;font-size: 18px;"></i></a>' +
                        //            '<a href="#"><i class="fa fa-history bc-history" style="padding-right: 5px; color: black;font-size: 18px;"></i></a>';
                        //    },
                        //    "targets": 8
                        //}
                    ],
                    //"drawCallback": function (settings) {
                    //    //var json = settings.json;
                    //    var json = $.parseJSON(settings.json.d);

                    //    backupCardList.PageCount = json.pageCount;

                    //    var options = '';
                    //    for (var pi = 0; pi < json.pageCount; pi++) {
                    //        options += '<option ' + (backupCardList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                    //    }
                    //    $('.backupCardList #tableData #sltPageIndex').html(options);
                    //    $('.backupCardList #tableData #sltPageIndex').selectpicker('refresh');
                    //    $('.backupCardList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (backupCardList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + backupCardList.PageCount + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                    //}
                });

                // order.dt search.dt
                backupCardList.dt.on('draw.dt', function () {
                    backupCardList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = ('' + ((backupCardList.PageIndex * backupCardList.PageSize) + (i + 1)));
                    });
                    backupCardList.dt.column(8, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        //var cid = backupCardList.dt.cells({ row: i, column: 9 }).data()[0];
                        //var chid = backupCardList.dt.cells({ row: i, column: 10 }).data()[0];
                        //var insurance = backupCardList.dt.cells({ row: i, column: 11 }).data()[0];

                        //var cardName = backupCardList.dt.cells({ row: i, column: 1 }).data()[0];
                        //var barCode = backupCardList.dt.cells({ row: i, column: 2 }).data()[0];
                        //var nfc = backupCardList.dt.cells({ row: i, column: 3 }).data()[0];

                        ////console.log('cid:' + cid + ', chid:' + chid);
                        ////console.log('insurance:' + insurance);

                        //if (!chid) {
                        //    //console.log('borrow');
                        //    $(cell).find(".bc-return").parent().hide();
                        //    $(cell).find(".bc-borrow").parent().show();
                        //    $(cell).find(".bc-borrow").parent().attr("onClick", 'BorrowBackupCard(\'' + cid + '\', \'' + insurance + '\')');
                        //}
                        //else {
                        //    //console.log('return');
                        //    $(cell).find(".bc-borrow").parent().hide();
                        //    $(cell).find(".bc-return").parent().show();
                        //    $(cell).find(".bc-return").parent().attr("onClick", 'ReturnBackupCard(\'' + chid + '\')');
                        //}
                        //$(cell).find(".bc-edit").parent().attr("onClick", 'EditBackupCard(\'' + cid + '\', \'' + cardName + '\', \'' + nfc + '\', \'' + barCode + '\', \'' + chid + '\', \'' + insurance + '\')');
                        //$(cell).find(".bc-remove").parent().attr("cid", cid);
                        //$(cell).find(".bc-history").parent().attr("href", "BackupCardHistory.aspx?cid=" + cid);
                    });
                });
            },
            RemoveItem: function (cid) {
                //console.log('backupCardList.RemoveItem');
                $("body").mLoading();
                
                $.ajax({
                    type: "POST",
                    url: "BackupCardList.aspx/RemoveItem_v2",
                    data: '{cid: \'' + cid + '\'}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: backupCardList.OnSuccessRemove,
                    failure: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                        $("body").mLoading('hide');
                        ////$("#modalWaitDialog").modal('hide');
                    },
                    error: function (response) {
                        alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                        $("body").mLoading('hide');
                        ////$("#modalWaitDialog").modal('hide');
                    }
                });
            },
            OnSuccessRemove: function (response) {
                var title = "";
                var body = "";
                $("body").mLoading('hide');
                var r = JSON.parse(response.d);
                if (r.success) {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                    backupCardList.ReloadListData();
                }
                else {
                    if (r.statusCode == 500) {
                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %> [' + r.message + ']';
                    }
                    else if (r.statusCode == 404) {
                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121006") %> [' + r.message + ']';
                    }
                }

                //$("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            ReloadListData: function () {
                //backupCardList.dt.draw();
                backupCardList.dt.ajax.reload();
            }
        }

        function EditBackupCard(cid, cardName, nfc, barCode, chid, insurance) {

            // Init element
            //console.log('EditBackupCard..');
            //console.log(cid + '..' + nfc + '..' + barCode + '..');

            $("#iptCardName2").attr('cid', cid);
            $("#iptCardName2").val(cardName);
            $("#iptNFC2").val(nfc.replace('null', ''));
            $("#iptBarCode2").val(barCode.replace('null', ''));
            $("#iptInsurance2").val(insurance);

            if (!chid) {
                // enable: card not yet borrowed
                $("#iptInsurance2").prop("disabled", false);
            }
            else {
                // disable: card was borrowed -- wait to return the card
                $("#iptInsurance2").prop("disabled", true);
            }

            EditBackupCardValidator();

            return false;
        }

        function BorrowBackupCard(cid, insurance) {

            // Init element
            //console.log('BorrowBackupCard..');

            $("#sltUserType2").val('');
            $("#iptUserName2").attr('cid', cid);
            $("#iptUserName2").attr('data-id', '');
            $("#iptUserName2").attr('data-name', '');
            $("#iptUserName2").val('');
            $("#iptUserName3").val('');
            $("#iptBalance2").val('');
            $("#spnInsurance2").text(insurance);

            $("#iptUserName2").hide();
            $("#iptUserName3").show();

            BorrowBackupCardValidator();

            return false;
        }

        function ReturnBackupCard(chid) {

            // Init element
            //console.log('ReturnBackupCard..');
            $("body").mLoading();
           
            $.ajax({
                type: "POST",
                url: "BackupCardList.aspx/GetBackupCardInfo",
                data: '{chid: \'' + chid + '\'}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("body").mLoading('hide');
                    var res = $.parseJSON(response.d);

                    if (res.success) {
                        $("#iptUserType").val(res.info.userType);
                        $("#iptUserName").val(res.info.userName);
                        $("#iptBalance").val(res.info.balance);
                        $("#iptBalance").attr('chid', chid);
                        $("#spnInsurance").text(res.info.insurance.toFixed(2));
                        $("#spnNetTotal").text((res.info.balance + res.info.insurance).toFixed(2));

                        ReturnBackupCardValidator();
                    }
                    else {
                        var title = "";
                        var body = "";

                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                        switch (res.statusCode) {
                            case 404:
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                break;
                            case 500:
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                break;
                        }

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    }
                },
                failure: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                    $("body").mLoading('hide');
                },
                error: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                    $("body").mLoading('hide');
                }
            });

            return false;
        }

        function AddBackupCard() {

            // Init element
            //console.log('AddBackupCard..');
            $("#iptCardName").val('');
            $("#iptBarCode").val('');
            $("#iptNFC").val('');
            $("#iptInsurance").val('');

            AddBackupCardValidator();

            return false;
        }

        function AddBackupCardValidator() {
            $("form").removeData('validator');
            $("form").validate({
                rules: {
                    iptCardName: "required",
                    iptNFC: {
                        required: function (element) {
                            return !$("#iptBarCode").val() && !$(element).val();
                        }
                    },
                    iptBarCode: {
                        required: function (element) {
                            return !$("#iptNFC").val() && !$(element).val();
                        }
                    },
                    iptInsurance: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    iptCardName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptNFC: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptBarCode: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptInsurance: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                tooltip_options: {
                    iptCardName: { placement: 'right', trigger: 'focus' },
                    iptNFC: { placement: 'right', trigger: 'focus' },
                    iptBarCode: { placement: 'right', trigger: 'focus' },
                    iptInsurance: { placement: 'right', trigger: 'focus' }
                },
                submitHandler: function (e) {
                }
            });
        }

        function EditBackupCardValidator() {
            $("form").removeData('validator');
            $("form").validate({
                rules: {
                    iptCardName2: "required",
                    iptInsurance2: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    iptCardName2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptInsurance2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                tooltip_options: {
                    iptCardName2: { placement: 'right', trigger: 'focus' },
                    iptInsurance2: { placement: 'right', trigger: 'focus' }
                },
                submitHandler: function (e) {
                }
            });
        }

        function ReturnBackupCardValidator() {
            $("form").removeData('validator');
            $("form").validate({
                rules: {
                    iptBalance: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    iptBalance: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                    }
                },
                tooltip_options: {
                    iptBalance: { placement: 'right', trigger: 'focus' }
                },
                submitHandler: function (e) {
                }
            });
        }

        function BorrowBackupCardValidator() {
            $("form").removeData('validator');
            $("form").validate({
                rules: {
                    sltUserType2: "required",
                    iptUserName2: {
                        required: function (element) {
                            return ($("#sltUserType2").val() == "0" || $("#sltUserType2").val() == "1") && !$(element).val();
                        }
                    },
                    iptUserName3: {
                        required: function (element) {
                            return $("#sltUserType2").val() == "2" && !$(element).val();
                        }
                    },
                    iptBalance2: {
                        required: true,
                        number: true,
                        greaterThan: "#spnInsurance2"
                    }
                },
                messages: {
                    sltUserType2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptUserName2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptUserName3: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptBalance2: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                        number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>",
                        greaterThan: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106165") %>"
                    }
                },
                tooltip_options: {
                    sltUserType2: { placement: 'right', trigger: 'focus' },
                    iptUserName2: { placement: 'right', trigger: 'focus' },
                    iptUserName3: { placement: 'right', trigger: 'focus' },
                    iptBalance2: { placement: 'right', trigger: 'focus' }
                },
                submitHandler: function (e) {
                }
            });
        }

        $(document).keypress(function (e) {
            if ($("#modalAddBackupCard").hasClass('in') && (e.keyCode == 13 || e.which == 13)) {
                //console.log('btnSaveAddBackupCard');
                $('#btnSaveAddBackupCard').click();
            }
            else if ($("#modalBorrowBackupCard").hasClass('in') && (e.keyCode == 13 || e.which == 13)) {
                //console.log('btnSaveBorrowBackupCard');
                $('#btnSaveBorrowBackupCard').click();
            }
            else if ($("#modalReturnBackupCard").hasClass('in') && (e.keyCode == 13 || e.which == 13)) {
                //console.log('btnSaveReturnBackupCard');
                $('#btnSaveReturnBackupCard').click();
            }
            else if ($("#modalEditBackupCard").hasClass('in') && (e.keyCode == 13 || e.which == 13)) {
                //console.log('btnSaveEditBackupCard');
                $('#btnSaveEditBackupCard').click();
            }
            else {
                //console.log('Press enter no modal popup..');
            }
        })

        $(document).ready(function () {

            $.ajaxSetup({
                statusCode: {
                    500: function () {
                        window.location.replace("/Default.aspx");
                    }
                }
            });

            if (jQuery().dataTable) {
                $.fn.dataTable.ext.errMode = 'none';
            }

            $.validator.addMethod("greaterThan",
                function (value, element, param) {
                    var $otherElement = $(param);
                    return parseInt(value, 10) > parseInt($otherElement.text(), 10);
                }
            );

            // Searching, Pagination event 
            $('.backupCardList #btnSearch').click(function () {

                backupCardList.PageIndex = 0;

                backupCardList.ReloadListData();

                return false;
            });

            $('.backupCardList #tableData #sltPageSize').change(function () {

                backupCardList.PageSize = parseInt($(".backupCardList #tableData #sltPageSize").children("option:selected").val());
                backupCardList.PageIndex = 0;

                backupCardList.ReloadListData();

                return false;
            });

            $('.backupCardList #tableData #sltPageIndex').change(function () {

                backupCardList.PageIndex = parseInt($(".backupCardList #tableData #sltPageIndex").children("option:selected").val());

                backupCardList.ReloadListData();

                return false;
            });

            $('.backupCardList #tableData #aPrevious').click(function () {

                if (backupCardList.PageIndex > 0) {
                    backupCardList.PageIndex--;
                    backupCardList.ReloadListData();
                }

                return false;
            });

            $('.backupCardList #tableData #aNext').click(function () {

                if (backupCardList.PageIndex < (backupCardList.PageCount - 1)) {
                    backupCardList.PageIndex++;
                    backupCardList.ReloadListData();
                }

                return false;
            });

            // Search

            // Modal Section
            $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                $title = $(e.relatedTarget).attr('data-title');
                $(this).find('.modal-title').text($title);
                $message = $(e.relatedTarget).attr('data-message');
                $(this).find('.modal-body p').text($message);
                $cid = $(e.relatedTarget).attr('cid');
                $(this).find('.modal-footer #modalConfirmRemove').attr('cid', $cid);
            });
            $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                $('#modalNotifyConfirmRemove').modal('hide');

                //$("#modalWaitDialog").modal('show');

                // Remove command
                backupCardList.RemoveItem($(this).attr('cid'));

            });

            $('#btnSaveEditBackupCard').click(function () {

                if ($("form").valid()) {
                    //console.log('SaveEditBackupCard..');

                    var data = {
                        "cid": $("#iptCardName2").attr('cid'),
                        "cardName": $("#iptCardName2").val(),
                        "insurance": $("#iptInsurance2").val()
                    }

                    //$("#modalWaitDialog").modal('show');
                    $("body").mLoading();
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "BackupCardList.aspx/SaveEditBackupCard_v2",
                        data: "{newBackupCard:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            $("body").mLoading('hide');
                            var title = "";
                            var body = "";

                            var res = $.parseJSON(response.d);

                            if (res.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106158") %>';

                                $('#modalEditBackupCard').modal('hide');

                                backupCardList.ReloadListData();
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                switch (res.statusCode) {
                                    case 404:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                    case 500:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                }
                            }

                            //$("#modalWaitDialog").modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                            $("#modalNotifyOnlyClose").modal('show');
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        }
                    });
                }

                return false;
            });

            $('#btnSaveBorrowBackupCard').click(function () {

                if ($("form").valid()) {
                    //console.log('SaveBorrowBackupCard..');

                    var userID = '';
                    var userName = '';
                    switch ($("#sltUserType2").val()) {
                        case "0":
                        case "1":
                            userID = $("#iptUserName2").attr('data-id');
                            userName = $("#iptUserName2").attr('data-name'); break;
                        case "2":
                            userName = $("#iptUserName3").val();
                            break;
                    }

                    var data = {
                        "cid": $("#iptUserName2").attr('cid'),
                        "userType": $("#sltUserType2").val(),
                        "userID": userID,
                        "userName": userName,
                        "balance": $("#iptBalance2").val(),
                    }

                    //$("#modalWaitDialog").modal('show');
                    $("body").mLoading();
                 
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "BackupCardList.aspx/SaveBorrowBackupCard_v2",
                        data: "{borrowBackupCard:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            $("body").mLoading('hide');
                            var title = "";
                            var body = "";

                            var res = $.parseJSON(response.d);

                            if (res.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106158") %>';

                                $('#modalBorrowBackupCard').modal('hide');

                                backupCardList.ReloadListData();
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                switch (res.statusCode) {
                                    case 404:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                    case 500:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                }
                            }
                           
                            //$("#modalWaitDialog").modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                            $("#modalNotifyOnlyClose").modal('show');
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        }
                    });
                }

                return false;
            });

            $('#btnSaveReturnBackupCard').click(function () {

                if ($("form").valid()) {
                    //console.log('SaveReturnBackupCard..');

                    var data = {
                        "chid": $("#iptBalance").attr('chid'),
                        "balance": $("#iptBalance").val(),
                    }

                    //$("#modalWaitDialog").modal('show');
                    $("body").mLoading();
                 
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "BackupCardList.aspx/SaveReturnBackupCard_v2",
                        data: "{returnBackupCard:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            $("body").mLoading('hide');
                            var title = "";
                            var body = "";

                            var res = $.parseJSON(response.d);

                            if (res.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106158") %>';

                                $('#modalReturnBackupCard').modal('hide');

                                backupCardList.ReloadListData();
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                switch (res.statusCode) {
                                    case 404:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                    case 500:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                }
                            }

                            //$("#modalWaitDialog").modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                            $("#modalNotifyOnlyClose").modal('show');
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>'); SaveAddBackupCard
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        }
                    });
                }

                return false;
            });

            $('#btnSaveAddBackupCard').click(function () {

                if ($("form").valid()) {
                    //console.log('SaveAddBackupCard..');

                    var data = {
                        "cardName": $("#iptCardName").val(),
                        "nfc": $("#iptNFC").val(),
                        "barCode": $("#iptBarCode").val(),
                        "insurance": $("#iptInsurance").val()
                    }

                    //$("#modalWaitDialog").modal('show');
                    $("body").mLoading();
                    
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "BackupCardList.aspx/SaveAddBackupCard_v2",
                        data: "{newBackupCard:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            $("body").mLoading('hide');
                            var title = "";
                            var body = "";

                            var res = $.parseJSON(response.d);

                            if (res.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106158") %>';

                                $('#modalAddBackupCard').modal('hide');

                                backupCardList.ReloadListData();
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                switch (res.statusCode) {
                                    case 404:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                    case 500:
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [' + res.message + ']';
                                        break;
                                }
                            }

                            //$("#modalWaitDialog").modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                            $("#modalNotifyOnlyClose").modal('show');
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                            $("body").mLoading('hide');
                            //$("#modalWaitDialog").modal('hide');
                        }
                    });
                }

                return false;
            });


            //$.ui.autocomplete.prototype._renderItem = function (ul, item) {
            //    var t = String(item.value).replace(
            //        new RegExp(this.term, "gi"),
            //        "<strong>$&</strong>");
            //    return $("<li></li>")
            //        .data("item.autocomplete", item)
            //        .append("<a>" + t + "</a>")
            //        .appendTo(ul);
            //};

            //$(".backupCardList #iptBackupCardName").autocomplete({
            //    source: function (request, response) {
            //        var param = { keyword: $('.backupCardList #iptBackupCardName').val() };
            //        $.ajax({
            //            url: "BackupCardList.aspx/GetBackupCardName",
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

            $("#iptBackupCardName").autoComplete({
                resolver: 'custom',
                minLength: 1,
                events: {
                    search: function (qry, callback) {
                        //console.log(1);
                        var param = { keyword: $('.backupCardList #iptBackupCardName').val() };
                        $.ajax({
                            url: "BackupCardList.aspx/GetBackupCardName",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            //dataFilter: function (data) { return data; },
                            //success: function (data) {
                            //    response($.map(data.d, function (item) {
                            //        return {
                            //            value: item
                            //        }
                            //    }))
                            //},
                            //error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //    console.log(textStatus);
                            //}
                        }).done(function (res) {
                            callback(res.d)
                        });

                        //// let's do a custom ajax call
                        //$.ajax(
                        //    'BackupCardList.aspx/GetBackupCardName',
                        //    {
                        //        data: { 'qry': qry }
                        //    }
                        //).done(function (res) {
                        //    callback(res.results)
                        //});
                    }
                }

            });

            $("#iptUserName2").autoComplete({
                resolver: 'custom',
                minLength: 1,
                events: {
                    search: function (qry, callback) {
                        //console.log(1);
                        var param = { keyword: $('#iptUserName2').val(), userType: $("#sltUserType2").val() };
                        $.ajax({
                            url: "BackupCardList.aspx/GetUserName",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            //dataFilter: function (data) { return data; },
                            //success: function (data) {
                            //    response($.map(data.d, function (item) {
                            //        return {
                            //            value: item
                            //        }
                            //    }))
                            //},
                            //error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //    console.log(textStatus);
                            //}
                        }).done(function (data) {
                            //callback(res.d)
                            callback($.map(data.d, function (item) {
                                return {
                                    text: item.name, // default: label, value
                                    value: item.id
                                }
                            }));
                        });

                        //// let's do a custom ajax call
                        //$.ajax(
                        //    'BackupCardList.aspx/GetBackupCardName',
                        //    {
                        //        data: { 'qry': qry }
                        //    }
                        //).done(function (res) {
                        //    callback(res.results)
                        //});
                    }
                }
            });

            $('#iptUserName2').on('autocomplete.select', function (evt, item) {
                var d = item;
                if (d) {
                    $("#iptUserName2").attr('data-id', d.value);
                    $("#iptUserName2").attr('data-name', d.text);
                }
            });

            //$("#iptUserName2").autocomplete({
            //    source: function (request, response) {
            //        var param = { keyword: $('#iptUserName2').val(), userType: $("#sltUserType2").val() };
            //        $.ajax({
            //            url: "BackupCardList.aspx/GetUserName",
            //            data: JSON.stringify(param),
            //            dataType: "json",
            //            type: "POST",
            //            contentType: "application/json; charset=utf-8",
            //            dataFilter: function (data) { return data; },
            //            success: function (data) {
            //                response($.map(data.d, function (item) {
            //                    return {
            //                        value: item.name, // default: label, value
            //                        id: item.id
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
            //        var item = ui.item;
            //        if (item) {
            //            $("#iptUserName2").attr('data-id', item.id);
            //            $("#iptUserName2").attr('data-name', item.value);
            //        }
            //    },
            //    minLength: 1
            //});

            $("#sltUserType2").change(function () {
                switch ($(this).val()) {
                    case "0":
                    case "1":
                        $("#iptUserName2").show();
                        $("#iptUserName3").hide();
                        $("#iptUserName2").attr('data-id', '');
                        $("#iptUserName2").attr('data-name', '');
                        $("#iptUserName2").val('');
                        $("#iptUserName2").focus();
                        break;
                    case "2":
                        $("#iptUserName2").hide();
                        $("#iptUserName3").show();
                        $("#iptUserName3").focus();
                        break;
                    default:
                        $("#iptUserName2").hide();
                        $("#iptUserName3").show();
                        $("#iptUserName3").focus();
                        break;
                }
            });

            // Initial control
            $('#iptBalance, #iptBalance2, #iptInsurance, #iptInsurance2').number(true, 2);

            //setting callback function for 'hidden.bs.modal' event
            $('#modalNotifyOnlyClose').on('shown.bs.modal', function (e) {
                // focus btn
                $('#modalClose').focus();
            }).on('hidden.bs.modal', function () {
                //remove the backdrop
                $('.modal-backdrop').remove();
                //console.log('.modal-backdrop');
            });

            $('#modalAddBackupCard').on('shown.bs.modal', function (e) {
                // focus btn
                $("#iptCardName").focus();
            });

            $('#modalBorrowBackupCard').on('shown.bs.modal', function (e) {
                // focus btn
                $("#iptUserName3").focus();
            });

            // Datatable Section
            backupCardList.LoadListData();
        });

    </script>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>--%>
