<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ApprovalStudentDataForm.aspx.cs" Inherits="FingerprintPayment.StudentInfo.ApprovalStudentDataForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .approval-form .btn.btn-success, .btn.btn-danger, .btn.btn-warning {
            width: 135px;
            margin: 0.3125rem 5px;
            color: #fff !important;
        }

        .approval-form .approval-footer {
            display: flex;
        }

            .approval-form .approval-footer .material-icons {
                margin-top: -1px;
            }

        .approval-form .nav-pills .nav-item {
            width: 20%;
            margin: 0px 10px;
        }

            .approval-form .nav-pills .nav-item .nav-link {
                border-radius: 10px;
                font-size: 20px;
                line-height: 2;
                box-shadow: 0 4px 20px 0px rgba(0, 0, 0, 0.14), 0 7px 10px -5px rgb(0 0 0 / 40%);
            }

                .approval-form .nav-pills .nav-item .nav-link.active {
                    color: #fff;
                    background-color: #fb8c00;
                    box-shadow: 0 4px 20px 0px rgba(0, 0, 0, 0.14), 0 7px 10px -5px rgb(176 128 39 / 40%);
                }

        .approval-form .panel {
            padding: 35px 0px;
        }

        .approval-form .panel-body {
            margin: 25px 0px 0px 0px;
        }

            .approval-form .panel-body .tab-content .content .row.input {
                padding: 14px 0px;
                justify-content: center;
            }

                .approval-form .panel-body .tab-content .content .row.input .not-edit {
                    padding-left: 20px;
                    color: #888;
                    font-weight: bold;
                }

                .approval-form .panel-body .tab-content .content .row.input label {
                    color: #000;
                    margin-bottom: 0px;
                    vertical-align: bottom;
                }

                .approval-form .panel-body .tab-content .content .row.input .form-control {
                    height: 30px;
                    padding: 0 5px;
                    margin-top: -2px;
                }

                    .approval-form .panel-body .tab-content .content .row.input .form-control.new-data {
                        color: #3ab93f;
                        opacity: 1;
                    }

        button.dropdown-toggle .filter-option-inner-inner {
            font-size: 14px;
        }

        .approval-form .panel-body .tab-content .content .row.input .selectpicker.new-data + button {
            opacity: 1;
        }

        .approval-form .panel-body .tab-content .content .row.input .selectpicker.new-data + button .filter-option-inner-inner {
            color: #3ab93f;
        }

        .approval-form .panel-body .tab-content .content .row.input .bootstrap-select > .dropdown-toggle {
            padding-right: 10px;
            padding-left: 5px !important;
        }

        .approval-form .panel-body .tab-content .content .row.input .bootstrap-select .btn.dropdown-toggle.select-with-transition {
            margin: -6px 0px 0px 0px;
        }

        .approval-form .panel-body .tab-content .content .row.input .dropdown-toggle.bs-placeholder .filter-option-inner-inner {
            color: #aaaaaa;
            font-size: 14px;
        }

        .approval-form .panel-body .tab-content .content .row.input .dropdown.bootstrap-select.col-sm-12 {
            padding-left: 0px;
            padding-right: 0px;
        }

        .approval-form .panel-body .tab-content .content .row.input p.old-data {
            color: red;
            font-size: 12px;
            padding-left: 5px;
            margin: -1px 0px -23px 0px;
        }

        .approval-form .panel-body .tab-content .content .row.input .form-group:has(.datepicker) {
            padding: 0px;
            margin: -1px 0px 0px 0px;
        }

        .approval-form .top-section {
            padding: 0px;
            margin-top: 25px;
        }

            .approval-form .top-section h4 {
                height: 40px;
            }

                .approval-form .top-section h4 span.top-section-title {
                    margin: 25px 0 0 40px;
                    float: left;
                }

            .approval-form .top-section hr {
                border-width: 2px;
                border-color: #b2b2b2;
            }

        .approval-form .form-control-feedback {
            color: #000;
            opacity: 1;
            top: 1px;
            right: -2px;
        }

        /* +Modal style */
        .modal .modal-footer .btn.btn-default, .modal .modal-footer .btn.btn-primary, .modal .modal-footer .btn.btn-danger, .modal .modal-footer .btn.btn-success {
            width: 95px !important;
        }

        .modal .modal-footer button {
            margin-left: 7px;
        }
        /* -Modal style */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101349") %>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card approval-form">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">description</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101349") %> <a class="btn btn-danger float-right d-none reject-all"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101365") %></a><a class="btn btn-success float-right d-none approve-all"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101364") %></a></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div class="panel with-nav-tabs panel-default">
                        <div class="panel-heading">
                            <ul class="nav nav-pills justify-content-center" role="tablist" id="approvalTabs">
                                <li class="nav-item active"><a class="nav-link active show" href="#tab1" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab2" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101170") %></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab3" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></a></li>
                            </ul>
                        </div>
                        <div class="panel-body">
                            <div class="tab-content">
                                <div class="tab-pane fade in active show" id="tab1">
                                    <div class="content">
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %></span> <a class="btn btn-danger float-right d-none reject-profile"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-profile"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-profile"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnStudentID">xxx</span>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnStudentStatus">xxx</span>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnStudentMoveInDate">xxx</span>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101051") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnStudentNumber">xxx</span>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnStudentClass">xxx</span>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnStudentClassRoom">xxx</span>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentGender"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentGender" name="sltStudentGender" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101062") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101062") %></option>
                                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentTitle" name="sltStudentTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                                    <asp:Literal ID="ltrStudentTitle" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentFirstNameTh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentFirstNameTh" name="iptStudentFirstNameTh"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="256" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentLastNameTh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentLastNameTh" name="iptStudentLastNameTh"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="256" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentFirstNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentFirstNameEn" name="iptStudentFirstNameEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101068") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentLastNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentLastNameEn" name="iptStudentLastNameEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentFirstNameOther"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101071") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentFirstNameOther" name="iptStudentFirstNameOther"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101071") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentLastNameOther"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101072") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentLastNameOther" name="iptStudentLastNameOther"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101072") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentNickNameTh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101073") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentNickNameTh" name="iptStudentNickNameTh"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101073") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentNickNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101074") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentNickNameEn" name="iptStudentNickNameEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101074") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <input id="iptStudentBirthday" name="iptStudentBirthday" type="text" class="form-control datepicker" />
                                                    <span class="form-control-feedback">
                                                        <i class="material-icons">event</i>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentRace" name="sltStudentRace" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                                    <asp:Literal ID="ltrStudentRace" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentNation" name="sltStudentNation" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                                    <asp:Literal ID="ltrStudentNation" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentReligion" name="sltStudentReligion" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                                    <asp:Literal ID="ltrStudentReligion" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltDisabilityCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101086") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltDisabilityCode" name="sltDisabilityCode" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111012") %>">
                                                    <asp:Literal ID="ltrDisabilityCode" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltDisadvantageCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101097") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltDisadvantageCode" name="sltDisadvantageCode" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111014") %>">
                                                    <asp:Literal ID="ltrDisadvantageCode" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentPhone" name="iptStudentPhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentEmail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentEmail" name="iptStudentEmail"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %>" maxlength="256" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentSonTotal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101111") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentSonTotal" name="sltStudentSonTotal" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101112") %>">
                                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101112") %></option>
                                                    <option value="1">1</option>
                                                    <option value="2">2</option>
                                                    <option value="3">3</option>
                                                    <option value="4">4</option>
                                                    <option value="5">5</option>
                                                    <option value="6">6</option>
                                                    <option value="7">7</option>
                                                    <option value="8">8</option>
                                                    <option value="9">9</option>
                                                    <option value="10">10</option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptStudentSonNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptStudentSonNumber" name="iptStudentSonNumber"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %>" maxlength="2" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltStudentBrethrenStudyHere"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101114") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltStudentBrethrenStudyHere" name="sltStudentBrethrenStudyHere" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101115") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101115") %></option>
                                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                                    <option value="1">1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="2">2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="3">3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="4">4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="5">5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="6">6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="7">7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="8">8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="9">9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                    <option value="10">10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptNote2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101124") %>:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <textarea class="form-control" rows="3" id="iptNote2"></textarea>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %></span> <a class="btn btn-danger float-right d-none reject-permanent-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-permanent-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-permanent-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomeCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomeCode" name="iptRegisterHomeCode"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomeNo" name="iptRegisterHomeNo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomeSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomeSoi" name="iptRegisterHomeSoi"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomeMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomeMoo" name="iptRegisterHomeMoo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomeRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomeRoad" name="iptRegisterHomeRoad"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltRegisterHomeProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltRegisterHomeProvince" name="sltRegisterHomeProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" data-element-district="#sltRegisterHomeAmphoe">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                                    <asp:Literal ID="ltrRegisterHomeProvince" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltRegisterHomeAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltRegisterHomeAmphoe" name="sltRegisterHomeAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>" data-element-subdistrict="#sltRegisterHomeTombon">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltRegisterHomeTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltRegisterHomeTombon" name="sltRegisterHomeTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomePostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomePostalCode" name="iptRegisterHomePostalCode"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptRegisterHomePhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptRegisterHomePhone" name="iptRegisterHomePhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptBornFrom"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101143") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptBornFrom" name="iptBornFrom"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101143") %>" maxlength="100" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltBornFromProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101144") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltBornFromProvince" name="sltBornFromProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01933") %>" data-element-district="#sltBornFromAmphoe">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101144") %></option>
                                                    <asp:Literal ID="ltrBornFromProvince" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltBornFromAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101145") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltBornFromAmphoe" name="sltBornFromAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01931") %>" data-element-subdistrict="#sltBornFromTombon">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101145") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltBornFromTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101146") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltBornFromTombon" name="sltBornFromTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01932") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101146") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %></span> <a class="btn btn-danger float-right d-none reject-contact-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-contact-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-contact-address"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-check form-check-inline">
                                                    <label class="form-check-label" style="font-weight: bold;">
                                                        <input id="UseHouseAddress" name="UseHouseAddress" class="form-check-input" type="checkbox" />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>
                                                        <span class="form-check-sign">
                                                            <span class="check"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeNo" name="iptHomeNo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeSoi" name="iptHomeSoi"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeMoo" name="iptHomeMoo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeRoad" name="iptHomeRoad"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltHomeProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltHomeProvince" name="sltHomeProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" data-element-district="#sltHomeAmphoe">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                                    <asp:Literal ID="ltrHomeProvince" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltHomeAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltHomeAmphoe" name="sltHomeAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>" data-element-subdistrict="#sltHomeTombon">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltHomeTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltHomeTombon" name="sltHomeTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomePostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomePostalCode" name="iptHomePostalCode"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomePhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomePhone" name="iptHomePhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltHomeStayWithTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101149") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltHomeStayWithTitle" name="sltHomeStayWithTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111013") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                                    <asp:Literal ID="ltrHomeStayWithTitle" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeStayWithName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeStayWithName" name="iptHomeStayWithName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeStayWithLast"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeStayWithLast" name="iptHomeStayWithLast"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeStayWithEmergencyCall"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeStayWithEmergencyCall" name="iptHomeStayWithEmergencyCall"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeStayWithEmergencyEmail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101154") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeStayWithEmergencyEmail" name="iptHomeStayWithEmergencyEmail"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101154") %>" maxlength="100" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeFriendName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeFriendName" name="iptHomeFriendName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeFriendLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeFriendLastName" name="iptHomeFriendLastName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptHomeFriendPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptHomeFriendPhone" name="iptHomeFriendPhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltHomeHomeType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltHomeHomeType" name="sltHomeHomeType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01927") %>">
                                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101159") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %></option>
                                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %></option>
                                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101162") %></option>
                                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101163") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="tab2">
                                    <div class="content">
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101171") %></span> <a class="btn btn-danger float-right d-none reject-education"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-education"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-education"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101172") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnOldSchoolName">xxx</span>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111002") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnOldSchoolLocation">xxx</span>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101173") %>:
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnOldSchoolGPA">xxx</span>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101175") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnCredit">xxx</span>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnOldSchoolGraduated">xxx</span>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101178") %> :
                                            </div>
                                            <div class="col-md-3 not-edit">
                                                <span id="spnMoveOutReason">xxx</span>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="tab3">
                                    <div class="content">
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></span> <a class="btn btn-danger float-right d-none reject-father-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-father-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-father-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherTitle" name="sltFatherTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                                    <asp:Literal ID="ltrFatherTitle" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherName" name="iptFatherName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherLastName" name="iptFatherLastName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherNameEn" name="iptFatherNameEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherNameLastEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherNameLastEn" name="iptFatherNameLastEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <input id="iptFatherBirthday" name="iptFatherBirthday" type="text" class="form-control datepicker" />
                                                    <span class="form-control-feedback">
                                                        <i class="material-icons">event</i>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherIdentification" name="iptFatherIdentification"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherRace" name="sltFatherRace" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                                    <asp:Literal ID="ltrFatherRace" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherNation" name="sltFatherNation" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                                    <asp:Literal ID="ltrFatherNation" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherReligion" name="sltFatherReligion" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                                    <asp:Literal ID="ltrFatherReligion" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherGraduated" name="sltFatherGraduated" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>">
                                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherHomeNo" name="iptFatherHomeNo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherSoi" name="iptFatherSoi"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherMoo" name="iptFatherMoo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherRoad" name="iptFatherRoad"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherProvince" name="sltFatherProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" data-element-district="#sltFatherAmphoe">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                                    <asp:Literal ID="ltrFatherProvince" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherAmphoe" name="sltFatherAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>" data-element-subdistrict="#sltFatherTombon">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltFatherTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltFatherTombon" name="sltFatherTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherPostalCode" name="iptFatherPostalCode"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherJob" name="iptFatherJob"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %>" maxlength="100" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherIncome"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherIncome" name="iptFatherIncome"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherWorkPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherWorkPlace" name="iptFatherWorkPlace"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>" maxlength="200" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherPhone" name="iptFatherPhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherPhone2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherPhone2" name="iptFatherPhone2"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptFatherPhone3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptFatherPhone3" name="iptFatherPhone3"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></span> <a class="btn btn-danger float-right d-none reject-mother-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-mother-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-mother-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherTitle" name="sltMotherTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                                    <asp:Literal ID="ltrMotherTitle" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherName" name="iptMotherName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherLastName" name="iptMotherLastName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherNameEn" name="iptMotherNameEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherNameLastEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherNameLastEn" name="iptMotherNameLastEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <input id="iptMotherBirthday" name="iptMotherBirthday" type="text" class="form-control datepicker" />
                                                    <span class="form-control-feedback">
                                                        <i class="material-icons">event</i>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherIdentification" name="iptMotherIdentification"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherRace" name="sltMotherRace" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                                    <asp:Literal ID="ltrMotherRace" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherNation" name="sltMotherNation" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                                    <asp:Literal ID="ltrMotherNation" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherReligion" name="sltMotherReligion" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                                    <asp:Literal ID="ltrMotherReligion" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherGraduated" name="sltMotherGraduated" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>">
                                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-check form-check-inline">
                                                    <label class="form-check-label" style="font-weight: bold;">
                                                        <input id="IsFatherAddress" name="IsFatherAddress" class="form-check-input" type="checkbox" />
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101369") %>
                                                        <span class="form-check-sign">
                                                            <span class="check"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherHomeNo" name="iptMotherHomeNo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherSoi" name="iptMotherSoi"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherMoo" name="iptMotherMoo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherRoad" name="iptMotherRoad"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherProvince" name="sltMotherProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" data-element-district="#sltMotherAmphoe">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                                    <asp:Literal ID="ltrMotherProvince" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherAmphoe" name="sltMotherAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>" data-element-subdistrict="#sltMotherTombon">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltMotherTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltMotherTombon" name="sltMotherTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherPostalCode" name="iptMotherPostalCode"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherJob" name="iptMotherJob"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %>" maxlength="100" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherIncome"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherIncome" name="iptMotherIncome"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherWorkPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherWorkPlace" name="iptMotherWorkPlace"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>" maxlength="200" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherPhone" name="iptMotherPhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherPhone2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherPhone2" name="iptMotherPhone2"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptMotherPhone3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptMotherPhone3" name="iptMotherPhone3"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                        <div class="row">
                                            <div class="col-md-12 top-section">
                                                <h4><span class="top-section-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></span> <a class="btn btn-danger float-right d-none reject-parent-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102232") %></a><a class="btn btn-success float-right d-none approve-parent-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102233") %></a><a class="btn btn-warning float-right d-none recovery-parent-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111001") %></a></h4>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltCopyDataFrom"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101191") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltCopyDataFrom" name="sltCopyDataFrom" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101182") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101182") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentRelate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentRelate" name="sltParentRelate" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101193") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101193") %></option>
                                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></option>
                                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %></option>
                                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %></option>
                                                    <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentTitle" name="sltParentTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                                    <asp:Literal ID="ltrParentTitle" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentName" name="iptParentName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentLastName" name="iptParentLastName"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentNameEn" name="iptParentNameEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentNameLastEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentNameLastEn" name="iptParentNameLastEn"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <input id="iptParentBirthday" name="iptParentBirthday" type="text" class="form-control datepicker" />
                                                    <span class="form-control-feedback">
                                                        <i class="material-icons">event</i>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentIdentification" name="iptParentIdentification"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentRace" name="sltParentRace" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                                    <asp:Literal ID="ltrParentRace" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentNation" name="sltParentNation" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                                    <asp:Literal ID="ltrParentNation" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentReligion" name="sltParentReligion" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                                    <asp:Literal ID="ltrParentReligion" runat="server" />
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentGraduated" name="sltParentGraduated" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>">
                                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentHomeNo" name="iptParentHomeNo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentSoi" name="iptParentSoi"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentMoo" name="iptParentMoo"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentRoad" name="iptParentRoad"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentProvince" name="sltParentProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" data-element-district="#sltParentAmphoe">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                                    <asp:Literal ID="ltrParentProvince" runat="server"></asp:Literal>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentAmphoe" name="sltParentAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>" data-element-subdistrict="#sltParentTombon">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentTombon" name="sltParentTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentPostalCode" name="iptParentPostalCode"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="sltTuitionReimbursement"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101197") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltTuitionReimbursement" name="sltTuitionReimbursement" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101198") %>">
                                                    <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101198") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101199") %></option>
                                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101200") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="sltParentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <select id="sltParentStatus" name="sltParentStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101202") %>">
                                                    <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101202") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101203") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101204") %></option>
                                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101205") %></option>
                                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %></option>
                                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %></option>
                                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101207") %></option>
                                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101208") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentJob" name="iptParentJob"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %>" maxlength="100" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentIncome"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentIncome" name="iptParentIncome"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>" maxlength="10" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentWorkPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentWorkPlace" name="iptParentWorkPlace"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>" maxlength="200" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentPhone" name="iptParentPhone"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %>" maxlength="50" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <div class="row input">
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentPhone2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentPhone2" name="iptParentPhone2"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <label for="iptParentPhone3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <input type="text" class="form-control" id="iptParentPhone3" name="iptParentPhone3"
                                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %>" maxlength="20" />
                                            </div>
                                            <div class="col-md-1"></div>
                                        </div>
                                        <%--end section--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- tab content-->
                </div>
                <!-- end content-->
                <div class="card-footer">
                    <a href="ApprovalUpdateStudentDataList.aspx" class="approval-footer"><span class="material-icons text-warning">chevron_left</span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                </div>
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script type="text/javascript">

        function LoadDistrict(obj, provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ApprovalStudentDataForm.aspx/LoadDistrict",
                    data: '{provinceID: ' + provinceID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var list = response.d;

                        $(obj).empty();

                        var options = '<option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>';
                        $(list).each(function () {

                            options += '<option value="' + this.id + '">' + this.name + '</option>';

                        });

                        $(obj).html(options);
                        $(obj).selectpicker('refresh');
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

        function LoadSubDistrict(obj, districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "ApprovalStudentDataForm.aspx/LoadSubDistrict",
                    data: '{districtID: ' + districtID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var plans = response.d;

                        $(obj).empty();

                        var options = '<option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>';
                        $(plans).each(function () {

                            options += '<option value="' + this.id + '">' + this.name + '</option>';

                        });

                        $(obj).html(options);
                        $(obj).selectpicker('refresh');
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

            var approvalForm = {
                GetItem: function (sid, requestDate) {
                    $.ajax({
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/GetRequestApprovalStudentInfo",
                        data: '{sid: ' + sid + ', requestDate: \'' + requestDate + '\'}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: approvalForm.OnSuccessGet,
                        beforeSend: function () {
                            // Handle the beforeSend event
                            $("body").mLoading();
                        },
                        complete: function () {
                            // Handle the complete event
                            $("body").mLoading('hide');
                        },
                        failure: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');
                        },
                        error: function (response) {
                            alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');
                        }
                    });
                },
                OnSuccessGet: function (response) {
                    var r = JSON.parse(response.d);
                    console.log(r);

                    if (r.success) {
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %>
                        $('#spnStudentID').text(r.data.profile.studentId.value);
                        $('#spnStudentStatus').text(r.data.profile.studentStatus.value);
                        $('#spnStudentMoveInDate').text(r.data.profile.studentMoveInDate.value);
                        $('#spnStudentNumber').text(r.data.profile.studentNumber.value);
                        $('#spnStudentClass').text(r.data.profile.studentClass.value);
                        $('#spnStudentClassRoom').text(r.data.profile.studentClassRoom.value);

                        $("#sltStudentGender").selectpicker('val', r.data.profile.studentGender.value);
                        $("#sltStudentTitle").selectpicker('val', r.data.profile.studentTitle.value);
                        $("#iptStudentFirstNameTh").val(r.data.profile.studentFirstNameTh.value);
                        $("#iptStudentLastNameTh").val(r.data.profile.studentLastNameTh.value);
                        $("#iptStudentFirstNameEn").val(r.data.profile.studentFirstNameEn.value);
                        $("#iptStudentLastNameEn").val(r.data.profile.studentLastNameEn.value);
                        $("#iptStudentFirstNameOther").val(r.data.profile.studentFirstNameOther.value);
                        $("#iptStudentLastNameOther").val(r.data.profile.studentLastNameOther.value);
                        $("#iptStudentNickNameTh").val(r.data.profile.studentNickNameTh.value);
                        $("#iptStudentNickNameEn").val(r.data.profile.studentNickNameEn.value);
                        $("#iptStudentBirthday").val(r.data.profile.studentBirthday.value);
                        $("#sltStudentRace").selectpicker('val', r.data.profile.studentRace.value);
                        $("#sltStudentNation").selectpicker('val', r.data.profile.studentNation.value);
                        $("#sltStudentReligion").selectpicker('val', r.data.profile.studentReligion.value);
                        $("#sltDisabilityCode").selectpicker('val', r.data.profile.disabilityCode.value);
                        $("#sltDisadvantageCode").selectpicker('val', r.data.profile.disadvantageCode.value);
                        $("#iptStudentPhone").val(r.data.profile.studentPhone.value);
                        $("#iptStudentEmail").val(r.data.profile.studentEmail.value);
                        $("#sltStudentSonTotal").selectpicker('val', r.data.profile.studentSonTotal.value);
                        $("#iptStudentSonNumber").val(r.data.profile.studentSonNumber.value);
                        $("#sltStudentBrethrenStudyHere").selectpicker('val', r.data.profile.studentBrethrenStudyHere.value);
                        $("#iptNote2").val(r.data.profile.note2.value);

                        if (r.data.profile.requestUpdateData) {
                            if (r.data.profile.studentGender.isNewValue) $('#sltStudentGender').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentGender.dbValue ? $('#sltStudentGender option[value=' + r.data.profile.studentGender.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.studentTitle.isNewValue) $('#sltStudentTitle').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentTitle.dbValue ? $('#sltStudentTitle option[value=' + r.data.profile.studentTitle.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.studentFirstNameTh.isNewValue) $('#iptStudentFirstNameTh').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentFirstNameTh.dbValue + `</p>`);
                            if (r.data.profile.studentLastNameTh.isNewValue) $('#iptStudentLastNameTh').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentLastNameTh.dbValue + `</p>`);
                            if (r.data.profile.studentFirstNameEn.isNewValue) $('#iptStudentFirstNameEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentFirstNameEn.dbValue + `</p>`);
                            if (r.data.profile.studentLastNameEn.isNewValue) $('#iptStudentLastNameEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentLastNameEn.dbValue + `</p>`);
                            if (r.data.profile.studentFirstNameOther.isNewValue) $('#iptStudentFirstNameOther').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentFirstNameOther.dbValue + `</p>`);
                            if (r.data.profile.studentLastNameOther.isNewValue) $('#iptStudentLastNameOther').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentLastNameOther.dbValue + `</p>`);
                            if (r.data.profile.studentNickNameTh.isNewValue) $('#iptStudentNickNameTh').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentNickNameTh.dbValue + `</p>`);
                            if (r.data.profile.studentNickNameEn.isNewValue) $('#iptStudentNickNameEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentNickNameEn.dbValue + `</p>`);
                            if (r.data.profile.studentBirthday.isNewValue) $('#iptStudentBirthday').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentBirthday.dbValue + `</p>`);
                            if (r.data.profile.studentRace.isNewValue) $('#sltStudentRace').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentRace.dbValue ? $('#sltStudentRace option[value=' + r.data.profile.studentRace.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.studentNation.isNewValue) $('#sltStudentNation').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentNation.dbValue ? $('#sltStudentNation option[value=' + r.data.profile.studentNation.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.studentReligion.isNewValue) $('#sltStudentReligion').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentReligion.dbValue ? $('#sltStudentReligion option[value=' + r.data.profile.studentReligion.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.disabilityCode.isNewValue) $('#sltDisabilityCode').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.disabilityCode.dbValue ? $('#sltDisabilityCode option[value=' + r.data.profile.disabilityCode.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.disadvantageCode.isNewValue) $('#sltDisadvantageCode').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.disadvantageCode.dbValue ? $('#sltDisadvantageCode option[value=' + r.data.profile.disadvantageCode.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.studentPhone.isNewValue) $('#iptStudentPhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentPhone.dbValue + `</p>`);
                            if (r.data.profile.studentEmail.isNewValue) $('#iptStudentEmail').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentEmail.dbValue + `</p>`);
                            if (r.data.profile.studentSonTotal.isNewValue) $('#sltStudentSonTotal').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentSonTotal.dbValue ? $('#sltStudentSonTotal option[value=' + r.data.profile.studentSonTotal.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.studentSonNumber.isNewValue) $('#iptStudentSonNumber').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.studentSonNumber.dbValue + `</p>`);
                            if (r.data.profile.studentBrethrenStudyHere.isNewValue) $('#sltStudentBrethrenStudyHere').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.profile.studentBrethrenStudyHere.dbValue ? $('#sltStudentBrethrenStudyHere option[value=' + r.data.profile.studentBrethrenStudyHere.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.profile.note2.isNewValue) $('#iptNote2').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.profile.note2.dbValue + `</p>`);

                            if (r.data.profile.approveStatus == 'pending' && r.data.profile.dataChanged) {
                                $('.reject-profile, .approve-profile').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');

                                $('.reject-all, .approve-all').data('profile-id', r.data.profile.id).data('profile-sid', r.data.profile.sId).removeClass('d-none');
                            }
                            //else if (r.data.profile.approveStatus == 'approve') {
                            //    $('.recovery-profile').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');
                            //}
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>
                        $("#iptRegisterHomeCode").val(r.data.permanentAddress.registerHomeCode.value);
                        $("#iptRegisterHomeNo").val(r.data.permanentAddress.registerHomeNo.value);
                        $("#iptRegisterHomeSoi").val(r.data.permanentAddress.registerHomeSoi.value);
                        $("#iptRegisterHomeMoo").val(r.data.permanentAddress.registerHomeMoo.value);
                        $("#iptRegisterHomeRoad").val(r.data.permanentAddress.registerHomeRoad.value);
                        $("#sltRegisterHomeProvince").selectpicker('val', r.data.permanentAddress.registerHomeProvince.value);
                        LoadDistrict('#sltRegisterHomeAmphoe', $("#sltRegisterHomeProvince").val());
                        $("#sltRegisterHomeAmphoe").selectpicker('val', r.data.permanentAddress.registerHomeAmphoe.value);
                        LoadSubDistrict('#sltRegisterHomeTombon', $("#sltRegisterHomeAmphoe").val());
                        $("#sltRegisterHomeTombon").selectpicker('val', r.data.permanentAddress.registerHomeTombon.value);
                        $("#iptRegisterHomePostalCode").val(r.data.permanentAddress.registerHomePostalCode.value);
                        $("#iptRegisterHomePhone").val(r.data.permanentAddress.registerHomePhone.value);
                        $("#iptBornFrom").val(r.data.permanentAddress.bornFrom.value);
                        $("#sltBornFromProvince").selectpicker('val', r.data.permanentAddress.bornFromProvince.value);
                        LoadDistrict('#sltBornFromAmphoe', $("#sltBornFromProvince").val());
                        $("#sltBornFromAmphoe").selectpicker('val', r.data.permanentAddress.bornFromAmphoe.value);
                        LoadSubDistrict('#sltBornFromTombon', $("#sltBornFromAmphoe").val());
                        $("#sltBornFromTombon").selectpicker('val', r.data.permanentAddress.bornFromTombon.value);

                        if (r.data.permanentAddress.requestUpdateData) {
                            if (r.data.permanentAddress.registerHomeCode.isNewValue) $('#iptRegisterHomeCode').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomeCode.dbValue + `</p>`);
                            if (r.data.permanentAddress.registerHomeNo.isNewValue) $('#iptRegisterHomeNo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomeNo.dbValue + `</p>`);
                            if (r.data.permanentAddress.registerHomeSoi.isNewValue) $('#iptRegisterHomeSoi').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomeSoi.dbValue + `</p>`);
                            if (r.data.permanentAddress.registerHomeMoo.isNewValue) $('#iptRegisterHomeMoo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomeMoo.dbValue + `</p>`);
                            if (r.data.permanentAddress.registerHomeRoad.isNewValue) $('#iptRegisterHomeRoad').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomeRoad.dbValue + `</p>`);
                            if (r.data.permanentAddress.registerHomeProvince.isNewValue) $('#sltRegisterHomeProvince').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.permanentAddress.registerHomeProvince.dbValue ? $('#sltRegisterHomeProvince option[value=' + r.data.permanentAddress.registerHomeProvince.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.permanentAddress.registerHomeAmphoe.isNewValue) $('#sltRegisterHomeAmphoe').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.permanentAddress.registerHomeAmphoe.dbValue ? $('#sltRegisterHomeAmphoe option[value=' + r.data.permanentAddress.registerHomeAmphoe.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.permanentAddress.registerHomeTombon.isNewValue) $('#sltRegisterHomeTombon').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.permanentAddress.registerHomeTombon.dbValue ? $('#sltRegisterHomeTombon option[value=' + r.data.permanentAddress.registerHomeTombon.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.permanentAddress.registerHomePostalCode.isNewValue) $('#iptRegisterHomePostalCode').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomePostalCode.dbValue + `</p>`);
                            if (r.data.permanentAddress.registerHomePhone.isNewValue) $('#iptRegisterHomePhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.registerHomePhone.dbValue + `</p>`);
                            if (r.data.permanentAddress.bornFrom.isNewValue) $('#iptBornFrom').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.permanentAddress.bornFrom.dbValue + `</p>`);
                            if (r.data.permanentAddress.bornFromProvince.isNewValue) $('#sltBornFromProvince').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.permanentAddress.bornFromProvince.dbValue ? $('#sltBornFromProvince option[value=' + r.data.permanentAddress.bornFromProvince.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.permanentAddress.bornFromAmphoe.isNewValue) $('#sltBornFromAmphoe').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.permanentAddress.bornFromAmphoe.dbValue ? $('#sltBornFromAmphoe option[value=' + r.data.permanentAddress.bornFromAmphoe.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.permanentAddress.bornFromTombon.isNewValue) $('#sltBornFromTombon').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.permanentAddress.bornFromTombon.dbValue ? $('#sltBornFromTombon option[value=' + r.data.permanentAddress.bornFromTombon.dbValue + ']').text() : '') + `</p>`);

                            if (r.data.permanentAddress.approveStatus == 'pending' && r.data.permanentAddress.dataChanged) {
                                $('.reject-permanent-address, .approve-permanent-address').data('id', r.data.permanentAddress.id).data('sid', r.data.permanentAddress.sId).removeClass('d-none');

                                $('.reject-all, .approve-all').data('permanent-address-id', r.data.permanentAddress.id).data('permanent-address-sid', r.data.permanentAddress.sId).removeClass('d-none');
                            }
                            //else if (r.data.permanentAddress.approveStatus == 'approve') {
                            //    $('.recovery-permanent-address').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');
                            //}
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %>
                        $("#UseHouseAddress").prop("checked", r.data.contactAddress.useHouseAddress.value);
                        $("#iptHomeNo").val(r.data.contactAddress.homeNo.value);
                        $("#iptHomeSoi").val(r.data.contactAddress.homeSoi.value);
                        $("#iptHomeMoo").val(r.data.contactAddress.homeMoo.value);
                        $("#iptHomeRoad").val(r.data.contactAddress.homeRoad.value);
                        $("#sltHomeProvince").selectpicker('val', r.data.contactAddress.homeProvince.value);
                        LoadDistrict('#sltHomeAmphoe', $("#sltHomeProvince").val());
                        $("#sltHomeAmphoe").selectpicker('val', r.data.contactAddress.homeAmphoe.value);
                        LoadSubDistrict('#sltHomeTombon', $("#sltHomeAmphoe").val());
                        $("#sltHomeTombon").selectpicker('val', r.data.contactAddress.homeTombon.value);
                        $("#iptHomePostalCode").val(r.data.contactAddress.homePostalCode.value);
                        $("#iptHomePhone").val(r.data.contactAddress.homePhone.value);
                        $("#sltHomeStayWithTitle").selectpicker('val', r.data.contactAddress.homeStayWithTitle.value);
                        $("#iptHomeStayWithName").val(r.data.contactAddress.homeStayWithName.value);
                        $("#iptHomeStayWithLast").val(r.data.contactAddress.homeStayWithLast.value);
                        $("#iptHomeStayWithEmergencyCall").val(r.data.contactAddress.homeStayWithEmergencyCall.value);
                        $("#iptHomeStayWithEmergencyEmail").val(r.data.contactAddress.homeStayWithEmergencyEmail.value);
                        $("#iptHomeFriendName").val(r.data.contactAddress.homeFriendName.value);
                        $("#iptHomeFriendLastName").val(r.data.contactAddress.homeFriendLastName.value);
                        $("#iptHomeFriendPhone").val(r.data.contactAddress.homeFriendPhone.value);
                        $("#sltHomeHomeType").selectpicker('val', r.data.contactAddress.homeHomeType.value);

                        if (r.data.contactAddress.requestUpdateData) {
                            if (r.data.contactAddress.homeNo.isNewValue) $('#iptHomeNo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeNo.dbValue + `</p>`);
                            if (r.data.contactAddress.homeSoi.isNewValue) $('#iptHomeSoi').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeSoi.dbValue + `</p>`);
                            if (r.data.contactAddress.homeMoo.isNewValue) $('#iptHomeMoo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeMoo.dbValue + `</p>`);
                            if (r.data.contactAddress.homeRoad.isNewValue) $('#iptHomeRoad').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeRoad.dbValue + `</p>`);
                            if (r.data.contactAddress.homeProvince.isNewValue) $('#sltHomeProvince').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.contactAddress.homeProvince.dbValue ? $('#sltHomeProvince option[value=' + r.data.contactAddress.homeProvince.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.contactAddress.homeAmphoe.isNewValue) $('#sltHomeAmphoe').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.contactAddress.homeAmphoe.dbValue ? $('#sltHomeAmphoe option[value=' + r.data.contactAddress.homeAmphoe.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.contactAddress.homeTombon.isNewValue) $('#sltHomeTombon').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.contactAddress.homeTombon.dbValue ? $('#sltHomeTombon option[value=' + r.data.contactAddress.homeTombon.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.contactAddress.homePostalCode.isNewValue) $('#iptHomePostalCode').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homePostalCode.dbValue + `</p>`);
                            if (r.data.contactAddress.homePhone.isNewValue) $('#iptHomePhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homePhone.dbValue + `</p>`);
                            if (r.data.contactAddress.homeStayWithTitle.isNewValue) $('#sltHomeStayWithTitle').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.contactAddress.homeStayWithTitle.dbValue ? $('#sltHomeStayWithTitle option[value=' + r.data.contactAddress.homeStayWithTitle.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.contactAddress.homeStayWithName.isNewValue) $('#iptHomeStayWithName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeStayWithName.dbValue + `</p>`);
                            if (r.data.contactAddress.homeStayWithLast.isNewValue) $('#iptHomeStayWithLast').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeStayWithLast.dbValue + `</p>`);
                            if (r.data.contactAddress.homeStayWithEmergencyCall.isNewValue) $('#iptHomeStayWithEmergencyCall').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeStayWithEmergencyCall.dbValue + `</p>`);
                            if (r.data.contactAddress.homeStayWithEmergencyEmail.isNewValue) $('#iptHomeStayWithEmergencyEmail').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeStayWithEmergencyEmail.dbValue + `</p>`);
                            if (r.data.contactAddress.homeFriendName.isNewValue) $('#iptHomeFriendName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeFriendName.dbValue + `</p>`);
                            if (r.data.contactAddress.homeFriendLastName.isNewValue) $('#iptHomeFriendLastName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeFriendLastName.dbValue + `</p>`);
                            if (r.data.contactAddress.homeFriendPhone.isNewValue) $('#iptHomeFriendPhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.contactAddress.homeFriendPhone.dbValue + `</p>`);
                            if (r.data.contactAddress.homeHomeType.isNewValue) $('#sltHomeHomeType').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.contactAddress.homeHomeType.dbValue ? $('#sltHomeHomeType option[value=' + r.data.contactAddress.homeHomeType.dbValue + ']').text() : '') + `</p>`);

                            if (r.data.contactAddress.approveStatus == 'pending' && r.data.contactAddress.dataChanged) {
                                $('.reject-contact-address, .approve-contact-address').data('id', r.data.contactAddress.id).data('sid', r.data.contactAddress.sId).removeClass('d-none');

                                $('.reject-all, .approve-all').data('contact-address-id', r.data.contactAddress.id).data('contact-address-sid', r.data.contactAddress.sId).removeClass('d-none');
                            }
                            //else if (r.data.contactAddress.approveStatus == 'approve') {
                            //    $('.recovery-contact-address').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');
                            //}
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101171") %>
                        $('#spnOldSchoolName').text(r.data.education.oldSchoolName.value);
                        $('#spnOldSchoolLocation').text(r.data.education.oldSchoolLocation.value);
                        $('#spnOldSchoolGPA').text(r.data.education.oldSchoolGPA.value);
                        $('#spnCredit').text(r.data.education.credit.value);
                        $('#spnOldSchoolGraduated').text(r.data.education.oldSchoolGraduated.value);
                        $('#spnMoveOutReason').text(r.data.education.moveOutReason.value);

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %>
                        $("#sltFatherTitle").selectpicker('val', r.data.fatherInfo.fatherTitle.value);
                        $("#iptFatherName").val(r.data.fatherInfo.fatherName.value);
                        $("#iptFatherLastName").val(r.data.fatherInfo.fatherLastName.value);
                        $("#iptFatherNameEn").val(r.data.fatherInfo.fatherNameEn.value);
                        $("#iptFatherNameLastEn").val(r.data.fatherInfo.fatherNameLastEn.value);
                        $("#iptFatherBirthday").val(r.data.fatherInfo.fatherBirthday.value);
                        $("#iptFatherIdentification").val(r.data.fatherInfo.fatherIdentification.value);
                        $("#sltFatherRace").selectpicker('val', r.data.fatherInfo.fatherRace.value);
                        $("#sltFatherNation").selectpicker('val', r.data.fatherInfo.fatherNation.value);
                        $("#sltFatherReligion").selectpicker('val', r.data.fatherInfo.fatherReligion.value);
                        $("#sltFatherGraduated").selectpicker('val', r.data.fatherInfo.fatherGraduated.value);
                        $("#iptFatherHomeNo").val(r.data.fatherInfo.fatherHomeNo.value);
                        $("#iptFatherSoi").val(r.data.fatherInfo.fatherSoi.value);
                        $("#iptFatherMoo").val(r.data.fatherInfo.fatherMoo.value);
                        $("#iptFatherRoad").val(r.data.fatherInfo.fatherRoad.value);
                        $("#sltFatherProvince").selectpicker('val', r.data.fatherInfo.fatherProvince.value);
                        LoadDistrict('#sltFatherAmphoe', $("#sltFatherProvince").val());
                        $("#sltFatherAmphoe").selectpicker('val', r.data.fatherInfo.fatherAmphoe.value);
                        LoadSubDistrict('#sltFatherTombon', $("#sltFatherAmphoe").val());
                        $("#sltFatherTombon").selectpicker('val', r.data.fatherInfo.fatherTombon.value);
                        $("#iptFatherPostalCode").val(r.data.fatherInfo.fatherPostalCode.value);
                        $("#iptFatherJob").val(r.data.fatherInfo.fatherJob.value);
                        $("#iptFatherIncome").val(r.data.fatherInfo.fatherIncome.value);
                        $("#iptFatherWorkPlace").val(r.data.fatherInfo.fatherWorkPlace.value);
                        $("#iptFatherPhone").val(r.data.fatherInfo.fatherPhone.value);
                        $("#iptFatherPhone2").val(r.data.fatherInfo.fatherPhone2.value);
                        $("#iptFatherPhone3").val(r.data.fatherInfo.fatherPhone3.value);

                        if (r.data.fatherInfo.requestUpdateData) {
                            if (r.data.fatherInfo.fatherTitle.isNewValue) $('#sltFatherTitle').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherTitle.dbValue ? $('#sltFatherTitle option[value=' + r.data.fatherInfo.fatherTitle.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherName.isNewValue) $('#iptFatherName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherName.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherLastName.isNewValue) $('#iptFatherLastName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherLastName.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherNameEn.isNewValue) $('#iptFatherNameEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherNameEn.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherNameLastEn.isNewValue) $('#iptFatherNameLastEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherNameLastEn.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherBirthday.isNewValue) $('#iptFatherBirthday').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherBirthday.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherIdentification.isNewValue) $('#iptFatherIdentification').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherIdentification.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherRace.isNewValue) $('#sltFatherRace').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherRace.dbValue ? $('#sltFatherRace option[value=' + r.data.fatherInfo.fatherRace.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherNation.isNewValue) $('#sltFatherNation').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherNation.dbValue ? $('#sltFatherNation option[value=' + r.data.fatherInfo.fatherNation.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherReligion.isNewValue) $('#sltFatherReligion').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherReligion.dbValue ? $('#sltFatherReligion option[value=' + r.data.fatherInfo.fatherReligion.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherGraduated.isNewValue) $('#sltFatherGraduated').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherGraduated.dbValue ? $('#sltFatherGraduated option[value=' + r.data.fatherInfo.fatherGraduated.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherHomeNo.isNewValue) $('#iptFatherHomeNo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherHomeNo.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherSoi.isNewValue) $('#iptFatherSoi').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherSoi.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherMoo.isNewValue) $('#iptFatherMoo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherMoo.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherRoad.isNewValue) $('#iptFatherRoad').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherRoad.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherProvince.isNewValue) $('#sltFatherProvince').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherProvince.dbValue ? $('#sltFatherProvince option[value=' + r.data.fatherInfo.fatherProvince.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherAmphoe.isNewValue) $('#sltFatherAmphoe').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherAmphoe.dbValue ? $('#sltFatherAmphoe option[value=' + r.data.fatherInfo.fatherAmphoe.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherTombon.isNewValue) $('#sltFatherTombon').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.fatherInfo.fatherTombon.dbValue ? $('#sltFatherTombon option[value=' + r.data.fatherInfo.fatherTombon.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.fatherInfo.fatherPostalCode.isNewValue) $('#iptFatherPostalCode').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherPostalCode.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherJob.isNewValue) $('#iptFatherJob').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherJob.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherIncome.isNewValue) $('#iptFatherIncome').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherIncome.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherWorkPlace.isNewValue) $('#iptFatherWorkPlace').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherWorkPlace.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherPhone.isNewValue) $('#iptFatherPhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherPhone.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherPhone2.isNewValue) $('#iptFatherPhone2').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherPhone2.dbValue + `</p>`);
                            if (r.data.fatherInfo.fatherPhone3.isNewValue) $('#iptFatherPhone3').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.fatherInfo.fatherPhone3.dbValue + `</p>`);

                            if (r.data.fatherInfo.approveStatus == 'pending' && r.data.fatherInfo.dataChanged) {
                                $('.reject-father-info, .approve-father-info').data('id', r.data.fatherInfo.id).data('sid', r.data.fatherInfo.sId).removeClass('d-none');

                                $('.reject-all, .approve-all').data('father-info-id', r.data.fatherInfo.id).data('father-info-sid', r.data.fatherInfo.sId).removeClass('d-none');
                            }
                            //else if (r.data.fatherInfo.approveStatus == 'approve') {
                            //    $('.recovery-father-info').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');
                            //}
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %>
                        $("#sltMotherTitle").selectpicker('val', r.data.motherInfo.motherTitle.value);
                        $("#iptMotherName").val(r.data.motherInfo.motherName.value);
                        $("#iptMotherLastName").val(r.data.motherInfo.motherLastName.value);
                        $("#iptMotherNameEn").val(r.data.motherInfo.motherNameEn.value);
                        $("#iptMotherNameLastEn").val(r.data.motherInfo.motherNameLastEn.value);
                        $("#iptMotherBirthday").val(r.data.motherInfo.motherBirthday.value);
                        $("#iptMotherIdentification").val(r.data.motherInfo.motherIdentification.value);
                        $("#sltMotherRace").selectpicker('val', r.data.motherInfo.motherRace.value);
                        $("#sltMotherNation").selectpicker('val', r.data.motherInfo.motherNation.value);
                        $("#sltMotherReligion").selectpicker('val', r.data.motherInfo.motherReligion.value);
                        $("#sltMotherGraduated").selectpicker('val', r.data.motherInfo.motherGraduated.value);
                        $('#IsFatherAddress').prop("checked", r.data.motherInfo.isFatherAddress.value)
                        $("#iptMotherHomeNo").val(r.data.motherInfo.motherHomeNo.value);
                        $("#iptMotherSoi").val(r.data.motherInfo.motherSoi.value);
                        $("#iptMotherMoo").val(r.data.motherInfo.motherMoo.value);
                        $("#iptMotherRoad").val(r.data.motherInfo.motherRoad.value);
                        $("#sltMotherProvince").selectpicker('val', r.data.motherInfo.motherProvince.value);
                        LoadDistrict('#sltMotherAmphoe', $("#sltMotherProvince").val());
                        $("#sltMotherAmphoe").selectpicker('val', r.data.motherInfo.motherAmphoe.value);
                        LoadSubDistrict('#sltMotherTombon', $("#sltMotherAmphoe").val());
                        $("#sltMotherTombon").selectpicker('val', r.data.motherInfo.motherTombon.value);
                        $("#iptMotherPostalCode").val(r.data.motherInfo.motherPostalCode.value);
                        $("#iptMotherJob").val(r.data.motherInfo.motherJob.value);
                        $("#iptMotherIncome").val(r.data.motherInfo.motherIncome.value);
                        $("#iptMotherWorkPlace").val(r.data.motherInfo.motherWorkPlace.value);
                        $("#iptMotherPhone").val(r.data.motherInfo.motherPhone.value);
                        $("#iptMotherPhone2").val(r.data.motherInfo.motherPhone2.value);
                        $("#iptMotherPhone3").val(r.data.motherInfo.motherPhone3.value);

                        if (r.data.motherInfo.requestUpdateData) {
                            if (r.data.motherInfo.motherTitle.isNewValue) $('#sltMotherTitle').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherTitle.dbValue ? $('#sltMotherTitle option[value=' + r.data.motherInfo.motherTitle.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherName.isNewValue) $('#iptMotherName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherName.dbValue + `</p>`);
                            if (r.data.motherInfo.motherLastName.isNewValue) $('#iptMotherLastName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherLastName.dbValue + `</p>`);
                            if (r.data.motherInfo.motherNameEn.isNewValue) $('#iptMotherNameEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherNameEn.dbValue + `</p>`);
                            if (r.data.motherInfo.motherNameLastEn.isNewValue) $('#iptMotherNameLastEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherNameLastEn.dbValue + `</p>`);
                            if (r.data.motherInfo.motherBirthday.isNewValue) $('#iptMotherBirthday').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherBirthday.dbValue + `</p>`);
                            if (r.data.motherInfo.motherIdentification.isNewValue) $('#iptMotherIdentification').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherIdentification.dbValue + `</p>`);
                            if (r.data.motherInfo.motherRace.isNewValue) $('#sltMotherRace').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherRace.dbValue ? $('#sltMotherRace option[value=' + r.data.motherInfo.motherRace.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherNation.isNewValue) $('#sltMotherNation').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherNation.dbValue ? $('#sltMotherNation option[value=' + r.data.motherInfo.motherNation.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherReligion.isNewValue) $('#sltMotherReligion').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherReligion.dbValue ? $('#sltMotherReligion option[value=' + r.data.motherInfo.motherReligion.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherGraduated.isNewValue) $('#sltMotherGraduated').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherGraduated.dbValue ? $('#sltMotherGraduated option[value=' + r.data.motherInfo.motherGraduated.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherHomeNo.isNewValue) $('#iptMotherHomeNo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherHomeNo.dbValue + `</p>`);
                            if (r.data.motherInfo.motherSoi.isNewValue) $('#iptMotherSoi').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherSoi.dbValue + `</p>`);
                            if (r.data.motherInfo.motherMoo.isNewValue) $('#iptMotherMoo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherMoo.dbValue + `</p>`);
                            if (r.data.motherInfo.motherRoad.isNewValue) $('#iptMotherRoad').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherRoad.dbValue + `</p>`);
                            if (r.data.motherInfo.motherProvince.isNewValue) $('#sltMotherProvince').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherProvince.dbValue ? $('#sltMotherProvince option[value=' + r.data.motherInfo.motherProvince.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherAmphoe.isNewValue) $('#sltMotherAmphoe').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherAmphoe.dbValue ? $('#sltMotherAmphoe option[value=' + r.data.motherInfo.motherAmphoe.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherTombon.isNewValue) $('#sltMotherTombon').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.motherInfo.motherTombon.dbValue ? $('#sltMotherTombon option[value=' + r.data.motherInfo.motherTombon.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.motherInfo.motherPostalCode.isNewValue) $('#iptMotherPostalCode').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherPostalCode.dbValue + `</p>`);
                            if (r.data.motherInfo.motherJob.isNewValue) $('#iptMotherJob').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherJob.dbValue + `</p>`);
                            if (r.data.motherInfo.motherIncome.isNewValue) $('#iptMotherIncome').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherIncome.dbValue + `</p>`);
                            if (r.data.motherInfo.motherWorkPlace.isNewValue) $('#iptMotherWorkPlace').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherWorkPlace.dbValue + `</p>`);
                            if (r.data.motherInfo.motherPhone.isNewValue) $('#iptMotherPhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherPhone.dbValue + `</p>`);
                            if (r.data.motherInfo.motherPhone2.isNewValue) $('#iptMotherPhone2').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherPhone2.dbValue + `</p>`);
                            if (r.data.motherInfo.motherPhone3.isNewValue) $('#iptMotherPhone3').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.motherInfo.motherPhone3.dbValue + `</p>`);

                            if (r.data.motherInfo.approveStatus == 'pending' && r.data.motherInfo.dataChanged) {
                                $('.reject-mother-info, .approve-mother-info').data('id', r.data.motherInfo.id).data('sid', r.data.motherInfo.sId).removeClass('d-none');

                                $('.reject-all, .approve-all').data('mother-info-id', r.data.motherInfo.id).data('mother-info-sid', r.data.motherInfo.sId).removeClass('d-none');
                            }
                            //else if (r.data.motherInfo.approveStatus == 'approve') {
                            //    $('.recovery-mother-info').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');
                            //}
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>
                        $("#sltCopyDataFrom").selectpicker('val', r.data.parentInfo.copyDataFrom.value);
                        $("#sltParentRelate").selectpicker('val', r.data.parentInfo.parentRelate.value);
                        $("#sltParentTitle").selectpicker('val', r.data.parentInfo.parentTitle.value);
                        $("#iptParentName").val(r.data.parentInfo.parentName.value);
                        $("#iptParentLastName").val(r.data.parentInfo.parentLastName.value);
                        $("#iptParentNameEn").val(r.data.parentInfo.parentNameEn.value);
                        $("#iptParentNameLastEn").val(r.data.parentInfo.parentNameLastEn.value);
                        $("#iptParentBirthday").val(r.data.parentInfo.parentBirthday.value);
                        $("#iptParentIdentification").val(r.data.parentInfo.parentIdentification.value);
                        $("#sltParentRace").selectpicker('val', r.data.parentInfo.parentRace.value);
                        $("#sltParentNation").selectpicker('val', r.data.parentInfo.parentNation.value);
                        $("#sltParentReligion").selectpicker('val', r.data.parentInfo.parentReligion.value);
                        $("#sltParentGraduated").selectpicker('val', r.data.parentInfo.parentGraduated.value);
                        $("#iptParentHomeNo").val(r.data.parentInfo.parentHomeNo.value);
                        $("#iptParentSoi").val(r.data.parentInfo.parentSoi.value);
                        $("#iptParentMoo").val(r.data.parentInfo.parentMoo.value);
                        $("#iptParentRoad").val(r.data.parentInfo.parentRoad.value);
                        $("#sltParentProvince").selectpicker('val', r.data.parentInfo.parentProvince.value);
                        LoadDistrict('#sltParentAmphoe', $("#sltParentProvince").val());
                        $("#sltParentAmphoe").selectpicker('val', r.data.parentInfo.parentAmphoe.value);
                        LoadSubDistrict('#sltParentTombon', $("#sltParentAmphoe").val());
                        $("#sltParentTombon").selectpicker('val', r.data.parentInfo.parentTombon.value);
                        $("#iptParentPostalCode").val(r.data.parentInfo.parentPostalCode.value);
                        $("#sltTuitionReimbursement").selectpicker('val', r.data.parentInfo.tuitionReimbursement.value);
                        $("#sltParentStatus").selectpicker('val', r.data.parentInfo.parentStatus.value);
                        $("#iptParentJob").val(r.data.parentInfo.parentJob.value);
                        $("#iptParentIncome").val(r.data.parentInfo.parentIncome.value);
                        $("#iptParentWorkPlace").val(r.data.parentInfo.parentWorkPlace.value);
                        $("#iptParentPhone").val(r.data.parentInfo.parentPhone.value);
                        $("#iptParentPhone2").val(r.data.parentInfo.parentPhone2.value);
                        $("#iptParentPhone3").val(r.data.parentInfo.parentPhone3.value);

                        if (r.data.parentInfo.requestUpdateData) {
                            if (r.data.parentInfo.parentRelate.isNewValue) $('#sltParentRelate').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentRelate.dbValue ? $('#sltParentRelate option[value=' + r.data.parentInfo.parentRelate.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentTitle.isNewValue) $('#sltParentTitle').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentTitle.dbValue ? $('#sltParentTitle option[value=' + r.data.parentInfo.parentTitle.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentName.isNewValue) $('#iptParentName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentName.dbValue + `</p>`);
                            if (r.data.parentInfo.parentLastName.isNewValue) $('#iptParentLastName').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentLastName.dbValue + `</p>`);
                            if (r.data.parentInfo.parentNameEn.isNewValue) $('#iptParentNameEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentNameEn.dbValue + `</p>`);
                            if (r.data.parentInfo.parentNameLastEn.isNewValue) $('#iptParentNameLastEn').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentNameLastEn.dbValue + `</p>`);
                            if (r.data.parentInfo.parentBirthday.isNewValue) $('#iptParentBirthday').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentBirthday.dbValue + `</p>`);
                            if (r.data.parentInfo.parentIdentification.isNewValue) $('#iptParentIdentification').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentIdentification.dbValue + `</p>`);
                            if (r.data.parentInfo.parentRace.isNewValue) $('#sltParentRace').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentRace.dbValue ? $('#sltParentRace option[value=' + r.data.parentInfo.parentRace.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentNation.isNewValue) $('#sltParentNation').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentNation.dbValue ? $('#sltParentNation option[value=' + r.data.parentInfo.parentNation.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentReligion.isNewValue) $('#sltParentReligion').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentReligion.dbValue ? $('#sltParentReligion option[value=' + r.data.parentInfo.parentReligion.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentGraduated.isNewValue) $('#sltParentGraduated').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentGraduated.dbValue ? $('#sltParentGraduated option[value=' + r.data.parentInfo.parentGraduated.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentHomeNo.isNewValue) $('#iptParentHomeNo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentHomeNo.dbValue + `</p>`);
                            if (r.data.parentInfo.parentSoi.isNewValue) $('#iptParentSoi').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentSoi.dbValue + `</p>`);
                            if (r.data.parentInfo.parentMoo.isNewValue) $('#iptParentMoo').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentMoo.dbValue + `</p>`);
                            if (r.data.parentInfo.parentRoad.isNewValue) $('#iptParentRoad').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentRoad.dbValue + `</p>`);
                            if (r.data.parentInfo.parentProvince.isNewValue) $('#sltParentProvince').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentProvince.dbValue ? $('#sltParentProvince option[value=' + r.data.parentInfo.parentProvince.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentAmphoe.isNewValue) $('#sltParentAmphoe').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentAmphoe.dbValue ? $('#sltParentAmphoe option[value=' + r.data.parentInfo.parentAmphoe.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentTombon.isNewValue) $('#sltParentTombon').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentTombon.dbValue ? $('#sltParentTombon option[value=' + r.data.parentInfo.parentTombon.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentPostalCode.isNewValue) $('#iptParentPostalCode').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentPostalCode.dbValue + `</p>`);
                            if (r.data.parentInfo.tuitionReimbursement.isNewValue) $('#sltTuitionReimbursement').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.tuitionReimbursement.dbValue ? $('#sltTuitionReimbursement option[value=' + r.data.parentInfo.tuitionReimbursement.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentStatus.isNewValue) $('#sltParentStatus').addClass('new-data').parent().after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + (!!r.data.parentInfo.parentStatus.dbValue ? $('#sltParentStatus option[value=' + r.data.parentInfo.parentStatus.dbValue + ']').text() : '') + `</p>`);
                            if (r.data.parentInfo.parentJob.isNewValue) $('#iptParentJob').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentJob.dbValue + `</p>`);
                            if (r.data.parentInfo.parentIncome.isNewValue) $('#iptParentIncome').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentIncome.dbValue + `</p>`);
                            if (r.data.parentInfo.parentWorkPlace.isNewValue) $('#iptParentWorkPlace').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentWorkPlace.dbValue + `</p>`);
                            if (r.data.parentInfo.parentPhone.isNewValue) $('#iptParentPhone').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentPhone.dbValue + `</p>`);
                            if (r.data.parentInfo.parentPhone2.isNewValue) $('#iptParentPhone2').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentPhone2.dbValue + `</p>`);
                            if (r.data.parentInfo.parentPhone3.isNewValue) $('#iptParentPhone3').addClass('new-data').after(`<p class="old-data"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101366") %> : ` + r.data.parentInfo.parentPhone3.dbValue + `</p>`);

                            if (r.data.parentInfo.approveStatus == 'pending' && r.data.parentInfo.dataChanged) {
                                $('.reject-parent-info, .approve-parent-info').data('id', r.data.parentInfo.id).data('sid', r.data.parentInfo.sId).removeClass('d-none');

                                $('.reject-all, .approve-all').data('parent-info-id', r.data.parentInfo.id).data('parent-info-sid', r.data.parentInfo.sId).removeClass('d-none');
                            }
                            //else if (r.data.parentInfo.approveStatus == 'approve') {
                            //    $('.recovery-parent-info').data('id', r.data.profile.id).data('sid', r.data.profile.sId).removeClass('d-none');
                            //}
                        }
                    }
                    else {
                        var title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                        var body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111005") %> [' + r.message + ']';

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').html(body);
                        $("#modalNotifyOnlyClose").modal('show');

                    }
                }
            }

            $("#sltRegisterHomeProvince, #sltBornFromProvince, #sltHomeProvince, #sltFatherProvince, #sltMotherProvince, #sltParentProvince").change(function () {

                LoadDistrict($(this).data('element-district'), $(this).val());

            });

            $("#sltRegisterHomeAmphoe, #sltBornFromAmphoe, #sltHomeAmphoe, #sltFatherAmphoe, #sltMotherAmphoe, #sltParentAmphoe").change(function () {

                LoadSubDistrict($(this).data('element-subdistrict'), $(this).val());

            });


            $('.approve-all').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101376") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var profileEdited = {};
                    if ($('.approve-all').data('profile-id')) {
                        profileEdited = {
                            id: $('.approve-all').data('profile-id'),
                            studentId: $('.approve-all').data('profile-sid'),
                            studentGender: $('#sltStudentGender').val(),
                            studentTitle: $('#sltStudentTitle').val(),
                            studentFirstNameTh: $('#iptStudentFirstNameTh').val(),
                            studentLastNameTh: $('#iptStudentLastNameTh').val(),
                            studentFirstNameEn: $('#iptStudentFirstNameEn').val(),
                            studentLastNameEn: $('#iptStudentLastNameEn').val(),
                            studentFirstNameOther: $('#iptStudentFirstNameOther').val(),
                            studentLastNameOther: $('#iptStudentLastNameOther').val(),
                            studentNickNameTh: $('#iptStudentNickNameTh').val(),
                            studentNickNameEn: $('#iptStudentNickNameEn').val(),
                            studentBirthday: $('#iptStudentBirthday').val(),
                            studentRace: $('#sltStudentRace').val(),
                            studentNation: $('#sltStudentNation').val(),
                            studentReligion: $('#sltStudentReligion').val(),
                            disabilityCode: $('#sltDisabilityCode').val(),
                            disadvantageCode: $('#sltDisadvantageCode').val(),
                            studentPhone: $('#iptStudentPhone').val(),
                            studentEmail: $('#iptStudentEmail').val(),
                            studentSonTotal: $('#sltStudentSonTotal').val(),
                            studentSonNumber: $('#iptStudentSonNumber').val(),
                            studentBrethrenStudyHere: $('#sltStudentBrethrenStudyHere').val(),
                            Note2: $('#iptNote2').val()
                        };
                    }

                    var permanentAddressEdited = {};
                    if ($('.approve-all').data('permanent-address-id')) {
                        permanentAddressEdited = {
                            id: $('.approve-all').data('permanent-address-id'),
                            studentId: $('.approve-all').data('permanent-address-sid'),
                            registerHomeCode: $('#iptRegisterHomeCode').val(),
                            registerHomeNo: $('#iptRegisterHomeNo').val(),
                            registerHomeSoi: $('#iptRegisterHomeSoi').val(),
                            registerHomeMoo: $('#iptRegisterHomeMoo').val(),
                            registerHomeRoad: $('#iptRegisterHomeRoad').val(),
                            registerHomeProvince: $('#sltRegisterHomeProvince').val(),
                            registerHomeAmphoe: $('#sltRegisterHomeAmphoe').val(),
                            registerHomeTombon: $('#sltRegisterHomeTombon').val(),
                            registerHomePostalCode: $('#iptRegisterHomePostalCode').val(),
                            registerHomePhone: $('#iptRegisterHomePhone').val(),
                            bornFrom: $('#iptBornFrom').val(),
                            bornFromProvince: $('#sltBornFromProvince').val(),
                            bornFromAmphoe: $('#sltBornFromAmphoe').val(),
                            bornFromTombon: $('#sltBornFromTombon').val()
                        };
                    }

                    var contactAddressEdited = {};
                    if ($('.approve-all').data('contact-address-id')) {
                        contactAddressEdited = {
                            id: $('.approve-all').data('contact-address-id'),
                            studentId: $('.approve-all').data('contact-address-sid'),
                            homeNo: $('#iptHomeNo').val(),
                            homeSoi: $('#iptHomeSoi').val(),
                            homeMoo: $('#iptHomeMoo').val(),
                            homeRoad: $('#iptHomeRoad').val(),
                            homeProvince: $('#sltHomeProvince').val(),
                            homeAmphoe: $('#sltHomeAmphoe').val(),
                            homeTombon: $('#sltHomeTombon').val(),
                            homePostalCode: $('#iptHomePostalCode').val(),
                            homePhone: $('#iptHomePhone').val(),
                            homeStayWithTitle: $('#sltHomeStayWithTitle').val(),
                            homeStayWithName: $('#iptHomeStayWithName').val(),
                            homeStayWithLast: $('#iptHomeStayWithLast').val(),
                            homeStayWithEmergencyCall: $('#iptHomeStayWithEmergencyCall').val(),
                            homeStayWithEmergencyEmail: $('#iptHomeStayWithEmergencyEmail').val(),
                            homeFriendName: $('#iptHomeFriendName').val(),
                            homeFriendLastName: $('#iptHomeFriendLastName').val(),
                            homeFriendPhone: $('#iptHomeFriendPhone').val(),
                            homeHomeType: $('#sltHomeHomeType').val()
                        };
                    }

                    var fatherInfoEdited = {};
                    if ($('.approve-all').data('father-info-id')) {
                        fatherInfoEdited = {
                            id: $('.approve-all').data('father-info-id'),
                            studentId: $('.approve-all').data('father-info-sid'),
                            fatherTitle: $('#sltFatherTitle').val(),
                            fatherName: $('#iptFatherName').val(),
                            fatherLastName: $('#iptFatherLastName').val(),
                            fatherNameEn: $('#iptFatherNameEn').val(),
                            fatherNameLastEn: $('#iptFatherNameLastEn').val(),
                            fatherBirthday: $('#iptFatherBirthday').val(),
                            fatherIdentification: $('#iptFatherIdentification').val(),
                            fatherRace: $('#sltFatherRace').val(),
                            fatherNation: $('#sltFatherNation').val(),
                            fatherReligion: $('#sltFatherReligion').val(),
                            fatherGraduated: $('#sltFatherGraduated').val(),
                            fatherHomeNo: $('#iptFatherHomeNo').val(),
                            fatherSoi: $('#iptFatherSoi').val(),
                            fatherMoo: $('#iptFatherMoo').val(),
                            fatherRoad: $('#iptFatherRoad').val(),
                            fatherProvince: $('#sltFatherProvince').val(),
                            fatherAmphoe: $('#sltFatherAmphoe').val(),
                            fatherTombon: $('#sltFatherTombon').val(),
                            fatherPostalCode: $('#iptFatherPostalCode').val(),
                            fatherJob: $('#iptFatherJob').val(),
                            fatherIncome: $('#iptFatherIncome').val(),
                            fatherWorkPlace: $('#iptFatherWorkPlace').val(),
                            fatherPhone: $('#iptFatherPhone').val(),
                            fatherPhone2: $('#iptFatherPhone2').val(),
                            fatherPhone3: $('#iptFatherPhone3').val()
                        };
                    }

                    var motherInfoEdited = {};
                    if ($('.approve-all').data('mother-info-id')) {
                        motherInfoEdited = {
                            id: $('.approve-all').data('mother-info-id'),
                            studentId: $('.approve-all').data('mother-info-sid'),
                            motherTitle: $('#sltMotherTitle').val(),
                            motherName: $('#iptMotherName').val(),
                            motherLastName: $('#iptMotherLastName').val(),
                            motherNameEn: $('#iptMotherNameEn').val(),
                            motherNameLastEn: $('#iptMotherNameLastEn').val(),
                            motherBirthday: $('#iptMotherBirthday').val(),
                            motherIdentification: $('#iptMotherIdentification').val(),
                            motherRace: $('#sltMotherRace').val(),
                            motherNation: $('#sltMotherNation').val(),
                            motherReligion: $('#sltMotherReligion').val(),
                            motherGraduated: $('#sltMotherGraduated').val(),
                            motherHomeNo: $('#iptMotherHomeNo').val(),
                            motherSoi: $('#iptMotherSoi').val(),
                            motherMoo: $('#iptMotherMoo').val(),
                            motherRoad: $('#iptMotherRoad').val(),
                            motherProvince: $('#sltMotherProvince').val(),
                            motherAmphoe: $('#sltMotherAmphoe').val(),
                            motherTombon: $('#sltMotherTombon').val(),
                            motherPostalCode: $('#iptMotherPostalCode').val(),
                            motherJob: $('#iptMotherJob').val(),
                            motherIncome: $('#iptMotherIncome').val(),
                            motherWorkPlace: $('#iptMotherWorkPlace').val(),
                            motherPhone: $('#iptMotherPhone').val(),
                            motherPhone2: $('#iptMotherPhone2').val(),
                            motherPhone3: $('#iptMotherPhone3').val()
                        };
                    }

                    var parentInfoEdited = {};
                    if ($('.approve-all').data('parent-info-id')) {
                        parentInfoEdited = {
                            id: $('.approve-all').data('parent-info-id'),
                            studentId: $('.approve-all').data('parent-info-sid'),
                            parentRelate: $('#sltParentRelate').val(),
                            parentTitle: $('#sltParentTitle').val(),
                            parentName: $('#iptParentName').val(),
                            parentLastName: $('#iptParentLastName').val(),
                            parentNameEn: $('#iptParentNameEn').val(),
                            parentNameLastEn: $('#iptParentNameLastEn').val(),
                            parentBirthday: $('#iptParentBirthday').val(),
                            parentIdentification: $('#iptParentIdentification').val(),
                            parentRace: $('#sltParentRace').val(),
                            parentNation: $('#sltParentNation').val(),
                            parentReligion: $('#sltParentReligion').val(),
                            parentGraduated: $('#sltParentGraduated').val(),
                            parentHomeNo: $('#iptParentHomeNo').val(),
                            parentSoi: $('#iptParentSoi').val(),
                            parentMoo: $('#iptParentMoo').val(),
                            parentRoad: $('#iptParentRoad').val(),
                            parentProvince: $('#sltParentProvince').val(),
                            parentAmphoe: $('#sltParentAmphoe').val(),
                            parentTombon: $('#sltParentTombon').val(),
                            parentPostalCode: $('#iptParentPostalCode').val(),
                            tuitionReimbursement: $('#sltTuitionReimbursement').val(),
                            parentStatus: $('#sltParentStatus').val(),
                            parentJob: $('#iptParentJob').val(),
                            parentIncome: $('#iptParentIncome').val(),
                            parentWorkPlace: $('#iptParentWorkPlace').val(),
                            parentPhone: $('#iptParentPhone').val(),
                            parentPhone2: $('#iptParentPhone2').val(),
                            parentPhone3: $('#iptParentPhone3').val()
                        };
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApproveAll",
                        data: JSON.stringify({ profileEdited, permanentAddressEdited, contactAddressEdited, fatherInfoEdited, motherInfoEdited, parentInfoEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101377") %>';

                                $('.reject-all, .approve-all, .reject-profile, .approve-profile, .reject-permanent-address, .approve-permanent-address, .reject-contact-address, .approve-contact-address, .reject-father-info, .approve-father-info, .reject-mother-info, .approve-mother-info, .reject-parent-info, .approve-parent-info').addClass('d-none');

                                // After close message popup
                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                    // Redirect to student list
                                    window.location.replace("ApprovalUpdateStudentDataList.aspx");
                                });
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111006") %>[' + r.section + '][' + r.message + ']';
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
                });
            });

            $('.approve-profile').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101367") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var profileEdited = {
                        id: $('.approve-profile').data('id'),
                        studentId: $('.approve-profile').data('sid'),
                        studentGender: $('#sltStudentGender').val(),
                        studentTitle: $('#sltStudentTitle').val(),
                        studentFirstNameTh: $('#iptStudentFirstNameTh').val(),
                        studentLastNameTh: $('#iptStudentLastNameTh').val(),
                        studentFirstNameEn: $('#iptStudentFirstNameEn').val(),
                        studentLastNameEn: $('#iptStudentLastNameEn').val(),
                        studentFirstNameOther: $('#iptStudentFirstNameOther').val(),
                        studentLastNameOther: $('#iptStudentLastNameOther').val(),
                        studentNickNameTh: $('#iptStudentNickNameTh').val(),
                        studentNickNameEn: $('#iptStudentNickNameEn').val(),
                        studentBirthday: $('#iptStudentBirthday').val(),
                        studentRace: $('#sltStudentRace').val(),
                        studentNation: $('#sltStudentNation').val(),
                        studentReligion: $('#sltStudentReligion').val(),
                        disabilityCode: $('#sltDisabilityCode').val(),
                        disadvantageCode: $('#sltDisadvantageCode').val(),
                        studentPhone: $('#iptStudentPhone').val(),
                        studentEmail: $('#iptStudentEmail').val(),
                        studentSonTotal: $('#sltStudentSonTotal').val(),
                        studentSonNumber: $('#iptStudentSonNumber').val(),
                        studentBrethrenStudyHere: $('#sltStudentBrethrenStudyHere').val(),
                        Note2: $('#iptNote2').val()
                    };

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApproveProfile",
                        data: JSON.stringify({ profileEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101368") %>';

                                $('.reject-profile, .approve-profile').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133341") %>[' + r.message + ']';
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
                });
            });

            $('.approve-permanent-address').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101367") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var permanentAddressEdited = {
                        id: $('.approve-permanent-address').data('id'),
                        studentId: $('.approve-permanent-address').data('sid'),
                        registerHomeCode: $('#iptRegisterHomeCode').val(),
                        registerHomeNo: $('#iptRegisterHomeNo').val(),
                        registerHomeSoi: $('#iptRegisterHomeSoi').val(),
                        registerHomeMoo: $('#iptRegisterHomeMoo').val(),
                        registerHomeRoad: $('#iptRegisterHomeRoad').val(),
                        registerHomeProvince: $('#sltRegisterHomeProvince').val(),
                        registerHomeAmphoe: $('#sltRegisterHomeAmphoe').val(),
                        registerHomeTombon: $('#sltRegisterHomeTombon').val(),
                        registerHomePostalCode: $('#iptRegisterHomePostalCode').val(),
                        registerHomePhone: $('#iptRegisterHomePhone').val(),
                        bornFrom: $('#iptBornFrom').val(),
                        bornFromProvince: $('#sltBornFromProvince').val(),
                        bornFromAmphoe: $('#sltBornFromAmphoe').val(),
                        bornFromTombon: $('#sltBornFromTombon').val()
                    };

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApprovePermanentAddress",
                        data: JSON.stringify({ permanentAddressEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101368") %>';

                                $('.reject-permanent-address, .approve-permanent-address').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133341") %>[' + r.message + ']';
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
                });
            });

            $('.approve-contact-address').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101367") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var contactAddressEdited = {
                        id: $('.approve-contact-address').data('id'),
                        studentId: $('.approve-contact-address').data('sid'),
                        homeNo: $('#iptHomeNo').val(),
                        homeSoi: $('#iptHomeSoi').val(),
                        homeMoo: $('#iptHomeMoo').val(),
                        homeRoad: $('#iptHomeRoad').val(),
                        homeProvince: $('#sltHomeProvince').val(),
                        homeAmphoe: $('#sltHomeAmphoe').val(),
                        homeTombon: $('#sltHomeTombon').val(),
                        homePostalCode: $('#iptHomePostalCode').val(),
                        homePhone: $('#iptHomePhone').val(),
                        homeStayWithTitle: $('#sltHomeStayWithTitle').val(),
                        homeStayWithName: $('#iptHomeStayWithName').val(),
                        homeStayWithLast: $('#iptHomeStayWithLast').val(),
                        homeStayWithEmergencyCall: $('#iptHomeStayWithEmergencyCall').val(),
                        homeStayWithEmergencyEmail: $('#iptHomeStayWithEmergencyEmail').val(),
                        homeFriendName: $('#iptHomeFriendName').val(),
                        homeFriendLastName: $('#iptHomeFriendLastName').val(),
                        homeFriendPhone: $('#iptHomeFriendPhone').val(),
                        homeHomeType: $('#sltHomeHomeType').val()
                    };

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApproveContactAddress",
                        data: JSON.stringify({ contactAddressEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101368") %>';

                                $('.reject-contact-address, .approve-contact-address').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133341") %>[' + r.message + ']';
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
                });
            });

            $('.approve-father-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101367") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var fatherInfoEdited = {
                        id: $('.approve-father-info').data('id'),
                        studentId: $('.approve-father-info').data('sid'),
                        fatherTitle: $('#sltFatherTitle').val(),
                        fatherName: $('#iptFatherName').val(),
                        fatherLastName: $('#iptFatherLastName').val(),
                        fatherNameEn: $('#iptFatherNameEn').val(),
                        fatherNameLastEn: $('#iptFatherNameLastEn').val(),
                        fatherBirthday: $('#iptFatherBirthday').val(),
                        fatherIdentification: $('#iptFatherIdentification').val(),
                        fatherRace: $('#sltFatherRace').val(),
                        fatherNation: $('#sltFatherNation').val(),
                        fatherReligion: $('#sltFatherReligion').val(),
                        fatherGraduated: $('#sltFatherGraduated').val(),
                        fatherHomeNo: $('#iptFatherHomeNo').val(),
                        fatherSoi: $('#iptFatherSoi').val(),
                        fatherMoo: $('#iptFatherMoo').val(),
                        fatherRoad: $('#iptFatherRoad').val(),
                        fatherProvince: $('#sltFatherProvince').val(),
                        fatherAmphoe: $('#sltFatherAmphoe').val(),
                        fatherTombon: $('#sltFatherTombon').val(),
                        fatherPostalCode: $('#iptFatherPostalCode').val(),
                        fatherJob: $('#iptFatherJob').val(),
                        fatherIncome: $('#iptFatherIncome').val(),
                        fatherWorkPlace: $('#iptFatherWorkPlace').val(),
                        fatherPhone: $('#iptFatherPhone').val(),
                        fatherPhone2: $('#iptFatherPhone2').val(),
                        fatherPhone3: $('#iptFatherPhone3').val()
                    };

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApproveFatherInfo",
                        data: JSON.stringify({ fatherInfoEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101368") %>';

                                $('.reject-father-info, .approve-father-info').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133341") %>[' + r.message + ']';
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
                });
            });

            $('.approve-mother-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101367") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var motherInfoEdited = {
                        id: $('.approve-mother-info').data('id'),
                        studentId: $('.approve-mother-info').data('sid'),
                        motherTitle: $('#sltMotherTitle').val(),
                        motherName: $('#iptMotherName').val(),
                        motherLastName: $('#iptMotherLastName').val(),
                        motherNameEn: $('#iptMotherNameEn').val(),
                        motherNameLastEn: $('#iptMotherNameLastEn').val(),
                        motherBirthday: $('#iptMotherBirthday').val(),
                        motherIdentification: $('#iptMotherIdentification').val(),
                        motherRace: $('#sltMotherRace').val(),
                        motherNation: $('#sltMotherNation').val(),
                        motherReligion: $('#sltMotherReligion').val(),
                        motherGraduated: $('#sltMotherGraduated').val(),
                        motherHomeNo: $('#iptMotherHomeNo').val(),
                        motherSoi: $('#iptMotherSoi').val(),
                        motherMoo: $('#iptMotherMoo').val(),
                        motherRoad: $('#iptMotherRoad').val(),
                        motherProvince: $('#sltMotherProvince').val(),
                        motherAmphoe: $('#sltMotherAmphoe').val(),
                        motherTombon: $('#sltMotherTombon').val(),
                        motherPostalCode: $('#iptMotherPostalCode').val(),
                        motherJob: $('#iptMotherJob').val(),
                        motherIncome: $('#iptMotherIncome').val(),
                        motherWorkPlace: $('#iptMotherWorkPlace').val(),
                        motherPhone: $('#iptMotherPhone').val(),
                        motherPhone2: $('#iptMotherPhone2').val(),
                        motherPhone3: $('#iptMotherPhone3').val()
                    };

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApproveMotherInfo",
                        data: JSON.stringify({ motherInfoEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101368") %>';

                                $('.reject-mother-info, .approve-mother-info').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133341") %>[' + r.message + ']';
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
                });
            });

            $('.approve-parent-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101367") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var parentInfoEdited = {
                        id: $('.approve-parent-info').data('id'),
                        studentId: $('.approve-parent-info').data('sid'),
                        parentRelate: $('#sltParentRelate').val(),
                        parentTitle: $('#sltParentTitle').val(),
                        parentName: $('#iptParentName').val(),
                        parentLastName: $('#iptParentLastName').val(),
                        parentNameEn: $('#iptParentNameEn').val(),
                        parentNameLastEn: $('#iptParentNameLastEn').val(),
                        parentBirthday: $('#iptParentBirthday').val(),
                        parentIdentification: $('#iptParentIdentification').val(),
                        parentRace: $('#sltParentRace').val(),
                        parentNation: $('#sltParentNation').val(),
                        parentReligion: $('#sltParentReligion').val(),
                        parentGraduated: $('#sltParentGraduated').val(),
                        parentHomeNo: $('#iptParentHomeNo').val(),
                        parentSoi: $('#iptParentSoi').val(),
                        parentMoo: $('#iptParentMoo').val(),
                        parentRoad: $('#iptParentRoad').val(),
                        parentProvince: $('#sltParentProvince').val(),
                        parentAmphoe: $('#sltParentAmphoe').val(),
                        parentTombon: $('#sltParentTombon').val(),
                        parentPostalCode: $('#iptParentPostalCode').val(),
                        tuitionReimbursement: $('#sltTuitionReimbursement').val(),
                        parentStatus: $('#sltParentStatus').val(),
                        parentJob: $('#iptParentJob').val(),
                        parentIncome: $('#iptParentIncome').val(),
                        parentWorkPlace: $('#iptParentWorkPlace').val(),
                        parentPhone: $('#iptParentPhone').val(),
                        parentPhone2: $('#iptParentPhone2').val(),
                        parentPhone3: $('#iptParentPhone3').val()
                    };

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveApproveParentInfo",
                        data: JSON.stringify({ parentInfoEdited }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101368") %>';

                                $('.reject-parent-info, .approve-parent-info').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133341") %>[' + r.message + ']';
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
                });
            });


            $('.recovery-profile').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111009") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/RecoveryProfile",
                        data: JSON.stringify({ id: $('.recovery-profile').data('id'), sid: $('.recovery-profile').data('sid') }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111007") %>';
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111008") %>[' + r.message + ']';
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
                });
            });

            $('.recovery-permanent-address').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111009") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/RecoveryPermanentAddress",
                        data: JSON.stringify({ id: $('.recovery-permanent-address').data('id'), sid: $('.recovery-permanent-address').data('sid') }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111007") %>';
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111008") %>[' + r.message + ']';
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
                });
            });

            $('.recovery-contact-address').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111009") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/RecoveryContactAddress",
                        data: JSON.stringify({ id: $('.recovery-contact-address').data('id'), sid: $('.recovery-contact-address').data('sid') }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111007") %>';
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111008") %>[' + r.message + ']';
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
                });
            });

            $('.recovery-father-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111009") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/RecoveryFatherInfo",
                        data: JSON.stringify({ id: $('.recovery-father-info').data('id'), sid: $('.recovery-father-info').data('sid') }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111007") %>';
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111008") %>[' + r.message + ']';
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
                });
            });

            $('.recovery-mother-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111009") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/RecoveryMotherInfo",
                        data: JSON.stringify({ id: $('.recovery-mother-info').data('id'), sid: $('.recovery-mother-info').data('sid') }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111007") %>';
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111008") %>[' + r.message + ']';
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
                });
            });

            $('.recovery-parent-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111009") %>');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/RecoveryParentInfo",
                        data: JSON.stringify({ id: $('.recovery-parent-info').data('id'), sid: $('.recovery-parent-info').data('sid') }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111007") %>';
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111008") %>[' + r.message + ']';
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
                });
            });


            $('.reject-all').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101373") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101374") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var profileReject = {};
                    if ($('.reject-all').data('profile-id')) {
                        profileReject = {
                            id: $('.reject-all').data('profile-id'),
                            studentId: $('.reject-all').data('profile-sid'),
                            comment: $('#iptApproveComment').val()
                        }
                    }

                    var permanentAddressReject = {};
                    if ($('.reject-all').data('permanent-address-id')) {
                        permanentAddressReject = {
                            id: $('.reject-all').data('permanent-address-id'),
                            studentId: $('.reject-all').data('permanent-address-sid'),
                            comment: $('#iptApproveComment').val()
                        }
                    }

                    var contactAddressReject = {};
                    if ($('.reject-all').data('contact-address-id')) {
                        contactAddressReject = {
                            id: $('.reject-all').data('contact-address-id'),
                            studentId: $('.reject-all').data('contact-address-sid'),
                            comment: $('#iptApproveComment').val()
                        }
                    }

                    var fatherInfoReject = {};
                    if ($('.reject-all').data('father-info-id')) {
                        fatherInfoReject = {
                            id: $('.reject-all').data('father-info-id'),
                            studentId: $('.reject-all').data('father-info-sid'),
                            comment: $('#iptApproveComment').val()
                        }
                    }

                    var motherInfoReject = {};
                    if ($('.reject-all').data('mother-info-id')) {
                        motherInfoReject = {
                            id: $('.reject-all').data('mother-info-id'),
                            studentId: $('.reject-all').data('mother-info-sid'),
                            comment: $('#iptApproveComment').val()
                        }
                    }

                    var parentInfoReject = {};
                    if ($('.reject-all').data('parent-info-id')) {
                        parentInfoReject = {
                            id: $('.reject-all').data('parent-info-id'),
                            studentId: $('.reject-all').data('parent-info-sid'),
                            comment: $('#iptApproveComment').val()
                        }
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectAll",
                        data: JSON.stringify({ profileReject, permanentAddressReject, contactAddressReject, fatherInfoReject, motherInfoReject, parentInfoReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101375") %>';

                                $('.reject-all, .approve-all, .reject-profile, .approve-profile, .reject-permanent-address, .approve-permanent-address, .reject-contact-address, .approve-contact-address, .reject-father-info, .approve-father-info, .reject-mother-info, .approve-mother-info, .reject-parent-info, .approve-parent-info').addClass('d-none');

                                // After close message popup
                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                    // Redirect to student list
                                    window.location.replace("ApprovalUpdateStudentDataList.aspx");
                                });
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111010") %>[' + r.message + ']';
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
                });
            });

            $('.reject-profile').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101370") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101371") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var profileReject = {
                        id: $('.reject-profile').data('id'),
                        studentId: $('.reject-profile').data('sid'),
                        comment: $('#iptApproveComment').val()
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectProfile",
                        data: JSON.stringify({ profileReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101372") %>';

                                $('.reject-profile, .approve-profile').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111011") %>[' + r.message + ']';
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
                });
            });

            $('.reject-permanent-address').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101370") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101371") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var permanentAddressReject = {
                        id: $('.reject-permanent-address').data('id'),
                        studentId: $('.reject-permanent-address').data('sid'),
                        comment: $('#iptApproveComment').val()
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectPermanentAddress",
                        data: JSON.stringify({ permanentAddressReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101372") %>';

                                $('.reject-permanent-address, .approve-permanent-address').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111011") %>[' + r.message + ']';
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
                });
            });

            $('.reject-contact-address').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101370") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101371") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var contactAddressReject = {
                        id: $('.reject-contact-address').data('id'),
                        studentId: $('.reject-contact-address').data('sid'),
                        comment: $('#iptApproveComment').val()
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectContactAddress",
                        data: JSON.stringify({ contactAddressReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101372") %>';

                                $('.reject-contact-address, .approve-contact-address').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111011") %>[' + r.message + ']';
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
                });
            });

            $('.reject-father-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101370") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101371") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var fatherInfoReject = {
                        id: $('.reject-father-info').data('id'),
                        studentId: $('.reject-father-info').data('sid'),
                        comment: $('#iptApproveComment').val()
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectFatherInfo",
                        data: JSON.stringify({ fatherInfoReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101372") %>';

                                $('.reject-father-info, .approve-father-info').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111011") %>[' + r.message + ']';
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
                });
            });

            $('.reject-mother-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101370") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101371") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var motherInfoReject = {
                        id: $('.reject-mother-info').data('id'),
                        studentId: $('.reject-mother-info').data('sid'),
                        comment: $('#iptApproveComment').val()
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectMotherInfo",
                        data: JSON.stringify({ motherInfoReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101372") %>';

                                $('.reject-mother-info, .approve-mother-info').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111011") %>[' + r.message + ']';
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
                });
            });

            $('.reject-parent-info').click(function () {
                $('#modalNotifyConfirmSave').find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101370") %>');
                $('#modalNotifyConfirmSave').find('.modal-body p').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101371") %>: <input type="text" class="form-control" id="iptApproveComment" name="iptApproveComment" maxlength="100" />');
                $('#modalNotifyConfirmSave').modal('show');

                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                    $('#modalNotifyConfirmSave').modal('hide');

                    $("#modalWaitDialog").modal('show');

                    // Prepare data
                    var parentInfoReject = {
                        id: $('.reject-parent-info').data('id'),
                        studentId: $('.reject-parent-info').data('sid'),
                        comment: $('#iptApproveComment').val()
                    }

                    // Save command
                    $.ajax({
                        async: false,
                        type: "POST",
                        url: "ApprovalStudentDataForm.aspx/SaveRejectParentInfo",
                        data: JSON.stringify({ parentInfoReject }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var title = "";
                            var body = "";

                            var r = JSON.parse(response.d);
                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101372") %>';

                                $('.reject-parent-info, .approve-parent-info').addClass('d-none');
                            }
                            else {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111011") %>[' + r.message + ']';
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
                });
            });

            // Modal Section
            $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                //$(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111017") %>');
                //$(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
            });

            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function () {
                if (($("#modalWaitDialog").data('bs.modal') || {})._isShown) {
                    setTimeout(function () {
                        $("#modalWaitDialog").modal('hide');
                    }, 1000);
                }
            });

            // Initial data
            $('#iptStudentSonNumber').number(true, 0);

            $('.approval-form .datepicker').datetimepicker({
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

            $(".approval-form .datepicker").attr('maxlength', '10');

            // Load info command
            approvalForm.GetItem('<%=Request.QueryString["sid"]%>', '<%=Request.QueryString["requestDate"]%>');

            // Disable all input
            $(".form-control, .selectpicker").prop("disabled", true);
            $('.selectpicker').selectpicker('refresh');
        });
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
