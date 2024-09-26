<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_OnlineLearn.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Online_Learning.Index_OnlineLearn" %>


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


        function readPIC(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#blah')
                        .attr('src', e.target.result);
                };

                reader.readAsDataURL(input.files[0]);
            }
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
                        <h2 class="modal-title" id="exampleModalLabel"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132733") %></strong></h2>
                    </div>

                    <div class="row modal-body">

                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right">แนบภาพ :</label>
                            <div class="col-sm-6">
                                <input type='file' onchange="readPIC(this);" />
                                <img id="blah" />
                            </div>
                        </div>

                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206358") %> :</label>
                            <div class="col-sm-6">
                                <input type='file' onchange="readFILE(this);" />
                                <img id="infile" />
                            </div>
                        </div>

                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right">แนบลิงค์ :</label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" placeholder="https://youtu.be/6bwi63VGYes" disabled="disabled" />
                            </div>
                            <div class="text-center">
                                <iframe width="380" height="290" src="https://www.youtube.com/embed/6bwi63VGYes"></iframe>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div class="col-sm-6">
                            <button type="button" class="btn btn-success" onclick="Printsetting()" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></strong></button>
                        </div>
                        <div class="col-sm-6 text-left">
                            <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="Canclesetting()"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></strong></button>
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
                <input type="button" class="btn btn-primary btn-block" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData();" />
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
                        <td>ภาษาไทยประถม 1</td>
                        <td>
                            <button type="button" class="btn btn-success" onclick='Modelsetting()'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132733") %></button></td>
                    </tr>
                    <tr class="tbBodt">
                        <td>2</td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132117") %></td>
                        <td>คณิตศาสตร์เบื่องต้น</td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132732") %></td>
                        <td>
                            <button type="button" class="btn btn-success" onclick="Modelsetting()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132733") %></button></td>
                    </tr>
                </tbody>
            </table>
        </div>



    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
