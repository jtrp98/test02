<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="roomlist.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.roomlist1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/bootstrap-select.css" rel="stylesheet" />
    <script src="/Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/createTable.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#modalpopup-data .modal-header h1").html("ข้้อมูลห้องเรียน");
            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "sClassID": $("#sClassID").val(), "sClass": $("#sClass").val(),
                };
                PageMethods.updatedata(data, function (result) {
                    loaddata();
                },
                    function (result) {
                        alert(result._meassage);
                    })
            });

            $("#modalpopup-data-cancel").click(function () {
                clearinput();
            });

            $("#modalconfirm-delete-confirm").click(function () {
                var behavior_id = $("#deleteid").val();
                PageMethods.deletedata(behavior_id, function (result) {
                    loaddata();
                },
                    function (result) {
                        alert(result._meassage);
                    })
            })
        })

        function popupadd() {
            clearinput();
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132655") %>");
            $("#modalpopup-data").modal("show");
        }

        function popupedit(behavior_id) {
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801005") %>");
            PageMethods.getdata(behavior_id,
                function (result) {
                    console.log(result);
                    if (result.length > 0) {
                        $("#modalpopup-data").modal("show");
                        $("#sClassID").val(result[0].Rooms_Id);
                        $("#sClass").val(result[0].Rooms_Name);
                    }
                },
                function (result) {
                    console.log(result);
                })
        }

        function popupdelete(behavior_id) {
            $("#modalconfirm-delete h3").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801007") %>");
            $("#modalconfirm-delete").modal("show");
            $("#modalconfirm-delete #modalconfirm-delete-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801008") %>");
            $("#deleteid").val(behavior_id);
        }

        function clearinput() {
            $("#sClassID").val("");
            $("#sClass").val("");
        }

    </script>
    <script type="text/javascript">
        var pageSize = 20, pageNumber = 1;
        var wording = "", product_type = null;
        let shop_id = getUrlParameter("shop_id");
        let product_id = 0;
        var load_data = [];
        var temp = new TemplateTable();
        var BehaviorType = 1;
        $(document).ready(function () {

            temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
            temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });

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
        })

        function changePage(stepPage) {
            if (stepPage === 1) pageNumber += 1;
            else pageNumber -= 1;
            loaddata();
            document.documentElement.scrollTo(0, 0);
        }

        function Search() {
            pageNumber = 1;
            loaddata();
        }

        function deletedata(id) {
            product_id = id;
            $('#modalconfirm-delete #modal-delete-header').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>");
            $('#modalconfirm-delete #modalconfirm-delete-content').html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>");
            $('#modalconfirm-delete').modal('show');
        }

        function loaddata() {
            var search_data = {
                "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber
            };
            $('#target').html('<tr><td colspan="5" class="text-center"><i class="fa fa-spin fa-refresh fa-3x"></i><h1>Loading</h1></td></tr>');
            PageMethods.returnlist(search_data,
                function (respones) {
                    temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
                    temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
                    temp.RenderRows(respones);
                    $("#modalpopup-data").modal("hide");
                    $("#modalconfirm-delete").modal("hide");
                },
                function (respones) {
                    console.log(respones);
                }
            )
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <div class="full-card planelist-container">

        <div id="Div1" class="full-card box-content row--space holiday-table-container">
            <div class="tab-content row">
                <div class="tab-pane fade in active" id="bad" style="background: white;">
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <table class="cool-table"
                                style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="2" border="0">
                                <thead>
                                    <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                        <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <%--<th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132656") %></th>--%>
                                        <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801043") %></th>
                                        <th class="centertext" scope="col" style="">
                                            <a href="#" onclick="clearinput()" class="btn btn-success"
                                                data-toggle="modal" data-target="#modalpopup-data">
                                                <span class="glyphicon glyphicon-plus"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                                        </th>
                                        <th class="centertext" scope="col" style=""></th>
                                    </tr>
                                </thead>
                                <tbody id="target">
                                    <% var q = returnlist(new Search { pageSize = 20, pageNumber = 1 });

                                        foreach (var data in q.DATA)
                                        {%>
                                    <tr class="itemCell" style="font-weight: unset; font-style: normal; text-decoration: none;">
                                        <td class="center" scope="col" style="width: 10%;"><%=data.Row %></td>
                                        <%--<td class="center" scope="col" style="width: 20%;"><%=data.Rooms_Id %></td>--%>
                                        <td class="center" scope="col" style="width: 20%;"><%=data.Rooms_Name %></td>
                                        <td class="center" scope="col" style="width: 12%;">
                                            <div class="btn btn-info minor-button" data-toggle="modal"
                                                data-target="#modalpopup-data" onclick="popupedit('<%= data.Rooms_Id %>');">
                                                <span class="glyphicon glyphicon-pencil global-btn"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                            </div>
                                        </td>
                                        <td class="center" scope="col" style="width: 12%;">
                                            <div class="btn btn-danger minor-button" onclick="popupdelete('<%= data.Rooms_Id %>');">
                                                <span class="glyphicon glyphicon-trash"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                            </div>
                                        </td>
                                    </tr>
                                    <%}
                                    %>
                                </tbody>
                                <tfoot>
                                    <tr style="color: #337AB7; background-color: #337AB7; border-color: #337AB7;">
                                        <td colspan="10">
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
                                                                    <%  int i = 1;
                                                                        for (i = 1; i <= q.FOOT.pageSize; i++)
                                                                        {
                                                                    %>
                                                                    <option value="<%= i%>"><%= i%></option>
                                                                    <%
                                                                        }
                                                                    %>
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
                                                        <span id="spane_pageNumber" style="color: White;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> 1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> <%= q.FOOT.pageSize %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %></span>
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
            </div>
        </div>
    </div>
    <script id="tmpl-mustache" type="x-tmpl-mustache">
        {{#DATA}}
        <tr class="itemCell" style="font-weight: normal; font-style: normal; text-decoration: none;">       
            <td class="center" scope="col" style="width: 10%;">{{Row}}</td>
            <td class="center" scope="col" style="width: 12%;">{{Rooms_Name}}</td>
            <td class="center" scope="col" style="width: 12%;">
                <div class="btn btn-info minor-button" data-toggle="modal"
                    data-target="#modalpopup-data" onclick="popupedit('{{Rooms_Id}}');">
                    <span class="glyphicon glyphicon-pencil global-btn"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                </div>
            </td>
            <td class="center" scope="col" style="width: 12%;" onclick="popupdelete('{{Rooms_Id}}');">
                <div class="btn btn-danger minor-button">
                    <span class="glyphicon glyphicon-trash"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                </div>
            </td>
        </tr>
        {{/DATA}}
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
    <div id="productpopup">
        <div class="row planadd-row">
            <div class="col-xs-12 col-md-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801045") %></label>
            </div>
            <div class="col-xs-12 col-md-8">
                <input type="hidden" id="sClassID" value="0" />
                <input class="form-control" type="text" clientidmode="Static"
                    id="sClass" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
