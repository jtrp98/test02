<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="CreatebyGroupHistory.aspx.cs"
    Inherits="FingerprintPayment.Modules.Invoices.CreatebyGroupHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/Content/font-awesome.css" rel="stylesheet" />
    <style type="text/css">
        .material-icons.spin {
            animation-name: spin;
            animation-duration: 5000ms;
            animation-iteration-count: infinite;
            animation-timing-function: linear;
            /* transform: rotate(3deg); */
            /* transform: rotate(0.3rad);/ */
            /* transform: rotate(3grad); */
            /* transform: rotate(.03turn);  */
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02720") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02116") %> 
            </p>
        </div>
    </div>


    <div class="History row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">search</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210022") %></h4>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="table-responsive">
                                <a href="CreatebyGroup.aspx" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02116") %></a>
                                <table id="tableHistory" class="table table-borderless">
                                    <thead class="text-primary">
                                        <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132486") %></th>
                                        <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501017") %></th>
                                        <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></th>
                                        <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></th>
                                        <th width="3%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                        <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107053") %></th>
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
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <div class="History-Detail row" style="display: none;">
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
                        <label class="col-sm-3 col-form-label text-left" id="Fd_YearNumber"></label>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <label class="col-sm-3 col-form-label text-left" id="Fd_TermNumber"></label>
                    </div>
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                        <label class="col-sm-3 col-form-label text-left" id="Fd_LevelName"></label>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <label class="col-sm-3 col-form-label text-left" id=""></label>
                    </div>
                </div>
            </div>

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
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132487") %></label>
                        <label class="col-sm-3 col-form-label text-left" id="issuedDate"></label>

                        <div class="col-sm-1"></div>
                        <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132474") %></label>
                        <label class="col-sm-3 col-form-label text-left" id="dueDate"></label>

                        <div class="col-sm-2"></div>
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
                        <div class="col-md-3 mr-auto text-right">
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
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <br />
            </div>
        </div>
        <!-- end content-->
    </div>
    <!--  end card  -->



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script>
        $(function () {
            Swal.fire({
                //html: '<i class="fa fa-circle-o-notch fa-spin" style="font-size:40px"></i><br/>Loading',
                html: '<span class= "btn-label" ><i class="material-icons spin" style="font-size: 100px">sync</i></span ><br/>Loading',
                timerProgressBar: true,
                showConfirmButton: false,
                allowOutsideClick: false,
            }).then((result) => {
            })

        });

        $(document).ready(function () {
            $(document).ajaxStart(function () {
                // Show image container
                Swal.fire({
                    //html: '<i class="fa fa-circle-o-notch fa-spin" style="font-size:40px"></i><br/>Loading',
                    html: '<span class= "btn-label" ><i class="material-icons spin" style="font-size: 100px">sync</i></span ><br/>Loading',
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

            LoadHistory();

            $("#btnBack").click(function () {
                $(".History-Detail").hide();
                $(".History").show();
            });
        });

        function LoadData(Fd_UID) {
            let send_data = { "Fd_UID": Fd_UID };
            $.ajax({
                async: false,
                type: "POST",
                url: "CreatebyGroupHistory.aspx/LoadData",
                data: JSON.stringify({ "Fd_UID": Fd_UID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    let data = response.d;
                    let rows = "";

                    $("#Fd_YearNumber").html(data.Fd_YearNumber);
                    $("#Fd_TermNumber").html(data.Fd_TermNumber);
                    $("#Fd_LevelName").html(data.Fd_LevelName);

                    let json = $.parseJSON(response.d.Fd_JSON);

                    $("#dueDate").html(json.dueDate);
                    $("#issuedDate").html(json.issuedDate);

                    randerStudent(json.StudentInfo);
                    randerProduct(json.ProductInfo);

                    console.log(json);
                    $(".History-Detail").show();
                    $(".History").hide();
                    Swal.close();
                },
                failure: function (response) {
                    console.log(response.d);
                    Swal.close();
                },
                error: function (response) {
                    console.log(response.d);
                    Swal.close();
                }
            });
        }

        function LoadHistory() {
            let send_data = { "startDate": "", "endDate": "" };
            $.ajax({
                async: false,
                type: "POST",
                url: "CreatebyGroupHistory.aspx/LoadHistory",
                data: JSON.stringify({ data: send_data }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    let data = response.d;
                    let rows = "";

                    $.each(data, function (e, row) {
                        let Fd_InvoiceStatus = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>";
                        if (row.Fd_InvoiceStatus != "Draft") Fd_InvoiceStatus = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00913") %>";

                        rows += `
                    <tr data-productid="` + row.nPaymentID + `">
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;">`+ (e + 1) + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                               <a href="#" onclick="LoadData('`+ row.Fd_UID + `')" class="col-md-12" style="margin-bottom: 0px;" >` + row.Fd_HistoryID + `</a>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" >`+ row.Fd_CreateBy + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" >`+ row.Fd_LevelName + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" >`+ row.Fd_Amount.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" >`+ Fd_InvoiceStatus + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" >`+ row.Fd_CreateDate + `</label>
                            </div>
                        </td>
                    </tr>`;
                    });

                    $("#tableHistory tbody").html(rows);

                    Swal.close();
                },
                failure: function (response) {
                    console.log(response.d);
                    Swal.close();
                },
                error: function (response) {
                    console.log(response.d);
                    Swal.close();
                }
            });
        }

        function randerStudent(data) {
            let rows = "";
            $.each(data, function (index, row) {
                rows += `
<tr data-sid="` + row.sID + `">
    <td>
        <div class="row">
            <label class="col-md-12" style="margin-bottom: 0px;">`+ (index + 1) + `</label>
        </div>
    </td>
    <td>
        <div class="row">
            <label class="col-md-12" style="margin-bottom: 0px;">`+ row.StudentId + `</label>
        </div>
    </td>
    <td>
        <label class="col-md-12" style="margin-bottom: 0px;">`+ row.StudentName + `</label>
    </td>
    <td>
        <label class="col-md-12" style="margin-bottom: 0px;">`+ row.ClassName + `</label>
    </td>
    <td>
        <label class="col-md-12 text-right" style="margin-bottom: 0px;">`+ (row.Discount ?? 0).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + `</label>
    </td>
</tr>`;
            });

            $('#tableStdent tbody tr').remove();
            $('#tableStdent tbody').html(rows);
        }

        function randerProduct(data) {
            let rows = "";

            $.each(data, function (e, row) {
                rows += `
                    <tr data-productid="` + row.nPaymentID + `">
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;">`+ (e + 1) + `</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <label class="col-md-12" style="margin-bottom: 0px;" id="Item-Name">`+ row.ProductName + `</label>
                            </div>
                        </td>
                        <td>
                            <label class="col-md-12 text-right" style="margin-bottom: 0px;">`+ (row.Price ?? 0).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + `</label>
                        </td>
                        <td>
                            <label class="col-md-12 text-right" style="margin-bottom: 0px;">`+ (row.Discount ?? 0).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + `</label>
                        </td>
                    </tr>`;
            });

            $("#tableProduct tbody").html(rows);
        }

    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
