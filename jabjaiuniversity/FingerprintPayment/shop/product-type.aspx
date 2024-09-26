<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="product-type.aspx.cs" Inherits="FingerprintPayment.product_type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap-select.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <%--<script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>--%>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="createTable.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content">
        <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %> <%= shop.shop_name %> </h1>
        <input type="hidden" value="" id="shop_id" />
        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-6 col-md-6 col-sm-12">
                    <div class="form-group">
                        <div class="input-group">
                            <div class="input-group-addon">
                                <span class="h3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601030") %></span>
                            </div>
                            <input type="text" id="txtSearch" class='form-control' style="width: 100%;" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
                            <div class="input-group-addon btn" onclick="Search();"><i class="fa fa-search"></i></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <table class="cool-table" cellspacing="0" cellpadding="2" border="0"
                        style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;">
                        <thead>
                            <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                                </th>
                                <th class="center" scope="col" style="width: 75%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601031") %></th>
                                <th class="centertext" scope="col" style="width: 15%;">
                                    <a class="btn btn-success" id="btnadd" style="min-width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                                </th>
                            </tr>
                        </thead>
                        <tbody id="target">
                            <tr>
                                <td colspan="4" class="text-center"><i class="fa fa-spin fa-refresh"></i>
                                    <h1>Loading</h1>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr style="color: #337AB7; background-color: #337AB7; border-color: #337AB7;">
                                <td colspan="3">
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
                    <script id="tmpl-mustache" type="x-tmpl-mustache">
                    {{#DATA}}
                        <tr class="itemCell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                            <td class="centertext">
                                {{index}}
                            </td>
                            <td class="paymentgroup_name center">{{type_name}}</td>
                            <td class="centertext">
                                <a style="cursor:pointer;" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>"
                                    onclick="editdata('{{type_id}}','{{type_name}}');" ><i class="glyphicon glyphicon-edit info"></i></a>
                                <a style="cursor:pointer;color:red;" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>"
                                    onclick="deletedata('{{type_id}}');" ><i class="glyphicon glyphicon-remove"></i></a>
                            </td>
                        </tr>
                    {{/DATA}}
                    </script>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div id="modal-popup-data" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog global-modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 id="headerpopup"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601033") %></h1>
                </div>
                <div class="modal-body product-add-container" id="modalpopupdata-content">
                    <div class="row">
                        <div class="col-xs-4">
                            <label class="pull-right global-label">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601031") %></label>
                        </div>
                        <div class="col-xs-8">
                            <input type="text" id="sType" name="sType" class="form-control" />
                            <input type="hidden" id="nTypeID" name="nTypeID" value="0" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <input type="submit" id="btnsave" class="btn btn-success global-btn" style="width: 100px;" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                    &nbsp;<input type="button" id="btnCancel" class="btn btn-danger global-btn" style="width: 100px;" data-dismiss="modal" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $("#btnsave").click(function (e) {
                e.preventDefault();

                if ($("#aspnetForm").validate()) {
                    $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
                    $("#modal-popup-data").modal("hide");
                    PageMethods.update_data({ "sType": $("#sType").val(), "nTypeID": $("#nTypeID").val(), "shop_id": $("#shop_id").val() }
                        , function (result) {
                            loaddata();
                            $.unblockUI();
                        }, function () {
                            $.unblockUI();
                        });
                }
            });

            //$("#aspnetForm").validate({
            //    rules: {
            //        sType: "required"
            //    },
            //    messages: {
            //        sType: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
            //    },
            //    tooltip_options: {
            //        sType: { placement: 'right', trigger: 'focus' }
            //    },
            //    submitHandler: function (form) {
            //        $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
            //        $("#modal-popup-data").modal("hide");
            //        PageMethods.update_data({ "sType": $("#sType").val(), "nTypeID": $("#nTypeID").val(), "shop_id": $("#shop_id").val() }
            //            , function (result) {
            //                loaddata();
            //                $.unblockUI();
            //            }, function () {
            //                $.unblockUI();
            //            });
            //    }
            //});
        });

        $("#btnadd").click(function () {
            clear();
        });
        //});;

        function editdata(id, name) {
            $("#modal-popup-data").modal("show");
            $("#sType").val(name);
            $("#nTypeID").val(id);
        }

        function clear() {
            $("#modal-popup-data").modal("show");
            $("#sType").val("");
            $("#nTypeID").val("0");
        }
    </script>

    <script type="text/javascript">
        var pageSize = 20, pageNumber = 1;
        var wording = "";
        let productType_id = 0;
        var load_data = <%= returnlist(new Search { pageNumber = 1, pageSize = 20, wording = "",shop_id = Request.QueryString["shop_id"] }) %>;
        var temp = new TemplateTable();
        $(document).ready(function () {
            temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
            temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
            temp.RenderRows(load_data);
            $("#shop_id").val(getUrlParameter("shop_id"));

            $("#pageNumber").change(function () {
                pageNumber = parseInt($(this).val());
                loaddata();
                document.documentElement.scrollTo(0, 0);
            });

            $("#pageSize").change(function () {
                pageNumber = 1;
                pageSize = parseInt($(this).val());
                loaddata();
                document.documentElement.scrollTo(0, 0);
            });

            $("#txtSearch").keypress(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    Search();
                }
            })

            $("#modalconfirm-delete-confirm").click(function (e) {
                e.preventDefault();
                $.blockUI({ message: '<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101325") %></h1>' });
                $('#modalconfirm-delete').modal('hide');
                PageMethods.delete_data(productType_id,
                    function (response) {
                        if (response === "Success") {
                            loaddata();                          
                            productType_id = 0;
                            $.unblockUI();
                        }
                    },
                    function (response) {
                        $.unblockUI();
                    });
            });
        })

        function deletedata(id) {
            productType_id = id;
            $('#modalconfirm-delete #modal-delete-header').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>");
            $('#modalconfirm-delete #modalconfirm-delete-content').html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>");
            $('#modalconfirm-delete').modal('show');
        }

        function changePage(stepPage) {
            if (stepPage === 1) pageNumber += 1;
            else pageNumber -= 1;
            loaddata();
            document.documentElement.scrollTo(0, 0);
        }

        function Search() {
            wording = $("#txtSearch").val();
            pageNumber = 1;
            loaddata();
        }

        function loaddata() {
            var data = { "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber, "shop_id": $("#shop_id").val() }
            $('#target').html('<tr><td colspan="3" class="text-center"><i class="fa fa-spin fa-refresh fa-3x"></i><h1>Loading</h1></td></tr>');
            PageMethods.returnlist(data,
                function (respones) {
                    respones = $.parseJSON(respones);
                    console.log(respones);
                    var template = $('#tmpl-mustache').html();
                    Mustache.parse(template);   // optional, speeds up future uses
                    var rendered = Mustache.render(template, respones);
                    $('#target').html(rendered);

                    $("#pageNumber option").remove();
                    var i = 1;
                    if (respones.FOOT.pageSize > 0) {
                        while (i <= respones.FOOT.pageSize) {
                            $("#pageNumber").append($("<option></option>")
                                .attr("value", i).text(i));
                            i++;
                        }
                    }
                    else {
                        $("#pageNumber").append($("<option></option>")
                            .attr("value", i).text(i));
                    }
                    $("#pageNumber").val(pageNumber);
                    $("#spane_pageNumber").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> " + pageNumber + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> " + respones.FOOT.pageSize + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>");
                    if ($("#pageNumber").val() === "1") $("#backbutton").hide();
                    else $("#backbutton").show();
                    if (parseInt($("#pageNumber").val()) === $("#pageNumber option").length) $("#nextbutton").hide();
                    else $("#nextbutton").show();
                    $('.selectpicker').selectpicker('refresh');
                },
                function (respones) {
                    console.log(respones);
                }
            )
        }
    </script>
</asp:Content>
