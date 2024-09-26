<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ParentCard.aspx.cs" EnableEventValidation="false" Inherits="FingerprintPayment.StudentCall.ParentCard" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />--%>

    <!-- load the CSS files in the right order -->
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/css/fileinput.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer/theme.min.css" rel="stylesheet" />

    <%--  <link href="../Scripts/selectize/selectize.bootstrap4.css" rel="stylesheet" />--%>
    <link href="../Content/Material/assets/js/plugins/selectize/selectize.bootstrap4.css" rel="stylesheet" />
    <!-- DatetimePicker -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.css" />
    <link href="../Content/Material/assets/css/toggle.css" rel="stylesheet" />
    <link href="Css/css.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        select.form-control {
            appearance: auto !important;
            -webkit-appearance: auto !important;
            -moz-appearance: auto !important;
            background-color: #FFF;
        }

        input.form-control {
            background-color: #FFF;
        }

        .selectize-control.single .selectize-input:after {
            content: none;
            border-style: none;
        }

        .krajee-default.file-preview-frame {
            margin-left: 32%;
        }

        .kv-file-content img {
            max-height: 185px !important;
        }


        #myModal2 label {
            margin: 10px 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106124") %>         
            </p>
        </div>
    </div>


    <form id="from1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdfsid" runat="server" />
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
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" data-style="select-with-transition" CssClass="col-md-12 selectpicker" required>
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlsublevel2" ClientIDMode="Static" AutoPostBack="false" runat="server" data-style="select-with-transition" CssClass="ddl2 selectpicker col-md-12 ">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>" Value="" />
                                </asp:DropDownList>

                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label  text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-md-3 ">
                                <uc1:StudentAutocomplete runat="server" ID="TextAutocomplete" />
                                <%-- <asp:TextBox ID="tags" runat="server" 
                                        class="form-control inputname"
                                        placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105020") %>" 
                                        data-noresults-text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105021") %>"
                                        data-url="/app_logic/studentlist.ashx"
                                        autocomplete="off"
                                        Width="100%" />--%>
                            </div>
                            <div class="col-md-7"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button type="button" id="btnSearch" class="btn btn-info search-btn " onclick="onSearch()">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                <button type="button" class="btn btn-success pull-right" data-toggle="modal" data-target="#myModal2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106127") %></button>
                            </div>

                            <%--  <div class="col-md-4">
                                
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106134") %></h4>
                    </div>
                    <div class="card-body ">

                        <table id="datatable1" class="table-hover dataTable" width="100%" style="width: 100%; border-radius: 6px;">
                            <thead>
                                <tr>
                                    <th width="5%" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                    <th width="8%" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %></th>
                                    <th align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                    <th width="20%" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106135") %> 
                                  <a href="#" class="btn btn-success" onclick="printAll(1);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106138") %></a>
                                    </th>
                                    <th width="20%" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106136") %> 
                                  <a href="#" class="btn btn-success" onclick="printAll(2);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106138") %></a>
                                    </th>
                                    <th width="20%" align="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106137") %> 
                                  <a href="#" class="btn btn-success" onclick="printAll(3);"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106138") %></a>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %></h3>
                    </div>
                    <div class="modal-body" style="">

                        <div class="row" style="padding: 5px;">
                            <div class="col-md-12">
                                <div class="text-center alert-warning">
                                    <asp:Label ID="Label2" ClientIDMode="Static" CssClass="has-error" Text="" runat="server" Style="display: none" />
                                </div>
                            </div>
                        </div>

                        <div class="row" style="padding: 5px;">

                            <div class="col-md-6">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106026") %></label>
                            </div>
                            <div class="col-md-6 text-right">
                                <select id="cardType" data-style="select-with-transition" class="selectpicker" data-width="100%">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></option>
                                </select>
                            </div>

                            <div class="col-md-12">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106128") %> </label>
                                <input id="filebg" name="filebg" type="file" accept="image/*">
                            </div>

                            <div class="col-md-12">
                                <br />
                            </div>

                            <div class="col-md-6">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106129") %></label>
                            </div>
                            <div class="col-md-6 text-right">
                                <select id="nameType" data-style="select-with-transition" class="selectpicker" data-width="100%">
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106130") %></option>
                                    <option value="2">English</option>
                                </select>
                            </div>

                            <div class="col-md-6">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106131") %></label>
                            </div>
                            <div class="col-md-6 text-right">
                                <label class="el-switch ">
                                    <input id="showLevel" class="card-status switch-button" type="checkbox" hidden="" data-toggle="toggle" data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-onstyle="success" data-offstyle="danger"
                                        <%= (Config.IsShowLevel == true ? "checked" : ""  )%>>
                                    <span class="el-switch-style"></span>
                                </label>
                            </div>

                            <div class="col-md-6">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106132") %></label>
                            </div>
                            <div class="col-md-6 text-right">
                                <label class="el-switch ">
                                    <input id="showLastname" class="card-status switch-button" type="checkbox" hidden="" data-toggle="toggle" data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-onstyle="success" data-offstyle="danger"
                                        <%= (Config.IsShowLastName == true ? "checked" : ""  )%>>
                                    <span class="el-switch-style"></span>
                                </label>
                            </div>

                            <div class="col-md-6">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106133") %></label>
                            </div>
                            <div class="col-md-6 text-right">
                                <label class="el-switch ">
                                    <input id="showParent" class="card-status switch-button" type="checkbox" hidden="" data-toggle="toggle" data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-onstyle="success" data-offstyle="danger"
                                        <%= (Config.IsShowParent == true ? "checked" : "" )%>>
                                    <span class="el-switch-style"></span>
                                </label>
                            </div>


                            <div class="hid" style="font-size: 30%">hidden</div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <%-- <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <%-- <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>
    <%--    <script type='text/javascript' src="/assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script type='text/javascript' src="/assets/plugins/datatables/dataTables.bootstrap.min.js"></script>--%>
    <%--  <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>--%>
    <%--    <script src="../Assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>--%>
    <script type='text/javascript' src="/assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>

    <%--    <script src="//cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js" type="text/javascript"></script>--%>

    <!-- load the JS files in the right order -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/piexif.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/plugins/purify.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/fileinput.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer/theme.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>
    <%--  <script src="../Scripts/selectize/selectize.js"></script>--%>
    <script src="../Content/Material/assets/js/plugins/selectize/selectize.js?v=2"></script>
    <%--    <script src="../Scripts/bootstrap-toggle.js"></script>--%>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <script>
        var $table;
        function ddlclass() {
            //$("body").mLoading();
            var ddl2 = document.getElementsByClassName("ddl2");

            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }

            setTimeout(function () {
                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + document.getElementById('<%=ddlsublevel.ClientID%>').value, function (Result) {
                    var opt = document.createElement("option");
                    // Assign text and value to Option object
                    opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>";
                    opt.value = "";
                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");

                        // Assign text and value to Option object
                        opt.text = Result[index].name;
                        opt.value = Result[index].value;
                        //if (getUrlParameter("idlv2") == Result[index].value) {
                        //    opt.selected = "selected";
                        //}
                        // Add an Option object to Drop Down List Box
                        document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    });
                    $('#<%=ddlsublevel2.ClientID%>').selectpicker('refresh');
                });

            }, 300);
            //$("body").mLoading('hide');
        }

        var availableValueUsers = [];

        function lightwellBuyer(request, response) {
            function hasMatch(s) {
                s += '';
                return s.toLowerCase().indexOf((request.term + '').toLowerCase()) !== -1;
            }
            var i, l, obj, matches = [];

            if (request.term === "") {
                response([]);
                return;
            }

            var availableValue = [];
            availableValue = availableValueUsers;
            for (i = 0, l = availableValue.length; i < l; i++) {
                obj = availableValue[i];
                //if (obj.code != null && obj.code != undefined && obj.code != "") {
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
                //}
            }
            response(matches.slice(0, 10));
        }

        function setupTagName() {
            //$("body").mLoading();
            //var availableTags = [];
            //$.get("/app_logic/studentlist.ashx", function (Result) {
            //    $.each(Result, function (index) {
            //        availableTags.push(Result[index].fullName);
            //    });

            //    $("#ctl00_MainContent_tags").autoComplete({
            //        source: availableTags
            //    });

            //});



        }

        function onSearch() {

            if (!$("#aspnetForm").valid()) {
                return;
            }

            if ($.fn.dataTable.isDataTable('#datatable1')) {

                $table.destroy();
              <%-- $table= $('#datatable1').DataTable({
                    paging: false,
                    searching: false,
                    ajax: "<%= ResolveUrl("~/StudentCall/ReportData.ashx") %>?date=" + $("#ctl00_MainContent_txtstart").val() + "&status=" + ($("#ctl00_MainContent_ddlStatus").val() || "0")
                        + "&lvl2=" + ($("#ddlsublevel2").val() || "0") + "&name=" + $("#ctl00_MainContent_tags").val(),
                });--%>
            }
            else {
              <%--  $table = $('#example').DataTable({
                    paging: false,
                    searching: false,
                    ajax: "<%= ResolveUrl("~/StudentCall/ReportData.ashx") %>?date=" + $("#ctl00_MainContent_txtstart").val() + "&status=" + ($("#ctl00_MainContent_ddlStatus").val() || "0")
                        + "&lvl2=" + ($("#ddlsublevel2").val() || "0") + "&name=" + $("#ctl00_MainContent_tags").val(),
                });--%>
            }

            $table = $('#datatable1').DataTable({
                paging: false,
                "pageLength": 20,
                "lengthChange": false,
                searching: false,
                ajax: "<%= ResolveUrl("~/StudentCall/Handler/PCard.ashx") %>?lvl1=" + ($("#ctl00_MainContent_ddlsublevel").val() || "0") + "&lvl2=" + ($("#ddlsublevel2").val() || "0") + "&name=" + SAC.GetStudentName(),
                //dom: 'Bfrtip',
                //buttons: [
                //    'excelHtml5'
                //],

                columns: [
                    { "data": "no" },
                    //  { "data": "level" },
                    { "data": "code" },
                    { "data": "title" },
                    { "data": "fullName" },
                    {
                        "orderable": false, "mRender": function (data, type, row) {
                            var index = 1;
                            return "<table class='subtbl-wrapper table' width='100%'  data-sid='" + row.sid + "' data-index='" + index + "'>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>\
                                        <td>\
                                            <label class='el-switch el-switch-lg'><input data-sid='" + row.sid + "' data-no='1' class='card-status switch-button' type='checkbox' hidden data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger'\
                                                "+ (row.status1 == "1" ? " checked='true' " : "") + " /><span class='el-switch-style'></span></label>\
                                        </td>\
                                    </tr >\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106003") %></td>\
                                        <td>\
                                            <div class='number-wrapper'><input class='card-nfc form-control' type='number'  value='"+ row.card1 + "' onblurx='onKeyNFC(this)' /><i class='glyphicon glyphicon-ok' style='display:none'></i>\
                                            <label class='error -nfc'  style='display:none'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %></label></div>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106139") %></td>\
                                        <td>\
                                             <label class='form-control encard-nfc'>"+ row.encard1 + "</label>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td>Barcode</td>\
                                        <td><div class='number-wrapper'><input type='text' class='form-control card-barcode'  value='" + (row.barcode1 + '').trim() + "'/>\
                                            <i class='glyphicon glyphicon-ok' style='display:none'></i>\
                                            <label class='error -barcode'  style='display:none'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %></label></div>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></td>\
                                        <td>\
                                            <select class=' card-type form-control'  >\
                                                <option value='1' "+ (row.type1 == "1" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></option>\
                                                <option value='2' "+ (row.type1 == "2" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></option>\
                                                <option value='3' "+ (row.type1 == "3" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></option>\
                                            </select>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %></td>\
                                        <td><input type='text' class='form-control card-parent' data-parents='"+ (row.parentall || '') + "' data-val='" + (row.parent1 + '') + "'/></td>\
                                    </tr>\
                                     <tr>\
                                       <td colspan=2><div class='row'><div class='col-md-6'><button type='button' class='btn btn-success save-nfc' onclick='saveCard(this,1);' disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button></div><div class='col-md-6'><button type='button' class='btn btn-success' onclick='printCard(1," + row.sid + ");'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133288") %></button></div></div></td>\
                                    </tr>\
                             </table>";
                        }
                    },
                    {
                        "orderable": false, "mRender": function (data, type, row) {
                            var index = 2;
                            return "<table class='subtbl-wrapper table' width='100%'  data-sid='" + row.sid + "' data-index='" + index + "'>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>\
                                        <td>\
                                            <label class='el-switch el-switch-lg'><input data-sid='" + row.sid + "' data-no='2' class='card-status switch-button' type='checkbox' hidden data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger'\
                                                "+ (row.status2 == "1" ? " checked='true' " : "") + " /><span class='el-switch-style'></span></label>\
                                        </td>\
                                    </tr >\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106003") %></td>\
                                        <td>\
                                            <div class='number-wrapper'><input class='card-nfc form-control' type='number'  value='"+ row.card2 + "' onblurx='onKeyNFC(this)' /><i class='glyphicon glyphicon-ok' style='display:none'></i>\
                                            <label class='error -nfc' style='display:none'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %></label></div>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106139") %></td>\
                                        <td>\
                                            <label class='form-control encard-nfc'>"+ row.encard2 + "</label>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td>Barcode</td>\
                                        <td><div class='number-wrapper'><input type='text' class='form-control card-barcode'  value='" + (row.barcode2 + '').trim() + "'/>\
                                            <i class='glyphicon glyphicon-ok' style='display:none'></i>\
                                            <label class='error -barcode'  style='display:none'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %></label></div>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></td>\
                                        <td>\
                                            <select class=' card-type form-control'  >\
                                                <option value='1' "+ (row.type2 == "1" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></option>\
                                                <option value='2' "+ (row.type2 == "2" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></option>\
                                                <option value='3' "+ (row.type2 == "3" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></option>\
                                            </select>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %></td>\
                                        <td><input type='text' class='form-control card-parent'  data-parents='"+ (row.parentall || '') + "' data-val='" + (row.parent2 + '') + "'/></td>\
                                    </tr>\
                                    <tr>\
                                         <td colspan=2><div class='row'><div class='col-md-6'><button type='button' class='btn btn-success save-nfc' onclick='saveCard(this,2);' disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button></div><div class='col-md-6'><button type='button' class='btn btn-success' onclick='printCard(2," + row.sid + ");'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133288") %></button></div></div></td>\
                                    </tr>\
                             </table>";
                        }
                    },
                    {
                        "orderable": false, "mRender": function (data, type, row) {
                            var index = 3;
                            return "<table class='subtbl-wrapper table' width='100%'  data-sid='" + row.sid + "' data-index='" + index + "'>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>\
                                        <td>\
                                             <label class='el-switch el-switch-lg'><input data-sid='" + row.sid + "' data-no='3' class='card-status switch-button' type='checkbox' hidden data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger'\
                                                 "+ (row.status3 == "1" ? " checked='true' " : "") + " /><span class='el-switch-style'></span></label>\
                                        </td>\
                                    </tr >\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106003") %></td>\
                                        <td>\
                                            <div class='number-wrapper'><input class='card-nfc form-control' type='number'  value='"+ row.card3 + "' onblurx='onKeyNFC(this)' /><i class='glyphicon glyphicon-ok' style='display:none'></i>\
                                            <label class='error -nfc'  style='display:none'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %></label></div>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106139") %></td>\
                                        <td>\
                                            <label class='form-control encard-nfc'>"+ row.encard3 + "</label>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td>Barcode</td>\
                                        <td><div class='number-wrapper'><input type='text' class='form-control card-barcode'  value='" + (row.barcode3 + '').trim() + "'/>\
                                            <i class='glyphicon glyphicon-ok' style='display:none'></i>\
                                            <label class='error -barcode'  style='display:none'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %></label></div>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></td>\
                                        <td>\
                                            <select class=' card-type form-control' data-style='select-with-transition' >\
                                                <option value='1' "+ (row.type3 == "1" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></option>\
                                                <option value='2' "+ (row.type3 == "2" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></option>\
                                                <option value='3' "+ (row.type3 == "3" ? "selected" : "") + "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></option>\
                                            </select>\
                                        </td>\
                                    </tr>\
                                    <tr>\
                                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %></td>\
                                        <td><input type='text' class='form-control card-parent'  data-parents='"+ (row.parentall || '') + "' data-val='" + (row.parent3 + '') + "'/></td>\
                                    </tr>\
                                    <tr>\
                                        <td colspan=2><div class='row'><div class='col-md-6'><button type='button' class='btn btn-success save-nfc' onclick='saveCard(this,3);' disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button></div><div class='col-md-6'><button type='button' class='btn btn-success' onclick='printCard(3," + row.sid + ");'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133288") %></button></div></div></td>\
                                    </tr>\
                             </table>";

                        }
                    },

                    //{
                    //    "data": "isAnnouce", "mRender": function (data, type, row) {
                    //        switch (data + "") {
                    //            case "0":
                    //                return "";

                    //            case "1":
                    //                switch (row.status) {
                    //                    case "1":
                    //                    case "2":
                    //                        return "<a href='#' onclick='sendAnnouce(" + row.sid + ")' class='enable-annouce roundbtn' ><i class='icofont-bullhorn'></i></a>";

                    //                    case "3":
                    //                        return "<a href='#' class='disable-annouce roundbtn'><i class='icofont-bullhorn'></i></a>";
                    //                }
                    //                return "";
                    //            default:
                    //        }

                    //        //return "<a href='Admin/Categories/Edit/" + data + "'>EDIT</a>";
                    //    }
                    //},
                ],
                "initComplete": function (settings, json) {
                    //$('.card-status').bootstrapToggle();

                    //$('.card-type').selectpicker();

                    $('.card-parent').each(function () {
                        var p = ($(this).data('parents') + '').split(',');
                        var val = $(this).data('val');
                        var _tags = [];

                        //if ((p[0] + '').trim()) {
                        if (p[0] !== undefined && (p[0] + '').trim() != "") {
                            _tags.push({
                                text: p[0].trim() + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %>)",
                                id: p[0].trim()
                            });
                        }
                        //if ((p[1] + '').trim()) {
                        if (p[1] !== undefined && (p[1] + '').trim() != "") {
                            _tags.push({
                                text: p[1].trim() + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %>)",
                                id: p[1].trim()
                            });
                        }
                        //if ((p[2] + '').trim()) {
                        if (p[2] !== undefined && (p[2] + '').trim() != "") {
                            _tags.push({
                                text: p[2].trim() + " (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %>)",
                                id: p[2].trim()
                            });
                        }

                        //var items = _tags.map(function (x) { return { item: x }; });
                        

                        var $selectize = $(this).selectize({
                            maxItems: 1,
                            create: true,
                            options: _tags,
                            labelField: "text",
                            valueField: "id",
                            sortField: 'text',
                            searchField: 'text',
                            create: function (input) {
                                return {
                                    id: input,
                                    text: input
                                }
                            },
                            onChange: function (value, isOnInitialize) {
                                if (value) {                             
                                    var $tbl = this.$input.parents('.subtbl-wrapper');
                                    $tbl.find('button.save-nfc').prop('disabled', false);
                                }                                
                            }
                        });

                        $selectize[0].selectize.setValue(val.trim());

                        //$(this).autocomplete({
                        //    source: _tags,
                        //    minLength: 0,
                        //})
                        //.focus(function () {
                        //    $(this).autocomplete("search", $(this).val());
                        //});
                    });
                },
            });

            //$('#datatable1').DataTable({
                //"processing": true,
                //"serverSide": true,    
              <%--  "ajax": {
                    "type": "POST",
                    "dataType": 'json',
                    "url": "<%= ResolveUrl("~/StudentCall/Report.aspx/GetData") %>",
                    "contentType": "application/json; charset=utf-8",
                    "data:": {
                        date: $("#ctl00_MainContent_txtstart").val(),
                        status: ($("#ctl00_MainContent_ddlStatus").val() || "0"),
                        lvl2: ($("#ddlsublevel2").val() || "0"),
                        name: $("#ctl00_MainContent_tags").val(),
                    },
                    "success": function (response) {
                        var source = $.parseJSON(response.d);
                        return source;
                    },
                    "error": function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    }
                },--%>
              <%-- ajax: "<%= ResolveUrl("~/StudentCall/ReportData.ashx") %>?date=" + $("#ctl00_MainContent_txtstart").val() + "&status=" + ($("#ctl00_MainContent_ddlStatus").val() || "0")
                    + "&lvl2=" + ($("#ddlsublevel2").val() || "0") + "&name=" + $("#ctl00_MainContent_tags").val(),--%>
            // });
        }

        function delay(callback, ms) {
            var timer = 0;
            return function () {
                var context = this, args = arguments;
                clearTimeout(timer);
                timer = setTimeout(function () {
                    callback.apply(context, args);
                }, ms || 0);
            };
        }

        function onKeyBarcode($this) {
            var $tbl = $this.parents('.subtbl-wrapper');
            var barcode = $this.val();
            var sid = $tbl.data('sid');
            var no = $tbl.data('index');
            $("body").mLoading();

            PageMethods.IsBarcodeValid(barcode, sid, no,
                function (res) {

                    switch (res.status) {
                        case 0:
                            $this.parent().find('i').hide();
                            $this.parent().find('label.error').hide();
                            $tbl.find('button.save-nfc').prop('disabled', false);
                            break;

                        case 1:
                            $this.parent().find('i').hide();
                            $this.parent().find('label.error').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %>').show();
                            $tbl.find('button.save-nfc').prop('disabled', true);
                            break;

                        case 2:
                            $this.parent().find('i').hide();
                            $this.parent().find('label.error').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133289") %> ' + res.fullName).show();
                            $tbl.find('button.save-nfc').prop('disabled', true);
                            break;

                        case 3:
                            $this.parent().find('i').show();
                            $this.parent().find('label.error').hide();
                            $tbl.find('button.save-nfc').prop('disabled', false);
                            break;
                    }
                    //if (res) {
                    //    $this.parent().find('i').show();
                    //    $this.parent().find('label.error').hide();
                    //    $tbl.find('button.save-nfc').prop('disabled', false);
                    //}
                    //else {
                    //    $this.parent().find('i').hide();
                    //    $this.parent().find('label.error').show();
                    //    $tbl.find('button.save-nfc').prop('disabled', true);
                    //}
                    $("body").mLoading('hide');
                },
                function (res) {
                    $("body").mLoading('hide');
                });


            //delay(function (e, $this) {

            //    var $tbl = $this.parents('.subtbl-wrapper');
            //    var nfc = $this.val();
            //    var sid = $tbl.data('sid');
            //    PageMethods.IsDuplicate(nfc, sid,
            //        function (res) {
            //            if (res) {
            //                $this.parent().find('label.error').show();
            //                $tbl.find('button').prop('disabled', true);
            //            }
            //            else {
            //                $this.parent().find('label.error').hide();
            //                $tbl.find('button').prop('disabled', fasle);
            //            }
            //        },
            //        function (res) {

            //        });                       

            //}, 800)
        }

        function onKeyNFC($this) {
            var $tbl = $this.parents('.subtbl-wrapper');
            var nfc = $this.val();
            var sid = $tbl.data('sid');
            var no = $tbl.data('index');
            $("body").mLoading();

            PageMethods.IsValid(nfc, sid, no,
                function (res) {

                    switch (res.status) {
                        case 0:
                            $this.parent().parent().find('i').hide();
                            $this.parent().parent().find('label.error').hide();
                            $tbl.find('button.save-nfc').prop('disabled', false);
                            break;

                        case 1:
                            $this.parent().parent().find('i').hide();
                            $this.parent().parent().find('label.error').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133287") %>').show();
                            $tbl.find('button.save-nfc').prop('disabled', true);
                            break;

                        case 2:
                            $this.parent().parent().find('i').hide();
                            $this.parent().parent().find('label.error').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133289") %> ' + res.fullName).show();
                            $tbl.find('button.save-nfc').prop('disabled', true);
                            break;

                        case 3:
                            $this.parent().parent().find('i').show();
                            $this.parent().parent().find('label.error').hide();
                            $tbl.find('button.save-nfc').prop('disabled', false);
                            break;
                    }

                    $("body").mLoading('hide');
                    //if (res) {
                    //    $this.parent().find('i').show();
                    //    $this.parent().find('label.error').hide();
                    //    $tbl.find('button.save-nfc').prop('disabled', false);
                    //}
                    //else {
                    //    $this.parent().find('i').hide();
                    //    $this.parent().find('label.error').show();
                    //    $tbl.find('button.save-nfc').prop('disabled', true);
                    //}
                },
                function (res) {
                    $("body").mLoading('hide');
                });


            //delay(function (e, $this) {

            //    var $tbl = $this.parents('.subtbl-wrapper');
            //    var nfc = $this.val();
            //    var sid = $tbl.data('sid');
            //    PageMethods.IsDuplicate(nfc, sid,
            //        function (res) {
            //            if (res) {
            //                $this.parent().find('label.error').show();
            //                $tbl.find('button').prop('disabled', true);
            //            }
            //            else {
            //                $this.parent().find('label.error').hide();
            //                $tbl.find('button').prop('disabled', fasle);
            //            }
            //        },
            //        function (res) {

            //        });                       

            //}, 800)
        }

        function saveCard(t, no) {
            var $tbl = $(t).parents('.subtbl-wrapper');
            var sid = $tbl.data('sid');
            var nfc = $tbl.find('.card-nfc').val();
            var status = $tbl.find('.card-status').prop('checked');
            var type = $tbl.find('select.card-type').val();
            var parent = $tbl.find('.card-parent').val();
            var barcode = $tbl.find('.card-barcode').val();

            $("body").mLoading();
            PageMethods.SaveCard(sid, no, status, nfc, type, parent, barcode,
                function (response) {
                    if (response.status) {

                        switch (response.typeNFC) {
                            case 0:
                                $tbl.find('label.error.-nfc').hide();
                                $tbl.find('.encard-nfc').text(response.encrypt);//dup not save nfc
                                break;
                            case 1:
                                $tbl.find('.card-nfc').val('');//dup not save nfc
                                $tbl.find('label.error.-nfc').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133289") %>').show();
                                break;

                            default: break;
                        }

                        switch (response.typeBarcode) {
                            case 0:
                                $tbl.find('label.error.-barcode').hide();
                                $tbl.find('.encard-nfc').text(response.encrypt);//dup not save nfc
                                break;
                            case 1:
                                $tbl.find('.card-nfc').val('');//dup not save nfc
                                $tbl.find('label.error.-barcode').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133289") %>').show();
                                break;

                            default: break;
                        }

                        Swal.fire({
                            type: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %>',
                        })

                    } else {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                        })
                    }
                    $("body").mLoading('hide');
                },
                function (response) {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                    });
                    $("body").mLoading('hide');
                });
        }

        function printCard(no, sid) {
            window.open("PrintCard.aspx?no=" + no + "&sid=" + sid, "_blank"); //$("#ddlsublevel2").val()
            return false;
        }

        function printAll(no) {
            // var param = new URLSearchParams(window.location.search);

            window.open("PrintCard.aspx?type=all&no=" + no + "&lvl1=" + ($("#ctl00_MainContent_ddlsublevel").val()) + "&lvl2=" + ($("#ddlsublevel2").val()), "_blank"); //$("#ddlsublevel2").val()
            return false;
        }

        function sendAnnouce(sid) {
            PageMethods.ResendAnnouncement(sid,
                function (response) {
                    console.log("sent");
                },
                function (response) {
                    console.log("sent fail");
                });
        }

        var timeout = null

        $(function () {

            //setupTagName();

            //$('#ctl00_MainContent_tags').autoComplete({
            //    resolverSettings: {
            //        fail: function () {
            //            console.log('fail');
            //        }
            //    },
            //    formatResult: function (item) {
            //        return {
            //            value: item.sID,
            //            text: item.fullName
            //        };
            //    }
            //});
            $('#cardType').val('<%= Config.CardType %>');
            $('#cardType').selectpicker('refresh');

            $('#nameType').val('<%= Config.NameType %>');
            $('#nameType').selectpicker('refresh');

            $("#showLevel,#showLastname,#showParent,#cardType,#nameType").on("change", function () {
                $("body").mLoading('show');

                var type = '';
                var val;

                switch ($(this).attr('id')) {

                    case 'showLevel': type = 'level'; val = $(this).is(":checked");
                        break;

                    case 'showLastname': type = 'lastname'; val = $(this).is(":checked");
                        break;

                    case 'showParent': type = 'parent'; val = $(this).is(":checked");
                        break;

                    case 'cardType': type = 'cardType'; val = +$(this).val();
                        break;

                    case 'nameType': type = 'nameType'; val = +$(this).val();
                        break;

                    default:
                        break;
                }

                PageMethods.SaveData(type, val,
                    function (response) {
                        $("body").mLoading('hide');
                        if (response.status == "ok") {
                            //Swal.fire({
                            //    type: 'success',
                            //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>',
                            //    //text: 'Something went wrong!',                      
                            //});
                        }
                        else {
                            Swal.fire({
                                type: 'error',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133290") %>',
                                //text: 'Something went wrong!',                       
                            });
                        }
                    },
                    function (response) {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133290") %>',
                            //text: 'Something went wrong!',                       
                        });
                        $("body").mLoading('hide');
                    }
                );
            });


            $('#divDStart').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
            });

            $(".bs-datepicker").keydown(function (e) {
                e.preventDefault();
            });

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        var _class = element.attr('class');

                        if (_class.includes('selectpicker')) {
                            //error.insertAfter(element.parent());
                            element.parent().append(error);
                        }
                        else {
                            error.insertAfter(element);
                        }
                    }

                });
            }

            $("#datatable1").on("change", ".card-status", function () {
                var $this = $(this);
                var sid = $this.data('sid');
                var no = $this.data('no');

                $("body").mLoading();
                PageMethods.ToggleCard(sid, no,
                    function (response) {
                        Swal.fire({
                            type: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>',
                            //text: 'Something went wrong!',                      
                        });
                        $("body").mLoading('hide');
                    },
                    function (response) {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133290") %>',
                            //text: 'Something went wrong!',                       
                        });
                        $("body").mLoading('hide');
                    });

            });

            $("#datatable1").on("keyup", ".card-nfc", function () {
                var $this = $(this);

                //delay(onKeyNFC($this), 1000);
                //var text = this.value
                clearTimeout(timeout)
                timeout = setTimeout(function () {
                    onKeyNFC($this);
                }, 1000)
            });

            $("#datatable1").on("keyup", ".card-barcode", function () {
                var $this = $(this);

                //delay(onKeyNFC($this), 1000);
                //var text = this.value
                clearTimeout(timeout)
                timeout = setTimeout(function () {
                    onKeyBarcode($this);
                }, 1000)
            });

            var $el1 = $("#filebg");
            $el1.fileinput({
                allowedFileExtensions: ['jpg', 'png', 'gif'],
                uploadUrl: "<%= ResolveUrl("~/StudentCall/UploadFileBG.ashx") %>",
                uploadAsync: true,
                deleteUrl: "<%= ResolveUrl("~/StudentCall/RemoveFileBG.ashx") %>",
                showUpload: false, // hide upload button                                 
                maxFileCount: 1,
                browseOnZoneClick: true,
                initialPreviewAsData: true,
                autoReplace: true,
                overwriteInitial: true,
                showRemove: false,
                showUpload: false, // <------ just set this from true to false
                initialPreviewFileType: 'image',
                overwriteInitial: true,
                //maxImageWidth: 204,
                //maxImageHeight: 325,
                //resizePreference: 'height',
                //resizeImage: true,
                <% if (!string.IsNullOrEmpty(Config.BgCard))
        {%>

                initialPreview: ["<%=Config.BgCard %>"],
                initialPreviewConfig: [{
                    key: "<%= UserData.CompanyID %>",
                }]
                <%} %>
            }).on("filebatchselected", function (event, files) {
                $el1.fileinput("upload");
            }).on('filebeforeload', function (event, file, index, reader) {
                $el1.fileinput('clear')
                //$('.file-preview .file-preview-thumbnails').html();
            });;

        });
    </script>
</asp:Content>
<%--<table width="100%">
    <tr>
        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></td>
        <td>
            <input class='card-status' type='checkbox' data-toggle='toggle' data-on='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132589") %>' data-off='<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132588") %>' data-onstyle='success' data-offstyle='danger'
                data-sid='1' data-index='1' checked='true' />
        </td>
    </tr>
    <tr>
        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106003") %></td>
        <td>
            <input class='card-nfc form-control' type='text' data-sid='1' data-index='1' />
        </td>
    </tr>
    <tr>
        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></td>
        <td>
            <select class="form-control card-type">
                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></option>
                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></option>
                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></option>
            </select>
        </td>
    </tr>
    <tr>
        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106143") %></td>
        <td><input type='text' class='form-control card-parent'  data-sid='1'  data-index='1' /></td>
    </tr>
</table>--%>
