<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="Tuitionfeesetting-1.aspx.cs" Inherits="FingerprintPayment.TuitionFee.Tuitionfeesetting_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-dialog.js" type="text/javascript"></script>
    <link href="../Content/bootstrap-dialog.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>

    <style type="text/css">
        table.tableSection {
            display: table;
            width: 100%;
        }

            table.tableSection thead,
            table.tableSection tbody {
                float: left;
                width: 100%;
            }

                table.tableSection thead th {
                    vertical-align: top;
                }

            table.tableSection tbody {
                overflow-y: scroll;
                /* Giving height to make the tbody scroll */
                /* Giving height dynamically is recommended */
                height: 420px;
            }

            table.tableSection tr {
                width: 100%;
                display: table;
                /* Keeping the texts of both thead and tbody in same alignment */
                text-align: left;
            }

            table.tableSection th,
            table.tableSection td {
                width: 33%;
            }

            table.tableSection tr > td:last-child {
                /* removing fraction of width i.e 2% to align the tbody columns with thead columns. */
                /* It is must as we need to consider the tbody scroll width too */
                /* if the width is in pixels, then (width - 18px) would be enough */
                width: 31%;
            }

            /** for older browsers (IE8), if you know number of columns in your table **/

            table.tableSection tr > td:first-child + td + td {
                width: 31%;
            }

            table.tableSection thead {
                padding-right: 18px;
                /* 18px is approx. value of width of scroll bar */
                /*width: calc(100% - 20px);*/
            }

        .datepicker {
            padding: 0;
        }

        .fa-times {
            color: red;
            cursor: pointer;
        }

        .glyphicon-plus {
            cursor: pointer;
        }

        select {
            text-align: center;
            text-align-last: center;
            /* webkit*/
        }

        option {
            text-align: left;
            /* reset to left*/
        }
    </style>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="script/Tuitionfeesetting.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Services>
            <asp:ServiceReference Path="~/App_Logic/WSDataService.asmx" />
        </Services>
    </asp:ScriptManager>
    <div class="full-card box-content employeeslist-container group-list">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00110") %></h3>
                <div class="row">
                    <div class="col-xs-12 col-md-3">
                        <select id="year_id" name="year_id" class="chosen-container form-control bottom">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %></option>
                        </select>
                    </div>
                    <div class="col-xs-12 col-md-3">
                        <select id="term_id" name="term_id" class="chosen-container form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %></option>
                        </select>
                    </div>
                    <div class="col-xs-12 col-md-6"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div class="row">
                    <div class="col-xs-12 col-md-6 disabled"></div>
                </div>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00111") %></h3>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-lg-12">
                <table id="tableAddRow" class="table table-bordered table-hover tableSection" style="margin-bottom: 0px; border-top: 0px; border-right: 0px; font-size: 18px;">
                    <thead>
                        <tr style="border-top: 1px solid #ddd; background-color: #337AB7; color: white;">
                            <th style="width: 5%" class="center">
                                <input type="checkbox" value="1" class="form-check-input" checked onchange="$('#tableAddRow tbody input[type=checkbox]').prop('checked',$(this).prop('checked'))" />
                            </th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101212") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107039") %></th>
                            <th style="width: 10%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502004") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01985") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01964") %></th>
                            <th style="width: 10%; vertical-align: middle;" class="center">
                                <span class="btn btn-success glyphicon glyphicon-plus addBtn" id="addBtn_0" style="font-size: 15px;"></span>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-xs-12 col-md-6">
                <button type="submit" class="btn btn-success" id="submit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                <div class="btn btn-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></div>
            </div>
            <div class="col-xs-12 col-md-6 right">
                <button type="submit" class="btn btn-primary disabled" id="submitpeak" <%--title="กรุณากดปุ่มบันทึกก่อนทำการส่งให้ระบบัญชี Peak"--%> data-toggle="tooltip"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501014") %></button>
            </div>
        </div>
    </div>
    <asp:Label ID="lblMsg" runat="server" />
    <div class="modal fade" id="dialog-confirm">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    <p>One fine body…</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
    <div class="row">
        <div class="col-md-12 col-xs-12 center">
            <label id="message"></label>
        </div>
    </div>
</asp:Content>
