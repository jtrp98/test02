<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="CumulativeGradeInfo.aspx.cs" Inherits="FingerprintPayment.Pages.Grade.CumulativeGradeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="../../assets/plugins/datatables/jquery.dataTables.min.css" />
    <link href="../../Assets/plugins/datatables/rowGroup.dataTables.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/plugins/datatables/material-components-web.min.css" />
    <%--<link rel="stylesheet" href="../../assets/plugins/datatables/dataTables.material.min.css" />--%>
    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <style>
        .IsScoreFromScoreEntryPage {
            padding-top: 15px !important;
        }

        .dropdown-menu {
        }

        .bootstrap-select .dropdown-menu {
            z-index: 9999 !important;
        }

            .bootstrap-select .dropdown-menu li, .dropdown-menu .show, .bootstrap-select .dropdown-menu.inner {
                z-index: 9999 !important;
            }

        .dropdown-content {
            z-index: 1020 !important;
        }

        .NumberYear > .bootstrap-select, .PlanName > .bootstrap-select,
        .CourseCode > .bootstrap-select, .STerm > .bootstrap-select, .GetGradeLabel > .bootstrap-select, .NCredit > .bootstrap-select  {
            width: 100% !important;
        }

        .GetScore100 > input, .GetGradeLabel > input, .GetBehaviorLabel > input, .GetReadWrite > input, .GetSamattana > input {
            width: 100% !important;
        }

        table {
            border-collapse: collapse;
            table-layout: fixed;
            margin: 0 auto;
        }

            table td {
                white-space: normal;
            }

            table tr {
                height: 50px;
            }

        .dataTables tbody tr {
            min-height: 35px;
            max-height: 50px;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 0px !important;
            border-bottom: 0px !important;
            background-color: #434243 !important;
            color: white;
            font-size: 70%;
        }

            table.dataTable thead th input, table.dataTable thead td input {
                color: #555;
            }

        table.dataTable tbody th {
            padding: 3px 3px !important;
            border-bottom: 0px !important;
        }

        table.dataTable thead th {
            border-top: 1px solid #dddddd;
            border-bottom: 1px solid #dddddd;
            border-right: 1px solid #dddddd;
        }

            table.dataTable thead th:first-child {
                border-left: 1px solid #dddddd;
            }

        table.dataTable tbody td {
            padding: 3px 3px;
            border-bottom: 1px solid #DD2467;
            border-right: 1px solid #dddddd;
        }

            table.dataTable tbody td:first-child {
                border-left: 1px solid #dddddd;
            }

        .VerticalAlignBottom {
            vertical-align: bottom !important;
        }

        .AssessmentScoreBox {
            width: 81%;
            /*  margin: 3px;
            margin-left: 6px !important;*/
            border-radius: 1rem !important;
            border: 1px solid #dddddd;
            text-align: center;
        }

        th.NumberYear, th.PlanName, th.CourseCode, th.STerm, th.NCredit, th.CourseTotalHour,
        th.GetScore100, th.GetGradeLabel, th.GetBehaviorLabel, th.GetReadWrite, th.GetSamattana {
            text-align: center !important;
        }

        th {
            padding: 3px !important;
            height: 200px;
        }

            th.rotate > label.CourseTotalHour {
                transform: translate(-30px, 0px)rotate(270deg);
                width: auto;
                margin: 0px !important;
                font-weight: bold !important;
                /*padding:3px !important;*/
            }

            th.rotate > label.GetBehaviorLabel, th.rotate > label.GetReadWrite {
                transform: translate(-35px, 0px)rotate(270deg);
                width: auto;
                margin: 0px !important;
                font-weight: bold !important;
                /*padding:3px !important;*/
            }

            th.rotate > label.RowNumber, th.rotate > label.NumberYear, th.rotate > label.PlanName, th.rotate > label.CourseCode,
            th.rotate > label.STerm, th.rotate > label.NCredit, th.rotate > label.GetScore100,
            th.rotate > label.GetGradeLabel, th.rotate > label.GetSamattana {
                transform: translate(0px, 0px)rotate(270deg);
                width: auto;
                margin: 0px !important;
                font-weight: bold !important;
                /*padding:3px !important;*/
            }

        td.NumberYear, td.STerm, td.NCredit, td.CourseTotalHour, td.GetScore100, td.GetGradeLabel, td.GetSamattana, td.IsScoreFromScoreEntryPage {
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.js"></script>
    <script type='text/javascript' src="../../assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="../../assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.min.js"></script>
    <script type='text/javascript' src="../../Content/Material/assets/js/plugins/bootstrap-selectpicker.js"></script>
    <script src="../../Assets/plugins/datatables/dataTables.rowsGroup.js"></script>
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="../../Scripts/CumulativeGradeInfo.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmssss") %>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-5">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></label>
                                    <div class="col-md-8">
                                        <div class="form-group ">
                                             <asp:DropDownList ID="ddlYear" ClientIDMode="Static" onchange="onSelectedYear()" runat="server" data-style="select-with-transition" CssClass="selectpicker" data-size="7" DataValueField="Value" DataTextField="Text" />
                                            
                                           <%-- <input type="text" id="txtStudentCode" class="form-control" value="" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                            <input type="hidden" id="hdnSId" />
                                            <input type="hidden" id="hdnNTSubLevel" />
                                            <input type="hidden" id="hdnLevelName" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7"></div>
                            <div class="col-md-5">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                                    <label id="lblStudentName" class="col-md-8 col-form-label" style="text-align: left; padding-left: 5px" />
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                    <label id="lblClassLevel" class="col-md-8 col-form-label" style="text-align: left; padding-left: 5px" />
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                    <label id="lblYear" class="col-md-8 col-form-label" style="text-align: left; padding-left: 5px" />
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-5">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                    <label id="lblTerm" class="col-md-8 col-form-label" style="text-align: left; padding-left: 5px" />

                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" onclick="onSearch()" id="btnSearch" disabled class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">history</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206137") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row" id="rowStudentGradeInfo">
                            <div class="col-md-12">
                             <%--   Main Table Content--%>
                                <table id="StudentGradeInfo" class="table-hover dataTable" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th class="rotate">
                                                <label class="RowNumber bold" for="SlNo" id="SlNOHeaderText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></label>
                                            </th>
                                            <th>
                                                <label for="nGradeId" class="bold">nGradeId</label>
                                            </th>
                                            <th>
                                                <label for="nYear" class="bold">nYear</label>
                                            </th>
                                            <th>
                                                <label for="nTerm" class="bold">nTerm</label>
                                            </th>
                                            <th>
                                                <label for="PlanId" class="bold">PlanId</label>
                                            </th>
                                            <th>
                                                <label for="sPlaneId" class="bold">sPlaneId</label>
                                            </th>
                                            <th class="rotate">
                                                <label for="NumberYear" class="bold NumberYear"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                            </th>
                                            <th class="rotate">
                                                <label for="PlanName" class="bold PlanName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103202") %></label>
                                            </th>

                                            <th class="rotate">
                                                <label for="CourseCode" class="bold CourseCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
                                            </th>
                                            <%-- <th>
                                                <label for="CourseName" class="bold">CourseName</label>
                                            </th>--%>
                                            <th class="rotate">
                                                <label for="STerm" class="bold STerm"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                            </th>

                                            <th class="rotate text-nowrap">
                                                <label for="NCredit" class="bold NCredit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206138") %></label>
                                            </th>
                                            <th class="rotate text-nowrap">
                                                <label for="CourseTotalHour" class="bold CourseTotalHour"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206139") %></label>
                                            </th>
                                            <th class="rotate text-nowrap">
                                                <label for="GetScore100" class="bold GetScore100"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203075") %></label>
                                            </th>
                                            <th class="rotate text-nowrap">
                                                <label for="GetGradeLabel" class="bold GetGradeLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206140") %></label>
                                            </th>
                                            <th class="rotate text-nowrap">
                                                <label for="GetBehaviorLabel" class="bold GetBehaviorLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206141") %></label>
                                            </th>
                                            <th class="rotate text-nowrap">
                                                <label for="GetReadWrite" class="bold GetReadWrite"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206142") %></label>
                                            </th>
                                            <th class="rotate text-nowrap">
                                                <label for="GetSamattana" class="bold GetSamattana"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206070") %></label>
                                            </th>
                                            <th>
                                                <label for="Action" class="bold"></label>
                                            </th>
                                            <th>
                                                <label for="OptYear" class="bold"></label>
                                            </th>
                                            <th>
                                                <label for="OptPlanName" class="bold"></label>
                                            </th>
                                            <th>
                                                <label for="OptCourseCode" class="bold"></label>
                                            </th>
                                            <th>
                                                <label for="OptTerm" class="bold"></label>
                                            </th>
                                            <th>
                                                <label for="IsSavedData" class="bold"></label>
                                            </th>
                                        </tr>
                                    </thead>
                                </table>
                                <button type="button" class="btn btn-fill btn-success" id="addRow">
                                    <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
