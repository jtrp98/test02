<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="yearsettings.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.yearsettings1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>
    <style type="text/css">
        .ui-datepicker {
            z-index: 9999 !important;
        }

        .input-group-addon, .glyphicon {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">

        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            if (getUrlParameter("id") == undefined) {
                $("#YearId").val("0")
                $("#Year").val("0")
            } else {
                $("#YearId").val(getUrlParameter("id"));
                $("#Year").val(getUrlParameter("year"));
                $("select[id*=ddlnYear]").val(getUrlParameter("year"))
                $("select[id*=ddlnYear]").attr("disabled", "disabled")
                getListData();
            }

            $("#btnnewitem").click(function () {
                $("#modalsystem").find('input').val("");
                $("#modalsystem").modal('show');
                $('#btnadd').val("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>");
                $(".modal-header h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %>");
            });

            $('#btnadd').click(function () {
                var data = {
                    dStart: getDate($("input[id*=txtdStart]").val()),
                    dEnd: getDate($("input[id*=txtdEnd]").val()),
                    sTerm: $("input[id*=sTerm]").val(),
                    nTerm: $("input[id*=nTerm]").val(),
                    nYear: $("#YearId").val()
                };

                $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
                PageMethods.UpdateData($("#YearId").val(), $("select[id*=ddlnYear]").val(), data, function (result) {
                    var data = $.parseJSON(result);
                    if (data.Status === 200) {
                        $("#YearId").val(data.Data.YearId);
                        $("#Year").val(data.Data.Year);
                        $("#modalsystem").modal('hide');
                        getListData();
                        $("select[id*=ddlnYear]").attr("disabled", "disabled")
                        $.unblockUI();
                        errorMessage("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132682") %>")
                    }
                    else if (data.Status === 401) {
                        $.unblockUI();
                        errorMessage(data.Des);
                    }
                    else {
                        $.unblockUI();
                        errorMessage("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00500") %>")
                    }
                });
            });
        });

        function getListData() {
            $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
            PageMethods.ListData($("#YearId").val(), function (result) {
                $("#table-data tbody tr").remove();
                var data = $.parseJSON(result);
                let _str = "";
                $.each(data, function (e, s) {
                    _str += '<tr style="background-color:White;"><td>' + s.sTerm + '</td>';
                    _str += '<td >' + s.dStart + '</td>';
                    _str += '<td >' + s.dEnd + '</td>';
                    _str += '<td ><span class="glyphicon glyphicon-edit" aria-hidden="true" onclick="editdata(\'' + s.nTerm + "'" + ')"></span > ';
                    _str += "<span class='glyphicon glyphicon-remove' aria-hidden='true' onclick='deldata(" + '"' + s.nTerm + '"' + ")' ></span ></td >";
                    _str += '</tr>'
                });
                $("#table-data tbody").html(_str);
                $.unblockUI();
            })
        }

        function editdata(nTerm) {
            $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
            PageMethods.GetData(nTerm, function (result) {
                var data = $.parseJSON(result);
                $("input[id*=txtdStart]").val(data.dStart);
                $("input[id*=txtdEnd]").val(data.dEnd);
                $("input[id*=sTerm]").val(data.sTerm);
                $("input[id*=nTerm]").val(data.nTerm);
                $('#btnadd').val("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>");
                $(".modal-header h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>");
                $("#modalsystem").modal('toggle');
                $.unblockUI();
            });
        }
        function deldata(nTerm) {
            $.confirm({
                title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                content: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132683") %></h3>',
                theme: 'bootstrap',
                buttons: {
                    confirm: {
                        text: '<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></span>',
                        btnClass: 'bttn btn-danger col-md-4',
                        //keys: ['enter', 'shift'],
                        action: function () {
                            $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
                            PageMethods.DeleteData(nTerm, function (result) {
                                var jsonRes = JSON.parse(result);
                                if (jsonRes.StatusCode == "200") {
                                    getListData();
                                    errorMessage("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132684") %>");
                                } else {
                                    errorMessage("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132685") %>");
                                    $.unblockUI();
                                }
                            });
                        }
                    },
                    Cancel: {
                        text: '<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></span>',
                        btnClass: 'bttn btn-default col-md-4 pull-right',
                        //keys: ['enter', 'shift'],
                        action: function () {
                        }
                    }
                }
            });
        }

        function errorMessage(contentMessage) {
            $.confirm({
                title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                content: '<h3>' + contentMessage + '</h3>',
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": function () {
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <input id="YearId" type="hidden" />
    <input id="Year" type="hidden" />
    <div class="full-card box-content yearsettting-container">
        <div class="row">
            <%--  <div class="col-sm-12" id="list" style="min-height: 200px;">
            </div>--%>
            <table class="table" id="table-data">
                <thead>
                    <tr style="background-color: #3F51B5; color: white; text-align: center;">
                        <td style="width: 35%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132624") %></td>
                        <td style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132686") %></td>
                        <td style="width: 25%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %></td>
                        <td>
                            <input type="button" class="btn btn-success" id="btnnewitem" style="min-width: auto;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %>" /></td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="row">
            <div class="col-md-3 col-xs-12"></div>
            <%--            <div class="col-md-3 col-xs-6">
                <input type="button" id="btnsave" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" class="btn btn-success col-xs-12 col-md-11" />
            </div>--%>
            <div class="col-md-3 col-xs-6">
                <a href="/Modules/TimeAttendance/yearlist.aspx" class="btn btn-danger col-xs-12 col-md-11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></a>
            </div>
            <div class="col-md-3 col-xs-12"></div>
        </div>
    </div>
    <div id="modalsystem" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 25%; font-size: 22px;">
        <div class="modal-md" style="top: 50px;">
            <div class="modal-content" style="max-width: 900px;">
                <div class="modal-header center">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102152") %></h1>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6 col-xs-12">
                            <div class="col-md-4 col-xs-12 col-row-space">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>
                            </div>
                            <div class="col-md-8 col-xs-12 col-row-space">
                                <asp:DropDownList CssClass="yearsettting-dropdown form-control" ID="ddlnYear" runat="server">
                                    <asp:ListItem Value="2550" Text="2550" />
                                    <asp:ListItem Value="2551" Text="2551" />
                                    <asp:ListItem Value="2552" Text="2552" />
                                    <asp:ListItem Value="2553" Text="2553" />
                                    <asp:ListItem Value="2554" Text="2554" />
                                    <asp:ListItem Value="2555" Text="2555" />
                                    <asp:ListItem Value="2556" Text="2556" />
                                    <asp:ListItem Value="2557" Text="2557" />
                                    <asp:ListItem Value="2558" Text="2558" />
                                    <asp:ListItem Value="2559" Text="2559" />
                                    <asp:ListItem Value="2560" Text="2560" />
                                    <asp:ListItem Value="2561" Text="2561" />
                                    <asp:ListItem Value="2562" Text="2562" />
                                    <asp:ListItem Value="2563" Text="2563" />
                                    <asp:ListItem Value="2564" Text="2564" />
                                    <asp:ListItem Value="2565" Text="2565" />
                                    <asp:ListItem Value="2566" Text="2566" />
                                    <asp:ListItem Value="2567" Text="2567" />
                                    <asp:ListItem Value="2568" Text="2568" />
                                    <asp:ListItem Value="2569" Text="2569" />
                                    <asp:ListItem Value="2570" Text="2570" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-12">
                            <div class="col-md-4 col-xs-12 col-row-space">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %>
                            </div>
                            <div class="col-md-8 col-xs-12 col-row-space">
                                <input type="text" class="form-control" id="sTerm" />
                                <input type="hidden" class="form-control" id="nTerm" />
                            </div>
                        </div>
                    </div>
                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-6 col-xs-12">
                            <div class="col-md-4 col-xs-12 col-row-space">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132686") %>
                            </div>
                            <div class="col-md-8 col-xs-12 date-container col-row-space">
                                <div class="input-group" data-placement="left" data-align="top" data-autoclose="true">
                                    <asp:TextBox ID="txtdStart" runat="server" class="form-control datepicker" data-date-format="mm/dd/yyyy"
                                        aria-describedby="basic-addon2" placeholder="mm/dd/yyyy" />
                                    <span class="input-group-addon" onclick="$('input[id*=txtdStart]').datepicker('show')">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-12">
                            <div class="col-md-4 col-xs-12 col-row-space">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %>
                            </div>
                            <div class="col-md-8 col-xs-12 date-container col-row-space">
                                <div class="input-group" data-placement="left" data-align="top" data-autoclose="true" data-provide="datepicker">
                                    <asp:TextBox ID="txtdEnd" runat="server" class="form-control datepicker" data-date-format="mm/dd/yyyy"
                                        aria-describedby="basic-addon2" placeholder="mm/dd/yyyy" />
                                    <span class="input-group-addon" onclick="$('input[id*=txtdEnd]').datepicker('show')">
                                        <span class="fa  fa-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row--space"></div>
                    <div class="row">
                        <div class="col-md-3 col-xs-12"></div>
                        <div class="col-md-3 col-xs-6">
                            <div id="btnadd" class="btn btn-primary col-xs-12 col-md-11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></div>
                        </div>
                        <div class="col-md-3 col-xs-6">
                            <div id="btncancel" class="btn btn-danger col-xs-12 col-md-11" onclick="$('#modalsystem').modal('hide')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></div>
                        </div>
                        <div class="col-md-3 col-xs-12"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
