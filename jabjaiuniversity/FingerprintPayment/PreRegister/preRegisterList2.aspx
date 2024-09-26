<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="preRegisterList2.aspx.cs" Inherits="FingerprintPayment.PreRegister.preRegisterList2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style type="text/css">
        /*Validate*/
        .error {
            color: red;
        }

        /*Navbar*/
        #navbarExamResult.dropdown-toggle:after, #navbarLinks.dropdown-toggle:after {
            margin-right: 0px;
        }

        #navbarMenu .nav-item {
            padding: 7px 15px 7px 15px;
            /*color: #9c27b0;*/
        }

            #navbarMenu .nav-item a {
                color: inherit;
            }

        .must-checked-student.disabled {
            color: #995b00 !important;
        }

        .modal-title {
            margin-top: 0px;
        }

        #navStudentAction.navbar-default .navbar-nav > li > a {
            color: #555;
        }

        #navStudentAction.navbar-default .navbar-nav > .disabled > a, .navbar-default .navbar-nav > .disabled > a:hover, .navbar-default .navbar-nav > .disabled > a:focus {
            color: #ccc;
            background-color: transparent;
        }

        #navStudentAction ul.dropdown-menu li a {
            font-size: 24px;
        }

        .modal-body .table {
            margin-bottom: 0px;
        }

            .modal-body .table tr th {
                text-align: center;
            }

            .modal-body .table tr td {
                padding: 5px 0px 0px 0px;
            }

                .modal-body .table tr td:nth-child(1), .modal-body .table tr td:nth-child(3) {
                    text-align: center;
                    vertical-align: middle;
                }

                    .modal-body .table tr td:nth-child(3) a {
                        text-decoration: none;
                    }

                .modal-body .table tr td:nth-child(2) span {
                    font-size: 0.76em;
                    display: block;
                    margin: -4px 0px 5px 0px;
                }

        .fa.fa-download {
            vertical-align: middle;
        }

        .exam-results, .complete-documents {
            cursor: pointer;
        }

            .exam-results.pass, .complete-documents.pass {
                color: #23A818;
            }

            .exam-results.not-pass, .complete-documents.not-pass {
                color: red;
            }

            .exam-results.not-specified, .complete-documents.not-specified {
                color: gray;
                opacity: 0.7;
            }
            .exam-results.substitute {
                color: dodgerblue;
            }

        .error-message {
            font-size: 23px;
        }

        .bg-warning {
            background-color: #ff9800 !important;
        }

        #ttaCompleteDocumentsInfo::-webkit-input-placeholder {
            padding: 0px;
        }

        #ttaCompleteDocumentsInfo::-moz-input-placeholder {
            padding: 0px;
        }

        #ttaCompleteDocumentsInfo:-moz-input-placeholder {
            padding: 0px;
        }

        #ttaCompleteDocumentsInfo:-ms-input-placeholder {
            padding: 0px;
        }

        .exam-results-tooltip, .complete-documents-tooltip {
            text-align: left;
        }

            .exam-results-tooltip span, .complete-documents-tooltip span {
            }

        .tooltip-inner {
            max-width: inherit !important;
        }

        #formExamResults .filter-option-inner-inner, #formCompleteDocuments .filter-option-inner-inner {
            margin-top: -4px;
        }

        #formExamResults .bootstrap-select .btn.dropdown-toggle.select-with-transition, #formCompleteDocuments .bootstrap-select .btn.dropdown-toggle.select-with-transition {
            height: 30px;
        }

        #formExamResults .popup-form .div-row-padding, #formCompleteDocuments .popup-form .div-row-padding {
            padding: 0px 0px;
        }

        @media (min-width: 1200px) {
            .modal-lg {
                max-width: 1140px;
                width: auto;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %>
            </p>
        </div>
    </div>

    <div class="registerList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h4>
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
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                        <div class="col-sm-3">
                            <select id="sltRegisterStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103003") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103004") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103005") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01460") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                        <div class="col-sm-3">
                            <select id="sltOptionLevel" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                <asp:Literal ID="ltrOptionLevel" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %></label>
                        <div class="col-sm-3">
                            <select id="sltCourseType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103007") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103009") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row div-coutime-branch" style="display: none;">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132766") %></label>
                        <div class="col-sm-3">
                            <select id="sltCourseTime" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406026") %>">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132767") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132768") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132769") %></option>
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02951") %></label>
                        <div class="col-sm-3">
                            <input id="iptBranch" name="iptBranch" type="text" class="form-control" style="width: 100%;" />
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-sm-3">
                            <div class="form-group bmd-form-group">
                                <input id="iptStudentName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131239") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" />
                            </div>
                        </div>
                        <div class="col-sm-1 div-plan" style="display: none;"></div>
                        <label class="col-sm-1 col-form-label text-left div-plan" style="display: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></label>
                        <div class="col-sm-3 div-plan" style="display: none;">
                            <select id="sltPlan" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103219") %>">
                                <asp:Literal ID="ltrPlan" runat="server" />
                            </select>
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

    <div class="registerList row">
        <div class="col-md-12">
            <div class="card">
                <%--<div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></h4>
                </div>--%>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="material-datatables">
                        <div id="datatables_wrapper" class="dataTables_wrapper dt-bootstrap4">
                            <div class="row">
                                <div class="col-md-12">
                                    <nav class="navbar navbar-expand-lg navbar-light bg-light bg-warning">
                                        <a class="navbar-brand" href="#">
                                            <i class="material-icons" style="font-size: 36px; margin-top: -4px;">school</i>
                                        </a>
                                        <div class="collapse navbar-collapse" id="navbarMenu">
                                            <ul class="navbar-nav">
                                                <li class="nav-item"><a id="aGenerateExamSeatNo" class="must-checked-student disabled" href="#"><i class="material-icons">format_list_numbered</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103010") %></a></li>
                                                <li class="nav-item dropdown">
                                                    <a class="dropdown-toggle must-checked-student disabled" href="#" id="navbarExamResult" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="material-icons">playlist_add_check_circle</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103011") %></a>
                                                    <div class="dropdown-menu" aria-labelledby="navbarExamResult">
                                                        <a id="aPassExam" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103037") %></a>
                                                        <a id="aFailExam" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103038") %></a>
                                                        <a id="aSubstitute" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103023") %></a>
                                                        <a id="aPassExamSendInvoice" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103039") %></a>
                                                    </div>
                                                </li>
                                                <li class="nav-item"><a id="aRemoveMoreStudent" class="must-checked-student disabled" href="#"><i class="material-icons">delete</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></a></li>
                                                <li class="nav-item dropdown">
                                                    <a class="dropdown-toggle text-nowrap" href="#" id="navbarExport" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="material-icons">cloud_download</i> Export Excel</a>
                                                    <div class="dropdown-menu" aria-labelledby="navbarExport">
                                                        <a id="aExportStudentData" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103040") %></a>
                                                        <a id="aExportStudentDataWithBackupPlans" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103041") %></a>
                                                        <a id="aExportStudentAmount" class="dropdown-item" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103042") %></a>
                                                    </div>
                                                </li>
                                                <li class="nav-item"><a id="aMoveMoreStudent" class="must-checked-student disabled" href="#"><i class="material-icons">exit_to_app</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103012") %></a></li>
                                                <li class="nav-item dropdown">
                                                    <a class="dropdown-toggle" href="#" id="navbarLinks" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="material-icons">link</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103013") %></a>
                                                    <div class="dropdown-menu" aria-labelledby="navbarLinks">
                                                        <a href="#" class="dropdown-item" data-toggle="modal" data-target="#modalRegisterLink" onclick="return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103043") %></a>
                                                        <a href="#" class="dropdown-item" data-toggle="modal" data-target="#modalExamResultLink" onclick="return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103044") %></a>
                                                        <a href="#" class="dropdown-item" data-toggle="modal" data-target="#modalDocumentResultLink" onclick="return false;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103045") %></a>
                                                    </div>
                                                </li>
                                                <li class="nav-item"><a id="aSaveMoreCompleteDocuments" data-status="" class="must-checked-student disabled" href="#"><i class="material-icons">task</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103014") %></a></li>
                                            </ul>
                                        </div>
                                    </nav>
                                </div>
                            </div>
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
                                                <th style="width: 3%" class="text-center">
                                                    <div class="form-check">
                                                        <label class="form-check-label">
                                                            <input class="form-check-input check-all" type="checkbox">
                                                            <span class="form-check-sign">
                                                                <span class="check"></span>
                                                            </span>
                                                        </label>
                                                    </div>
                                                </th>
                                                <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th style="width: 12%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                                <th style="width: 8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103015") %></th>
                                                <th style="width: 7%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                                <th style="width: 8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></th>
                                                <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103017") %></th>
                                                <th style="width: 6%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103018") %></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103019") %></th>
                                                <th style="width: 9%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103020") %></th>
                                                <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103011") %></th>
                                                <th style="width: 8%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103014") %></th>
                                                <th style="width: 8%" class="text-center">
                                                    <a href="#" onclick="return RegisterStudent();" class="btn btn-success gvbutton" style="text-decoration: blink; padding: 12px 17px 12px 11px;"><i class="material-icons">add</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103021") %></a></th>
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

    <div class="modal fade" id="modalRegisterLink" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103162") %></h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="padding: 5px;">
                            <div class="col-md-12">
                                <asp:Label ID="lblRegisterLink" class="form-control" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding: 4px;">
                            <div class="col-md-12">
                                <label>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103163") %>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalExamResultLink" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103171") %></h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="padding: 5px;">
                            <div class="col-md-12">
                                <asp:Label ID="lblExamResultLink" class="form-control" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding: 4px;">
                            <div class="col-md-12">
                                <label>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103172") %>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDocumentResultLink" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103173") %></h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="padding: 5px;">
                            <div class="col-md-12">
                                <asp:Label ID="lblDocumentResultLink" class="form-control" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding: 4px;">
                            <div class="col-md-12">
                                <label>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103172") %>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalDelete" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %></h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <label class="col-md-4 mb-4 col-form-label text-right" for="iptDeleteStudentName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                        <div class="col-md-7 mb-7 div-text-input">
                            <input name="iptDeleteStudentName" type="text" id="iptDeleteStudentName" disabled="disabled" class="form-control" style="width: 80%;" />
                        </div>
                    </div>
                    <div class="row">
                        <label class="col-md-4 mb-4 col-form-label text-right" for="iptDeleteStudentID"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                        <div class="col-md-7 mb-7 div-text-input">
                            <input name="iptDeleteStudentID" type="text" id="iptDeleteStudentID" disabled="disabled" class="form-control" style="width: 80%;" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <input type="submit" id="btnDeleteStudent" name="btnDeleteStudent" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>" class="btn btn-primary global-btn" />
                    <button type="button" class="btn btn-default global-btn" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalUpdatePayment" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></h3>
                </div>
                <div class="modal-body">
                    <form id="formUpdatePayment">
                        <div class="popup-form">
                            <div class="row div-row-padding">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltPaymentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltPaymentStatus" name="sltPaymentStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>">
                                        <%--<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %></option>--%>
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103003") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103004") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103005") %></option>
                                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103012") %></option>
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding move-student-section">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="iptStudentIDPaymentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132788") %></label>
                                <div class="col-md-7 mb-7 div-text-input">
                                    <input name="iptStudentIDPaymentStatus" type="text" id="iptStudentIDPaymentStatus" class="form-control move-student" />
                                </div>
                            </div>
                            <div class="row div-row-padding move-student-section">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltOptionLevelPaymentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103160") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltOptionLevelPaymentStatus" name="sltOptionLevelPaymentStatus" class="selectpicker col-sm-12 move-student" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                        <asp:Literal ID="ltrOptionLevelPaymentStatus" runat="server" />
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding move-student-section">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltClassRoomPaymentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltClassRoomPaymentStatus" name="sltClassRoomPaymentStatus" class="selectpicker col-sm-12 move-student" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>">
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding move-student-section">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="iptMoveDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103161") %></label>
                                <div class="col-md-7 mb-7">
                                    <div class="form-group div-datepicker">
                                        <input id='iptMoveDate' type="text" class="form-control datepicker move-student" maxlength="10" value="<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row div-row-padding move-student-section">
                                <label class="col-md-4 mb-4 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                <div class="col-md-7">
                                    <p id="spnSchoolYearMoveOne" style="margin-top: 7px; font-size: 14px; margin-left: 5px; color: gray;"></p>
                                </div>
                            </div>
                            <div class="row div-row-padding move-student-section">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltTermMoveOne"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltTermMoveOne" name="sltTermMoveOne" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <div class="col-md-12">
                        <input type="submit" id="btnUpdatePayment" name="btnUpdatePayment" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" class="btn btn-primary" disabled style="width: 100px;" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalMoveMoreStudent" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103012") %></h3>
                </div>
                <div class="modal-body">
                    <form id="formMoveMoreStudent">
                        <div class="popup-form">
                            <div class="row div-row-padding">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltOptionLevelMoveMoreStudent"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103160") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltOptionLevelMoveMoreStudent" name="sltOptionLevelMoveMoreStudent" class="selectpicker col-sm-12 move-more-student" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                        <asp:Literal ID="ltrOptionLevelMoveMoreStudent" runat="server" />
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltClassRoomMoveMoreStudent"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltClassRoomMoveMoreStudent" name="sltClassRoomMoveMoreStudent" class="selectpicker col-sm-12 move-more-student" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>">
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="iptMoveMoreStudentDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103161") %></label>
                                <div class="col-md-7 mb-7">
                                    <div class="form-group div-datepicker">
                                        <input id='iptMoveMoreStudentDate' type="text" class="form-control datepicker move-more-student" maxlength="10" value="<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                        <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row div-row-padding">
                                <label class="col-md-4 mb-4 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                <div class="col-md-7">
                                    <p id="spnSchoolYearMoveMore" style="margin-top: 7px; font-size: 14px; margin-left: 5px; color: gray;"></p>
                                </div>
                            </div>
                            <div class="row div-row-padding">
                                <label class="col-md-4 mb-4 col-form-label text-right" for="sltTermMoveMore"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                <div class="col-md-7 mb-7 div-select-input">
                                    <select id="sltTermMoveMore" name="sltTermMoveMore" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <div class="col-md-12">
                        <input type="submit" id="btnSaveMoveMoreStudent" name="btnSaveMoveMoreStudent" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" class="btn btn-primary" style="width: 100px;" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bd-example-modal-lg" id="modalAttachFile" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103095") %></h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="padding: 5px;">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <th width="75%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103123") %></th>
                                        <th width="15%"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-12 text-right">
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalExamResults" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103011") %></h3>
                </div>
                <div class="modal-body">
                    <form id="formExamResults">
                        <div class="popup-form">
                            <div class="row div-row-padding">
                                <label class="col-md-4 col-form-label text-right text-nowrap" for="sltExamResultsStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %>:</label>
                                <div class="col-md-7 mb-1 div-select-input">
                                    <select id="sltExamResultsStatus" name="sltExamResultsStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>">
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %></option>
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103023") %></option>
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding update-by">
                                <label class="col-md-4 col-form-label text-right" for="spnExamResultsUpdateBy"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %>:</label>
                                <div class="col-md-7">
                                    <p id="spnExamResultsUpdateBy" style="margin-top: 6px; font-size: 14px; margin-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132812") %></p>
                                </div>
                            </div>
                            <div class="row div-row-padding update-date">
                                <label class="col-md-4 col-form-label text-right" for="spnExamResultsUpdateDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>:</label>
                                <div class="col-md-7">
                                    <p id="spnExamResultsUpdateDate" style="margin-top: 6px; font-size: 14px; margin-left: 5px;">21/01/2566</p>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <div class="col-md-12">
                        <input type="submit" id="btnSaveExamResults" name="btnSaveExamResults" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" class="btn btn-success" style="width: 100px;" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalCompleteDocuments" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103014") %></h3>
                </div>
                <div class="modal-body">
                    <form id="formCompleteDocuments">
                        <div class="popup-form">
                            <div class="row div-row-padding">
                                <label class="col-md-3 col-form-label text-right text-nowrap" for="sltCompleteDocumentsStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %>:</label>
                                <div class="col-md-9 mb-1 div-select-input">
                                    <select id="sltCompleteDocumentsStatus" name="sltCompleteDocumentsStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>">
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103028") %></option>
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103027") %></option>
                                    </select>
                                </div>
                            </div>
                            <div class="row div-row-padding not-complete-info">
                                <label class="col-md-3 col-form-label text-right" for="ttaCompleteDocumentsInfo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %>:</label>
                                <div class="col-md-9 mb-2">
                                    <textarea class="form-control" id="ttaCompleteDocumentsInfo" rows="7" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103168") %>" maxlength="1000"></textarea>
                                </div>
                            </div>
                            <div class="row div-row-padding update-by">
                                <label class="col-md-3 col-form-label text-right" for="spnCompleteDocumentsUpdateBy"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %>:</label>
                                <div class="col-md-9">
                                    <p id="spnCompleteDocumentsUpdateBy" style="margin-top: 6px; font-size: 14px; margin-left: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132812") %></p>
                                </div>
                            </div>
                            <div class="row div-row-padding update-date">
                                <label class="col-md-3 col-form-label text-right" for="spnCompleteDocumentsUpdateDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>:</label>
                                <div class="col-md-9">
                                    <p id="spnCompleteDocumentsUpdateDate" style="margin-top: 6px; font-size: 14px; margin-left: 5px;">21/01/2566</p>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <div class="col-md-12">
                        <input type="submit" id="btnSaveCompleteDocuments" name="btnSaveCompleteDocuments" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" class="btn btn-success" style="width: 100px;" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script type="text/javascript">

        var registerList = {
            ClickChangePaymentStatus: false,
            PageIndex: 0,
            PageSize: 100,
            PageCount: 0,
            TotalRows: 0,
            dt: null,
            LoadListData: function () {
                registerList.dt = $(".registerList #tableData").DataTable({
                    "processing": true,
                    "serverSide": true,
                    "info": false,
                    "searching": false,
                    "paging": false,
                    "stateSave": true,
                    "ajax": {
                        "url": "preRegisterList2.aspx/LoadRegister",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "data": function (d) {
                            d.year = $(".registerList #sltYear").children("option:selected").val();
                            d.regStatus = $(".registerList #sltRegisterStatus").children("option:selected").val();
                            d.optLevel = $(".registerList #sltOptionLevel").children("option:selected").val();
                            d.couType = $(".registerList #sltCourseType").children("option:selected").val();
                            d.couTime = $(".registerList #sltCourseTime").children("option:selected").val();
                            d.branch = $(".registerList #iptBranch").val();
                            d.stdName = $(".registerList #iptStudentName").val();
                            d.plan = $(".registerList #sltPlan").children("option:selected").val();
                            d.plan = (d.plan === undefined ? "" : d.plan);
                            d.page = registerList.PageIndex;
                            d.length = registerList.PageSize;

                            return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                        },
                        "dataSrc": function (json) {
                            var jsond = $.parseJSON(json.d);
                            //console.log(jsond);
                            return jsond.data;
                        }
                    },
                    "columns": [
                        { "data": "check", "orderable": false },
                        { "data": "no", "orderable": false },
                        { "data": "StudentName", "orderable": true },
                        { "data": "RegisterDate", "orderable": true },
                        { "data": "StudentCode", "orderable": true },
                        { "data": "LevelName", "orderable": true },
                        { "data": "PlanName", "orderable": true },
                        { "data": "ExamRoom", "orderable": true },
                        { "data": "ExamSeatNo", "orderable": true },
                        { "data": "Status", "orderable": true },
                        { "data": "MoveStatus", "orderable": true },//10
                        { "data": "ExamResults", "orderable": true },
                        { "data": "CompleteDocuments", "orderable": true },
                        { "data": "action", "orderable": false },
                        { "data": "rid", "orderable": false },
                        { "data": "schid", "orderable": false },
                        { "data": "ExamResultsUpdateBy", "orderable": false },
                        { "data": "ExamResultsUpdateDate", "orderable": false },
                        { "data": "CompleteDocumentsUpdateBy", "orderable": false },
                        { "data": "CompleteDocumentsUpdateDate", "orderable": false },
                        { "data": "CompleteDocumentsInfo", "orderable": false }
                    ],
                    "order": [[14, "desc"]],
                    "columnDefs": [
                        { className: "vertical-align-middle text-center", "targets": [0, 1, 13] },
                        { className: "text-center", "targets": [1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13] },
                        { "targets": [14, 15, 16, 17, 18, 19, 20], "visible": false },
                        {
                            "render": function (data, type, row) {
                                // row.rid
                                return `<div class="form-check">
                                            <label class="form-check-label">
                                                <input class="form-check-input check-one" type="checkbox">
                                                <span class="form-check-sign">
                                                    <span class="check"></span>
                                                </span>
                                            </label>
                                        </div>`;
                            },
                            "targets": 0
                        },
                        {
                            "render": function (data, type, row) {
                                return '<div class="btn-group">' +
                                    '<button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 12px 10px 12px 7px;">' +
                                    '<i class="material-icons">dvr</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103029") %><span class="caret"></span>' +
                                    '</button>' +
                                    '<ul class="dropdown-menu pull-right">' +
                                    '<li><a class="pre-attach-file" href="#" onclick="return false;" data-toggle="modal" data-target="#modalAttachFile"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103030") %></a></li>' +
                                    '<li><a class="pre-edit" target="_blank" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103031") %></a></li>' +
                                    '<li><a class="pre-chg-status" href="#" onclick="return false;" data-toggle="modal" data-target="#modalUpdatePayment"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103164") %></a></li>' +
                                    '<li><a class="send-email" target="_blank" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103033") %></a></li>' +
                                    '<li><a class="pre-print" target="_blank" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103034") %></a></li>' +
                                    '<li><a class="pre-print2" target="_blank" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103035") %></a></li>' +
                                    //'<li><a class="pre-print3" target="_blank" href="#"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132813") %></a></li>' +
                                    '<li><a class="pre-del" href="#" onclick="return false;" data-toggle="modal" data-target="#modalDelete"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103036") %></a></li>' +
                                    '</ul>' +
                                    '</div>';
                            },
                            "targets": 13
                        },
                        {
                            "render": function (data, type, row) {
                                //console.log(data, type, row);
                                var examResults = '';
                                switch (data) {
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>': examResults = "<span class='exam-results pass' data-rid='" + row.rid + "' data-status='1' data-update-by='" + row.ExamResultsUpdateBy + "' data-update-date='" + row.ExamResultsUpdateDate + "' data-toggle='tooltip' data-html='true' title='<div class=\"exam-results-tooltip\"><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %></span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %></b> " + row.ExamResultsUpdateBy + "</span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></b> " + row.ExamResultsUpdateDate + "</span></div>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %></span>"; break;
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>': examResults = "<span class='exam-results not-pass' data-rid='" + row.rid + "' data-status='0' data-update-by='" + row.ExamResultsUpdateBy + "' data-update-date='" + row.ExamResultsUpdateDate + "' data-toggle='tooltip' data-html='true' title='<div class=\"exam-results-tooltip\"><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %></span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %></b> " + row.ExamResultsUpdateBy + "</span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></b> " + row.ExamResultsUpdateDate + "</span></div>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %></span>"; break;
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103023") %>': examResults = "<span class='exam-results substitute' data-rid='" + row.rid + "' data-status='2' data-update-by='" + row.ExamResultsUpdateBy + "' data-update-date='" + row.ExamResultsUpdateDate + "' data-toggle='tooltip' data-html='true' title='<div class=\"exam-results-tooltip\"><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103023") %></span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %></b> " + row.ExamResultsUpdateBy + "</span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></b> " + row.ExamResultsUpdateDate + "</span></div>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103023") %></span>"; break;
                                    default: examResults = "<span class='exam-results not-specified' data-rid='" + row.rid + "' data-status='' data-update-by='" + row.ExamResultsUpdateBy + "' data-update-date='" + row.ExamResultsUpdateDate + "'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></span>"; break;
                                }
                                return examResults;
                            },
                            "targets": 11
                        },
                        {
                            "render": function (data, type, row) {
                                //console.log(data, type, row);
                                var completeDocuments = '';
                                switch (data) {
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103028") %>': completeDocuments = "<span class='complete-documents pass' data-rid='" + row.rid + "' data-status='1' data-update-by='" + row.CompleteDocumentsUpdateBy + "' data-update-date='" + row.CompleteDocumentsUpdateDate + "' data-toggle='tooltip' data-html='true' title='<div class=\"complete-documents-tooltip\"><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103028") %></span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %></b> " + row.CompleteDocumentsUpdateBy + "</span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></b> " + row.CompleteDocumentsUpdateDate + "</span></div>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103028") %></span>"; break;
                                    case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103027") %>': completeDocuments = "<span class='complete-documents not-pass' data-rid='" + row.rid + "' data-status='0' data-update-by='" + row.CompleteDocumentsUpdateBy + "' data-update-date='" + row.CompleteDocumentsUpdateDate + "' data-not-complete-info='" + row.CompleteDocumentsInfo + "' data-toggle='tooltip' data-html='true' title='<div class=\"complete-documents-tooltip\"><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></b> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103027") %></span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></b> " + row.CompleteDocumentsInfo?.replace(/(?:\r\n|\r|\n)/g, '<br>') + "</span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %></b> " + row.CompleteDocumentsUpdateBy + "</span> <br><span><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></b> " + row.CompleteDocumentsUpdateDate + "</span></div>'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103027") %></span>"; break;
                                    default: completeDocuments = "<span class='complete-documents not-specified' data-rid='" + row.rid + "' data-status='' data-update-by='" + row.CompleteDocumentsUpdateBy + "' data-update-date='" + row.CompleteDocumentsUpdateDate + "'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103026") %></span>"; break;
                                }
                                return completeDocuments;
                            },
                            "targets": 12
                        }
                    ],
                    "drawCallback": function (settings) {
                        //var json = settings.json;
                        var json = $.parseJSON(settings.json.d);
                        //console.log(json);

                        registerList.PageCount = json.pageCount;
                        registerList.TotalRows = json.recordsTotal;

                        var pageLRSize = 3;
                        var previous = '<li class="paginate_button page-item previous" id="datatables_previous"><a href="#" aria-controls="datatables" data-dt-idx="100" tabindex="0" class="page-link">Prev</a></li>';
                        var previousDot = '';
                        var next = '<li class="paginate_button page-item next" id="datatables_next"><a href="#" aria-controls="datatables" data-dt-idx="101" tabindex="0" class="page-link">Next</a></li>';
                        var nextDot = '';
                        var elements = '';

                        if (registerList.PageIndex - pageLRSize > 1) {
                            previousDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="102" tabindex="0" class="page-link">…</a></li>';
                        }

                        if (registerList.PageIndex + pageLRSize < json.pageCount - 1) {
                            nextDot = '<li class="paginate_button page-item disabled" id="tableData_ellipsis"><a href="#" aria-controls="tableData" data-dt-idx="103" tabindex="0" class="page-link">…</a></li>';
                        }

                        for (var pi = 0; pi < json.pageCount; pi++) {
                            if (pi == 0) {
                                elements += '<li class="paginate_button page-item ' + (registerList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                                elements += previousDot;
                            }
                            else if (pi == json.pageCount - 1) {
                                elements += nextDot;
                                elements += '<li class="paginate_button page-item ' + (registerList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                            else if (registerList.PageIndex - pageLRSize <= pi && registerList.PageIndex + pageLRSize >= pi) {
                                elements += '<li class="paginate_button page-item ' + (registerList.PageIndex == pi ? 'active' : '') + '"><a href="#" aria-controls="datatables" data-dt-idx="' + pi + '" tabindex="0" class="page-link">' + (pi + 1) + '</a></li>';
                            }
                        }

                        $('.pagination').html(previous + elements + next);

                        $('.dataTables_info').html('Showing ' + ((registerList.PageIndex * registerList.PageSize) + 1) + ' to ' + ((registerList.PageIndex * registerList.PageSize) + registerList.PageSize) + ' of ' + registerList.TotalRows + ' rows');

                        $('.exam-results[data-toggle="tooltip"], .complete-documents[data-toggle="tooltip"]').tooltip();
                    },
                    "initComplete": function (settings, json) {
                        $('.exam-results[data-toggle="tooltip"], .complete-documents[data-toggle="tooltip"]').tooltip();
                    }
                });

                // order.dt search.dt
                registerList.dt.on('draw.dt', function () {
                    registerList.dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var rid = registerList.dt.cells({ row: i, column: 14 }).data()[0];
                        $(cell).find(".check-one").attr("rid", rid);
                    });
                    registerList.dt.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = (registerList.PageIndex * registerList.PageSize) + (i + 1) + '.';
                    });
                    registerList.dt.column(13, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        var sname = registerList.dt.cells({ row: i, column: 2 }).data()[0];
                        var sid = registerList.dt.cells({ row: i, column: 4 }).data()[0];
                        var rid = registerList.dt.cells({ row: i, column: 14 }).data()[0];
                        var schid = registerList.dt.cells({ row: i, column: 15 }).data()[0];
                        $(cell).find(".pre-attach-file").attr("onclick", "ListAttachFile('" + rid + "')");
                        $(cell).find(".pre-edit").attr("href", "/preRegister/preRegisterEdit2.aspx?id=" + rid);
                        $(cell).find(".pre-chg-status").attr("onclick", "UpdatePaymentStatus('" + sid + "', '" + rid + "')");
                        $(cell).find(".send-email").attr("href", "/preRegister/RegisterOnlineSendEmail.aspx?id=" + rid);
                        $(cell).find(".pre-print").attr("href", "/preRegister/registerPaper.aspx?id=" + rid + "&idSchool=" + schid + "&mode=1");
                        $(cell).find(".pre-print2").attr("href", "/preRegister/registerPaper.aspx?id=" + rid + "&idSchool=" + schid + "&mode=2");
                        //$(cell).find(".pre-print3").attr("href", "/preRegister/registerPaperHistory.aspx?id=" + rid + "&idSchool=" + schid + "");
                        $(cell).find(".pre-del").attr("onclick", "ShowConfirmDelete('" + sid + "', '" + sname + "', '" + rid + "')");
                    });
                });
            },
            ReloadListData: function () {
                registerList.dt.draw();

                $('.must-checked-student').addClass("disabled");
            }
        }

        function LoadPlan(subLevelID) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "preRegisterList2.aspx/LoadPlan",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessLoadPlan,
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }

        function OnSuccessLoadPlan(response) {
            var plans = response.d;

            $('#sltPlan').empty();

            if (plans.length > 0) {

                var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                $(plans).each(function () {

                    options += '<option value="' + this.ID + '">' + this.Planname + '</option>';

                });
                $('#sltPlan').html(options);
                $('#sltPlan').selectpicker('refresh');

                $('.div-plan').show();
            }
            else {
                $('.div-plan').hide();
            }
        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "preRegisterList2.aspx/LoadTermSubLevel2",
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

        function DeleteStudentRegister(registerID) {
            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/DeleteStudentRegister",
                data: '{registerID: ' + registerID + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDeleteStudentRegister,
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

        function DeleteMoreStudentRegister(registerIDs) {

            var data = {
                registerIDs: registerIDs
            };

            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/DeleteMoreStudentRegister",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessDeleteStudentRegister,
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

        function OnSuccessDeleteStudentRegister(response) {
            var title = "";
            var body = "";

            switch (response.d) {
                case "complete":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101245") %>';

                    registerList.ReloadListData();
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

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalDelete').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }


        function GenerateExamSeatNo(registerIDs) {
            var data = {
                registerIDs: registerIDs
            };

            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/GenerateExamSeatNo",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessGenerateExamSeatNo,
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

        function OnSuccessGenerateExamSeatNo(response) {
            var title = "";
            var body = "";

            var r = JSON.parse(response.d);
            if (r.success) {

                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132789") %>';
                if (r.failCount > 0) {
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132790") %>: ' + r.successCount + ', <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01407") %>: ' + r.failCount + '<br />Error Message: <div class="error-message">' + r.errorMessage + '</div>';
                }

                registerList.ReloadListData();
            }
            else {
                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132791") %>: ' + r.message;
            }

            $("#modalWaitDialog").modal('hide');

            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
            $("#modalNotifyOnlyClose").find('.modal-body p').html(body);

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalDelete').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }

        function SaveExamResultStudent(registerIDs, examResult) {

            var data = {
                registerIDs: registerIDs,
                examResult: examResult
            };

            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/SaveExamResultStudent",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessSaveExamResultStudent,
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

        function OnSuccessSaveExamResultStudent(response) {
            var title = "";
            var body = "";

            switch (response.d) {
                case "complete":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132792") %>';

                    registerList.ReloadListData();
                    break;
                case "warning":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132793") %>';

                    break;
                case "error":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132794") %>';

                    break;
            }

            $("#modalWaitDialog").modal('hide');

            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalExamResults').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }

        function ShowConfirmDelete(studentID, studentName, registerID) {
            $('#iptDeleteStudentName').val(studentName);
            $('#iptDeleteStudentID').val(studentID);
            $('#btnDeleteStudent').attr('data-rid', registerID);
        }

        function UpdatePaymentStatus(studentID, registerID) {
            $('#iptStudentIDPaymentStatus').val(studentID);
            $('#btnUpdatePayment').attr('data-rid', registerID).attr('data-sid', studentID);

            // Get SchoolYear
            GetSchoolYear($("#iptMoveDate").val(), '#spnSchoolYearMoveOne', '#sltTermMoveOne');
        }

        function ListAttachFile(registerID) {
            // List Attach File
            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/ListAttachFile",
                data: '{registerID: ' + registerID + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var r = JSON.parse(response.d);
                    //console.log(r);
                    var dataRows = '';
                    $.each(r.data, function (i, obj) {
                        dataRows += `<tr>
                                        <td>`+ obj.no + `.</td>
                                        <td>`+ obj.fieldName + `<br />
                                            <span>(`+ obj.fieldNameEn + `)</span></td>
                                        <td>`+ (obj.filePath === null ? `` : `<a href="` + obj.filePath + `"><i class="fa fa-download" aria-hidden="true"></i>&nbsp;Download</a>`) + `</td>
                                    </tr>`;
                    });
                    $('#modalAttachFile table tbody').html(dataRows);
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

        function SavePaymentStatus(registerID, paymentStatusID) {
            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/SavePaymentStatus",
                data: '{registerID: ' + registerID + ', paymentStatusID: ' + paymentStatusID + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessSavePaymentStatus,
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

        function OnSuccessSavePaymentStatus(response) {
            var title = "";
            var body = "";

            switch (response.d) {
                case "complete":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132795") %>';

                    registerList.ReloadListData();
                    break;
                case "warning":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132796") %>';

                    break;
                case "error":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132797") %>';

                    break;
            }

            $("#modalWaitDialog").modal('hide');

            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalUpdatePayment').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }

        function MoveStudent(registerID, level2ID, dateMove, schoolYear, studentID, term) {
            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/MoveStudent",
                data: "{registerID: " + registerID + ", level2ID: " + level2ID + ", dateMove: '" + dateMove + "', schoolYear: " + schoolYear + ", studentID: '" + studentID + "', termID: '" + term + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessMoveStudent,
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

        function OnSuccessMoveStudent(response) {
            var title = "";
            var body = "";

            var r = JSON.parse(response.d); //console.log(r);
            if (r.success) {
                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132798") %>';

                registerList.ReloadListData();
            }
            else {
                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132799") %>: ' + r.message;
            }


            $("#modalWaitDialog").modal('hide');

            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalUpdatePayment').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }

        function MoveMoreStudent(registerIDs, level2ID, dateMove, schoolYear, term) {
            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/MoveMoreStudent",
                data: JSON.stringify({ "registerIDs": registerIDs, "level2ID": level2ID, "dateMove": dateMove, "schoolYear": schoolYear, "termID": term }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessMoveMoreStudent,
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

        function OnSuccessMoveMoreStudent(response) {
            var title = "";
            var body = "";

            var r = JSON.parse(response.d);
            if (r.success) {
                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132798") %>';

                registerList.ReloadListData();
            }
            else {
                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132799") %>:' + r.message;
            }


            $("#modalWaitDialog").modal('hide');

            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
            $("#modalNotifyOnlyClose").find('.modal-body p').html(body);

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalUpdatePayment').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }

        function GetSchoolYear(dateMove, objYear, objTerm) {

            var data = {
                dateMove: dateMove
            };

            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/GetSchoolYear",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var res = $.parseJSON(response.d);
                    //console.log(res);
                    $(objTerm).empty();
                    switch (res.result) {
                        case "success":
                            //$(objTerm).empty();
                            $.each(res.data.terms, function (i, termObj) {
                                //if (res.data.cTermID == termObj.termID) {
                                //    $(objTerm).append('<option value="' + termObj.termID + '">' + termObj.term + '</option>');
                                //}
                                //else {
                                //    $(objTerm).append('<option value="' + termObj.termID + '">' + termObj.term + '</option>');
                                //}
                                $(objTerm).append('<option value="' + termObj.termID + '">' + termObj.term + '</option>');
                            });
                            $(objTerm).append('<option value="0">ทุกเทอม</option>');
                            $(objTerm).selectpicker('refresh');

                            $(objYear).html(res.data.year);
                            $(objYear).attr('data-year', res.data.yearID);
                            break;
                        case "warning":
                            $(objYear).css("color", "orange");
                            $(objYear).text('Warning: ' + res.message);
                            $(objYear).attr('data-year', '');
                            break;
                        case "error":
                            $(objYear).css("color", "red");
                            $(objYear).text('Error: ' + res.message);
                            $(objYear).attr('data-year', '');
                            break;
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
        }

        function RegisterStudent() {
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
            //                window.open("/preRegister/preRegisterAddUser2.aspx", "_blank").focus();
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

            window.open("/preRegister/preRegisterAddUser2.aspx", "_blank").focus();

            return false;
        }

        function SaveCompleteDocuments(registerIDs, completeResult, notCompleteInfo) {

            var data = {
                registerIDs: registerIDs,
                completeResult: completeResult,
                notCompleteInfo: notCompleteInfo
            };

            $.ajax({
                async: false,
                type: "POST",
                url: "preRegisterList2.aspx/SaveCompleteDocuments",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessSaveCompleteDocuments,
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

        function OnSuccessSaveCompleteDocuments(response) {
            var title = "";
            var body = "";

            switch (response.d) {
                case "complete":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103169") %>';

                    registerList.ReloadListData();
                    break;
                case "warning":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132800") %>';

                    break;
                case "error":
                    title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                    body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132801") %>';

                    break;
            }

            $("#modalWaitDialog").modal('hide');

            $("#modalNotifyOnlyClose").find('.modal-title').text(title);
            $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

            $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalCompleteDocuments').css('z-index')) + 1);
            $("#modalNotifyOnlyClose").modal('show');
        }


        $(document).ready(function () {

            // Searching, Pagination event 
            $('.registerList #btnSearch').click(function () {

                registerList.PageIndex = 0;

                registerList.ReloadListData();

                return false;
            });

            $('.registerList #datatables_length').change(function () {

                registerList.PageSize = parseInt($("#datatables_length").children("option:selected").val());
                registerList.PageIndex = 0;

                registerList.ReloadListData();

                return false;
            });

            $('.registerList ul.pagination').on('click', 'li.paginate_button a', function () {

                var pi = parseInt($(this).attr("data-dt-idx"));

                if (pi == 100) {
                    if (registerList.PageIndex > 0) {
                        registerList.PageIndex--;
                        registerList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + registerList.PageIndex + ']').addClass('active');
                    }
                }
                else if (pi == 101) {
                    if (registerList.PageIndex < (registerList.PageCount - 1)) {
                        registerList.PageIndex++;
                        registerList.ReloadListData();

                        $('.pagination .paginate_button.page-item.active').removeClass('active');
                        $('.pagination .paginate_button.page-item a[data-dt-idx=' + registerList.PageIndex + ']').addClass('active');
                    }
                }
                else {
                    registerList.PageIndex = parseInt($(this).attr("data-dt-idx"));
                    registerList.ReloadListData();

                    $('.pagination .paginate_button.page-item.active').removeClass('active');
                    $(this).addClass('active');
                }

                return false;
            });

            $('.registerList #tableData #datatables_previous').click(function () {

                if (registerList.PageIndex > 0) {
                    registerList.PageIndex--;
                    registerList.ReloadListData();
                }

                return false;
            });

            $('.registerList #tableData #datatables_next').click(function () {

                if (registerList.PageIndex < (registerList.PageCount - 1)) {
                    registerList.PageIndex++;
                    registerList.ReloadListData();
                }

                return false;
            });

            $('.check-all').click(function () {
                $(".check-one").prop('checked', $(this).prop('checked'));

                // Enable or Disable when checked student
                if ($(".check-one:checked").length) $('.must-checked-student').removeClass("disabled"); else $('.must-checked-student').addClass("disabled");
            });

            $('#tableData').on('change', 'tbody input.check-one', function () {
                $(".check-all").prop('checked', ($('.check-one').length == $('.check-one').filter(":checked").length));

                // Enable or Disable when checked student
                if ($(".check-one:checked").length) $('.must-checked-student').removeClass("disabled"); else $('.must-checked-student').addClass("disabled");
            });

            $("#sltOptionLevel").change(function () {

                //var subLevel = $('option:selected', this).attr('data-level');
                var subLevel = $('option:selected', this).attr('data-level-name');
                if (subLevel == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>" || subLevel == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %>") {
                    // show CourseTime, Branch
                    $('.div-coutime-branch').show();
                } else {
                    // hide CourseTime, Branch
                    $('.div-coutime-branch').hide();
                }

                if ($("#sltOptionLevel").val() != "") {
                    // show plan
                    $('.div-plan').show();
                    LoadPlan($("#sltOptionLevel").val());
                }
                else {
                    // hide plan
                    $('.div-plan').hide();
                }

            });

            $('.registerList #iptStudentName').autoComplete({
                resolverSettings: {
                    fail: function () {
                        console.log('fail');
                    }
                },
                resolver: 'custom',
                events: {
                    search: function (qry, callback) {
                        var param = { keyword: qry };
                        $.ajax({
                            url: "preRegisterList2.aspx/GetStudentName",
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

            // Export File
            $('.registerList #btnExport').click(function () {

                //var dt = $(".registerList #tableData").DataTable();
                var order = registerList.dt.order();

                var year = $(".registerList #sltYear").children("option:selected").val();
                var regStatus = $(".registerList #sltRegisterStatus").children("option:selected").val();
                var optLevel = $(".registerList #sltOptionLevel").children("option:selected").val();
                var couType = $(".registerList #sltCourseType").children("option:selected").val();
                var couTime = $(".registerList #sltCourseTime").children("option:selected").val();
                var branch = $(".registerList #iptBranch").val();
                var stdName = $(".registerList #iptStudentName").val();
                var plan = $(".registerList #sltPlan").children("option:selected").val();
                plan = (plan === undefined ? "" : plan);

                window.open("Ashx/ExportPreRegisterExcel.ashx?year=" + year + "&regStatus=" + regStatus + "&optLevel=" + optLevel + "&couType=" + couType + "&couTime=" + couTime + "&branch=" + branch + "&stdName=" + stdName + "&plan=" + plan + "&col=" + order[0][0] + "&dir=" + order[0][1]);

                return false;
            });
            $('.registerList #aExportStudentData').click(function () {

                var order = registerList.dt.order();

                var year = $(".registerList #sltYear").children("option:selected").val();
                if (!year) year = $(".registerList #sltYear").find('option:not(:empty)').first().val();
                var regStatus = $(".registerList #sltRegisterStatus").children("option:selected").val();
                var optLevel = $(".registerList #sltOptionLevel").children("option:selected").val();
                var couType = $(".registerList #sltCourseType").children("option:selected").val();
                var couTime = $(".registerList #sltCourseTime").children("option:selected").val();
                var branch = $(".registerList #iptBranch").val();
                var stdName = $(".registerList #iptStudentName").val();
                var plan = $(".registerList #sltPlan").children("option:selected").val();
                plan = (plan === undefined ? "" : plan);

                window.open("Ashx/ExportPreRegisterExcel.ashx?year=" + year + "&regStatus=" + regStatus + "&optLevel=" + optLevel + "&couType=" + couType + "&couTime=" + couTime + "&branch=" + branch + "&stdName=" + stdName + "&plan=" + plan + "&col=" + order[0][0] + "&dir=" + order[0][1]);

                return false;
            });
            $('.registerList #aExportStudentDataWithBackupPlans').click(function () {

                var order = registerList.dt.order();

                var year = $(".registerList #sltYear").children("option:selected").val();
                var regStatus = $(".registerList #sltRegisterStatus").children("option:selected").val();
                var optLevel = $(".registerList #sltOptionLevel").children("option:selected").val();
                var couType = $(".registerList #sltCourseType").children("option:selected").val();
                var couTime = $(".registerList #sltCourseTime").children("option:selected").val();
                var branch = $(".registerList #iptBranch").val();
                var stdName = $(".registerList #iptStudentName").val();
                var plan = $(".registerList #sltPlan").children("option:selected").val();
                plan = (plan === undefined ? "" : plan);

                window.open("Ashx/ExportPreRegisterBackupPlansExcel.ashx?year=" + year + "&regStatus=" + regStatus + "&optLevel=" + optLevel + "&couType=" + couType + "&couTime=" + couTime + "&branch=" + branch + "&stdName=" + stdName + "&plan=" + plan + "&col=" + order[0][0] + "&dir=" + order[0][1]);

                return false;
            });

            $('.registerList #aExportStudentAmount').click(function () {

                var year = $(".registerList #sltYear").children("option:selected").val();
                var regStatus = $(".registerList #sltRegisterStatus").children("option:selected").val();
                var optLevel = $(".registerList #sltOptionLevel").children("option:selected").val();
                var couType = $(".registerList #sltCourseType").children("option:selected").val();
                var couTime = $(".registerList #sltCourseTime").children("option:selected").val();
                var branch = $(".registerList #iptBranch").val();
                var stdName = $(".registerList #iptStudentName").val();
                var plan = $(".registerList #sltPlan").children("option:selected").val();
                plan = (plan === undefined ? "" : plan);

                window.open("Ashx/ExportPreRegisterExcelStudentAmount.ashx?year=" + year + "&regStatus=" + regStatus + "&optLevel=" + optLevel + "&couType=" + couType + "&couTime=" + couTime + "&branch=" + branch + "&stdName=" + stdName + "&plan=" + plan);

                return false;
            });

            // List Action
            $('.registerList #btnMoveMoreStudent').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    $("#modalMoveMoreStudent").modal('show');

                    // Get SchoolYear
                    GetSchoolYear($("#iptMoveMoreStudentDate").val(), '#spnSchoolYearMoveMore', '#sltTermMoveMore');
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132802") %></h3>' });
                }

                return false;
            });
            $('.registerList #aMoveMoreStudent').click(function () {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/App_Logic/StudentLimitInContact.ashx",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //console.log(response);
                        var rids = [];
                        $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                            rids.push($(this).attr('rid'));
                        });

                        if (response.success) {
                            if (response.data.limitInContact > 0 && response.data.remainingNumber > 0 && (response.data.remainingNumber - rids.length) >= 0) {
                                if (rids.length > 0) {
                                    $("#modalMoveMoreStudent").modal('show');

                                    // Get SchoolYear
                                    GetSchoolYear($("#iptMoveMoreStudentDate").val(), '#spnSchoolYearMoveMore', '#sltTermMoveMore');
                                } else {
                                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132802") %></h3>' });
                                }
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
            });

            $('#btnSaveMoveMoreStudent').click(function () {

                if ($("#formMoveMoreStudent").valid()) {

                    var rids = [];
                    $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                        rids.push(parseInt($(this).attr('rid')));
                    });

                    if (rids.length > 0) {

                        var level2ID = $("#sltClassRoomMoveMoreStudent").val();
                        var dateMove = $("#iptMoveMoreStudentDate").val();
                        var schoolYear = $('#spnSchoolYearMoveMore').attr('data-year');
                        var term = $('#sltTermMoveMore').val();
                        if (!schoolYear) schoolYear = 0;

                        if (schoolYear != 0 && term !== null) {
                            $('#modalWaitDialog').css('z-index', parseInt($('#modalMoveMoreStudent').css('z-index')) + 1);
                            $("#modalWaitDialog").modal('show');

                            MoveMoreStudent(rids, level2ID, dateMove, schoolYear, term);
                        }
                        else {
                            swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3 style="font-size: 1.3625rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132814") %></h3>' });
                        }
                    } else {
                        swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132802") %></h3>' });
                    }
                }

                return false;
            });

            $('.registerList #aPassExam').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    SaveExamResultStudent(rids, '1');
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132803") %></h3>' });
                }

                return false;
            });
            $('.registerList #aPassExamSendInvoice').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    SaveExamResultStudent(rids, '3');
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132803") %></h3>' });
                }

                return false;
            });

            $('.registerList #aFailExam').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    SaveExamResultStudent(rids, '0');
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3 style="font-size: 1.4625rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132804") %></h3>' });
                }

                return false;
            });
            $('.registerList #aSubstitute').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    SaveExamResultStudent(rids, '2');
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3 style="font-size: 1.4625rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132805") %></h3>' });
                }

                return false;
            });
            $('.registerList #aSaveMoreCompleteDocuments').click(function () {

                $('#modalCompleteDocuments').modal('show');

                $('#sltCompleteDocumentsStatus').selectpicker('val', $(this).data('status')).trigger('change');
                $('#btnSaveCompleteDocuments').data('rid', 0).prop('disabled', true);

                $("#sltCompleteDocumentsStatus").val('default');
                $("#sltCompleteDocumentsStatus").selectpicker("refresh");
                $('#ttaCompleteDocumentsInfo').val('');
                $('#spnCompleteDocumentsUpdateBy').text('<%=(string)Session["Emp_Name"]%>');
                $('#spnCompleteDocumentsUpdateDate').text('<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>');

                return false;
            });

            // Record Action
            $('#btnDeleteStudent').click(function () {
                var rid = $(this).attr('data-rid');

                $('#modalWaitDialog').css('z-index', parseInt($('#modalDelete').css('z-index')) + 1);
                $("#modalWaitDialog").modal('show');

                DeleteStudentRegister(rid);
            });
            $('.registerList #aRemoveMoreStudent').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    DeleteMoreStudentRegister(rids);
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132806") %></h3>' });
                }

                return false;
            });
            $('.registerList #aGenerateExamSeatNo').click(function () {

                var rids = [];
                $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                    rids.push($(this).attr('rid'));
                });

                if (rids.length > 0) {
                    GenerateExamSeatNo(rids);
                } else {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132807") %></h3>' });
                }

                return false;
            });

            $('#tableData').on('click', 'span.exam-results', function () {

                $('#modalExamResults').modal('show');

                $('#sltExamResultsStatus').selectpicker('val', $(this).data('status')).trigger('change');
                $('#btnSaveExamResults').data('rid', $(this).data('rid')).prop('disabled', true);

                switch ($(this).data('status')) {
                    case 1:
                    case 0:
                    case 2:
                        $('#spnExamResultsUpdateBy').text($(this).data('update-by') ?? '');
                        $('#spnExamResultsUpdateDate').text($(this).data('update-date') ?? '');
                        break;
                    default:
                        $("#sltExamResultsStatus").val('default');
                        $("#sltExamResultsStatus").selectpicker("refresh");
                        $('#spnExamResultsUpdateBy').text('<%=(string)Session["Emp_Name"]%>');
                        $('#spnExamResultsUpdateDate').text('<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>');
                        break;
                }

                return false;
            });

            $('#tableData').on('click', 'span.complete-documents', function () {

                $('#modalCompleteDocuments').modal('show');

                $('#sltCompleteDocumentsStatus').selectpicker('val', $(this).data('status')).trigger('change');
                $('#btnSaveCompleteDocuments').data('rid', $(this).data('rid')).prop('disabled', true);

                switch ($(this).data('status')) {
                    case 1:
                        $('#spnCompleteDocumentsUpdateBy').text($(this).data('update-by') ?? '');
                        $('#spnCompleteDocumentsUpdateDate').text($(this).data('update-date') ?? '');
                        break;
                    case 0:
                        $('#ttaCompleteDocumentsInfo').val($(this).data('not-complete-info') ?? '');
                        $('#spnCompleteDocumentsUpdateBy').text($(this).data('update-by') ?? '');
                        $('#spnCompleteDocumentsUpdateDate').text($(this).data('update-date') ?? '');
                        break;
                    default:
                        $("#sltCompleteDocumentsStatus").val('default');
                        $("#sltCompleteDocumentsStatus").selectpicker("refresh");
                        $('#ttaCompleteDocumentsInfo').val('');
                        $('#spnCompleteDocumentsUpdateBy').text('<%=(string)Session["Emp_Name"]%>');
                        $('#spnCompleteDocumentsUpdateDate').text('<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>');
                        break;
                }

                return false;
            });

            // Modal Form
            $("#sltOptionLevelPaymentStatus").change(function () {

                LoadTermSubLevel2($("#sltOptionLevelPaymentStatus").val(), '#sltClassRoomPaymentStatus');

            });

            $("#sltOptionLevelMoveMoreStudent").change(function () {

                LoadTermSubLevel2($("#sltOptionLevelMoveMoreStudent").val(), '#sltClassRoomMoveMoreStudent');

            });


            $("#modalUpdatePayment").on('show.bs.modal', function (e) {
                $("#sltPaymentStatus").val('default').selectpicker("refresh").trigger('change');
            })

            $("#sltPaymentStatus").on('show.bs.select', function (e, clickedIndex) {
                $("#sltPaymentStatus").data('choice', $("#sltPaymentStatus").val());
                registerList.ClickChangePaymentStatus = true;
            }).on('hide.bs.select', function (e, clickedIndex) {
                registerList.ClickChangePaymentStatus = false;
            }).change(function () {

                switch ($("#sltPaymentStatus").val()) {
                    case "0":
                    case "1":
                    case "2":
                        // enable btn save
                        $("#btnUpdatePayment").attr("disabled", false);

                        $('.move-student-section').hide();
                        break;
                    case "3":
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
                                        // enable btn save
                                        $("#btnUpdatePayment").attr("disabled", false);

                                        $('.move-student-section').show();
                                    }
                                    else {
                                        systemMessage.LimitInContact(response);
                                        $("#sltPaymentStatus").selectpicker('val', $("#sltPaymentStatus").data('choice'));
                                        return false;
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
                        break;
                    default:
                        // disable btn save
                        $("#btnUpdatePayment").attr("disabled", true);

                        $('.move-student-section').hide();
                        break;
                }

            });

            $('#btnUpdatePayment').click(function () {
                var rid = $(this).attr('data-rid');
                //var sid = $(this).attr('data-sid');

                switch ($("#sltPaymentStatus").val()) {
                    case "0":
                    case "1":
                    case "2":

                        $('#modalWaitDialog').css('z-index', parseInt($('#modalUpdatePayment').css('z-index')) + 1);
                        $("#modalWaitDialog").modal('show');

                        SavePaymentStatus(rid, $("#sltPaymentStatus").val());
                        break;
                    case "3":
                        if ($('#formUpdatePayment').valid()) {

                            $('#modalWaitDialog').css('z-index', parseInt($('#modalUpdatePayment').css('z-index')) + 1);
                            $("#modalWaitDialog").modal('show');

                            var level2ID = $("#sltClassRoomPaymentStatus").val();
                            var dateMove = $('#iptMoveDate').val();
                            var schoolYear = $('#spnSchoolYearMoveOne').attr('data-year');
                            var term = $('#sltTermMoveOne').val();
                            if (!schoolYear) schoolYear = 0;

                            if (schoolYear != 0 && term !== null) {
                                var sid = $("#iptStudentIDPaymentStatus").val();

                                MoveStudent(rid, level2ID, dateMove, schoolYear, sid, term);
                            }
                            else {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132815") %>');
                            }
                        }
                        break;
                    default: break;
                }

                return false;
            });

            $("#sltExamResultsStatus").change(function () {
                switch ($(this).val()) {
                    case '1':
                    case '0':
                    case '2':
                        $('#formExamResults .update-by, #formExamResults .update-date').show();
                        break;
                    default:
                        $('#formExamResults .update-by, #formExamResults .update-date').hide();
                        break;
                }

                $('#spnExamResultsUpdateBy').text('<%=(string)Session["Emp_Name"]%>');
                $('#spnExamResultsUpdateDate').text('<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>');

                $('#btnSaveExamResults').prop('disabled', false);
            });

            $("#sltCompleteDocumentsStatus").change(function () {
                switch ($(this).val()) {
                    case '1':
                        $('#formCompleteDocuments .not-complete-info').hide();
                        $('#formCompleteDocuments .update-by, #formCompleteDocuments .update-date').show();
                        break;
                    case '0':
                        $('#ttaCompleteDocumentsInfo').val('');

                        $('#formCompleteDocuments .not-complete-info, #formCompleteDocuments .update-by, #formCompleteDocuments .update-date').show();
                        break;
                    default:
                        $('#formCompleteDocuments .not-complete-info, #formCompleteDocuments .update-by, #formCompleteDocuments .update-date').hide();
                        break;
                }

                $('#spnCompleteDocumentsUpdateBy').text('<%=(string)Session["Emp_Name"]%>');
                $('#spnCompleteDocumentsUpdateDate').text('<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%>');

                $('#btnSaveCompleteDocuments').prop('disabled', false);
            });

            $('#btnSaveExamResults').click(function () {

                if (($('#sltExamResultsStatus').val() ?? '') == '') {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132808") %></h3>' });
                    return false;
                }

                var rids = [];
                rids.push($(this).data('rid'));
                SaveExamResultStudent(rids, $('#sltExamResultsStatus').val());

                return false;
            });

            $('#btnSaveCompleteDocuments').click(function () {

                if (($('#sltCompleteDocumentsStatus').val() ?? '') == '') {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132809") %></h3>' });
                    return false;
                }

                if (($('#sltCompleteDocumentsStatus').val() ?? '') == '0' && $('#ttaCompleteDocumentsInfo').val() == '') {
                    swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3 style="font-size: 1.4625rem;">กรุณาระบุรายละเอียดกรณีเลือกสถานะ \'<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103027") %>\' !</h3>' });
                    return false;
                }

                var rids = [];
                if ($(this).data('rid') == 0) {
                    $('.registerList #tableData input[type=checkbox].check-one:checked').each(function (index) {
                        rids.push($(this).attr('rid'));
                    });

                    if (rids.length > 0) {
                        SaveCompleteDocuments(rids, $('#sltCompleteDocumentsStatus').val(), $('#ttaCompleteDocumentsInfo').val());
                    } else {
                        swal({ title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>', buttonsStyling: false, confirmButtonClass: 'btn btn-info', html: '<h3 style="font-size: 1.2625rem;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132810") %></h3>' });
                    }
                }
                else {
                    rids.push($(this).data('rid'));
                    SaveCompleteDocuments(rids, $('#sltCompleteDocumentsStatus').val(), $('#ttaCompleteDocumentsInfo').val());
                }

                return false;
            });

            // Validate form on modal
            $.validator.addMethod("thaiDate",
                function (value, element) {
                    return value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>])[/., -](24|25)?\d{2}$/);
                },
                "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132811") %>"
            );

            $("#formUpdatePayment").validate({
                rules: {
                    iptStudentIDPaymentStatus: "required",
                    sltOptionLevelPaymentStatus: "required",
                    sltClassRoomPaymentStatus: "required",
                    iptMoveDate: {
                        required: true,
                        thaiDate: true
                    },
                    sltTermMoveOne: "required"
                },
                messages: {
                    iptStudentIDPaymentStatus: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltOptionLevelPaymentStatus: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltClassRoomPaymentStatus: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMoveDate: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    sltTermMoveOne: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltOptionLevelPaymentStatus":
                        case "sltClassRoomPaymentStatus":
                        case "sltTermMoveOne": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            $("#formMoveMoreStudent").validate({
                rules: {
                    sltOptionLevelMoveMoreStudent: "required",
                    sltClassRoomMoveMoreStudent: "required",
                    iptMoveMoreStudentDate: {
                        required: true,
                        thaiDate: true
                    },
                    sltTermMoveMore: "required"
                },
                messages: {
                    sltOptionLevelMoveMoreStudent: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltClassRoomMoveMoreStudent: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptMoveMoreStudentDate: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                    },
                    sltTermMoveMore: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                focusInvalid: false,
                invalidHandler: function () {
                    $(this).find(":input.error:first").focus();
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "sltOptionLevelMoveMoreStudent":
                        case "sltClassRoomMoveMoreStudent":
                        case "sltTermMoveMore": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                }
            });

            // Initial control
            $('#iptMoveDate').datetimepicker({
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
            }).on('dp.change', function (e) {
                GetSchoolYear($("#iptMoveDate").val(), '#spnSchoolYearMoveOne', '#sltTermMoveOne');
            });

            $('#iptMoveMoreStudentDate').datetimepicker({
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
            }).on('dp.change', function (e) {
                GetSchoolYear($("#iptMoveMoreStudentDate").val(), '#spnSchoolYearMoveMore', '#sltTermMoveMore');
            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
                if (($("#modalExamResults").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalExamResults").modal('hide');
                    }, 1000);
                }
                if (($("#modalCompleteDocuments").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalCompleteDocuments").modal('hide');
                    }, 1000);
                }
            });

            // Datatable Section
            registerList.LoadListData();

            // Init
            $('.move-student-section').hide();

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
