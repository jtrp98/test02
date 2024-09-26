<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_UploadYear.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.UpLoad_Year.Index_UploadYear" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>


    <style type="text/css">

        div.allFont {
            font-size: 24px;
        }

        tr.tbHead {
            background-color: #337ab7;
            color: white;
            font-weight: bold;
            text-align: center;
        }

        tr.tbBodt {
            text-align: center;
        }

        img {
            max-height: 80px;
            max-width: 80px;
        }

        input[type=file] {
            padding: 10px;
        }

    </style>


    <script type="text/javascript">

        function Modelsetting() {
            $("#exampleModal").modal()
        }

        function readFILE(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#infile')
                        .attr('src', e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="full-card box-content">

        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">

                    <div class="modal-header">
                        <h2 class="modal-title" id="exampleModalLabel"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133400") %></strong></h2>
                    </div>

                    <div class="row modal-body">
                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206358") %> :</label>
                            <div class="col-sm-6">
                                <input type='file' onchange="readFILE(this);" />
                                <img id="infile" />
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
                    <label class="control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                </div>
                <div class="col-sm-7 text-left">
                    <select id="select_month" class="form-control">
                        <option value="1">2563</option>
                        <option value="2">2562</option>
                        <option value="3">2561</option>
                        <option value="4">2560</option>
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
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></td>
                        <td>&nbsp</td>
                    </tr>
                </thead>
                <tbody>
                    <tr class="tbBodt">
                        <td>1</td>
                        <td>2563</td>
                        <td class="text-center">
                            <button type="button" class="btn btn-danger" onclick='Modelsetting()'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133401") %></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>






    </div>


</asp:Content>

