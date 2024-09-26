<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Material2.Master" CodeBehind="CheckPaymentGroup.aspx.cs" Inherits="FingerprintPayment.Modules.Reports.CheckPaymentGroup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-form.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Content/hummingbird-treeview/hummingbird-treeview.min.css" rel="stylesheet" type="text/css" />
    <style>
        .material-form .form-control {
            padding: 0px 0px 0px 5px;
            margin-top: 0px;
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

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02720") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503027") %> 
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
                            <select id="sltClass" name="sltClass"
                                class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>" multiple="multiple" required="required">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601030") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltProduct" name="sltProduct"
                                class="selectpicker col-sm-12" data-style="select-with-transition" title="" required="required">
                                <asp:Literal ID="ltrProduct" runat="server" />
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132544") %></option>
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></label>
                        <div class="col-sm-3 div-select-input">
                            <select id="sltStatus" name="sltStatus"
                                class="selectpicker col-sm-12" data-style="select-with-transition" required="required">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501030") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %></option>
                            </select>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                        <div class="col-sm-3">
                            <div class="form-group div-datepicker">
                                <input id="DateStart" name="DateStart" type="text" class="form-control datepicker">
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">today</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %></label>
                        <div class="col-sm-3 div-select-input">
                            <div class="form-group div-datepicker">
                                <input id="DateEnd" name="DateEnd" type="text" class="form-control datepicker">
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">today</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-2"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-3 div-select-input"></div>
                        <div class="col-sm-5 div-select-input">
                            <button type="button" class="btn btn-primary btn-sm" style="font-size: 20px" id="btnAdvSearch" onclick="GetReportsData()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                            <button type="button" class="btn btn-success btn-sm" style="font-size: 20px" id="btnExport" onclick="export_excel();">Export Excel</button>
                        </div>
                        <div class="col-sm-2 div-select-input">
                        </div>
                    </div>
                </div>
            </div>
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
                        <table class="table" id="table-data">
                            <thead>
                                <tr>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503028") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503029") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503030") %></th>
                                    <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot></tfoot>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script src="/Content/hummingbird-treeview/hummingbird-treeview.min.js"></script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <link rel="stylesheet" href="../../Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="../../Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script src="../../Scripts/FileSaver.js" type="text/javascript"></script>
    <script src="../../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script>
        var idx = 0;

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
        });

        $(function () {

            // Search
            $("#sltYear").change(function () {
                LoadTerm($(this).val(), '#sltTerm');
            });

            $("#sltLevel").change(function () {
                LoadTermSubLevel2($(this).val(), '#sltClass');
                LoadProduct($("#sltLevel").val(), $("#sltTerm").val(), '#sltProduct');
            });

        })

        function GetReportsData() {
            let data = {
                "TermId": $("[id*=sltTerm]").val(),
                "SubLevel2Id": JSON.stringify($("[id*=sltClass]").val()).replace("[", "").replace("]", "").replaceAll("\"", ""),
                "StudentId": "", "Product": $("[id*=sltProduct]").val(), "PaymentType": $("[id*=sltStatus]").val(),
                "DateStart": $("#DateStart").val(), "DateEnd": $("#DateEnd").val()
            };

            let errorMessage = "";

            if (data.SubLevel2Id == "") {
                errorMessage = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111085") %>";
            } else if (data.Product == "") {
                errorMessage = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00062") %>";
            }

            if (errorMessage != "") {
                Swal.fire({
                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                    html: "<h4>" + errorMessage + "</h4>",
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
                url: "/Modules/Reports/CheckPaymentGroup.aspx/ReturnList",
                data: JSON.stringify({ "search": data }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    // Show image container                
                },
                success: function (response) {
                    $("#table-data tbody tr").remove();
                    $("#table-data tfoot tr").remove();
                    let data = response.d.Data;
                    let _html = "";
                    let GrandTotal = 0;
                    let OutstandingAmount = 0;
                    let Paid_PaymentAmount = 0;

                    $.each(data, function (e, s) {
                        let rowspan = s.Term_Data.length;
                        if (rowspan > 0) {
                            _html += "<tr><td rowspan=" + rowspan + " class='align-top'>" + (e + 1) + "</td><td rowspan=" + rowspan + " class='align-top'>" + s.SubLevel + "/" + s.nTSubLevel2 + "</td>";

                            $.each(s.Term_Data, function (e1, s1) {
                                GrandTotal += s1.GrandTotal ?? 0;
                                OutstandingAmount += s1.OutstandingAmount ?? 0;
                                Paid_PaymentAmount += s1.Paid_PaymentAmount ?? 0;

                                _html += "<td class=\"text-center\">" + s1.StudentID;
                                _html += "<td class=\"text-center\">" + s1.StudentName;
                                _html += "<td class=\"text-right\">" + s1.GrandTotal.toLocaleString(0, { minimumFractionDigits: 2 });
                                _html += "<td class=\"text-right\">" + s1.OutstandingAmount.toLocaleString(0, { minimumFractionDigits: 2 });
                                _html += "<td class=\"text-right\">" + s1.Paid_PaymentAmount.toLocaleString(0, { minimumFractionDigits: 2 });

                                let ReceiptNo = "";
                                $.each(s1.ReceiptNo, function (e2, s2) {
                                    if (ReceiptNo === "") ReceiptNo = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02753") %> " + s2;
                                    else ReceiptNo = ReceiptNo + "," + s2;
                                });
                                _html += "<td class=\"text-left\">" + ReceiptNo + "</td>";
                                _html += "</tr><tr>"
                            });

                            _html += "</td>"
                        }
                    })

                    let _tfoot = "<tr><td colspan='4' class=\"text-right\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %><td class=\"text-right\">" + GrandTotal.toLocaleString(0, { minimumFractionDigits: 2 })
                        + "<td class=\"text-right\">" + OutstandingAmount.toLocaleString(0, { minimumFractionDigits: 2 })
                        + "<td class=\"text-right\">" + Paid_PaymentAmount.toLocaleString(0, { minimumFractionDigits: 2 }) + "<td>";

                    let percent_OutstandingAmount = 0;
                    let percent_Paid_PaymentAmount = 0;

                    if (OutstandingAmount == 0 && Paid_PaymentAmount == 0) {
                        percent_OutstandingAmount = 100.00;
                        percent_Paid_PaymentAmount = 0.00;
                    } else {
                        percent_OutstandingAmount = ((OutstandingAmount * 100) / GrandTotal);
                        percent_Paid_PaymentAmount = ((Paid_PaymentAmount * 100) / GrandTotal);

                    }

                    _tfoot += "<tr><td colspan='4' class=\"text-right\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206205") %><td class=\"text-right\">100.00</td>"
                        + "<td class=\"text-right\">" + percent_OutstandingAmount.toLocaleString(0, { minimumFractionDigits: 2 })
                        + "<td class=\"text-right\">" + percent_Paid_PaymentAmount.toLocaleString(0, { minimumFractionDigits: 2 }) + "<td>";

                    $("#table-data tbody").append(_html);
                    $("#table-data tfoot").append(_tfoot);

                    console.log();
                    //$("#mypanel").html(text);
                },
                failure: function (response) {
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }

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

                        LoadProduct($("#sltLevel").val(), $("#sltTerm").val(), '#sltProduct');
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

        function LoadProduct(subLevelID, TermID, objResult) {
            if (subLevelID) {
                $("body").mLoading({
                    icon: "/scripts/blockUI/ProgressGreen.gif"
                });
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/Modules/Reports/CheckPaymentGroup.aspx/LoadProduct?subLevelID=" + subLevelID + "&TermID=" + TermID,
                    //url: "/StudentInfo/StudentList.aspx/LoadProduct",
                    data: '{subLevelID: ' + subLevelID + ',TermID: "' + TermID + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // Show image container
                    },
                    success: function (response) {
                        $("body").mLoading("hide");
                        var subLevel2 = response.d.Data;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132544") %></option>';
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

        function export_excel() {
            $("body").mLoading();
            var d = new Date();

            let datestring = ("0" + d.getDate()).slice(-2) + ("0" + (d.getMonth() + 1)).slice(-2) +
                d.getFullYear() + ("0" + d.getHours()).slice(-2) + ("0" + d.getMinutes()).slice(-2) + ("0" + d.getMilliseconds()).slice(-2);

            var json;
            var xhr;
            let DateStart = "", DateEnd = "", sltClass = "";

            $.each($("[id*=sltClass] option:selected"), function (e, s) {
                if (sltClass != "") sltClass += ",";
                sltClass += $(s).text();
            })

            var searchData = {
                "TermId": $("[id*=sltTerm]").val(),
                "SubLevel2Id": JSON.stringify($("[id*=sltClass]").val()).replace("[", "").replace("]", "").replaceAll("\"", ""),
                "StudentId": "", "Product": $("[id*=sltProduct]").val(), "PaymentType": $("[id*=sltStatus]").val(),
                "DateStart": $("#DateStart").val(), "DateEnd": $("#DateEnd").val(),
                "filter": [$("[id*=sltYear] option:selected").text(), $("[id*=sltTerm] option:selected").text(),
                $("[id*=sltLevel] option:selected").text(), sltClass,
                $("[id*=sltProduct] option:selected").text(), $("[id*=sltStatus] option:selected").text(),
                $("[id*=DateStart]").val(), $("[id*=DateEnd]").val()]
            };

            var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132545") %> ' + datestring + '.xls';
            //this.RenderHtml_Detail('table_exports', true);
            json = JSON.stringify({ search: searchData });
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/Modules/Reports/CheckPaymentGroup.aspx/ExportExcel", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, file_name);
                $("body").mLoading('hide');
            };
            xhr.send(json);

            var param = {
                "filename": file_name,
                "tabledata": $("#export_excel").html()
            };

        };

    </script>

</asp:Content>
