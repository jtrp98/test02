<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GradeViewSetting.aspx.cs" Inherits="FingerprintPayment.Pages.Settings.GradeViewSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <link href="../../Styles/gradeviewsetting.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <div class="studentList">
            <div class="row" style="padding-bottom: 10px;">
                <label class="col-md-5">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206180") %></label>

            </div>

            <table id="dtGridViewSetting" class="table table-bordered table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th style="width: 5%; border-bottom: 0px" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                        <th style="width: 15%; border-bottom: 0px" class="text-center hidden"></th>
                        <th style="width: 15%; border-bottom: 0px" class="text-center hidden"></th>
                        <th style="width: 15%; border-bottom: 0px" class="text-center hidden"></th>
                        <th style="width: 10%; border-bottom: 0px" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                        <th style="width: 13%; border-bottom: 0px" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %></th>
                        <th style="width: 15%; border-bottom: 0px" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106099") %></th>
                        <th style="width: 15%; border-bottom: 0px" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106100") %></th>
                        <th style="width: 15%; border-bottom: 0px" class="text-center">&nbsp;
                            <button type="button" style="width: 110px" class="btn btn-primary btn-lg glyphicon glyphicon-cog" id="GradeViewFor100Model" onclick="OpenGradeViewFor100()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00094") %></button>
                        </th>
                    </tr>
                </thead>
            </table>

            <div class="modal fade" id="ModalForBlockClass" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206185") %></h2>
                        </div>
                        <div class="modal-body" style="width: 640px;">

                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label class="pull-right" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></label>
                                </div>
                                <div class="col-md-7">
                                    <select class="ddl2 form-control" onchange="ClassOnChange(this)" id="ClassList"></select>
                                    <input type="hidden" id="gradeViewSettingId" />
                                    <input type="hidden" id="nTerm" />
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-12" style="width: 90%; min-height: 200px; overflow-y: scroll" id="divClassRoomBlockList">
                                    <table id="ClassRoomBlockList" class="table table-bordered table-hover" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th style="text-align: left; min-width: 70%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                                                </th>
                                                <th style="text-align: left; min-width: 70%">GradeViewSettingId
                                                </th>
                                                <th style="text-align: left; min-width: 70%">RoomListSettingId
                                                </th>
                                                <th style="text-align: left; min-width: 70%">nTermSubLevel2
                                                </th>
                                                <th style="text-align: left; min-width: 70%">IsRoomBlocked
                                                </th>
                                                <th style="text-align: left; min-width: 70%">
                                                    <input name="ClassRoomSelectAll" value="1" id="ClassRoomSelectAll" type="checkbox" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>

                                            </tr>
                                        </thead>

                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="btn btn-primary global-btn" id="btnClassRoomBlockDataSave"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="ModalForBlockStudent" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206185") %></h2>
                        </div>
                        <div class="modal-body" style="width: 640px;">

                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label class="pull-right" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></label>
                                </div>
                                <div class="col-md-7">
                                    <select class="ddl2 form-control" onchange="StudentBlockClassOnChange(this)" id="ClassListForStudentBlock"></select>


                                </div>
                            </div>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label class="pull-right" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></label>
                                </div>
                                <div class="col-md-7">
                                    <select class="ddl2 form-control" onchange="StudentBlockRoomOnChange(this)" id="StudentBlockRoomList"></select>
                                </div>
                            </div>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-12" style="width: 90%; min-height: 200px; overflow-y: scroll" id="divStudentBlockList">
                                    <table id="StudentBlockList" class="table table-bordered table-hover" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th style="text-align: left; min-width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                                                </th>
                                                <th style="text-align: left; min-width: 0%">GradeViewSettingId
                                                </th>
                                                <th style="text-align: left; min-width: 0%">StudentBlockListSettingId
                                                </th>
                                                <th style="text-align: left; min-width: 0%">IsStudentBlocked
                                                </th>
                                                <th style="text-align: left; min-width: 0%"></th>
                                                <th style="text-align: left; min-width: 30%">
                                                    <input name="StudentSelectAll" value="1" id="StudentSelectAll" type="checkbox" />
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>
                                                </th>
                                                <th style="text-align: left; min-width: 60%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204046") %>
                                                </th>


                                            </tr>
                                        </thead>

                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="btn btn-primary global-btn" id="btnStudentBlockDataSave"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="GradeViewFor100Setting" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206181") %></h2>
                        </div>
                        <div class="modal-body" style="width: 640px;">

                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-11">
                                    <label class="pull-left" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206182") %></label>
                                </div>

                            </div>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-10">1.	<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204046") %></div>

                            </div>
                            <div class="row form-check">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input class="form-check-input" type="radio"  style="width:17px;height:17px;" name="GradeViewFor100" onclick="UpdateGradeViewFor100(0);" id="GradeViewFor50"/>
                                <label class="form-check-label" for="GradeViewFor50" style="top:-3px;position:relative">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206183") %>
                                </label>
                                    </div>
                            </div>
                            <div class="row form-check">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input class="form-check-input" type="radio" style="width:17px;height:17px;" onclick="UpdateGradeViewFor100(1);" name="GradeViewFor100" id="GradeViewFor100" />
                                <label class="form-check-label" for="GradeViewFor100" style="top:-3px;position:relative">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206184") %>
                                </label>
                                    </div>
                            </div>
                            <%--<div class="row">

                                <div class="col-md-2"></div>
                                <div class="col-md-9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206183") %></div>

                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206184") %></div>

                            </div>--%>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-10">2.	<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00102") %></div>

                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input type="checkbox" checked="checked" disabled="disabled"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206184") %></div>
                            </div>

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input type="checkbox" checked="checked" disabled="disabled"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206185") %></div>
                            </div>

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input type="checkbox" checked="checked" disabled="disabled"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211055") %></div>
                            </div>
                            <div class="row--space"></div>


                            <div class="row">
                                <div class="col-md-11">
                                    <label class="pull-left" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206185") %></label>
                                </div>

                            </div>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-10">1.	<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204046") %></div>

                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input type="checkbox" checked="checked" disabled="disabled"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206184") %></div>

                            </div>
                            <div class="row--space"></div>
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-10">2.	<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00101") %></div>

                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input type="checkbox" checked="checked" disabled="disabled"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206186") %></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input type="checkbox" checked="checked" disabled="disabled"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206187") %></div>
                            </div>
                              <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-9"><input class="form-check-input" type="checkbox"  style="width:17px;height:17px;" name="GradeViewAutoBlock" onclick="UpdateGradeViewAutoBlock(this);" id="GradeViewAutoBlock"/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %></div>
                            </div>
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <%--  <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>--%>

    <!-- DataTables -->
    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>

    <%-- <script type="text/javascript" src="/scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="/scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js"></script>--%>

    <script language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="/Scripts/gradeviewsetting.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmssss") %>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
