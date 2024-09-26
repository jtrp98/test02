<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .dropdown .dropdown-toggle:after {
            margin-right: -18px;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: transparent;
            opacity: 1;
        }

        .modal small {
            display: block;
            font-size: 65%;
            margin-top: -2px;
        }

        .modal .form-group {
            margin: 0px 0 0;
        }

        .row.h41 {
            height: 41px;
        }

        .auto-gen-pass, .create-pass {
            display: none;
            margin-top: -12px;
            padding-bottom: 10px;
        }

        label.error {
            display: flex;
            font-size: 11px !important;
            margin-top: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>
            </p>
        </div>
    </div>

    <div class="studentList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        <div class="col-sm-3">
                            <select id="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-sm-3">
                            <select id="sltTerm" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
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
                            <select id="sltClass" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-sm-3">
                            <div class="form-group bmd-form-group">
                                <input id="iptStudentName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101013") %>">
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label"></label>
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11 mr-auto text-center">
                            <button id="btnSearch" class="btn btn-info">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                            </button>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div class="studentList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="material-datatables">
                        <div id="datatables_wrapper" class="dataTables_wrapper dt-bootstrap4">
                            <div class="row">
                                <div class="col-sm-12 col-md-6">
                                    <div class="dataTables_length">
                                        <label>
                                            Show
                                            <select id="datatables_length" aria-controls="datatables" class="custom-select custom-select-sm form-control form-control-sm" style="text-align-last: center;">
                                                <option value="10">10</option>
                                                <option value="20" selected>20</option>
                                                <option value="50">50</option>
                                                <option value="100">100</option>
                                            </select>
                                            rows</label>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-6">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <table id="tableData" class="table table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>
                                                    <p style="height: 0px; margin: -2px 0px 2px 0px; font-size: 0.9em;">(Username)</p>
                                                </th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                                <th style="width: 14%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                                <th style="width: 15%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                                <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></th>
                                                <th style="width: 18%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                <th style="width: 15%" class="text-center">
                                                    <div class="btn-group">
                                                        <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>
                                                        </button>
                                                        <ul class="dropdown-menu pull-right">
                                                            <li><a href="#" onclick="return AddStudent();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101023") %></a></li>
                                                            <li><a href="#" data-toggle="modal" data-target="#modalSortStudentNo" onclick="return ResetModalSortStudentNo();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101024") %></a></li>
                                                            <li><a href="#" data-toggle="modal" data-target="#modalCopyStudentNo" onclick="return ModalCopyStudentNo();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101025") %></a></li>
                                                            <li><a href="#" data-toggle="modal" data-target="#modalManageStudentTitle" onclick="return ResetModalManageStudentTitle();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101026") %></a></li>
                                                            <li><a href="#" onclick="return ImportStudent();"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101027") %></a></li>
                                                            <li><a target="_blank" href="/StudentInfo/ProfileImage.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101028") %></a></li>
                                                            <%=FunctionDeleteAllStudentData %>
                                                        </ul>
                                                    </div>
                                                </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-5">
                                    <div class="dataTables_info" role="status" aria-live="polite">Showing 1 to 10 of 40 rows</div>
                                </div>
                                <div class="col-sm-12 col-md-7">
                                    <div class="dataTables_paginate paging_full_numbers">
                                        <ul class="pagination">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div id="modalShowPassword" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 650px; max-width: 900px;">
                <div class="modal-header text-center" style="padding: 0px; display: block;">
                    <h3 id="modalTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %><small>Password Information</small></h3>
                </div>
                <div class="modal-body product-add-container">
                    <div id="divShowPassword">
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %><small>User</small></label>
                            <div class="col-sm-8">
                                <div class="form-group bmd-form-group" style="padding-right: 25px; margin-top: 7px;">
                                    <span id="spnFullname" style="padding-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111079") %></span>
                                    <input id="iptUsername" type="text" class="form-control" readonly="readonly" data-sid="" style="width: 70%; display: none;" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label text-left text-nowrap"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101228") %><small>First Password</small></label>
                            <div class="col-sm-8">
                                <div class="input-group" id="showHidePassword" style="display: inline-flex; width: 60%;">
                                    <input id="iptPassword" class="form-control" type="password" value="0000" placeholder="password" readonly="readonly" style="padding-left: 0px;" />
                                    <div class="input-group-addon" style="margin-left: -30px;">
                                        <a href="" class="btn btn-primary global-btn" style="padding: 5px 6px;"><i class="fa fa-eye-slash" aria-hidden="true" style=""></i></a>
                                    </div>
                                </div>
                                <button type="button" id="btnChangePassword" class="btn btn-primary" style="padding: 5px 15px 2px 13px; font-size: 10px;"><i class="fa fa-key" aria-hidden="true" style="margin-top: -10px; margin-right: 7px; font-size: 15px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101229") %><small style="padding-left: 20px;">Change Password</small></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label text-left">PIN</label>
                            <div class="col-sm-8">
                                <button type="button" id="btnResetPIN" class="btn btn-outline-primary">Reset PIN</button>
                            </div>
                        </div>
                    </div>
                    <div id="divResetPassword" style="display: none;">
                        <form id="frmResetPassword">
                            <div class="row">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %><small>User</small></label>
                                <div class="col-sm-8">
                                    <div class="form-group bmd-form-group" style="padding-right: 25px; margin-top: 7px;">
                                        <span id="spnFullname2" style="padding-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111079") %></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <input type="radio" name="rdoResetPassword" id="rdoAutoGeneratePassword" value="auto-gen-pass" />
                                    <label class="form-check-label" for="rdoAutoGeneratePassword" style="font-weight: normal; vertical-align: middle; margin-left: 5px; color: black;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101231") %><small>Automatically Generate Password</small>
                                    </label>
                                </div>
                            </div>
                            <div class="row auto-gen-pass">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label text-left text-nowrap" style="padding-left: 38px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %><small>New Password</small></label>
                                <div class="col-sm-8">
                                    <div class="input-group" id="showNewPassword" style="display: inline-flex; width: 60%;">
                                        <input id="iptNewPassword" name="iptNewPassword" class="form-control" type="text" style="padding-left: 0px; padding-right: 30px; text-align: center;" maxlength="20" />
                                        <div class="input-group-addon" style="margin-left: -30px;">
                                            <a href="" class="btn btn-primary global-btn eye" style="padding: 5px 6px;"><i class="fa fa-eye" aria-hidden="true" style=""></i></a>
                                            <a href="" class="btn btn-primary global-btn refresh" style="padding: 5px 6px;"><i class="fa fa-refresh" aria-hidden="true" style=""></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <input type="radio" name="rdoResetPassword" id="rdoCreatePassword" value="create-pass" />
                                    <label class="form-check-label" for="rdoCreatePassword" style="font-weight: normal; vertical-align: middle; margin-left: 5px; color: black;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101233") %><small>Create Password</small>
                                    </label>
                                </div>
                            </div>
                            <div class="row create-pass">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label text-left text-nowrap" style="padding-left: 38px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %><small>New Password</small></label>
                                <div class="col-sm-8">
                                    <div class="input-group" id="showNewPassword2" style="display: inline-flex; width: 60%;">
                                        <input id="iptNewPassword2" name="iptNewPassword2" class="form-control" type="text" style="padding-left: 0px; padding-right: 30px; text-align: center;" maxlength="20" />
                                        <div class="input-group-addon" style="margin-left: -30px;">
                                            <a href="" class="btn btn-primary global-btn eye" style="padding: 5px 6px;"><i class="fa fa-eye" aria-hidden="true" style=""></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row create-pass">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-11 col-form-label text-left text-nowrap" style="padding-left: 38px; font-size: 10px; margin-top: -7px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101234") %><small>Password must be at least 6 characters.</small></label>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <input type="checkbox" id="chkConfirmMailToParent" disabled />
                                    <label class="form-check-label" for="chkConfirmMailToParent" style="font-weight: normal; vertical-align: middle; margin-left: 5px; color: #ccc;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101235") %><small>Confirm Password Reset</small>
                                    </label>
                                </div>
                            </div>
                            <div class="row h41">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-11">
                                    <label class="form-check-label" style="font-weight: normal; color: black; font-size: 12px;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101236") %>
                                        <small style="margin-top: 1px; font-size: 75%;">Note : The application will automatically log the user out, and forced to set a new password after logging in.</small>
                                    </label>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="row message_response">
                        <div class="col-xs-12 center">
                            <label class="text-danger"></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnClose" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <i class="fa fa-close" aria-hidden="true" style="margin-right: 7px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    <button type="button" id="btnResetPassword" class="btn btn-success global-btn" style="display: none;">
                        <i class="fa fa-save" aria-hidden="true" style="margin-right: 7px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111080") %></button>
                    <button type="button" id="btnCancelResetPassword" class="btn btn-danger global-btn" style="display: none;">
                        <i class="fa fa-close" aria-hidden="true" style="margin-right: 7px;"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalGenerateQrCode" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 25%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header text-center" style="padding: 0px; top: 25%; display: block;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00018") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row" style="display: block;">
                            <div class="col-xs-12 text-center">
                                <img id="imgQrCode" alt="" src="//:0" width="250px" height="250px" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" class="btn btn-danger global-btn"
                        data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalImportStudentData" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 25%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101027") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-4">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111081") %></label>
                                </div>
                                <div class="col-xs-8">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-4">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101232") %></label>
                                </div>
                                <div class="col-xs-8">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-4">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111082") %></label>
                                </div>
                                <div class="col-xs-8">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveImportStudentData" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalSortStudentNo" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 400px;">

                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101246") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-md-11">
                                <b>1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101247") %></b>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9">
                                <input type="checkbox" id="chkStudentID" disabled checked />
                                <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101248") %></b>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9" style="padding-left: 36px;">
                                <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101249") %></b>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9" style="padding-left: 36px; font-size: 20px;">
                                <input type="radio" name="rdoSex" id="rdoSex1" value="0" />
                                <label class="form-check-label" for="rdoSex1" style="font-weight: normal;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101250") %>
                                </label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9" style="padding-left: 36px; font-size: 20px;">
                                <input type="radio" name="rdoSex" id="rdoSex2" value="1" />
                                <label class="form-check-label" for="rdoSex2" style="font-weight: normal;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101251") %>
                                </label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9">
                                <input type="checkbox" id="chkMoveIn" />
                                <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101252") %></b>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9">
                                <input type="checkbox" id="chkMoveInAfterMonth6" />
                                <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101253") %></b>
                                <%--<b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111083") %></b>--%>
                                <%--<b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111084") %></b>--%>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <b>2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %></b>
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9" style="padding-left: 36px; font-size: 20px;">
                                <input type="radio" name="rdoClass" id="rdoClass1" value="all" />
                                <label class="form-check-label" for="rdoClass1" style="font-weight: normal;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>
                                </label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9" style="padding-left: 36px; font-size: 20px;">
                                <input type="radio" name="rdoClass" id="rdoClass2" value="room" />
                                <label class="form-check-label" for="rdoClass2" style="font-weight: normal;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101255") %>
                                </label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row class-room" style="display: none;">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-10" style="padding-left: 36px; font-size: 20px;">
                                <div class="row" style="padding-left: 17px;">
                                    <label class="col-md-5 col-form-label text-left" style="font-weight: normal; font-size: 14px; white-space: nowrap; padding-top: 14px;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %>:</label>
                                    <div class="col-md-7" style="padding-right: 0px;">
                                        <select id="sltSortLevel" name="sltSortLevel[]"
                                            class="selectpicker col-sm-12" data-style="select-with-transition" data-dropup-auto="false">
                                            <asp:Literal ID="ltrSortLevel" runat="server" />
                                        </select>
                                    </div>
                                </div>
                                <div class="row" style="padding-left: 17px;">
                                    <label class="col-md-5 col-form-label text-left" style="font-weight: normal; font-size: 14px; padding-top: 14px;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>:</label>
                                    <div class="col-md-7" style="padding-right: 0px;">
                                        <select id="sltSortClass" name="sltSortClass[]"
                                            class="selectpicker col-sm-12" data-style="select-with-transition" data-dropup-auto="false">
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <b>3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101256") %></b>
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-5" style="padding-left: 36px; font-size: 20px;">
                                <select id="sltSortTerm" name="sltSortTerm[]"
                                    class="selectpicker col-sm-12" data-style="select-with-transition" data-dropup-auto="false">
                                </select>
                            </div>
                            <div class="col-md-5">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveSortStudentNo" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" id="btnCancelSortStudentNo" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalCopyStudentNo" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 100%;">

                <div class="modal-header" style="padding: 0px 15px; top: 15%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101025") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <label class="col-md-4 col-form-label text-right" style="padding-top: 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101262") %></label>
                            <div class="col-md-3">
                                <select id="sltOriginYear" class="selectpicker col-sm-12" data-style="select-with-transition">
                                </select>
                            </div>
                            <label class="col-md-1 col-form-label" style="padding-top: 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                            <div class="col-md-3">
                                <select id="sltOriginTerm" class="selectpicker col-sm-12" data-style="select-with-transition">
                                </select>
                            </div>
                            <label class="col-md-1 col-form-label"></label>
                        </div>
                        <div class="row">
                            <label class="col-md-4 col-form-label text-right" style="padding-top: 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101263") %></label>
                            <div class="col-md-3">
                                <select id="sltDestinationYear" class="selectpicker col-sm-12" data-style="select-with-transition">
                                </select>
                            </div>
                            <label class="col-md-1 col-form-label" style="padding-top: 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                            <div class="col-md-3">
                                <select id="sltDestinationTerm" class="selectpicker col-sm-12" data-style="select-with-transition">
                                </select>
                            </div>
                            <label class="col-md-1 col-form-label"></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveCopyStudentNo" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" id="btnCancelCopyStudentNo" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="modalManageStudentTitle" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 10%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="width: 535px;">

                <div class="modal-header" style="padding: 0px 15px; top: 25%;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101026") %></h3>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10">
                                <b>1. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101267") %></b>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9">
                                <input type="radio" name="rdoTitleLower15" id="rdoTitleLower151" value="1" />
                                <label class="form-check-label" for="rdoTitleLower151" style="font-weight: normal;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101268") %>
                                </label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9">
                                <input type="radio" name="rdoTitleLower15" id="rdoTitleLower152" value="2" />
                                <label class="form-check-label" for="rdoTitleLower152" style="font-weight: normal;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101269") %>
                                </label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10">
                                <b>2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101270") %></b>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-9">
                                <input type="checkbox" id="chkTitleOver15" />
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101271") %></label>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10" style="display: flex;">
                                <b>3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101272") %></b>
                                <div class="form-group" style="margin: -7px 0 0 14px; padding-bottom: 0px;">
                                    <input id="iptTitleDate" name="iptTitleDate" type="text" class="form-control datepicker" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-10" style="display: flex;">
                                <label class="col-form-label text-left" style="font-weight: normal; font-size: 14px; white-space: nowrap; padding-top: 14px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                <div class="" style="margin: 2px 5px 0px 5px;">
                                    <select id="sltTitleLevel" name="sltTitleLevel[]"
                                        class="selectpicker" data-style="select-with-transition" data-dropup-auto="false" data-width="110px">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                        <asp:Literal ID="ltrTitleLevel" runat="server" />
                                    </select>
                                </div>
                                <label class="col-form-label text-left" style="font-weight: normal; font-size: 14px; padding-top: 14px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                <div class="" style="margin: 2px 5px 0px 5px;">
                                    <select id="sltTitleClass" name="sltTitleClass[]"
                                        class="selectpicker" data-style="select-with-transition" data-dropup-auto="false" data-width="122px">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-1">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveManageStudentTitle" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" id="btnCancelManageStudentTitle" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>



    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script type="text/javascript">

        var studentList = {
            YearTermData: [<%=YearTermData%>],
            PageIndex: 0,
            PageSize: 20,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                studentList.dt = $('.studentList #tableData').DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "StudentList.aspx/LoadStudent",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.year = $(".studentList #sltYear").children("option:selected").val();
                            d.term = $(".studentList #sltTerm").children("option:selected").val();
                            d.level = $(".studentList #sltLevel").children("option:selected").val();
                            d.className = $(".studentList #sltClass").children("option:selected").val();
                            d.stdName = $(".studentList #iptStudentName").val();
                            d.page = studentList.PageIndex;
                            d.length = studentList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            var jsond = $.parseJSON(json.d);
                            return jsond.data;
                        },
                        "beforeSend": function () {
                            // Handle the beforeSend event
                            //$("#modalWaitDialog").modal('show');
                        },
                        "complete": function () {
                            // Handle the complete event
                            //$("#modalWaitDialog").modal('hide');
                        }
                    },
                    "columns": [
                        { "data": "no", "orderable": false },
                        { "data": "No", "orderable": true },
                        { "data": "Code", "orderable": true },
                        { "data": "Title", "orderable": true },
                        { "data": "Name", "orderable": true },
                        { "data": "Lastname", "orderable": true },
                        { "data": "ClassName", "orderable": true },
                        { "data": "Status", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "sid", "orderable": false },
                        { "data": "fingerStatus", "orderable": false },
                        { "data": "tid", "orderable": false }
                    ],
                    "order": [[1, "asc"]],
                    "columnDefs": [
                        { className: "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                        { "targets": [9, 10, 11], "visible": false },
                        {
                            "render": function (data, type, row) {
                                return '<a href="#"><i class="fa fa-edit" style="padding-right: 5px; font-size: 22px;"></i></a>' +
                                    //'<a href="#"><i class="fa fa-key" style="padding-right: 5px; font-size: 22px;"></i></a>' +
                                    ((/true/).test('<%=HaveNewResetPasswordPermission%>'.toLowerCase()) ? '<a href="#"><i class="fa fa-key" style="padding-right: 5px; font-size: 22px;"></i></a>' : '') +
                                    '<a href="#"><i class="fa fa-qrcode" style="padding-right: 5px; font-size: 22px;"></i></a>' +
                                    '<a href="#" data-toggle="modal" data-target="#modalNotifyConfirmRemove" data-title="Confirm Dialog" data-message="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101240") %>"><i class="fa fa-remove text-danger" style="font-size: 22px;"></i></a>';
                            },
                            "targets": 8
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        // console.log(json);

                        studentList.PageCount = json.pageCount;
                        studentList.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (studentList.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (studentList.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (studentList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (studentList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (studentList.PageIndex - pageLRSize <= pi && studentList.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (studentList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((studentList.PageIndex * studentList.PageSize) + 1) + ' to ' + ((studentList.PageIndex * studentList.PageSize) + studentList.PageSize) + ' of ' + studentList.TotalRows + ' rows');
                    },
                    "createdRow": function (row, data, dataIndex) {
                        if (data.fingerStatus == "False") {
                            $(row).addClass('none-finger');
                        }
                    }
                });

                // order.dt search.dt
                studentList.dt.on('draw.dt', function () {
                    studentList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (studentList.PageIndex * studentList.PageSize) + (i + 1) + '.';
                    });
                    studentList.dt.column(8, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sid = studentList.dt.cells({ row: i, column: 9 }).data()[0];
                        var tid = studentList.dt.cells({ row: i, column: 11 }).data()[0];
                        $(cell).find(".fa-edit").parent().attr("class", "StudentDetail").attr("href", "StudentDetail.aspx?v=form&sid=" + sid + "&tid=" + tid).attr("target", "_blank").attr("id", sid);
                        if ((/true/).test('<%=HaveNewResetPasswordPermission%>'.toLowerCase())) $(cell).find(".fa-key").parent().attr("onClick", 'ShowPassword(' + sid + ')');
                        $(cell).find(".fa-qrcode").parent().attr("onClick", 'GenerateQrCode(' + sid + ')');
                        $(cell).find(".fa-remove").parent().attr("sid", sid);
                    });
                });
            },
            RemoveItem: function (sid) {
                $.ajax({
                    type: "POST",
                    url: "StudentList.aspx/RemoveItem",
                    data: '{sid: ' + sid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: studentList.OnSuccessRemove,
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

                var r = JSON.parse(response.d);
                if (r.success) {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                    studentList.ReloadListData();
                }
                else {
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121007") %>: ' + r.message;
                }

                $("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            ReloadListData: function () {
                studentList.dt.draw();
            }
        }

        function ShowPassword(sid) {
            $.ajax({
                async: false,
                type: "POST",
                url: "StudentList.aspx/ShowPassword",
                data: '{sid: ' + sid + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d != "error") {
                        $("span[id^=spnFullname]").text(response.d.userName);
                        $("#iptUsername").val(response.d.userName).attr('data-sid', sid);
                        $("#iptPassword").val(response.d.password);

                        $('#modalTitle').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %><small>Password Information</small>');
                        $('#divShowPassword').show();
                        $('#btnClose').show();

                        $('input[name=rdoResetPassword]').prop('checked', false);
                        $('.auto-gen-pass').hide();
                        $('.create-pass').hide();

                        $('#btnResetPassword').attr('disabled', 'disabled');

                        $('#divResetPassword').hide();
                        $('#btnResetPassword').hide();
                        $('#btnCancelResetPassword').hide();

                        $('#modalShowPassword').modal('show');
                    }
                },
                failure: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                },
                error: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                }
            });
        }

        function GenerateQrCode(sid) {
            $.ajax({
                async: false,
                type: "POST",
                url: "StudentList.aspx/GenerateQrCode",
                data: '{sid: ' + sid + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d != "error") {
                        $("#imgQrCode").attr('src', response.d.qrcode);

                        $('#modalGenerateQrCode').modal('show');
                    }
                },
                failure: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                },
                error: function (response) {
                    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                }
            });
        }

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "StudentList.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            var firstSelected = 'selected';
                            $(yearData).each(function () {

                                options += '<option value="' + this.id + '" ' + firstSelected + '>' + this.name + '</option>';

                                firstSelected = '';
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

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "StudentList.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
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

        function AddStudent() {
            $.ajax({
                async: false,
                type: "POST",
                url: "/App_Logic/StudentLimitInContact.ashx",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // console.log(response);
                    if (response.success) {
                        if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {
                            window.open("/StudentInfo/StudentDetail.aspx?v=view&sid=0", "_blank").focus();
                        }
                        else {
                            systemMessage.LimitInContact(response);
                        }
                    }
                    else {
                        console.log(response);
                    }
                },
                failure: function (response) {
                    console.log(response);
                },
                error: function (response) {
                    console.log(response);
                }
            });

            return false;
        }

        function ImportStudent() {

            window.open("/StudentInfo/ImportStudentData.aspx", "_blank").focus();

            //$.ajax({
            //    async: false,
            //    type: "POST",
            //    url: "/App_Logic/StudentLimitInContact.ashx",
            //    data: '{}',
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (response) {
            //        // console.log(response);
            //        if (response.success) {
            //            if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {
            //                window.open("/StudentInfo/ImportStudentData.aspx", "_blank").focus();
            //            }
            //            else {
            //                systemMessage.LimitInContact(response);
            //            }
            //        }
            //        else {
            //            console.log(response);
            //        }
            //    },
            //    failure: function (response) {
            //        console.log(response);
            //    },
            //    error: function (response) {
            //        console.log(response);
            //    }
            //});

            return false;
        }

        function ClearAllStudentData() {

            $("#modalWaitDialog").modal('show');

            $.ajax({
                async: true,
                type: "POST",
                url: "StudentList.aspx/ClearAllStudentData",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var title = "";
                    var body = "";

                    var r = JSON.parse(response.d);
                    if (r.success) {
                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111073") %>';
                    }
                    else {
                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111074") %>: ' + r.message;
                    }

                    $("#modalWaitDialog").modal('hide');

                    $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                    $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                    $("#modalNotifyOnlyClose").modal('show');
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

        function ResetModalSortStudentNo() {

            $("input[name='rdoSex']").attr("checked", false);
            $("#chkMoveIn").prop("checked", false);
            $("#chkMoveInAfterMonth6").prop("checked", false);
            $('#rdoClass2').prop('checked', true);
            $('input[name="rdoClass"]:radio').change();

            $("#sltSortTerm").empty();
            $("#sltSortTerm").append('<option selected="selected" value="all"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>');
            $("#sltTerm > option:not(.bs-title-option)").each(function () {
                $("#sltSortTerm").append('<option value="' + this.value + '">' + this.text + '</option>');
            });
            $('#sltSortTerm').selectpicker('refresh');

            return false;
        }

        function ModalCopyStudentNo() {

            if (studentList.YearTermData.length > 0) {

                // Year
                const yearData = [];
                const map = new Map();
                for (const item of studentList.YearTermData) {
                    if (!map.has(item.yearId)) {
                        map.set(item.yearId, true);
                        yearData.push({
                            no: item.no,
                            yearId: item.yearId,
                            yearNumber: item.yearNumber,
                            currentYear: item.currentYear
                        });
                    }
                }

                $('#sltOriginYear').empty();
                var options = '';
                $(yearData).each(function () {
                    options += '<option value="' + this.yearId + '" ' + (this.currentYear ? 'selected' : '') + ' data-no="' + this.no + '" data-current-year="' + this.currentYear + '">' + this.yearNumber + '</option>';
                });
                $('#sltOriginYear').html(options);
                $('#sltOriginYear').selectpicker('refresh');

                var currentYear = yearData.find(x => x.currentYear == true).yearId;

                SetTerm('#sltOriginTerm', currentYear, studentList.YearTermData);

                $('#sltOriginTerm').change();
            }

            return false;
        }

        function ResetModalManageStudentTitle() {

            $("input[name='rdoTitleLower15']").attr("checked", false);
            $("#chkTitleOver15").prop("checked", false);

            return false;
        }

        function SetTerm(obj, yearSelect, yearTermData) {

            // Term
            var termData = [];
            const map = new Map();
            for (const item of yearTermData.filter(x => x.yearId == yearSelect)) {
                if (!map.has(item.termId)) {
                    map.set(item.termId, true);
                    termData.push({
                        no: item.no,
                        termId: item.termId,
                        term: item.term,
                        currentTerm: item.currentTerm
                    });
                }
            }

            $(obj).empty();
            options = '';
            $(termData).each(function () {
                options += '<option value="' + this.termId + '" ' + (this.currentTerm ? 'selected' : '') + ' data-no="' + this.no + '" data-current-term="' + this.currentTerm + '">' + this.term + '</option>';
            });
            $(obj).html(options);
            $(obj).selectpicker('refresh');
        }

        function SetYearTermDestination(objYear, objTerm, originTermNo) {

            var YearTermDataMoreThanNo = $.grep(studentList.YearTermData, function (o, i) { return o.no > originTermNo; });

            // Year
            const yearData = [];
            const map = new Map();
            for (const item of YearTermDataMoreThanNo) {
                if (!map.has(item.yearId)) {
                    map.set(item.yearId, true);
                    yearData.push({
                        no: item.no,
                        yearId: item.yearId,
                        yearNumber: item.yearNumber,
                        currentYear: item.currentYear
                    });
                }
            }

            $(objYear).empty();
            var options = '';
            $(yearData).each(function () {
                options += '<option value="' + this.yearId + '" ' + (this.currentYear ? 'selected' : '') + ' data-no="' + this.no + '" data-current-year="' + this.currentYear + '">' + this.yearNumber + '</option>';
            });
            $(objYear).html(options);
            $(objYear).selectpicker('refresh');

            if (yearData.length > 0) {
                var currentYearSelect = yearData.find(x => x.currentYear == true);
                var firstYear = yearData[0].yearId;
                var currentYear = currentYearSelect ? currentYearSelect.yearId : firstYear;

                SetTerm(objTerm, currentYear, YearTermDataMoreThanNo);

                $('#btnSaveCopyStudentNo').prop('disabled', false);
            }
            else {
                $(objYear).html('<option>-</option>');
                $(objYear).selectpicker('refresh');

                $(objTerm).html('<option>-</option>');
                $(objTerm).selectpicker('refresh');

                $('#btnSaveCopyStudentNo').prop('disabled', true);
            }
        }

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


            $("form").validate({
                rules: {
                    iptNewPassword: {
                        required: function (element) {
                            return ($('input[name="rdoResetPassword"]:radio:checked').val() == 'auto-gen-pass') && !$(element).val();
                        },
                        minlength: 6,
                    },
                    iptNewPassword2: {
                        required: function (element) {
                            return ($('input[name="rdoResetPassword"]:radio:checked').val() == 'create-pass') && !$(element).val();
                        },
                        minlength: 6,
                    }
                },
                messages: {
                    iptNewPassword: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101237") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111086") %>",
                    },
                    iptNewPassword2: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101237") %>",
                        minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111086") %>",
                    }
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "iptNewPassword":
                        case "iptNewPassword2": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });


            // Searching, Pagination event 
            $('.studentList #btnSearch').click(function () {

                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $('.studentList #datatables_length').change(function () {

                studentList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                studentList.PageIndex = 0;

                studentList.ReloadListData();

                return false;
            });

            $('.studentList ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (studentList.PageIndex > 0) {
                        studentList.PageIndex--;
                        studentList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + studentList.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (studentList.PageIndex < (studentList.PageCount - 1)) {
                        studentList.PageIndex++;
                        studentList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + studentList.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    studentList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    studentList.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('.studentList #tableData #datatables_previous').click(function () {

                if (studentList.PageIndex > 0) {
                    studentList.PageIndex--;
                    studentList.ReloadListData();
                }

                return false;
            });

            $('.studentList #tableData #datatables_next').click(function () {

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

            // Modal Section
            $('#modalNotifyConfirmRemove').on('show.bs.modal', function (e) {
                $title = $(e.relatedTarget).attr('data-title');
                $(this).find('.modal-title').text($title);
                $message = $(e.relatedTarget).attr('data-message');
                $(this).find('.modal-body p').text($message);
                $sid = $(e.relatedTarget).attr('sid');
                $(this).find('.modal-footer #modalConfirmRemove').attr('sid', $sid);
            });
            $('#modalNotifyConfirmRemove').find('.modal-footer #modalConfirmRemove').on('click', function () {

                $('#modalNotifyConfirmRemove').modal('hide');

                $("#modalWaitDialog").modal('show');

                // Remove command
                studentList.RemoveItem($(this).attr('sid'));

            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });


            $("#showHidePassword a").on('click', function (event) {
                event.preventDefault();
                if ($('#showHidePassword input').attr("type") == "text") {
                    $('#showHidePassword input').attr('type', 'password');
                    $('#showHidePassword i').addClass("fa-eye-slash").removeClass("fa-eye");
                } else if ($('#showHidePassword input').attr("type") == "password") {
                    $('#showHidePassword input').attr('type', 'text');
                    $('#showHidePassword i').removeClass("fa-eye-slash").addClass("fa-eye");
                }
            });

            $("#showNewPassword a.eye").on('click', function (event) {
                event.preventDefault();
                if ($('#showNewPassword input').attr("type") == "text") {
                    $('#showNewPassword input').attr('type', 'password');
                    $('#showNewPassword i.fa-eye').addClass("fa-eye-slash").removeClass("fa-eye");
                } else if ($('#showNewPassword input').attr("type") == "password") {
                    $('#showNewPassword input').attr('type', 'text');
                    $('#showNewPassword i.fa-eye-slash').removeClass("fa-eye-slash").addClass("fa-eye");
                }
            });

            $("#showNewPassword2 a.eye").on('click', function (event) {
                event.preventDefault();
                if ($('#showNewPassword2 input').attr("type") == "text") {
                    $('#showNewPassword2 input').attr('type', 'password');
                    $('#showNewPassword2 i.fa-eye').addClass("fa-eye-slash").removeClass("fa-eye");
                } else if ($('#showNewPassword2 input').attr("type") == "password") {
                    $('#showNewPassword2 input').attr('type', 'text');
                    $('#showNewPassword2 i.fa-eye-slash').removeClass("fa-eye-slash").addClass("fa-eye");
                }
            });

            $("#showNewPassword a.refresh").on('click', function (event) {
                event.preventDefault();
                $('#showNewPassword input').val(Math.random().toString(16).slice(-10).toUpperCase());
            });

            $('input[name="rdoResetPassword"]:radio').on('change', function (e) {

                switch ($(this).val()) {
                    case "auto-gen-pass":
                        $('.auto-gen-pass').css('display', 'flex');
                        $('.create-pass').hide();
                        $('#showNewPassword input').val(Math.random().toString(16).slice(-10).toUpperCase());
                        break;
                    case "create-pass":
                        $('.auto-gen-pass').hide();
                        $('.create-pass').css('display', 'flex');
                        break;
                }

                $('#btnResetPassword').removeAttr('disabled');

                return false;
            });

            $('#btnChangePassword').click(function () {

                $('#divShowPassword').hide();
                $('#btnClose').hide();

                $('#modalTitle').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101230") %><small>Reset Password</small>');
                $('#divResetPassword').show();
                $('#btnResetPassword').show();
                $('#btnCancelResetPassword').show();

                return false;
            });

            $('#btnCancelResetPassword').click(function () {

                $('#modalTitle').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101226") %><small>Password Information</small>');
                $('#divShowPassword').show();
                $('#btnClose').show();

                $('#divResetPassword').hide();
                $('#btnResetPassword').hide();
                $('#btnCancelResetPassword').hide();

                return false;
            });

            $('#btnResetPIN').click(function () {

                $("#modalWaitDialog").modal('show');

                var sid = $("#iptUsername").attr('data-sid');

                $.ajax({
                    async: false,
                    type: "POST",
                    url: "StudentList.aspx/ResetPIN",
                    data: '{sid: ' + sid + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "success":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01751") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111087") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
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
            });

            $('#btnResetPassword').click(function () {

                if ($("#frmResetPassword").valid()) {

                    var newPassword = '';
                    switch ($('input[name="rdoResetPassword"]:radio:checked').val()) {
                        case 'auto-gen-pass': newPassword = $('#iptNewPassword').val(); break;
                        case 'create-pass': newPassword = $('#iptNewPassword2').val(); break;
                    }

                    var data = {
                        "sid": $("#iptUsername").attr('data-sid'),
                        "newPassword": newPassword
                    }

                    $("#modalWaitDialog").modal('show');

                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "StudentList.aspx/ResetPassword",
                        data: "{resetPasswordInfo:" + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                            var title = "";
                            var body = "";

                            var res = $.parseJSON(response.d);
                            //console.log(res);
                            switch (res.result) {
                                case "success":
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101238") %>';

                                    $('#modalShowPassword').modal('hide');

                                    break;
                                case "error":
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111075") %>[' + res.message + ']';

                                    break;
                            }

                            $('#modalShowPassword').modal('hide');
                            $("#modalWaitDialog").modal('hide');

                            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                            $("#modalNotifyOnlyClose").modal('show');
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

                return false;
            });

            $('input[name="rdoClass"]:radio').on('change', function (e) {

                switch ($(this).val()) {
                    case "all":
                        $('.class-room').hide();
                        break;
                    case "room":
                        $('.class-room').show();

                        var sortClassCount = $('#sltSortClass').children('option').length;
                        if (sortClassCount == 0) {

                            LoadTermSubLevel2($("#sltSortLevel").val(), '#sltSortClass');

                        }

                        break;
                }

                return false;
            });

            $("#sltSortLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltSortClass');

            });

            $('#btnSaveSortStudentNo').click(function () {

                swal.fire({
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>',
                    html: `
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111088") %> <span style="font-weight: bold;"><%=HttpContext.Current.Session["Emp_Name"]%></span>
<br/><br/>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101258") %>
<br/><br/>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101259") %>
<br/><br/>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101260") %>`,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonClass: 'btn btn-success',
                    cancelButtonClass: 'btn btn-danger',
                    confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>',
                    cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                    buttonsStyling: false
                }).then(function (result) {
                    if (result.value) {
                        var terms = [];
                        if ($("#sltSortTerm").children("option:selected").val() == 'all') {
                            $("#sltSortTerm option").each(function () {
                                if ($(this).val() != 'all') {
                                    terms.push($(this).val());
                                }
                            });
                        }
                        else {
                            terms.push($("#sltSortTerm").children("option:selected").val());
                        }

                        var sortOption = {
                            "studentID": $('#chkStudentID').is(":checked"),
                            "sex": !!$('input[name=rdoSex]:checked').val() ? $('input[name=rdoSex]:checked').val() : null,
                            "moveIn": $('#chkMoveIn').is(":checked"),
                            "moveInAfterMonth6": $('#chkMoveInAfterMonth6').is(":checked"),
                            "level": $('input[name=rdoClass]:checked').val(),
                            "levelID": $("#sltSortLevel").children("option:selected").val(),
                            "roomID": $("#sltSortClass").children("option:selected").val(),
                            "terms": terms
                        }

                        $("#modalWaitDialog").modal('show');

                        $.ajax({
                            async: true,
                            type: "POST",
                            url: "StudentList.aspx/SaveSortStudentNo",
                            data: "{sortOption:" + JSON.stringify(sortOption) + "}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {

                                var r = JSON.parse(response.d);

                                var title = "";
                                var body = "";

                                if (r.success) {
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101261") %>';

                                    $('#modalSortStudentNo').modal('hide');
                                }
                                else {
                                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111076") %>[' + r.message + ']';
                                }


                                $("#modalWaitDialog").modal('hide');

                                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                                $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                                $("#modalNotifyOnlyClose").modal('show');
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
                    //if (result.dismiss === 'cancel') console.log('cancel');
                });

                return false;
            });

            $('#sltOriginYear').change(function () {
                // Get term of year
                SetTerm('#sltOriginTerm', $(this).val(), studentList.YearTermData);

                // New set year & term destination
                SetYearTermDestination('#sltDestinationYear', '#sltDestinationTerm', $('#sltOriginTerm').find(':selected').data('no'));
            });

            $('#sltOriginTerm').change(function () {
                // New set year & term destination
                SetYearTermDestination('#sltDestinationYear', '#sltDestinationTerm', $(this).find(':selected').data('no'));
            });

            $('#sltDestinationYear').change(function () {
                // Get term of year
                var originTermNo = $('#sltOriginTerm').find(':selected').data('no')
                var YearTermDataMoreThanNo = $.grep(studentList.YearTermData, function (o, i) { return o.no > originTermNo; });

                SetTerm('#sltDestinationTerm', $(this).val(), YearTermDataMoreThanNo);
            });

            $('#btnSaveCopyStudentNo').click(function () {
                swal.fire({
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>',
                    html: `
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111088") %> <span style="font-weight: bold;"><%=HttpContext.Current.Session["Emp_Name"]%></span>
<br/><br/>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101264") %>
<br/><br/>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101259") %>
<br/><br/>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101260") %>`,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonClass: 'btn btn-success',
                    cancelButtonClass: 'btn btn-danger',
                    confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>',
                    cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                    buttonsStyling: false
                }).then(function (result) {
                    if (result.value) {

                        // origin & destination term
                        var copyOption = {
                            "originYearId": $('#sltOriginYear').children("option:selected").val(),
                            "originYear": $('#sltOriginYear').children("option:selected").text(),
                            "originTermId": $('#sltOriginTerm').children("option:selected").val(),
                            "originTerm": $('#sltOriginTerm').children("option:selected").text(),
                            "destinationYearId": $('#sltDestinationYear').children("option:selected").val(),
                            "destinationYear": $("#sltDestinationYear").children("option:selected").text(),
                            "destinationTermId": $("#sltDestinationTerm").children("option:selected").val(),
                            "destinationTerm": $("#sltDestinationTerm").children("option:selected").text()
                        }

                        if (copyOption.destinationTermId != '-') {

                            $("#modalWaitDialog").modal('show');

                            $.ajax({
                                async: true,
                                type: "POST",
                                url: "StudentList.aspx/SaveCopyStudentNo",
                                data: "{copyStudentNo:" + JSON.stringify(copyOption) + "}",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {

                                    var r = JSON.parse(response.d);

                                    var title = "";
                                    var body = "";

                                    if (r.success) {
                                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101265") %> [<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %> ' + r.noRowUpdated + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>]';

                                        $('#modalCopyStudentNo').modal('hide');
                                    }
                                    else {
                                        title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                        body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111077") %>[' + r.message + ']';
                                    }

                                    $("#modalWaitDialog").modal('hide');

                                    $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                                    $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                                    $("#modalNotifyOnlyClose").modal('show');
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
                    //if (result.dismiss === 'cancel') console.log('cancel');
                });

                return false;
            });


            $("#sltTitleLevel").change(function () {
                if ($(this).val()) {
                    LoadTermSubLevel2($(this).val(), '#sltTitleClass');
                }
                else {
                    $('#sltTitleClass').html('<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>');
                    $('#sltTitleClass').selectpicker('refresh');
                }
            });

            $('#btnSaveManageStudentTitle').click(function () {

                var titleOption = {
                    "ageLower15": !!$('input[name=rdoTitleLower15]:checked').val() ? $('input[name=rdoTitleLower15]:checked').val() : null,
                    "ageOver15": $('#chkTitleOver15').is(":checked"),
                    "changeTitleAsSpecifiedDate": { specifiedDate: $('#iptTitleDate').val(), levelId: $('#sltTitleLevel').val(), level: $('#sltTitleLevel option:selected').text(), classroomId: $('#sltTitleClass').val(), classroom: $('#sltTitleClass option:selected').text() }
                }

                //if (!titleOption.changeTitleAsSpecifiedDate.specifiedDate) {
                //    swal.fire({
                //        type: 'warning',
                //        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>',
                //        text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M1001003") %>!'
                //    });

                //    return false;
                //}

                $("#modalWaitDialog").modal('show');

                $.ajax({
                    async: true,
                    type: "POST",
                    url: "StudentList.aspx/SaveManageStudentTitle",
                    data: "{titleOption:" + JSON.stringify(titleOption) + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "success":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101273") %>';

                                $('#modalManageStudentTitle').modal('hide');

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111078") %>';

                                break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
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
            });

            $('.studentList #iptStudentName').autoComplete({
                resolverSettings: {
                    fail: function () {
                        console.log('fail');
                    }
                },
                resolver: 'custom',
                events: {
                    search: function (qry, callback) {
                        var param = { keyword: qry, termID: $('.studentList #sltTerm').val() };
                        $.ajax({
                            url: "StudentList.aspx/GetStudentName",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                callback(data.d);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus);
                            }
                        });
                    }
                },
                minLength: 1
            });

            // Initial control
            $('.datepicker').datetimepicker({
                keepOpen: false,
                debug: false,
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });

            $(".datepicker").attr('maxlength', '10');

            // Datatable Section
            studentList.LoadListData();
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
