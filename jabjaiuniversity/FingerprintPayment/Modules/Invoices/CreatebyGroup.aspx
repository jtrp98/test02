<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="CreatebyGroup.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.CreatebyGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .bd-example-modal-lg .modal-dialog {
            display: table;
            position: relative;
            margin: 0 auto;
            top: calc(50% - 24px);
        }

        .btn.btn-round.btn-info {
            background-color: #00bcd4;
            border-top: 1px solid #00bcd4;
            border-bottom: 1px solid #00bcd4;
            background-color: white;
            color: black;
        }

            .btn.btn-round.btn-info:last-child {
                border-radius: 0px 10px 10px 0px;
                border-right: 1px solid #00bcd4;
            }

            .btn.btn-round.btn-info:first-child {
                border-radius: 10px 0px 0px 10px;
                border-left: 1px solid #00bcd4;
            }

            .btn.btn-round.btn-info.active {
                background-color: #00bcd4;
                border-top: 1px solid #00bcd4;
                border-bottom: 1px solid #00bcd4;
                background-color: #00bcd4;
                color: white;
            }

        .filter-option-inner-inner {
            padding-top: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02720") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02116") %> 
            </p>
        </div>
    </div>

    <div class="employeeList row">
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
                        <div class="col-sm-3 div-select-input">
                            <select id="sltYear" name="sltYear[]"
                                class="selectpicker col-sm-12" data-style="select-with-transition">
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltTerm" name="sltTerm[]"
                                class="selectpicker col-sm-12" data-style="select-with-transition" required="required">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltLevel" name="sltLevel[]"
                                class="selectpicker col-sm-12" data-style="select-with-transition">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <asp:Literal ID="ltrLevel" runat="server" />
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltClass" name="sltClass[]" multiple="multiple"
                                class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>" required="required">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
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
                            <div id="btnSearch" class="btn btn-info" onclick="LoadStudent()">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->

            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">list</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02817") %></h4>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01985") %></label>
                        <div class="col-sm-3">
                            <div class="form-group div-datepicker">
                                <input id='iptissuedDate' type="text" class="form-control datepicker" required="required" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132474") %></label>
                        <div class="col-sm-3">
                            <div class="form-group div-datepicker">
                                <input id='iptDueDate' type="text" class="form-control datepicker" required="required" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltSearchInvoicesType" class="selectpicker col-sm-12" data-style="select-with-transition">
                                <option value="0" selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103207") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501004") %></option>
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltGropInvoices" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103220") %>">
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103207") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501004") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-11 mr-auto text-center">
                            <div id="btnAddProduct" class="btn btn-info">
                                <span class="btn-label">
                                    <i class="material-icons">add</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <div class="table-responsive">
                                <table id="tableProduct" class="table table-borderless">
                                    <thead class="text-primary">
                                        <th width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <th width="50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></th>
                                        <th width="15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></th>
                                        <th width="15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503017") %></th>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->

            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">people</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02816") %></h4>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-10">
                            <div class="btn-group" id="class-list">
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <table id="tableStdent" class="table table-borderless">
                                    <thead class="text-primary">
                                        <th width="10%">
                                            <div class="row">
                                                <div class="col-md-12 checkbox-radios">
                                                    <div class="form-check form-check-inline">
                                                        <label class="form-check-label">
                                                            <input class="form-check-input" type="checkbox" id="check-all">&nbsp;
                                                            <span class="form-check-sign"><span class="check"></span></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </th>
                                        <th width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                        <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                        <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132475") %></th>
                                        <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503017") %></th>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 mr-auto text-left">
                            <div id="btnDraft" class="btn btn-success" onclick="insertInoives('Draft')">
                                <span class="btn-label">
                                    <i class="material-icons">save</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>
                            </div>
                        </div>
                        <div class="col-md-6 mr-auto text-center">
                        </div>
                        <div class="col-md-3 mr-auto text-right">
                            <div id="btnBack" class="btn btn-default">
                                <span class="btn-label">
                                    <i class="material-icons">arrow_left</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00070") %>
                            </div>
                            <div id="btnSeve" class="btn btn-info" onclick="insertInoives('Approve')">
                                <span class="btn-label">
                                    <i class="material-icons">send</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00913") %>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />

    <link href="../../Content/jquery-confirm.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-confirm.js"></script>

    <link rel="stylesheet" href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="../../Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <!-- DatetimePicker -->
    <%--<link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />--%>

    <script type="text/javascript">
        var TermId = "";
        var Fd_LevelID = "";

        $(document).ready(function () {
            $(document).ajaxStart(function () {
                // Show image container
                Swal.fire({
                    html: '<i class="fa fa-circle-o-notch fa-spin" style="font-size:40px"></i><br/>Loading',
                    timerProgressBar: true,
                    showConfirmButton: false,
                    allowOutsideClick: false,
                }).then((result) => {
                })
                console.info("mLoading : show");
            });
            $(document).ajaxComplete(function () {
                // Hide image container
                console.info("mLoading : hide");
            });

        });

        $(function () {

            $("#btnBack").click(function () {
                window.location.href = "CreatebyGroupHistory.aspx";
            });

            $("#check-all").click(function () {
                $("#tableStdent tbody input[type=checkbox]").prop("checked", $("#check-all").prop("checked"));
            });

            // Search
            $("#sltYear").change(function () {
                LoadTerm($(this).val(), '#sltTerm');
            });

            $("#sltLevel").change(function () {
                LoadTermSubLevel2($(this).val(), '#sltClass');
            });

            LoadInvoices();

            $(".selectpicker").selectpicker('refresh');

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

        });

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $("body").mLoading({
                    icon: "/scripts/blockUI/ProgressGreen.gif"
                });
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // Show image container
                    },
                    success: function (response) {
                        $("body").mLoading("hide");
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        Swal.close();
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

        var InvoicesItem = [];
        var studentItem = [];

        function LoadStudent() {
            let send_data = "{ \"TermId\": \"" + $("#sltTerm").val() + "\", \"RoomId\": [" + $("#sltClass").val() + "] }";

            if ($.parseJSON(send_data).RoomId.length == 0) {
                Swal.fire({
                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                    html: "<h4> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132476") %></h4>",
                    showConfirmButton: true,
                    confirmButtonClass: "btn btn-danger",
                    confirmButtonText: '<span class="btn-label"><i class= "material-icons">close</i></span > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                    //type: 'success',
                    allowOutsideClick: false
                })
                return;
            }

            $.ajax({
                async: false,
                type: "POST",
                url: "CreatebyGroup.aspx/LoadStudent",
                data: send_data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    TermId = $("#sltTerm").val();
                    Fd_LevelID = $("#sltLevel").val();

                    studentItem = JSON.parse(response.d);
                    console.log(studentItem);

                    getClassData(null);

                    let _html = "";

                    $("#class-list button").remove();

                    _html += '<button type="button" class="btn btn-round btn-info active" onclick="getClassData(null)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></button>';

                    $.each(studentItem, function (e, s) {
                        _html += '<button type="button" class="btn btn-round btn-info" onclick="getClassData(' + s.key + ')">' + s.lable + '</button>';
                    });

                    $("#class-list").html(_html);

                    $(".btn.btn-round.btn-info").click(function () {
                        $(".btn.btn-round.btn-info").removeClass("active")
                        $(this).addClass("active");
                    });
                    Swal.close();
                },
                failure: function (response) {
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function getClassData(keyID) {
            let r = [];
            if (keyID === null) {
                $.each(studentItem, function (e, s) {
                    $.each(s.item, function (e1, s1) {
                        r.push(s1);
                    });
                });
            } else {
                var r1 = studentItem.find(f => f.key == keyID);
                if (r !== null) {
                    r = r1.item;
                }
            }

            if (r.length > 0) {
                var rows = '';
                $.each(r, function (index, row) {
                    rows += `
<tr data-sid="` + row.sID + `">
    <td>
        <div class="row">
            <div class="col-md-12 checkbox-radios">
                <div class="form-check form-check-inline">
                    <label class="form-check-label">
                        <input class="form-check-input" type="checkbox">&nbsp;
                        <span class="form-check-sign">
                            <span class="check"></span>
                        </span>
                    </label>
                </div>
            </div>
        </div>
    </td>
    <td>
        <div class="row">
            <label class="col-md-12" style="margin-bottom: 0px;">`+ (index + 1) + `</label>
        </div>
    </td>
    <td>
        <div class="row">
            <label class="col-md-12" style="margin-bottom: 0px;">`+ row.sStudentID + `</label>
        </div>
    </td>
    <td>
        <label class="col-md-12" style="margin-bottom: 0px;">`+ row.sName + ' ' + row.sLastname + `</label>
    </td>
    <td>
        <label class="col-md-12" style="margin-bottom: 0px;">`+ row.SubLevel + '/' + row.nTSubLevel2 + `</label>
    </td>
    <td>
        <input type="text" class="form-control" id="Discount" name="Discount" />
    </td>
</tr>`;
                });

                $('#tableStdent tbody tr').remove();
                $('#tableStdent tbody').html(rows);

                $("#tableStdent tbody input[type=checkbox]").prop("checked", $("#check-all").prop("checked"));

                $("#tableStdent tbody input[type=checkbox]").click(function () {
                    let e1 = $("#tableStdent tbody input[type=checkbox]:checked").length;
                    let e2 = $("#tableStdent tbody input[type=checkbox]").length;

                    $("#check-all").prop("checked", e1 == e2);
                });
            }

        }

        function LoadInvoices() {
            let objResult = "#sltGropInvoices";

            $.ajax({
                async: false,
                type: "POST",
                url: "CreatebyGroup.aspx/LoadGropInvoices",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = JSON.parse(response.d);

                    $(objResult).empty();

                    LoadItemList(objResult, data, "0");
                    $("#sltSearchInvoicesType").change(function () {
                        LoadItemList(objResult, data, $("#sltSearchInvoicesType").val());
                    });

                    $("#btnAddProduct").click(function () {
                        let _value = $(objResult).val();
                        if ($(objResult).val() == "") {
                            Swal.fire({
                                title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                                html: "<h4> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132481") %></h4>",
                                showConfirmButton: true,
                                confirmButtonClass: "btn btn-danger",
                                confirmButtonText: '<span class="btn-label"><i class= "material-icons">close</i></span > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                                //type: 'success',
                                allowOutsideClick: false
                            })
                            return;
                        }

                        if ($("#sltSearchInvoicesType").val() == "1") {
                            let _select = data.byItem.find(x => x.nPaymentID == _value);
                            console.log(_select);
                            if (InvoicesItem.find(x => x.nPaymentID == _value) === undefined) {
                                InvoicesItem.push({ "nPaymentID": _select.nPaymentID, "sPayment": _select.sPayment, "Price": _select.Price });
                            }
                        } else {
                            let _select = data.byGroup.find(x => x.PaymentGroupID == _value);
                            console.log(_select);
                            $.each(_select.Item, function (Index, data) {
                                if (InvoicesItem.find(x => x.nPaymentID == data.nPaymentID) === undefined) {
                                    InvoicesItem.push({ "nPaymentID": data.nPaymentID, "sPayment": data.sPayment, "Price": data.Price });
                                }
                            });
                        }
                        renderHtml();
                    });
                    Swal.close();
                },
                failure: function (response) {
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

        function renderHtml() {
            let rows = "";
            $.each(InvoicesItem, function (e, row) {
                rows += `
                    <tr data-productid="` + row.nPaymentID + `">
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;">`+ (e + 1) + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" id="Item-Name">`+ row.sPayment + `</label>
                            </div>
                        </td>
                        <td>
                            <input type="text" class="form-control" id="Price" name="Item-Price" value="` + row.Price + `"/>
                        </td>
                        <td>
                            <input type="text" class="form-control" id="Discount" name="Item-Discount"/>
                        </td>
                    </tr>`;
            });

            $("#tableProduct tbody").html(rows);
        }

        function LoadItemList(objResult, data, type) {

            if (type == "1") {
                var options = '';
                $(objResult).selectpicker({ title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103207") %>" });
                $(data.byItem).each(function () {

                    options += '<option value="' + this.nPaymentID + '">' + this.sPayment + '</option>';

                });
                $(objResult).html(options);
                $(objResult).selectpicker('refresh');

            } else {
                var options = '';
                $(objResult).selectpicker({ title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103220") %>" });
                $(data.byGroup).each(function () {

                    options += '<option value="' + this.PaymentGroupID + '">' + this.GroupName + '</option>';

                });

                $(objResult).html(options);
                $(objResult).selectpicker('refresh');

            }
        }

        function CheckInput(invoicesItems, iStudents) {
            let _c = false, _message = "";
            ////Trem
            //if ($("").val() === "") {
            //    _message = "";
            //}
            ////Class ID
            //else if ($("")) {
            //    _message = "";
            //}
            //DueDate
            if ($("#iptDueDate").val() === "" || $("#iptDueDate").val() == undefined) {
                _message = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132482") %>";
            }
            //issuedDate
            else if ($("#iptissuedDate").val() === "" || $("#iptissuedDate").val() == undefined) {
                _message = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132483") %>";
            }
            //invoicesItems
            else if (invoicesItems.length == 0) {
                _message = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132484") %>";
            }
            //iStudents
            else if (iStudents.length == 0) {
                _message = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132485") %>";
            } else {
                _c = true;
            }

            if (_c == false) {

                Swal.fire({
                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                    html: "<h4>" + _message + "</h4>",
                    showConfirmButton: true,
                    confirmButtonClass: "btn btn-success",
                    confirmButtonText: '<span class="btn-label"><i class= "material-icons">close</i></span > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                    type: 'success',
                    allowOutsideClick: false
                })
            }
            return _c;
        }

        function insertInoives(invoicesStatus) {
            let invoicesItems = [], iStudents = [];
            let issuedDate = $("#iptissuedDate").val(), DueDate = $("#iptDueDate").val();

            $.each($("#tableProduct tbody tr"), function (_index, _data) {
                let _Discount = $(_data).find("input[id=Discount]").val();
                let _Price = $(_data).find("input[id=Price]").val();
                let _ProductName = $(_data).find("label[id=Item-Name]").html();
                invoicesItems.push({ "ProductId": $(_data).attr("data-productid"), "Price": _Price, "Discount": _Discount, "ProductName": _ProductName });
            });

            $.each($("#tableStdent tbody tr"), function (_index, _data) {
                let _checked = $(_data).find("input[type=checkbox]").prop("checked");
                if (_checked) {
                    let _Discount = $(_data).find("input[id=Discount]").val();
                    iStudents.push({ "StudentId": $(_data).attr("data-sid"), "Discount": _Discount });
                }
            });

            let jsonDATA = {
                "invoicesItems": invoicesItems, "issuedDate": issuedDate, "DueDate": DueDate,
                "iStudents": iStudents, "TermId": TermId, "invoicesStatus": invoicesStatus,
                "Fd_LevelID": Fd_LevelID
            };

            if (CheckInput(invoicesItems, iStudents)) {
                swal({
                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132477") %></h3>',
                    text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132478") %>',
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonText: '<span class="btn-label"><i class= "material-icons">save</i></span > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>',
                    cancelButtonText: '<span class="btn-label"><i class= "material-icons">close</i></span > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                    confirmButtonClass: "btn btn-success",
                    cancelButtonClass: "btn btn-danger",
                    buttonsStyling: false,
                    allowOutsideClick: false
                }).then(function (e) {
                    if (e.value == true) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "CreatebyGroup.aspx/InsertInvoices",
                            data: JSON.stringify({ data: jsonDATA }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                var data = JSON.parse(response.d);

                                Swal.fire({
                                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132479") %> <br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132480") %>',
                                    showConfirmButton: true,
                                    confirmButtonClass: "btn btn-success",
                                    confirmButtonText: '<span class="btn-label"><i class= "material-icons">close</i></span > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                                    type: 'success',
                                    allowOutsideClick: false
                                }).then(function () {
                                    window.location.href = "/Modules/Invoices/CreatebyGroupHistory.aspx";
                                });

                                //$("#tableProduct tbody tr").remove();
                                //$("#tableStdent tbody tr").remove();
                                //invoicesItems = [];
                                //iStudents = [];
                            },
                            failure: function (response) {
                                console.log(response.d);
                            },
                            error: function (response) {
                                console.log(response.d);
                            }
                        });

                    } else {
                        console.log(e.dismiss);
                    }
                })
            }
        }

        function LoadTerm(yearID, objResult) {
            if (yearID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTerm",
                    data: '{yearID: ' + yearID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var yearData = response.d;

                        $(objResult).empty();

                        if (yearData.length > 0) {

                            var options = '';
                            $(yearData).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                            $(objResult).selectpicker('refresh');
                        }
                        Swal.close();
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

    </script>
</asp:Content>
