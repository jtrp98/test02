<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Config.aspx.cs" EnableEventValidation="false" Inherits="FingerprintPayment.StudentCall.Config" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="icofont/icofont.min.css" rel="stylesheet" />
    <style>
        /* label {
            font-weight: normal;
            font-size: 26px;
        }

            label.error {
                color: red;
                font-size: 24px;
            }*/

        .hummingbird-treeview {
            border: 1px solid #ccc;
            padding: 4px 15px;
        }

        #templateTR {
            display: none;
        }

        table.dataTable tbody tr:last-child td {
            border-bottom: 0px solid #000 !important;
        }

        table.dataTable tfoot tr:last-child td {
            border-bottom: 1px solid #000 !important;
        }

        .copyLink {
            cursor: pointer;
            margin-right: 5px;
            margin-top: 5px;
        }

        .jq-toast-single {
            font-family: THSarabunNew !important;
        }
    </style>

    <link href="../Content/Material/assets/css/toggle.css" rel="stylesheet" />
    <link href="/Content/hummingbird-treeview/hummingbird-treeview.min.css" rel="stylesheet" type="text/css" />
    <link href="//cdn.jsdelivr.net/npm/jquery-toast-plugin@1.3.2/dist/jquery.toast.min.css" rel="stylesheet">
    <%-- <link href="//cdn.jsdelivr.net/npm/jquery-bonsai@2.1.3/jquery.bonsai.min.css" rel="stylesheet">--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--<script src="//cdn.jsdelivr.net/npm/jquery-bonsai@2.1.3/jquery.bonsai.min.js"></script>--%>
    <script src="/Content/hummingbird-treeview/hummingbird-treeview.min.js"></script>
    <script src="//cdn.jsdelivr.net/npm/jquery-toast-plugin@1.3.2/dist/jquery.toast.min.js"></script>

    <script>      
        var gLevel;
        var delList = [];

        function onSave() {

            var $row = $('.rowGate');

            var gates = [];
            $row.each(function (index) {
                var $item = $(this);
                var selected = { "id": [], "dataid": [], "text": [] };
                //get all checked nodes
                $item.find('.hummingbird-base').hummingbird("getChecked", { list: selected, onlyEndNodes: true });

                var obj = {
                    id: $item.find('.token').val(),
                    gate: index + 1,
                    gateName: $item.find('.gateName').val(),
                    url: $item.find('.gateNameFullUrl').val(),
                    selected: JSON.stringify(selected.dataid.map(str => Number(str))),
                    shortUrl: $item.find('.gateNameUrl').val(),
                };
                gates.push(obj);

            });

            var data = {
                "status": $('#checkBtn').prop('checked'),
                "radius": $('#textRadius').val(),
                "gates": gates,
                'delList': delList,
            };
            console.log(data);

            $.ajax({
                async: true,
                type: "POST",
                url: "<%=Page.ResolveUrl("~/StudentCall/Config.aspx/SaveData")%>",
                data: JSON.stringify({ 'data': data }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (response) {
                      delList = [];
                      Swal.fire({
                          type: 'success',
                          title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %>',
                      }).then((result) => {
                          location.reload();
                      });
                    
                  },
                  failure: function (response) {
                      Swal.fire({
                          type: 'error',
                          title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                      });
                  },
                  error: function (response) {
                      Swal.fire({
                          type: 'error',
                          title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                      });
                  }
            });                      
        }

        function onNewGate() {
            var count = $('tr.rowGate').length;

            PageMethods.GetNewGate(count,
                function (response) {
                    CloneGate(response);
                },
                function (response) {

                }
            );
        }

        function onRemoveGate(t) {
            var $parent = $(t).parents('.rowGate');
            var id = $parent.find('.token ').val();
            delList.push(id);

            $parent.remove();
        }
        function CloneGate(item) {
            var $cloneDOM = $("#templateTR").clone();

            $cloneDOM.find('.token').val(item.id);
            $cloneDOM.find('.gateNo').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402026") %> ' + item.gate);
            $cloneDOM.find('.gateName').val(item.gateName);
            $cloneDOM.find('.gateLinkName').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402030") %> ' + item.gate);
            $cloneDOM.find('.gateNameUrl').val(item.shortUrl);
            $cloneDOM.find('.gateNameFullUrl').val(item.url);
            $cloneDOM.attr('id', '');
            $cloneDOM.find('#treeview').attr('id', 'treeview' + item.gate);
            $cloneDOM.attr('class', 'rowGate');
            $cloneDOM.appendTo("#datatable1 tbody");

            $("#treeview" + item.gate).hummingbird();
            $("#treeview" + item.gate).hummingbird("checkNode", { sel: "data-id", vals: JSON.parse(item.selected) });
        }

        $(function () {
            //$.fn.hummingbird.defaults.collapsedSymbol = "fa-arrow-circle-o-right";
            //$.fn.hummingbird.defaults.expandedSymbol = "fa-arrow-circle-o-down";

            /*            $.fn.hummingbird.defaults.collapseAll = true;*/
            //$.fn.hummingbird.defaults.checkboxes = "enabled";
            /* $.fn.hummingbird.defaults.checkboxesGroups = "enabled";*/
            // Collapsed Symbol
            // Collapsed Symbol
            $.fn.hummingbird.defaults.collapsedSymbol = "fa-caret-down";
            // Expand Symbol
            $.fn.hummingbird.defaults.expandedSymbol = "fa-caret-up";

          <%--  <% foreach (var g in Gates) { %>
                $("#treeview<%= g.Gate %>").hummingbird();
                $("#treeview<%= g.Gate %>").hummingbird("checkNode", { sel: "data-id", vals: ["43489"] });
            <%}%> --%>

            PageMethods.GetData(
                function (response) {
                    $('#checkBtn').prop('checked', response.status);
                    $('#checkText').text((response.status ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>"))
                    $('#textRadius').val(response.radius || "0");

                    response.gates.forEach(function (item, index) {
                        CloneGate(item);
                    });


                },
                function (response) {

                }
            );

            $('input.switch-button').on('click', function (e) {
                var $this = $(this);

                if ($this.is(':checked')) {
                    $('#checkText').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>");
                }
                else {
                    $('#checkText').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>");
                }
            });

            $('#datatable1').on('keydown', '.gateNameUrl', function (e) {
                e.preventDefault();
            });

            $('#datatable1').on('click', '.copyLink', function (e) {
                var $p = $(this).parent();
                var $i = $p.find('.gateNameUrl');
                $i.select();
                $i[0].setSelectionRange(0, 99999);
                document.execCommand("copy");

                $.toast({
                    text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133282") %>",
                    showHideTransition: 'slide',  // It can be plain, fade or slide
                    bgColor: '#eee',              // Background color for toast
                    textColor: '#000',            // text color
                    allowToastClose: true,       // Show the close button or not
                    hideAfter: 3000,              // `false` to make it sticky or time in miliseconds to hide after
                    stack: 5,                     // `fakse` to show one stack at a time count showing the number of toasts that can be shown at once
                    textAlign: 'left',            // Alignment of text i.e. left, right, center
                    position: 'bottom-right'       // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values to position the toast on page
                })
            });
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402020") %>
            </p>
        </div>
    </div>

    <form id="from1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release">
        </asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">directions_bus</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402019") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <table id="datatable1" class="table-hoverx dataTable" width="100%" style="width: 100%; border-radius: 6px;">
                                    <thead>
                                        <tr>
                                            <th width="10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th width="75%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                            <th width="15%" class="text-center"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td align="center">1.</td>
                                            <td>
                                                <label style="color: #333333;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402021") %></label></td>
                                            <td align="center">
                                                <div class="form-inline justify-content-center">
                                                    <div class=" form-group ">
                                                        <label class="el-switch el-switch">
                                                            <input type="checkbox" id="checkBtn" class="switch-button" hidden />
                                                            <span class="el-switch-style"></span>
                                                        </label>
                                                        &nbsp;
                                                        <span id="checkText"></span>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">2.</td>
                                            <td>
                                                <label style="color: #333333;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402023") %></label></td>
                                            <td align="center">
                                                <div class="form-inline justify-content-center">
                                                    <div class="form-group">
                                                        <input type="text" id="textRadius" class="form-control" style="width: 70px; text-align: center" />
                                                        &nbsp;<span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402024") %></span>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center"></td>
                                            <td>
                                                <div class="d-none">
                                                </div>

                                            </td>
                                            <td align="center"></td>
                                        </tr>
                                        <tr>
                                            <td align="center">3.</td>
                                            <td>
                                                <label style="color: #333333;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402025") %></label></td>
                                            <td align="center"></td>
                                        </tr>

                                        <tr id="templateTR">
                                            <td>
                                                <input type="text" class="form-control token d-none" value="" /></td>
                                            <td colspan="2">
                                                <div class="row">
                                                    <label class="gateNo col-sm-6 col-form-label text-left" style="color: #333333;">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402026") %>
                                                    </label>
                                                    <div class=" col-sm-6 col-form-label text-right" style="color: #333333;">
                                                        <button type="button" class="btn btn-danger search-btn " onclick="onRemoveGate(this)">
                                                            <span class="material-icons">delete</span>
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402028") %>
                                                        </button>
                                                    </div>
                                                    <label class="col-sm-6 col-form-label text-left" style="color: #333333;">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402027") %>
                                                    </label>

                                                    <div class="col-sm-6 text-right  ">
                                                        <div class="form-group">
                                                            <input type="text" class="form-control gateName" value="" />
                                                        </div>
                                                    </div>

                                                    <label class=" col-sm-12 col-form-label text-left" style="color: #333333;">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402029") %>
                                                    </label>

                                                    <div class="col-12 text-left  p-0">
                                                        <div id="treeview_container" class="hummingbird-treeview">
                                                            <ul id="treeview" class="hummingbird-base">
                                                                <% foreach (var t0 in TreeView)
                                                                    {%>
                                                                <li>
                                                                    <label>
                                                                        <input class="level0" id="l0-<%=t0.id %>" data-id="<%=t0.id %>" data-name="<%=t0.text %>" type="checkbox" />
                                                                        <%=t0.text %>
                                                                    </label>
                                                                    <i class="fa fa-plus"></i>
                                                                    <ul>
                                                                        <% foreach (var t1 in t0.children)
                                                                            {%>
                                                                        <li>
                                                                            <label>
                                                                                <input class="level1" id="l1-<%=t1.id %>" data-id="<%=t1.id %>" data-name="<%=t1.text %>" type="checkbox" />
                                                                                <%=t1.text %>
                                                                            </label>
                                                                            <i class="fa fa-plus"></i>
                                                                            <ul>
                                                                                <% foreach (var t2 in t1.children)
                                                                                    {%>
                                                                                <li>
                                                                                    <label>
                                                                                        <input class="hummingbird-end-node level2" id="l2-<%=t2.id %>" data-id="<%=t2.id %>" data-name="<%=t2.text %>" type="checkbox" />
                                                                                        <%=t2.text %>
                                                                                    </label>
                                                                                </li>
                                                                                <% } %>
                                                                            </ul>
                                                                        </li>
                                                                        <% } %>
                                                                    </ul>
                                                                    <% } %>
                                                                </li>
                                                            </ul>
                                                        </div>

                                                    </div>

                                                    <label class="gateLinkName col-sm-6 col-form-label text-left" style="color: #333333;">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402030") %>
                                                    </label>

                                                    <div class="col-sm-6 text-right  ">
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <span class="input-group-addon copyLink" alt="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133283") %>"><i class="material-icons">content_copy</i></span>
                                                                <input type="text" class="form-control gateNameUrl" value="" />
                                                            </div>
                                                            <input type="text" class="form-control gateNameFullUrl d-none" value="" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <hr />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td align="center"></td>
                                            <td colspan="2">
                                                <button type="button" class="btn btn-success search-btn " onclick="onNewGate()">
                                                    <span class="btn-label">
                                                        <i class="material-icons">add</i>
                                                    </span>
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402031") %>
                                                </button>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>

                            <div class="col-md-12 text-center">
                                <button type="button" id="btnSearch" class="btn btn-success search-btn " onclick="onSave()">
                                    <span class="btn-label">
                                        <i class="material-icons">save</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>



</asp:Content>
