<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="NewsEditForm.aspx.cs" Inherits="FingerprintPayment.Message.NewsEditForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-form.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Content/hummingbird-treeview/hummingbird-treeview.min.css" rel="stylesheet" type="text/css" />
    <style>
        .material-form .form-control {
            padding: 9px 0px 7px 5px;
            margin-top: 0px;
            line-height: 1.7 !important;
        }

        .file-drop-area {
            position: relative;
            text-align: center;
            padding: 25px;
            border: 1px dashed gray;
            border-radius: 5px;
            transition: 0.2s;
        }

            .file-drop-area.file-drag-over {
                color: #aaa;
                border-color: #aaa;
            }

        .choose-file-button {
            background-color: rgba(255, 255, 255, 0.04);
            border: 1px solid rgba(255, 255, 255, 0.1);
            border-radius: 3px;
            margin-right: 10px;
            font-size: 12px;
            text-transform: uppercase;
            vertical-align: middle;
        }

            .choose-file-button .material-icons {
                font-size: 38px;
            }

        .file-message {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis
        }

            .file-message span {
                text-decoration-line: underline;
                text-decoration-color: gray;
                text-underline-offset: 2px;
            }

        .file-input {
            position: absolute;
            left: 0;
            top: 0;
            height: 100%;
            width: 100%;
            cursor: pointer;
            opacity: 0
        }

        .files-uploaded-list {
            margin: 5px 0px 5px 0px;
        }

        .files-uploaded {
            position: relative;
            text-align: center;
            padding: 8px;
            border: 1px solid gray;
            border-radius: 5px;
            transition: 0.2s;
            display: inline-block;
            width: 100%;
        }

            .files-uploaded img {
                width: 65px;
                height: 65px;
                float: left;
            }

            .files-uploaded p.file-name {
                float: left;
                margin-left: 20px;
                overflow-wrap: break-word;
                text-align: left;
                width: 85%;
            }

            .files-uploaded .file-remove {
                float: right;
                margin-top: 2px;
                padding: 0px;
            }

            .files-uploaded .file-size {
                float: right;
                margin: 20px 10px 0px 0px;
            }

        .bootstrap-autocomplete.dropdown-menu {
            margin-left: -15px;
        }

        .user-table tr th, .user-list table tr th {
            font-weight: bold !important;
        }

        .user-table tr th, .user-table tr td {
            text-align: center;
        }

            .user-table tr td:nth-child(3) {
                text-align: left;
            }

        .form-check .form-check-label label {
            padding-right: 15px;
            color: black;
        }

        .modal-content .col-form-label span {
            color: black;
            font-weight: bold;
        }

        .modal-content .col-form-label {
            padding-bottom: 0px;
        }

        .modal-content p {
            margin: 0px;
            /*padding-left: 7px;*/
        }

        .file-list .files-uploaded {
            width: auto;
            border: 1px solid #eee;
            margin-right: 5px;
        }

            .file-list .files-uploaded .file-name {
                float: inherit;
                width: auto;
            }

        #tarMessage-error {
            margin-top: 8px;
            display: block;
        }

        .norow-message td {
            color: red;
        }

        @media (min-width: 320px) and (max-width: 767px) {
            .div-bag {
                width: fit-content;
                float: left;
                text-align: left;
            }
        }

        @media (min-width: 1200px) {
            .modal-xl {
                max-width: 950px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01561") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401011") %>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card material-form">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">email</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401011") %></h4>
                </div>
                <div class="card-body">
                    <div>
                        <form id="frmNewsForm" class="form-padding">
                            <div class="row">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401045") %><span class="required">*</span> : </span></label>
                                    <input id="iptTitle" name="iptTitle" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401012") %>" maxlength="250">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 mr-auto ml-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401046") %><span class="required">*</span> : </span></label>
                                    </div>
                                    <select id="sltMessageType" name="sltMessageType" class="selectpicker" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401014") %>">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401015") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401016") %></option>
                                    </select>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401047") %><span class="required">*</span> : </span></label>
                                    <select id="sltSendType" name="sltSendType" class="selectpicker" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401017") %>">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401018") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401019") %></option>
                                    </select>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401055") %><span class="required">*</span> : </span></label>
                                    <select id="sltAcceptType" name="sltAcceptType" class="selectpicker" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401021") %>">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401022") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401023") %></option>
                                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></option>
                                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401025") %></option>
                                        <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401026") %></option>
                                        <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401027") %></option>
                                        <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401028") %></option>
                                        <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401029") %></option>
                                        <asp:Literal ID="ltrAcceptType" runat="server" />
                                    </select>
                                </div>
                            </div>
                            <div class="row date-time" style="display: none;">
                                <div class="col-md-4 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132454") %><span class="required">*</span> : </span></label>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group div-datepicker">
                                                <input id="iptDate" name="iptDate" type="text" class="form-control datepicker" value="<%=DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH")) %>" />
                                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                                    <i class="material-icons">today</i>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group div-datepicker">
                                                <input id="iptTime" name="iptTime" type="text" class="form-control timepicker" value="<%=DateTime.Now.ToString("HH:mm", new System.Globalization.CultureInfo("th-TH")) %>">
                                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                                    <i class="material-icons">schedule</i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                    <div class=""></div>
                                    <div class=""></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 mr-auto ml-auto checkbox-radios">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401048") %><span class="required">*</span> : </span></label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="radio" name="iptGroupType" id="iptGroupType1" value="1" checked>
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401031") %></label>
                                            <span class="circle">
                                                <span class="check"></span>
                                            </span>
                                        </label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="radio" name="iptGroupType" id="iptGroupType2" value="2">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206367") %></label>
                                            <span class="circle">
                                                <span class="check"></span>
                                            </span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-4 ml-auto mr-auto send-group">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401049") %><span class="required">*</span> : </span></label>
                                    </div>
                                    <select id="sltGroup" name="sltGroup" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401034") %>">
                                        <asp:Literal ID="ltrGroup" runat="server" />
                                    </select>
                                </div>
                                <div class="col-md-4 ml-auto mr-auto send-individual" style="display: none;">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %><span class="required">*</span> : </span></label>
                                    </div>
                                    <select id="sltIndividualType" name="sltIndividualType" class="selectpicker" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106160") %>">
                                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %></option>
                                    </select>
                                </div>
                            </div>
                            <div class="row send-individual-student-ignore" style="display: none;">
                                <div class="col-md-4 ml-auto mr-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> : </span></label>
                                    </div>
                                    <select id="sltLevel" name="sltLevel" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                        <asp:Literal ID="ltrLevel" runat="server" />
                                    </select>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> : </span></label>
                                    </div>
                                    <select id="sltClass" name="sltClass" class="selectpicker" data-live-search="true" data-style="select-with-transition" data-width="100%" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                    </select>
                                </div>
                            </div>
                            <div class="row send-individual-list-ignore" style="display: none;">
                                <div class="col-md-4 ml-auto mr-auto">
                                    <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> : </span></label>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <input id="iptUserName" type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132461") %>" maxlength="100">
                                        </div>
                                        <div class="col-md-3">
                                            <button id="btnAddUser" class="btn btn-success" data-sid="0">
                                                <span class="btn-label">
                                                    <i class="material-icons">add</i>
                                                </span>
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mr-auto ml-auto">
                                </div>
                            </div>
                            <div class="row send-individual-list" style="display: none;">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <br />
                                </div>
                            </div>
                            <div class="row send-individual-list" style="display: none;">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <table class="table user-table">
                                        <thead>
                                            <tr>
                                                <th scope="col" width="7%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th scope="col" width="13%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></th>
                                                <th scope="col" width="50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                                <th scope="col" width="15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                                <th scope="col" width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                                <th scope="col" width="5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="norow-message">
                                                <td colspan="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132455") %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="row send-group send-group-level" style="display: none;">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %> : </span></label>
                                    </div>
                                    <div id="treeview_container" class="hummingbird-treeview">
                                        <ul id="treeview" class="hummingbird-base">
                                            <asp:Literal ID="ltrLevelTree" runat="server"></asp:Literal>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %> : </span></label>
                                        <div class="form-check" style="display: inline-block;">
                                            <label class="form-check-label" style="color: #000;">
                                                <input id="chkSendFileWithOwner" class="form-check-input" type="checkbox" value="">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401041") %>
                                                <span class="form-check-sign">
                                                    <span class="check"></span>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="files-uploaded-list">
                                        </div>
                                        <div class="file-drop-area">
                                            <span class="choose-file-button">
                                                <span class="material-icons">cloud_upload</span>
                                            </span>
                                            <span class="file-message"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132462") %> <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132463") %></span></span>
                                            <input class="file-input" type="file" multiple accept="application/msword, .docx, .csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel, .pptx, application/vnd.openxmlformats-officedocument.presentationml.presentation, application/vnd.ms-powerpoint, text/plain, application/pdf, image/*">
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-10 ml-auto mr-auto">
                                    <div class="">
                                        <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> : </span></label>
                                    </div>
                                    <textarea id="tarMessage" name="tarMessage" class="form-control" rows="5" onkeyup="CountCharacters(this, 4800)"></textarea>
                                    <span style="color: gray;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132464") %></span><span style="color: gray;" class="count-characters"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132465") %></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 text-center" style="padding: 40px 0px 20px 0px;">
                                    <button id="btnPreviewNews" class="btn btn-success">
                                        <span class="btn-label">
                                            <i class="material-icons">send</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02041") %>
                                    </button>
                                    <button id="btnCancelNews" class="btn btn-danger">
                                        <span class="btn-label">
                                            <i class="material-icons">close</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <br />
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

    <div id="modalPreviewNews" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="modalPreviewNewsLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" style="margin: 0 auto; top: 5%;">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="modalPreviewNewsLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401045") %> : </span></label>
                                <p id="pTitle"></p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401046") %> : </span></label>
                                <p id="pNewsType"></p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401047") %> : </span></label>
                                <p id="pSendType"></p>
                            </div>
                        </div>
                        <div class="row date-time" style="display: none;">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132456") %> : </span></label>
                                <p id="pDateTime"></p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401055") %> : </span></label>
                                <p id="pAcceptType"></p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401048") %> : </span></label>
                                <p id="pGroupType"></p>
                            </div>
                        </div>
                        <div class="row send-group">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401049") %> : </span></label>
                                <p id="pGroup"></p>
                            </div>
                        </div>
                        <div class="row send-individual-list" style="display: none;">
                            <div class="col-md-10 ml-auto mr-auto">
                                <div class="user-list">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th scope="col" width="7%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th scope="col" width="13%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></th>
                                                <th scope="col" width="50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                                <th scope="col" width="15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                                <th scope="col" width="15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <label class="col-form-label"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> : </span></label>
                                <p id="pMessage"></p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 ml-auto mr-auto">
                                <div class="row file-list">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSendNews" class="btn btn-success global-btn">
                        <i class="material-icons">send</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--https://www.jqueryscript.net/other/Collapsible-Tree-View-Checkboxes-jQuery-hummingbird.html--%>
    <script src="/Content/hummingbird-treeview/hummingbird-treeview.min.js"></script>
    <script>

        var newsForm = {
            UserData: [],
            FileData: [],
        }

        var allowedFiles = [".doc", ".docx", ".csv", ".xls", ".xlsx", ".ppt", ".pptx", ".text", ".txt", ".pdf", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pjpeg", ".tiff", ".pjp", ".jfif", ".svg", ".xbm", ".dib", ".jxl", ".svgz", ".webp", ".ico", ".tif", ".avif"];

        function FormatBytes(bytes, decimals = 2) {
            if (bytes === 0) return '0 Bytes';

            const k = 1024;
            const dm = decimals < 0 ? 0 : decimals;
            const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

            const i = Math.floor(Math.log(bytes) / Math.log(k));

            return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
        }

        function GetFileTypes(file) {
            var extension = file.substr((file.lastIndexOf('.') + 1));
            switch (extension) {
                case 'jpeg':
                case 'jpg':
                case 'png':
                case 'gif':
                    return 'image';
                    break;
                case 'pdf':
                    return '/images/FileTypes/pdf.png';
                    break;
                case 'doc':
                case 'docx':
                    return '/images/FileTypes/doc.png';
                    break;
                case 'xls':
                case 'xlsx':
                    return '/images/FileTypes/xls.png';
                    break;
                case 'csv':
                    return '/images/FileTypes/csv.png';
                    break;
                case 'ppt':
                case 'pptx':
                    return '/images/FileTypes/ppt.png';
                    break;
                case 'txt':
                    return '/images/FileTypes/txt.png';
                    break;
                default:
                    return '/images/FileTypes/file.png';
            }
        };

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "NewsEditForm.aspx/LoadTermSubLevel2",
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

        function CountCharacters(input, maxlength) {
            var len = input.value.length;
            if (len >= maxlength) {
                input.value = input.value.substring(0, maxlength);
            } else {
                $('.count-characters').text(" (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %> " + Number(maxlength - len).toLocaleString('en') + "/" + Number(maxlength).toLocaleString('en') + ")");
            }
        };

        $(document).ready(function () {

            // Init
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

            $('.timepicker').datetimepicker({
                // format: 'H:mm A',    // use this format if you want the 24hours timepicker
                format: 'HH:mm', //use this format if you want the 12hours timpiecker with AM/PM toggle
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

            $('body').tooltip({
                container: '.wrapper',
                selector: '[data-toggle="tooltip"]'
            });

            // Collapsed Symbol
            $.fn.hummingbird.defaults.collapsedSymbol = "fa-caret-down";
            // Expand Symbol
            $.fn.hummingbird.defaults.expandedSymbol = "fa-caret-up";

            $("#treeview").hummingbird();

            // Validate rule for groupForm
            $("#frmNewsForm").validate({
                rules: {
                    iptTitle: "required",
                    sltMessageType: "required",
                    sltSendType: "required",
                    iptDate: "required",
                    iptTime: "required",
                    sltAcceptType: "required",
                    sltGroup: "required",
                    /*sltIndividualType: "required",*/
                    tarMessage: "required"
                },
                messages: {
                    iptTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltMessageType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltSendType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptDate: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    iptTime: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltAcceptType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    sltGroup: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                    /*sltIndividualType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",*/
                    tarMessage: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "iptTitle":
                        case "iptDate":
                        case "iptTime":
                        case "tarMessage": error.insertAfter(element); break;
                        case "sltMessageType":
                        case "sltSendType":
                        case "sltAcceptType":
                        case "sltGroup":
                        case "sltIndividualType": error.insertAfter(element.parent()); break;
                        default: error.insertAfter(element); break;
                    }
                },
                onfocusout: false,
                invalidHandler: function (form, validator) {
                    var errors = validator.numberOfInvalids();
                    if (errors) {
                        validator.errorList[0].element.focus();
                    }
                }
            });

            // Upload File
            $('input[type=file]').change(function () {
                ImagesPreview($(this)[0].files, 'div.files-uploaded-list');
            });

            // Upload File - drag file
            $('.file-drop-area').on('dragover', function () {
                $(this).addClass('file-drag-over');
                return false;
            });
            $('.file-drop-area').on('dragleave', function () {
                $(this).removeClass('file-drag-over');
                return false;
            });
            $('.file-drop-area').on('drop', function (e) {
                e.preventDefault();

                $(this).removeClass('file-drag-over');

                var files = e.originalEvent.dataTransfer.files;
                ImagesPreview(files, 'div.files-uploaded-list');
                reader.readAsDataURL(files);
            });

            $("#sltSendType").change(function () {
                switch ($(this).val()) {
                    case '0':
                        $('.date-time').hide();
                        break;
                    case '1':
                        $('.date-time').show();
                        break;
                }
            });

            $(document).on('change', 'input:radio[id^="iptGroupType"]', function (event) {
                switch ($(this).val()) {
                    case '1':
                        $('.send-group').show();
                        $("#sltGroup").trigger('change');
                        $('.send-individual').hide();
                        $('.send-individual-student').hide();
                        $('.send-individual-list').hide();
                        break;
                    case '2':
                        $('.send-group').hide();
                        $('.send-individual').show();
                        switch ($('#sltIndividualType').val()) {
                            case '0':
                                $('.send-individual-student').show();
                                $('.send-individual-list').show();
                                break;
                            case '1':
                                $('.send-individual-student').hide();
                                $('.send-individual-list').show();
                                break;
                        }
                        break;
                    default: break;
                }
            });

            $('#sltGroup').on("changed.bs.select", function () {
                //console.log($('option:selected', this).attr("data-default"));
                switch ($('option:selected', this).attr("data-default")) {
                    case 'all-student-level':
                        $('.send-group-level').show();
                        break;
                    default:
                        $('.send-group-level').hide();
                        break;
                }
            });

            $("#sltIndividualType").change(function () {
                switch ($(this).val()) {
                    case '0':
                        $('.send-individual-student').show();
                        $('.send-individual-list').show();
                        break;
                    case '1':
                        $('.send-individual-student').hide();
                        $('.send-individual-list').show();
                        break;
                }
            });

            $("#sltLevel").change(function () {

                LoadTermSubLevel2($(this).val(), '#sltClass');

            });

            $(document).on('click', '.files-uploaded .file-remove', function () {

                var objIndex = newsForm.FileData.findIndex((obj => obj.id == $(this).data('id')));

                newsForm.FileData[objIndex].status = 'delete';

                $(this).closest('.files-uploaded').remove();

                $('.tooltip').tooltip('hide');

                return false;
            });

            $(document).on('click', '.user-table .user-remove', function () {

                var objIndex = newsForm.UserData.findIndex((obj => obj.id == $(this).data('id')));

                newsForm.UserData[objIndex].status = 'delete';

                $(this).closest('.user-row').remove();

                $('.tooltip').tooltip('hide');

                // Check exist row
                const userData = newsForm.UserData.filter(v => v.status != "delete");
                if (userData.length == 0) {
                    $('.norow-message').show();
                }

                return false;
            });

            // Multiple images preview in browser
            var ImagesPreview = function (files, placeToInsertImagePreview) {
                if (files) {
                    var filesAmount = files.length;
                    for (i = 0; i < filesAmount; i++) {
                        var file = files[i];

                        // 2097152 byte - 2 MB
                        if (file.size > 2097152) { alert('ไม่สามารถอัพโหลด \"' + file.name + '\" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132458") %>'); continue; }
                        if (file.size == 0) { alert('ไม่สามารถอัพโหลด \"' + file.name + '\" ได้เนื่องจากขนาดไฟล์มีขนาด 0 bytes!'); continue; }

                        // Check allow file upload
                        var regex = new RegExp("(.*?)(?:\((\d+)\))?(" + allowedFiles.join('|') + ")$");
                        if (!regex.test(file.name.toLowerCase())) { alert('ไม่สามารถอัพโหลดไฟล์นามสกุล \"' + file.type + '\" นี่ได้!'); continue; }

                        // Check file name length
                        if ($('#chkSendFileWithOwner').is(':checked') && !$('#chkSendFileWithOwnerOCR').is(':checked') && file.name.length > 20) { alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132460") %>'); continue; }

                        regex = new RegExp("(.*?)(?:\((\d+)\))?(.pdf)$");
                        if ($('#chkSendFileWithOwnerOCR').is(':checked') && !regex.test(file.name.toLowerCase())) { alert('ไม่สามารถอัพโหลดไฟล์นามสกุล \"' + file.type + '\" นี่ได้!'); continue; }

                        var newFileId = newsForm.FileData.length == 0 ? 1 : Math.max.apply(Math, newsForm.FileData.map(function (o) { return o.id; })) + 1;
                        // indb: true, false  
                        // status: new, modify, delete
                        newsForm.FileData.push({ id: newFileId, contentType: file.type, fileName: file.name, indb: false, status: 'new' });

                        var reader = new FileReader();
                        reader.fileName = file.name;
                        reader.fileSize = file.size;
                        reader.newFileId = newFileId;
                        reader.onload = function (event) {

                            var byteData = this.result.split(';')[1].replace("base64,", "");

                            //Find index of specific object using findIndex method.    
                            var objIndex = newsForm.FileData.findIndex((obj => obj.id == event.target.newFileId));

                            //Update object's byteData property.
                            newsForm.FileData[objIndex].byteData = byteData;

                            newsForm.FileData[objIndex].size = event.target.fileSize;

                            var thumbnailImage = GetFileTypes(event.target.fileName);
                            if (thumbnailImage == 'image') {
                                thumbnailImage = event.target.result;
                            }
                            newsForm.FileData[objIndex].thumbnail = thumbnailImage;

                            $(placeToInsertImagePreview).append(`<div class="files-uploaded" data-id="` + event.target.newFileId + `">
                                                <img src="`+ thumbnailImage + `" />
                                                <p class="file-name">`+ event.target.fileName + `</p>
                                                <button type="button" class="btn btn-danger btn-link file-remove" data-id="` + event.target.newFileId + `" data-toggle="tooltip" data-placement="top" title="Remove File">
                                                    <i class="material-icons">close</i>
                                                </button>
                                                <p class="file-size">`+ FormatBytes(event.target.fileSize) + `</p>
                                            </div>`);
                        }
                        reader.readAsDataURL(files[i]);
                    }

                    $('input[type=file]').val('');
                }
            };

            $('#iptUserName').autoComplete({
                resolverSettings: {
                    fail: function () {
                        console.log('fail');
                    }
                },
                resolver: 'custom',
                formatResult: function (item) {
                    return {
                        value: item.id,
                        text: item.name,
                        html: [item.name]
                    };
                },
                events: {
                    search: function (qry, callback) {
                        var param = { keyword: qry, individualType: $('#sltIndividualType').val(), levelID: $('#sltLevel').val(), roomID: $('#sltClass').val() };
                        $.ajax({
                            url: "NewsEditForm.aspx/GetStudentName",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) {
                                var jsonObj = $.parseJSON(data);
                                return jsonObj.d;
                            },
                            success: function (data) {
                                callback(data);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(textStatus);
                            }
                        });
                    }
                },
                minLength: 1
            });

            $('#iptUserName').on('autocomplete.select', function (evt, item) {

                $('#btnAddUser').data('sid', item.id);
                $('#btnAddUser').data('json', item);

            });

            $("#btnAddUser").bind({
                click: function () {

                    if ($(this).data('sid') != 0) {

                        var data = $(this).data('json');

                        // Add to table
                        var ri = newsForm.UserData.length + 1;
                        $('.user-table').append(`<tr class="user-row">
                                                    <th scope="row">`+ ri + `</th>
                                                    <td>`+ data.code + `</td>
                                                    <td>`+ data.name + `</td>
                                                    <td>`+ data.classroom + `</td>
                                                    <td>`+ data.type + `</td>
                                                    <td>
                                                        <button type="button" class="btn btn-danger btn-link user-remove" data-id="`+ data.id + `" data-toggle="tooltip" data-placement="top" title="Remove User">
                                                            <i class="material-icons">close</i>
                                                        </button>
                                                    </td>
                                                 </tr>`);

                        // Add to newsForm.UserData
                        newsForm.UserData.push({ id: data.id, code: data.code, name: data.name, classroom: data.classroom, typeId: data.typeId, type: data.type, indb: false, status: 'new' });

                        $('.norow-message').hide();

                        // Clear select sid on button
                        $(this).data('sid', 0);

                        $('#iptUserName').val('');
                    }

                    return false;
                }
            });

            $("#btnPreviewNews").bind({
                click: function () {

                    if ($("#frmNewsForm").valid()) {

                        $('#pTitle').html($('#iptTitle').val());
                        $('#pNewsType').html($('#sltMessageType').find(':selected').text());
                        $('#pSendType').html($('#sltSendType').find(':selected').text());
                        $('#pDateTime').html($('#iptDate').val() + ' ' + $('#iptTime').val());
                        $('#pAcceptType').html($('#sltAcceptType').find(':selected').text());
                        $('#pGroupType').html($('input[name=iptGroupType]:checked + label').text()); // group, individual
                        $('#pGroup').html($('#sltGroup').find(':selected').text());
                        $('#pMessage').html($('#tarMessage').val());

                        //$('#sltIndividualType').find(':selected').text(); // student, employee
                        //// for student
                        //$('#sltLevel').find(':selected').text();
                        //$('#sltClass').find(':selected').text();


                        // List user
                        $('.user-list table tbody').empty();
                        const userData = newsForm.UserData.filter(v => v.status != "delete");
                        for (var i = 0; i < userData.length; i++) {
                            var data = userData[i];
                            $('.user-list table').append(`<tr class="user-row">
                                                            <th scope="row">`+ (i + 1) + `</th>
                                                            <td>`+ data.code + `</td>
                                                            <td>`+ data.name + `</td>
                                                            <td>`+ data.classroom + `</td>
                                                            <td>`+ data.type + `</td>
                                                         </tr>`);
                        }

                        if ($('input[name=iptGroupType]:checked').val() == '2' && userData.length == 0) return false;


                        // List file
                        $('.file-list').empty();
                        const fileData = newsForm.FileData.filter(v => v.status != "delete");
                        for (var i = 0; i < fileData.length; i++) {
                            var data = fileData[i];
                            $('.file-list').append(`<div class="col-md-4 files-uploaded">
                                                    <img src="`+ data.thumbnail + `" />
                                                    <p class="file-name">`+ data.fileName + `</p>
                                                    <p class="file-size">`+ FormatBytes(data.size) + `</p>
                                                </div>`);
                        }

                        $("#modalPreviewNews").modal('show');
                    }

                    return false;
                }
            });

            $("#btnSendNews").bind({
                click: function () {

                    $("#modalWaitDialog").modal('show');

                    var newsData = {
                        smsId: '<%=Request.QueryString["nid"]%>',
                        title: $('#iptTitle').val(),
                        sendFileWithOwner: $('#chkSendFileWithOwner').is(':checked'),
                        files: [],
                        message: $('#tarMessage').val()
                    };

                    const fileData = newsForm.FileData.filter(v => v.status == "new" || (v.indb == true && v.status == "delete"));
                    for (var i = 0; i < fileData.length; i++) {
                        var data = fileData[i];
                        newsData.files.push({ contentType: data.contentType, fileName: data.fileName, byteData: data.byteData, indb: data.indb, status: data.status, id: data.id });
                    }

                    //console.log(newsData);

                    $.ajax({
                        type: "POST",
                        url: "NewsEditForm.aspx/SaveEditNews",
                        data: JSON.stringify({ newsData: newsData }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            //console.log(result.d);
                            var r = JSON.parse(result.d);
                            //console.log(r);
                            var title = "";
                            var body = "";

                            if (r.success) {
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00895") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                    // Redirect to student list
                                    window.location.replace("NewsList.aspx");
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
                        beforeSend: function () {
                            // Display upload-progress
                        },
                        error: function (response) {
                            alert(response.responseText);
                        }
                    });

                    return false;
                }
            });

            $("#btnCancelNews").bind({
                click: function () {

                    // Redirect to group list
                    window.location.replace("NewsView.aspx?nid=<%=Request.QueryString["nid"]%>");

                    return false;
                }
            });

            // Modal Section
            $('#modalNotifyOnlyClose').on('hidden.bs.modal', function (e) {
                if ($("#modalWaitDialog").data('bs.modal')?._isShown) {
                    $("#modalWaitDialog").modal('hide');
                }
            });


            // Set news data
            <%=NewsDataScriptInit%>

            newsForm.UserData = [<%=NewsUserData%>];
            const userData = newsForm.UserData.filter(v => v.status != "delete");
            if (userData.length == 0) {
                $('.norow-message').show();
            }
            else {
                $('.norow-message').hide();
                for (var i = 0; i < userData.length; i++) {
                    var data = userData[i];
                    $('.user-table').append(`<tr class="user-row">
                                            <th scope="row">`+ (i + 1) + `</th>
                                            <td>`+ data.code + `</td>
                                            <td>`+ data.name + `</td>
                                            <td>`+ data.classroom + `</td>
                                            <td>`+ data.type + `</td>
                                            <td><!--Remove User--></td>
                                        </tr>`);
                }
            }

            newsForm.FileData = [<%=NewsFileData%>];
            const fileData = newsForm.FileData.filter(v => v.status != "delete");
            for (var i = 0; i < fileData.length; i++) {
                var data = fileData[i];
                $('div.files-uploaded-list').append(`<div class="files-uploaded" data-id="` + data.id + `">
                                                        <img src="`+ data.thumbnail + `" />
                                                        <p class="file-name">`+ data.fileName + `</p>
                                                        <button type="button" class="btn btn-danger btn-link file-remove" data-id="` + data.id + `" data-toggle="tooltip" data-placement="top" title="Remove File">
                                                            <i class="material-icons">close</i>
                                                        </button>
                                                        <p class="file-size">`+ FormatBytes(data.size) + `</p>
                                                    </div>`);
            }

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
