<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popupAlart.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.PopUP.popupAlart" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#exampleModal").modal()
        });

        function searchData() {
            $("#exampleModal").modal()
        }

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card box-content">


        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <div class="row modal-body">

                        <div class="form-group col-sm-12">
                            <div class="text-center">
                                <img src="../images/exclamation-256.jpg" style="width: 200px; height: 200px;" />
                                <br />
                            </div>
                            <h1 class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131153") %> 
                                <br />
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132761") %>
                                <br />
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131157") %>
                            </h1>
                        </div>
                        <div class="col-sm-12 text-center">

                            <a href="../AdminMain.aspx" class="btn btn-danger"><strong>&nbsp <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %> &nbsp</strong></a>
                            <%--<button type="button" class="btn btn-danger" onclick="insertTEXT()" data-dismiss="modal"><strong>&nbsp <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %> &nbsp</strong></button>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <%--model--%>


        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select id="select_exam" class="form-control">
                        <option value="1">2562</option>
                    </select>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select class="form-control">
                        <option value="1">1</option>
                        <option value="2">2</option>
                    </select>
                </div>
            </div>
        </div>

        <div class="row student">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select class="form-control">
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111056") %></option>
                    </select>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <select class="form-control">
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111056") %> / 1</option>
                    </select>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary btn-block" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="searchData()" />
            </div>
        </div>

    </div>


</asp:Content>


