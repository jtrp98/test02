<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_EditBookName.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.EditBookName.Index_EditBookName" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">

        function Modelsetting() {
            $("#exampleModal").modal()
        }

        function insertTEXT() {
            var x = document.getElementById('bkName').value;
            document.getElementById('txtBookname').innerHTML = x;
        }

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card box-content">

        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <div class="modal-header">
                        <h2 class="modal-title" id="exampleModalLabel"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132116") %></strong></h2>
                    </div>

                    <div class="row modal-body">
                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>:</label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132118") %>" disabled="disabled" />
                            </div>
                        </div>

                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>:</label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132119") %>" disabled="disabled" />
                            </div>
                        </div>

                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201005") %>:</label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>" disabled="disabled" />
                            </div>
                        </div>

                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404013") %>:</label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" placeholder="ภาษาไทยประถม 1" id="bkName" />
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div class="col-sm-6">
                            <button type="button" class="btn btn-success" onclick="insertTEXT()" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></strong></button>
                        </div>
                        <div class="col-sm-6 text-left">
                            <button type="button" class="btn btn-danger" onclick="Canclesetting()" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></strong></button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <%--model--%>

        <div class="row">
            <div class="form-group col-sm-6 sort_type">
                <div class="col-sm-5">
                    <label class="control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> :</label>
                </div>
                <div class="col-sm-7 text-left">
                    <select id="select_month" class="form-control">
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111056") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111057") %></option>
                        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111058") %></option>
                        <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111059") %></option>
                        <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111060") %></option>
                        <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111061") %></option>
                        <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111062") %></option>
                        <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111063") %></option>
                        <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111064") %></option>
                        <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111065") %></option>
                        <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111066") %></option>
                        <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111067") %></option>
                    </select>
                </div>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary btn-block" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData()" />
            </div>
        </div>

        <br />

        <div class="allFont">
            <table class="table table-bordered">
                <thead>
                    <tr class="tbHead">
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404013") %></td>
                        <td>&nbsp</td>
                    </tr>
                </thead>
                <tbody>
                    <tr class="tbBodt">
                        <td>1</td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132118") %></td>
                        <td>ภาษาไทยเบื่องต้น</td>
                        <td id="txtBookname">ภาษาไทยประถม 1</td>
                        <td class="text-center">
                            <button type="button" class="btn btn-danger" onclick='Modelsetting()'>&nbsp <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %> &nbsp</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>


    </div>

</asp:Content>
