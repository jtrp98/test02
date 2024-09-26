<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp.Master" CodeBehind="Index_TowerBuilding.aspx.cs" Inherits="FingerprintPayment.Maintenance.Index_TowerBuilding" %>


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

        td {
            text-align: center;
        }
    </style>


    <script type="text/javascript">


        function Modelsetting() {
            $("#exampleModal").modal()
        }

        //function insertTable() {
        //    var table = document.getElementById('tbTown');

        //    var Number = document.getElementById('Number').value;
        //    var TownName = document.getElementById('TownName').value;

        //    var row = table.insertRow(3);
        //    var cell1 = row.insertCell(0);
        //    var cell2 = row.insertCell(1);
        //    var cell3 = row.insertCell(2);
        //    cell1.innerHTML = Number;
        //    cell2.innerHTML = TownName;
        //    cell3.innerHTML = "";
        //}

        function insertTable() {

            var Number = document.getElementById('Number').value;
            var TownName = document.getElementById('TownName').value;

            var student;
            var runNum = 1;
            for (var j = 1; j < 2; j++) {
                student = {
                    number: runNum++,
                    townname: TownName,
                    emty: "",
                };
                var table = document.getElementById('tbTown');
                var row = table.insertRow(j);
                var cell1 = row.insertCell(0);
                var cell2 = row.insertCell(1);
                var cell3 = row.insertCell(2);
                cell1.innerHTML = student.number;
                cell2.innerHTML = student.townname;
                cell3.innerHTML = student.emty;
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
                        <h2 class="modal-title" id="exampleModalLabel"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132345") %></strong></h2>
                    </div>

                    <div class="row modal-body">
                        <div class="form-group col-sm-12" hidden="hidden">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" id="Number" />
                            </div>
                        </div>
                        <div class="form-group col-sm-12">
                            <label class="col-sm-4 control-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132346") %>:</label>
                            <div class="col-sm-6 text-left">
                                <input class="form-control" type="text" size="31" id="TownName" />
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div class="col-sm-6">
                            <button type="button" class="btn btn-success" onclick="insertTable()" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></strong></button>
                        </div>
                        <div class="col-sm-6 text-left">
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></strong></button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <%--model--%>

        <div class="allFont">
            <table class="table table-bordered" id="tbTown">
                <thead>
                    <tr class="tbHead">
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></td>
                        <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132346") %></td>
                        <td>
                            <button type="button" class="btn btn-success" onclick='Modelsetting()'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></button>
                        </td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>



    </div>

</asp:Content>

