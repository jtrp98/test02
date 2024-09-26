<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true"
    CodeBehind="yearlist.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.yearlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .form-check .form-check-sign:before {
            /*  height: 10px !important;
            width: 10px !important;*/
        }

        .form-check .form-check-label span {
            /*left: 2px;
            top: -8px;*/
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            getPermission();
            Loadedatepicker();

            $("#btnAddItem").click(function () {
                $("#divChangePassword").append($("#Trem-Item").html());
                Loadedatepicker();
            });

            //PageMethods.GetYearData(function (result) {
            //    let _html = "";

            //    $.each(result, function (e, s) {
            //        _html += "<tr><td>"
            //        _html += "<td>"
            //        _html += "<td>"
            //        _html += "<td>"
            //    });

            //    $("#tableData tbody").html()
            //});
            $("#btnSaveChangePassword").click(function () {
                let data = [];
                let YearId = $("[name=YearId]").val(), numberYear = $("[name=Year]").val();
                for (i = 0; i < $("[name*=dStart]").length; i++) {
                    let _dStart = $("[name*=dStart]")[i];
                    let _dEnd = $("[name*=dEnd]")[i];
                    let _Term = $("[name*=TermName]")[i];
                    let _TermId = $("[name*=TermId]")[i];

                    if ($(_Term).val() != '') {
                        data.push({
                            "dStart": DataEN($(_dStart).val()),
                            "dEnd": DataEN($(_dEnd).val()),
                            "sTerm": $(_Term).val(),
                            "nTerm": $(_TermId).val()
                        });
                    }
                }

                PageMethods.UpdateData(YearId, numberYear, data
                    , $('#check1').is(':checked')
                    , $('#check2').is(':checked')
                    , $('#check3').is(':checked')
                    , function (result) {

                        $("#modal-data").modal("toggle");
                        loaddatatable();

                        var r = JSON.parse(result);
                        if (r.Status == 200) {
                            swal.fire({
                                title: '',
                                html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00891") %>',
                                type: 'info',
                                confirmButtonClass: 'btn btn-danger',
                                confirmButtonText: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                                buttonsStyling: false
                            });
                        }
                        else {
                            swal.fire({
                                title: '',
                                html: r.Des,
                                type: 'warning',
                                confirmButtonClass: 'btn btn-danger',
                                confirmButtonText: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                                buttonsStyling: false
                            }).then((res) => {
                                //if (res && r.Status != 400) {
                                //    getData(r.Data.YearId);
                                //}
                            });
                        }
                    });
            });

            loaddatatable();
        });

        function DataEN(_date) {
            if (_date != '') {
                let _d = new Date(moment(_date, "DD/MM/YYYY"));
                let _s = (_d.getMonth() + 1) + "/";
                _s += _d.getDate() + "/";
                _s += (_d.getFullYear() - 543);
                return moment(_s).format("MM/DD/YYYY");
            } else {
                return '';
            }
        }

        function Loadedatepicker() {
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
        }

        var table = null;

        function loaddatatable() {
            PageMethods.GetYearData(function (result) {
                if (table != null) {
                    table.destroy();
                }
                table = $('#tableData').DataTable({
                    "processing": true,
                    "data": result,
                    "searching": false,
                    "paging": false,
                    "columns": [
                        {
                            data: null,
                            className: "tab-1",
                            render: function (data, type, full, meta) {
                                //I want to get row index here somehow
                                return meta.row + 1;
                            }
                        },
                        { "data": "numberYear" },
                        { "data": "count" },
                        {
                            "className": "center",
                            "data": function (data) {
                                //if (data.isActive == true) {
                                //    return "<a href='#' onclick='changeStatus(" + data.nYear + ",this)' style='font-weight: bolder;'><span style='color:green;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132680") %></span></a>";
                                //}
                                //else {
                                //    return "<a href='#' onclick='changeStatus(" + data.nYear + ",this)' style='font-weight: bolder;'><span style='color:red;'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132681") %></span></a>";
                                //}
                                return '<div class="btn btn-info btnpermission" style="min-width: auto;" onclick="getData(' + data.nYear + ')"><i class="material-icons">playlist_add</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></div>';
                            }
                        },
                    ],
                    "fnRowCallback": function (nRow, aData, iDisplayIndex, Page) {
                        $("td:first", nRow).html(Page + 1);
                        return nRow;
                    },
                    "columnDefs": [
                        {
                            'targets': [0, 3],
                            'searchable': true,
                            'orderable': false
                        }
                    ]
                });

            });
        }

        function getData(nYear) {
            // Clear input
            $("#divChangePassword").find("[data-mode=add-item]").remove();

            $.each($("#divChangePassword").find('input'), function (index, row) {
                $(row).val('');
            });

            $('input[name^=TermName][value=1]').val(1);
            $('input[name^=TermName][value=2]').val(2);

            if (nYear == 0) {
                $('.copy-wrapper').show();
                $('#modal-data').modal('show');

                $('#Year').val("<%= (DateTime.Now.AddYears(543).Year) %>");
                $('#Year').prop('disabled', false);
                $('#Year').selectpicker('refresh');
            }
            else {
                $('.copy-wrapper').hide();
                PageMethods.ListData(nYear, function (result) {
                    let json = $.parseJSON(result);

                    $('#Year').val(json.YearName);
                    $('#YearId').val(json.Year);
                    $('#Year').prop('disabled', true);
                    $('#Year').selectpicker('refresh');

                    if (json.Data.length > 0) {
                        $.each(json.Data, function (index, row) {
                            if (index >= 2) {
                                $("#divChangePassword").append($("#Trem-Item").html());
                                Loadedatepicker();
                            }

                            let v1 = document.getElementsByName('TermName[]');
                            let v2 = document.getElementsByName('dStart[]');
                            let v3 = document.getElementsByName('dEnd[]');
                            let v4 = document.getElementsByName('TermId[]');

                            $(v1[index]).val(row.sTerm);
                            $(v2[index]).val(row.dStart);
                            $(v3[index]).val(row.dEnd);
                            $(v4[index]).val(row.nTerm);
                        });
                    }
                    else {
                        $('input[name^=TermName][value=1]').val(1);
                        $('input[name^=TermName][value=2]').val(2);
                    }

                    $('#modal-data').modal('show');
                });
            }
            return false;
        }

        function DeleteRow(_index) {
            // console.log();
            $(_index).parents("div[data-mode=add-item]").remove();
        }

        function getPermission() {
            PageMethods.GetPermission(OnMyMethodComplete);
        }
        function OnMyMethodComplete(result, userContext, methodName) {
            if (result == 1) {
                $('.btnpermission').css("display", "none");
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 31 && charCode < 48) || charCode > 57) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" ScriptMode="Release">
    </asp:ScriptManager>


    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803032") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01235") %>
            </p>
        </div>
    </div>

    <div class="employeeList row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="large material-icons">access_time</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802002") %></h4>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <table id="tableData" class="table table-no-bordered table-hover" cellspacing="0" width="100%" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></th>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802004") %></th>
                                        <th>
                                            <div class="btn btn-success btnpermission" style="min-width: auto;" onclick="getData(0);"><i class="material-icons">playlist_add</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
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


    <div id="modal-data" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 5%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="min-width: 700px;">
                <div class="modal-header text-center" style="padding: 0px; top: 5%; display: block;">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></h3>
                </div>
                <div class="row"></div>
                <div class="modal-body product-add-container" id="modalpopup-data-content">
                    <div id="divChangePassword" class="ml-4">
                        <form id="frmChangePassword">

                            <div class="row">
                                <div class="col-md-2 mb-2 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                                </div>
                                <div class="col-sm-3 pl-1">
                                    <select id="Year" name="Year" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %></option>
                                        <%
                                            for (int YearNumber = 2500; YearNumber <= 2600; YearNumber++)
                                            {
                                        %>
                                        <option value="<%= YearNumber%>"><%= YearNumber%></option>
                                        <%
                                            }
                                        %>
                                    </select>
                                    <%--<input id="Year" name="Year" type="text" class="form-control" style="padding-left: 7px;" required="required" maxlength="4" onkeypress="return isNumber(event)" onpaste="return false;" />--%>
                                    <input id="YearId" name="YearId" type="text" class="form-control" style="padding-left: 7px; display: none;" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2 mb-0 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                                </div>
                                <div class="col-sm-3 pl-1">
                                    <input id="TermName[]" name="TermName[]" type="text" class="form-control" style="padding-left: 7px;" value="1" required="required" disabled />
                                    <input id="TermId[]" name="TermId[]" type="text" class="form-control" style="padding-left: 7px; display: none;" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2 mb-0 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701070") %> :</label>
                                </div>
                                <div class="col-md-3 mb-3 pl-1">
                                    <div class="div-datepicker">
                                        <input name="dStart[]" type="text" class="form-control datepicker" required="required" />
                                        <span class="form-control-feedback mr-2" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %> :</label>
                                </div>
                                <div class="col-md-3 mb-3 pl-1">
                                    <div class="div-datepicker">
                                        <input name="dEnd[]" type="text" class="form-control datepicker" required="required" />
                                        <span class="form-control-feedback mr-2" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2 mb-0 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                                </div>
                                <div class="col-sm-3 pl-1">
                                    <input id="TermName[]" name="TermName[]" type="text" class="form-control" style="padding-left: 7px;" value="2" required="required" disabled />
                                    <input id="TermId[]" name="TermId[]" type="text" class="form-control" style="padding-left: 7px; display: none;" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2 mb-2 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701070") %> :</label>
                                </div>
                                <div class="col-md-3 mb-3 pl-1">
                                    <div class="div-datepicker">
                                        <input name="dStart[]" type="text" class="form-control datepicker" required="required" />
                                        <span class="form-control-feedback mr-2" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-3 mb-2 col-form-label text-right">
                                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %> :</label>
                                </div>
                                <div class="col-md-3 mb-3 pl-1">
                                    <div class="div-datepicker">
                                        <input name="dEnd[]" type="text" class="form-control datepicker" required="required" />
                                        <span class="form-control-feedback mr-2" style="color: #000; opacity: 1;">
                                            <i class="material-icons">event</i>
                                        </span>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12 text-center mt-3">
                                    <div class="btn btn-info" id="btnAddItem">
                                        <i class="large material-icons">add</i>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802005") %>
                                    </div>
                                </div>
                            </div>
                            <div class="copy-wrapper" style="display:none">
                                <div class="row mt-4">
                                    <div class="">
                                        <label for=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802006") %> :</label>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col-md-1 mb-0 ">
                                        <div class="form-check form-check-inline">

                                            <label class="form-check-label">
                                                <input class="form-check-input" type="checkbox" id="check1" name="check1">
                                                &nbsp;
         <span class="form-check-sign"><span class="check"></span></span>
                                            </label>

                                        </div>
                                    </div>
                                    <div class="col-md-10 pl-1">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132674") %> <span style="color:red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132675") %></span></label>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col-md-1 mb-0 ">
                                        <div class="form-check form-check-inline">
                                            <%-- <label class="form-check-label" style="">
                                            <input id="check2" name="check2" runat="server" class="form-check-input choose-term valid" type="checkbox" aria-required="true">
                                            <span class="form-check-sign">
                                                <span class="check"></span>
                                            </span>
                                        </label>--%>
                                            <label class="form-check-label">
                                                <input class="form-check-input" type="checkbox" id="check2" name="check2">&nbsp;
                                            <span class="form-check-sign"><span class="check"></span></span>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-10 pl-1">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132676") %> <span style="color:red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132677") %></span></label>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col-md-1 mb-0">
                                        <div class="form-check form-check-inline">
                                            <%--  <span class="form-check-label" style="">
                                            <input id="check3" name="check3" runat="server" class="form-check-input choose-term valid" type="checkbox" aria-required="true">
                                            <span class="form-check-sign">
                                                <span class="check"></span>
                                            </span>
                                        </span>--%>
                                            <label class="form-check-label">
                                                <input class="form-check-input" type="checkbox" id="check3" name="check3">&nbsp;
     <span class="form-check-sign"><span class="check"></span></span>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-10 pl-1">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132678") %> <span style="color:red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132679") %></span></label>
                                    </div>
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
                    <button type="button" id="btnSaveChangePassword" class="btn btn-success global-btn">
                        <i class="material-icons">save</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" id="btnCancelChangePassword" class="btn btn-danger global-btn" data-dismiss="modal">
                        <i class="material-icons">cancel</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <script type="text/template" id="Trem-Item">
        <div data-mode="add-item">
            <div class="row">
                <div class="col-md-2 mb-0 col-form-label text-right">
                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                </div>
                <div class="col-sm-3 pl-1">
                    <input id="TermName[]" name="TermName[]" type="text" class="form-control" style="padding-left: 7px;" required="required" />
                    <input id="TermId[]" name="TermId[]" type="text" class="form-control" style="padding-left: 7px; display: none;" />
                </div>

                <div class="col-md-2 m-0 p-0 pt-2 col-form-label text-left text-danger">
                    <i class="material-icons" style="cursor: pointer;" id='Delete[]' name="Delete[]" onclick="DeleteRow(this)">delete_forever</i>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2 mb-2 col-form-label text-right">
                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701070") %> :</label>
                </div>
                <div class="col-md-3 mb-3 pl-1">
                    <div class="div-datepicker">
                        <input name="dStart[]" type="text" class="form-control datepicker" required="required" />
                        <span class="form-control-feedback mr-2" style="color: #000; opacity: 1;">
                            <i class="material-icons">event</i>
                        </span>
                    </div>
                </div>
                <div class="col-md-3 mb-2 col-form-label text-right">
                    <label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701073") %> :</label>
                </div>
                <div class="col-md-3 mb-3 pl-1">
                    <div class="div-datepicker">
                        <input name="dEnd[]" type="text" class="form-control datepicker" required="required" />
                        <span class="form-control-feedback mr-2" style="color: #000; opacity: 1;">
                            <i class="material-icons">event</i>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
