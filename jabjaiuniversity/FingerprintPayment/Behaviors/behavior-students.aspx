<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="behavior-students.aspx.cs" Inherits="FingerprintPayment.behavior_students"
    EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="JSBehaviors.js"></script>


    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="../Content/bootstrap-select.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/createTable.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('#ctl00_MainContent_ddlsublevel').val(getUrlParameter("idlv"));

            funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2", getUrlParameter("idlv2"));
            availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");

            $('input[id*=txtSearch]').val(getUrlParameter("sname"));

            $('#ctl00_MainContent_ddlsublevel').change(function () {
                funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });


            $('input[id*=btnSearch]').click(function () {
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlSubLV2] option:selected').val();
                var param3var = $('input[id*=txtSearch]').val();
                window.location.href = "behavior-students.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&sname=" + param3var;
            });

            //$('input[id*=txtSearch]').autocomplete({
            //    width: 300,
            //    max: 10,
            //    delay: 100,
            //    minLength: 1,
            //    maxLength: 10,
            //    autoFocus: true,
            //    cacheLength: 1,
            //    scroll: true,
            //    highlight: false,
            //    source: function (request, response) {
            //        var results = $.ui.autocomplete.filter(availableValueplane, request.term);
            //        response(results.slice(0, 10));
            //    },
            //    select: function (event, ui) {
            //        event.preventDefault();
            //        $("input[id*=txtSearch]").val(ui.item.label);
            //    },
            //    focus: function (event, ui) {
            //        event.preventDefault();
            //    }
            //});
        });
    </script>
    <script type="text/javascript">
        var pageSize = 20, pageNumber = 1;
        var wording = "", product_type = null;
        let shop_id = getUrlParameter("shop_id");
        let product_id = 0;
        var load_data = [];
        var temp = new TemplateTable();
        var lv_id = null;
        var lv2_id = null;
        var behavior_list = [];
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

            $("#btnSearch").click(function () {
                Search();
            });

            $("#txtSearch").keypress(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    Search();
                }
            });

            $("select[id*=ddlcType]").change(function () {
                if ($(this).val() === "") product_type = null;
                else product_type = parseInt($(this).val());
                $("#txtSearch").val("");
                wording = $("#txtSearch").val();
                pageNumber = 1;
                loaddata();
            });

            $("#modalconfirm-delete-confirm").click(function (e) {
                e.preventDefault();
                PageMethods.delete_data(product_id,
                    function (response) {
                        if (response === "Success") {
                            loaddata();
                            $('#modalconfirm-delete').modal('hide');
                            product_id = 0;
                        }
                    },
                    function (response) {
                    });
            });

            PageMethods.BehaviorList(function (result) {
                behavior_list = $.parseJSON(result);
            });
        });

        function changePage(stepPage) {
            if (stepPage === 1) pageNumber += 1;
            else pageNumber -= 1;
            loaddata();
            document.documentElement.scrollTo(0, 0);
        }

        function Search() {
            wording = $("#ctl00_MainContent_txtSearch").val();
            lv_id = $("#ctl00_MainContent_ddlsublevel").val();
            lv2_id = $("#ddlSubLV2").val();
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
            var data = {
                "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber,
                "product_type": product_type, "shop_id": shop_id,
                "lv2_id": lv2_id, "lv_id": lv_id
            };
            $('#target').html('<tr><td colspan="7" class="text-center"><i class="fa fa-spin fa-refresh fa-3x"></i><h1>Loading</h1></td></tr>');
            PageMethods.returnlist(data,
                function (respones) {
                    temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
                    temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
                    temp.RenderRows(respones);
                },
                function (respones) {
                    console.log(respones);
                }
            );
        }

        function setpopup(user_id, student_Id, student_Name, type) {
            $("#studentID").val(student_Id);
            $("#studentName").val(student_Name);
            $("#user_id").val(user_id);
            if (type === "add") {
                $("#modalpopupbehavior .modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01249") %>");
            } else {
                $("#modalpopupbehavior .modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01815") %>");
            }
            var json_data = getObjects(behavior_list, 'behavior_type', type);

            $("#behavior_list option").remove();
            $.each(json_data, function (index, data) {
                $("#behavior_list").append($("<option>", { text: data.behavior_name, value: data.behavior_id }));
            });
        }

        function Update_behavior() {
            var data = { student_id: $("#user_id").val(), behavior_id: $("#behavior_list option:selected").val(), note: $("#Note").val() };
            $("#btnsave").addClass("disabled");
            PageMethods.UpdateScore(data, function (result) {
                console.log(result);
                $("#modalpopupbehavior").modal("hide");
                $("#Note").val("");
                $("#btnsave").removeClass("disabled");
                loaddata();
            }, function (result) {
                console.log(result);
                $("#Note").val("");
                $("button .btn-primary").removeClass("disabled");
            });
        }
    </script>
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />

        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <select id="ddlSubLV2" class="form-control">
                    </select>
                </div>
            </div>
        </div>
        <div class="form-group row" id="row-name">
            <div class="col-md-6 col-sm-12 col-name">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-button">
                <asp:Literal ID="ltrMenu" runat="server" />
            </div>
        </div>


        <div class="row">
            <div class="col-xs-12 button-section">
                <div id="btnSearch" class='btn btn-info search-btn'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <table class="cool-table"
                        style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="2" border="0">
                        <thead>
                            <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                <th class="center" scope="col" style="width: 20%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                <th class="center" scope="col" style="width: 12%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                <th class="center" scope="col" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                <th class="center" scope="col" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302011") %></th>
                                <%--<th class="center" scope="col" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132043") %></th>--%>
                                <th class="centertext" scope="col" style=""></th>
                                <th class="centertext" scope="col" style=""></th>
                                <th class="centertext" scope="col" style=""></th>
                            </tr>
                        </thead>
                        <tbody id="target">
                      <%--      <% var q = returnlist(new Search { pageSize = 20, pageNumber = 1 });

                                foreach (var data in q.DATA)
                                {%>
                            <tr class="itemCell" style="font-weight: unset; font-style: normal; text-decoration: none;">
                                <td class="center" scope="col" style="width: 10%;"><%=data.row %></td>
                                <td class="center" scope="col" style="width: 20%;"><%=data.sName %></td>
                                <td class="center" scope="col" style="width: 20%;"><%=data.sLastName %></td>
                                <td class="center" scope="col" style="width: 12%;"><%=data.sIdentification %></td>
                                <td class="center" scope="col" style="width: 12%"><%=data.SubLevel +" / "+ data.nTSubLevel2 %></td>
                                <td class="center" scope="col" style="width: 12%"><%=data.endScore %></td>
                                <td class="centertext" scope="col" style="">
                                    <div class="btn btn-success btnpermission" data-toggle="modal"
                                        data-target="#modalpopupbehavior"
                                        onclick="setpopup('<%=data.id %>','<%=data.sIdentification %>','<%=data.sName + " " + data.sLastName %>','add')">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                                    </div>
                                </td>
                                <td class="centertext" scope="col" style="">
                                    <div class="btn btn-warning btnpermission" data-toggle="modal"
                                        data-target="#modalpopupbehavior"
                                        onclick="setpopup('<%=data.id %>','<%=data.sIdentification %>','<%=data.sName + " " + data.sLastName %>','reduce')">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302004") %>
                                    </div>
                                </td>
                                <td class="centertext" scope="col" style="" target="_blank">
                                    <a href="/behaviors/behavior-details.aspx?id=<%=data.id %>" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %></a>
                                </td>
                            </tr>
                            <%}
                            %>--%>
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
                                                   <%--         <%  int i = 1;
                                                                for (i = 1; i <= q.FOOT.pageSize; i++)
                                                                {
                                                            %>
                                                            <option value="<%= i%>"><%= i%></option>
                                                            <%
                                                                }
                                                            %>--%>
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
                                                <span id="spane_pageNumber" style="color: White;">หน้าที่ 1 จากทั้งหมด</span>
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
                            <td class="center" scope="col" style="width: 10%;">{{row}}</td>
                            <td class="center" scope="col" style="width: 20%;">{{sName}}</td>
                            <td class="center" scope="col" style="width: 20%;">{{sLastName}}</td>
                            <td class="center" scope="col" style="width: 12%;">{{sIdentification}}</td>
                            <td class="center" scope="col" style="width: 12%">{{SubLevel}} / {{nTSubLevel2}}</td>
                            <td class="center" scope="col" style="width: 12%">{{endScore}}</td>
                            <td class="centertext" scope="col" style="">
                                <div class="btn btn-success btnpermission" data-toggle="modal"
                                        data-target="#modalpopupbehavior"
                                        onclick="setpopup('{{id}}','{{sIdentification}}','{{sName}} {{sLastName}}','add')"
                                        ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></div>
                            </td>
                            <td class="centertext" scope="col" style="">
                                <div class="btn btn-warning btnpermission" data-toggle="modal"
                                        data-target="#modalpopupbehavior"
                                        onclick="setpopup('{{id}}','{{sIdentification}}','{{sName}} {{sLastName}}','reduce')"
                                        ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302004") %></div>
                            </td>
                            <td class="centertext" scope="col" style="" target="_blank">
                                    <a href="/behaviors/behavior-details.aspx?id={{id}}" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302012") %></a>
                            </td>
                        </tr>
                    {{/DATA}}
                    </script>
                </div>
            </div>
        </div>
    </div>
    <div id="modalpopupbehavior" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog maclist-modal" style="top: 50px;">
            <div class="modal-content">
                <div class="modal-header">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %>         
                </div>
                <div class="modal-body" id="modalpopupdata-content">
                    <input type="hidden" id="user_id" />
                    <div class="row planadd-row">
                        <div class="col-xs-12">
                            <div class="col-xs-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02229") %></label>
                            </div>
                            <div class="col-xs-8">
                                <input class="form-control" type="text" id="studentID" readonly />
                            </div>

                        </div>
                    </div>
                    <div class="row planadd-row">
                        <div class="col-xs-12">
                            <div class="col-xs-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                            </div>
                            <div class="col-xs-8">
                                <input class="form-control" type="text" id="studentName" readonly />
                            </div>

                        </div>
                    </div>
                    <div class="row planadd-row">
                        <div class="col-xs-12">
                            <div class="col-xs-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01623") %></label>
                            </div>
                            <div class="col-xs-8">
                                <select id="behavior_list" class="form-control"></select>
                            </div>

                        </div>
                    </div>
                    <div class="row planadd-row">
                        <div class="col-xs-12">
                            <div class="col-xs-4">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></label>
                            </div>
                            <div class="col-xs-8">
                                <input class="form-control" type="text" id="Note" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnsave" class="btn btn-primary" onclick="Update_behavior();" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %></button>
                    <label class="btn btn-danger" onclick='$("#modalpopupbehavior").modal("hide");' onclick="" style="font-size: 26px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
