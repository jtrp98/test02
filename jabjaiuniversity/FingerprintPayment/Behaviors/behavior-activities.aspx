<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="behavior-activities.aspx.cs" Inherits="FingerprintPayment.behavior_activities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap-select.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/createTable.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#modalpopup-data-submit").click(function () {
                var data = {
                    "BehaviorId": $("#BehaviorId").val(), "BehaviorName": $("#BehaviorName").val(), "BehaviorNameEN": $("#BehaviorNameEN").val(),
                    "Score": $("#Score").val(), "Type": $("#Type").val()
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
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302003") %>");
            $("#modalpopup-data").modal("show");
        }

        function popupedit(behavior_id) {
            $("#modalpopup-data h1").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132033") %>");
            PageMethods.getdata(behavior_id,
                function (result) {
                    console.log(result);
                    if (result.length > 0) {
                        if (result[0].CreatedBy == null) {
                            $("#BehaviorName").attr("disabled", true);
                        } else {
                            $("#BehaviorName").attr("disabled", false);
                        }
                        $("#modalpopup-data").modal("show");
                        $("#BehaviorId").val(result[0].BehaviorId);
                        $("#BehaviorName").val(result[0].BehaviorName);
                        $("#BehaviorNameEN").val(result[0].BehaviorNameEN);
                        $("#Score").val(result[0].Score);
                        $("#Type").val(parseInt(result[0].Type));
                        $("#Type").attr("disabled", true);
                    }
                },
                function (result) {
                    console.log(result);
                })
        }

        function popupdelete(behavior_id) {
            $("#modalconfirm-delete h3").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01820") %>");
            $("#modalconfirm-delete").modal("show");
            $("#modalconfirm-delete #modalconfirm-delete-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801008") %>");
            $("#deleteid").val(behavior_id);
        }

        function clearinput() {
            $("#BehaviorId").val("0");
            $("#BehaviorName").val("");
            $("#Score").val("");
            $("#Type").val("0");
            $("#Type").attr("disabled", false);
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

        function Search(Type) {
            BehaviorType = Type;
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
                "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber,
                "BehaviorType": parseInt(BehaviorType)
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
        <%--        <a href="plans-term.aspx" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131220") %></a> <a href="periodslist.aspx"
            class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131226") %></a>--%>

        <div id="Div1" class="full-card box-content row--space holiday-table-container">
            <%--   <ul id="myTab" class="nav nav-tabs nav-tabs-title">
                <li class="active"><a href="#bad" data-toggle="tab" onclick="Search(0)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302004") %></a></li>
                <li><a data-toggle="tab" href="#bad" onclick="Search(1)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></a></li>
                <li><a data-toggle="tab" href="#bad" onclick="Search(2)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302005") %></a></li>
            </ul>--%>
            <ul class="nav nav-tabs" style="width: 90%; font-size: 30px;">
                <li class="active">
                    <a data-toggle="tab" style="color: #000;" href="#" onclick="Search(1)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302004") %></a>
                </li>
                <li>
                    <a data-toggle="tab" style="color: #000;" href="#" onclick="Search(0)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></a>
                </li>
                <li>
                    <a data-toggle="tab" style="color: #000;" href="#" onclick="Search(2)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302005") %></a>
                </li>
            </ul>
            <div class="tab-content row">
                <div class="tab-pane fade in active" id="bad" style="background: white;">
                    <div class="col-xs-12">
                        <div class="wrapper-table">
                            <table class="cool-table"
                                style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="2" border="0">
                                <thead>
                                    <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                        <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                        <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302006") %></th>
                                        <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302007") %></th>
                                        <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302008") %></th>
                                        <th class="centertext" scope="col" style="">
                                            <a href="#" onclick="clearinput()" class="btn btn-success btnpermission"
                                                data-toggle="modal" data-target="#modalpopup-data">
                                                <span class="glyphicon glyphicon-plus"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                                        </th>
                                        <th class="centertext" scope="col" style=""></th>
                                    </tr>
                                </thead>
                                <tbody id="target">
                                    <% var q = returnlist(new Search { pageSize = 20, pageNumber = 1, BehaviorType = 1 });

                                        foreach (var data in q.DATA)
                                        {%>
                                    <tr class="itemCell" style="font-weight: unset; font-style: normal; text-decoration: none;">
                                        <td class="center" scope="col" style="width: 10%;"><%=data.Row %></td>
                                        <td class="center" scope="col" style="width: 20%;"><%=data.Score %></td>
                                        <td class="center" scope="col" style="width: 20%;"><%=data.BehaviorName %></td>
                                        <td class="center" scope="col" style="width: 20%;"><%=data.BehaviorNameEN %></td>
                                        <td class="center" scope="col" style="width: 12%;">
                                            <div class="btn btn-info minor-button btnpermission" data-toggle="modal"
                                                data-target="#modalpopup-data" onclick="popupedit('<%= data.BehaviorId %>');">
                                                <span class="glyphicon glyphicon-pencil global-btn"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                            </div>
                                        </td>
                                        <td class="center" scope="col" style="width: 12%;">
                                            <div class="btn btn-danger minor-button btnpermission" onclick="popupdelete('<%= data.BehaviorId %>');">
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
            <td class="center" scope="col" style="width: 20%;">{{Score}}</td>
            <td class="center" scope="col" style="width: 12%;">{{BehaviorName}}</td>
            <td class="center" scope="col" style="width: 12%;">{{BehaviorNameEN}}</td>
            <td class="center" scope="col" style="width: 12%;">
                <div class="btn btn-info minor-button btnpermission" data-toggle="modal"
                    data-target="#modalpopup-data" onclick="popupedit('{{BehaviorId}}');">
                    <span class="glyphicon glyphicon-pencil global-btn"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                </div>
            </td>
            <td class="center" scope="col" style="width: 12%;" onclick="popupdelete('{{BehaviorId}}');">
                <div class="btn btn-danger minor-button btnpermission">
                    <span class="glyphicon glyphicon-trash"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                </div>
            </td>
        </tr>
        {{/DATA}}
    </script>
</asp:Content>

<asp:Content ClientIDMode="AutoID" ID="modalpopup" runat="server" ContentPlaceHolderID="modalpopup">
    <div id="productpopup">
        <div class="row planadd-row">
            <div class="col-xs-12 col-md-4">
                <label>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302007") %></label>
            </div>
            <div class="col-xs-12 col-md-8">
                <input type="hidden" id="BehaviorId" value="0" />
                <input class="form-control" type="text" clientidmode="Static"
                    id="BehaviorName" runat="server" />
            </div>
        </div>
            <div class="row planadd-row">
        <div class="col-xs-12 col-md-4">
            <label>
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302008") %></label>
        </div>
        <div class="col-xs-12 col-md-8">
            <input class="form-control" type="text" clientidmode="Static"
                id="BehaviorNameEN" runat="server" />
        </div>
    </div>
        <div class="row" id="row-name">
            <div class="col-xs-12 col-md-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
            </div>
            <div class="col-xs-12 col-md-8">
                <select id="Type" class="form-control" disabled>
                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01256") %></option>
                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01816") %></option>
                </select>
            </div>
        </div>
        <div class="row planadd-row">
            <div class="col-xs-12 col-md-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302006") %></label>
            </div>
            <div class="col-xs-12 col-md-8">
                <input class="form-control" id="Score" onkeypress='return event.charCode >= 48 && event.charCode <= 57' />
            </div>
        </div>
    </div>
</asp:Content>
