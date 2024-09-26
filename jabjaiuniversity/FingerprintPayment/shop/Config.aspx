<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Config.aspx.cs" EnableEventValidation="false" Inherits="FingerprintPayment.Shop.Config" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }

            label.error {
                color: red;
                font-size: 24px;
            }
    </style>


    <link href="../Content/Material/assets/css/toggle.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script>      

        function onSave() {

            var data = {
                "maxTopup": +($('#textMaxTopup').val()),
            };

            //if (data.maxTopup > 15000) {
            //    Swal.fire({
            //        type: 'warning',
            //        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133262") %>',
            //    });
            //    return;
            //}
            PageMethods.SaveData(data,
                function (response) {

                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %>',
                    });

                },
                function (response) {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                    });

                }
            );
        }

        $(function () {

            PageMethods.GetData(
                function (response) {
                    $('#textMaxTopup').val(response.maxTopup);
                },
                function (response) {

                }
            );

            //$('input.switch-button').on('click', function (e) {
            //    var $this = $(this);

            //    if ($this.is(':checked')) {
            //        $('#textMaxTopup').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>");
            //    }
            //    else {
            //        $('#checkText').text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>");
            //    }
            //});
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602034") %>
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
                            <i class="material-icons">settings</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602035") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <table id="datatable1" class="table-hover dataTable" width="100%" style="width: 100%; border-radius: 6px;">
                                    <thead>
                                        <tr>
                                            <th width="10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th width="75%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302015") %></th>
                                            <th width="15%" class="text-center"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td align="center">1.</td>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602036") %></td>
                                             <td align="center">
                                                <div class="form-inline justify-content-center">
                                                    <div class="form-group">
                                                        <input type="number" id="textMaxTopup" class="form-control" style="width: 90px;text-align:center"  min="0" />
                                                        &nbsp;<span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                     <%--<tfoot>
                                        <tr>
                                            <td colspan="3" class="text-right">
                                                <span>
                                                    <strong>
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133263") %>
                                                    </strong>
                                                </span>
                                            </td>
                                        </tr>
                                    </tfoot>--%>
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
