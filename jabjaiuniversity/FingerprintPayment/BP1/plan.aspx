<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="plan.aspx.cs" Inherits="FingerprintPayment.BP1.plan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="Scripts/jquery.multi-selection.v1.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
    <script src="../Scripts/select2/select2.js" type="text/javascript"></script>
    <script src="../Scripts/select2/i18n/th.js" type="text/javascript"></script>
    <link href="../Content/select2/select2.css" rel="stylesheet" />
    <script src="../shop/createTable.js" type="text/javascript"></script>
    <link href="../Content/bootstrap-select.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>

    <style type="text/css">
        body {
            font-family: THSarabun;
            font-size: 20px;
            line-height: 1.42857143;
            color: #333;
        }

        .statusOnline {
            background-color: palegreen;
            color: black;
        }

        .statusOffline {
            background-color: hotpink;
            color: black;
        }

        .icon-primary {
            color: #337AB7;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" EnablePageMethods="true" />
    <div class="full-card box-content userlist-container">
        <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201047") %></h1>
        <div class="row">
            <div class="col-md-2">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>
            </div>
            <div class="col-md-4">
                <select id="select-level" class="form-control">
                    <%=selectOption %>
                </select>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-lg-12">
                <table class="cool-table table" cellspacing="0" cellpadding="2" border="0"
                    style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;">
                    <thead>
                        <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                            <th class="text-center" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                            <th class="text-center data-order" data-order="" data-sort="subllevelName" style="width: 300px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> <i></i></th>
                            <th class="text-center data-order" data-order="" data-sort="courseCode" style="width: 300px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %> <i></i></th>
                            <th class="text-center data-order" data-order="" data-sort="SubjectName" style="width: 300px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %> <i></i></th>
                            <th class="text-center data-order" data-order="" data-sort="courseHour" style="width: 300px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132044") %> <i></i></th>
                            <th class="text-center data-order" data-order="" data-sort="nCredit" style="width: 300px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %> <i></i></th>
                            <th class="text-center data-order" data-order="" data-sort="courseGroup" style="width: 300px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %><i></i></th>
                            <th class="text-center">
                                <div class="btn btn-success" id="btnAddPlan"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201009") %></div>
                            </th>
                        </tr>
                    </thead>
                    <tbody id="target">
                        <tr>
                            <td colspan="8" class="text-center"><i class="fa fa-spin fa-refresh"></i>
                                <h1>Loading</h1>
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr style="color: #337AB7; background-color: #337AB7; border-color: #337AB7;">
                            <td colspan="8">
                                <table width="100%" class="tab" style="border-collapse: separate;">
                                    <tr>
                                        <td style="width: 40%">
                                            <div class="row">
                                                <div class="col-md-8 col-xs-12" style="margin-top: 10px;">
                                                    <span style="color: White; border-color: #337AB7;" class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:</span>
                                                </div>
                                                <div class="col-md-4 col-xs-12">
                                                    <select id="pageSize" class="selectpicker form-control">
                                                        <option selected="selected" value="20">20</option>
                                                        <option value="50">50</option>
                                                        <option value="100">100</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width: 30%">
                                            <div class="row">
                                                <div class="col-md-4 col-xs-12">
                                                    <div id="backbutton" style="color: White; border-color: #337AB7; margin-top: 10px;" onclick="changePage(-1)">
                                                        <span style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></span>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-xs-12">
                                                    <select id="pageNumber" class="selectpicker form-control" data-size="10">
                                                        <option selected="selected" value="1">1</option>
                                                        <option value="2">2</option>
                                                    </select>
                                                </div>
                                                <div class="col-md-4 col-xs-12">
                                                    <div id="nextbutton" style="color: White; border-color: #337AB7; margin-top: 10px;" onclick="changePage(1)">
                                                        <span style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width: 30%; text-align: right">
                                            <span id="spane_pageNumber" style="color: White;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301036") %></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>

    <script id="tmpl-mustache" type="x-tmpl-mustache">
        {{#DATA}}
            <tr class="itemCell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                <td class="centertext">
                    {{Index}}
                </td>
                <td class="paymentgroup_name center">{{subllevelName}}</td>
                <td class="paymentgroup_name center">{{courseCode}}</td>
                <td class="paymentgroup_name center">{{SubjectName}}</td>
                <td class="paymentgroup_name center">{{courseHour}}</td>
                <td class="paymentgroup_name center">{{nCredit}}</td>
                <td class="paymentgroup_name center">{{courseGroupName}}</td>
                <td class="centertext">
                   <i class="fa fa-edit icon-primary" onclick="getData({{SubjectID}})"></i>
                   <i class="fa fa-remove icon-primary" onclick="deleteData({{SubjectID}})"></i>
                </td>
            </tr>
        {{/DATA}}
    </script>

    <div class="modal fade" id="model-data" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201053") %></h2>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
                        </div>
                        <div class="col-md-7">
                            <input id="SubjectName" name="SubjectName" class="form-control pull-left" required />
                            <input id="SubjectID" name="SubjectID" type="hidden" value="0" />
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></label>
                        </div>
                        <div class="col-md-7">
                            <input id="courseCode" name="courseCode" class="form-control pull-left" required />
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                        </div>
                        <div class="col-md-7">
                            <select id="nTSubLevel" name="nTSubLevel" class="form-control pull-left" required>
                                <%= selectOption %>
                            </select>
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201005") %></label>
                        </div>
                        <div class="col-md-7">
                            <select id="courseType" name="courseType" class="editType pull-left form-control" required>
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01881") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201012") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201013") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201015") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201016") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201018") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201020") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201026") %></option>
                                <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201022") %></option>
                                <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201023") %></option>
                            </select>
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201010") %></label>
                        </div>
                        <div class="col-md-7">
                            <select id="courseGroup" name="courseGroup" class="editType pull-left form-control" required>
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132045") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201024") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201025") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201026") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201027") %></option>
                            </select>
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %></label>
                        </div>
                        <div class="col-md-7">
                            <select id="nCredit" name="nCredit" class="form-control pull-left" required>
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01939") %></option>
                                <option value="0">0</option>
                                <option value="0.5">0.5</option>
                                <option value="1">1</option>
                                <option value="1.5">1.5</option>
                                <option value="2">2</option>
                                <option value="2.5">2.5</option>
                                <option value="3">3</option>
                                <option value="3.5">3.5</option>
                                <option value="4">4</option>
                                <option value="4.5">4.5</option>
                                <option value="5">5</option>
                                <option value="5.5">5.5</option>
                                <option value="6">6</option>
                                <option value="6.5">6.5</option>
                                <option value="7">7</option>
                                <option value="7.5">7.5</option>
                                <option value="8">8</option>
                                <option value="8.5">8.5</option>
                                <option value="9">9</option>
                            </select>
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201055") %></label>
                        </div>
                        <div class="col-md-7">
                            <input id="courseHour" name="courseHour" class="form-control pull-left" required />
                        </div>
                    </div>

                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206139") %></label>
                        </div>
                        <div class="col-md-7">
                            <input id="courseTotalHour" name="courseTotalHour" class="form-control pull-left" required />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="btn btn-primary global-btn" id="btnModalSave"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                    <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <script src="Script/plan/init.js" type="text/javascript"></script>
    <script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
