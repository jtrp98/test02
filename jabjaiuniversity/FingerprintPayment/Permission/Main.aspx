<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="FingerprintPayment.Permission.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="asset/css.css" rel="stylesheet" />
    <link rel="stylesheet" href="/assets/plugins/datatables/jquery.dataTables.min.css" />

    <style>
        .col-tool a {
            padding: 0 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <span style="font-size: 18px;" class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></span>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <a href="ModifyGroup.aspx" class="btn btn-success pull-right">
                                    <span class="material-icons">add</span>
                                    &nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803022") %>
                                </a>
                            </div>
                            <div class="col-md-12">
                                <table id="lst-data" class=" table-hover dataTable" width="100%"></table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102185") %>
                        </h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12" id="logHistory">
                                <asp:Literal runat="server" ID="ltrLogHistory"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="asset/js.js"></script>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>    

        function onDelete(gid) {
            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803048") %>',
                icon: 'warning',
                showCancelButton: true,
                //confirmButtonColor: '#3085d6',
                //cancelButtonColor: '#d33',
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201067") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
            }).then((result) => {
                if (result.value) {

                    PageMethods.RemoveGroup(gid
                        , function (response) {
                            Swal.fire({
                                icon: 'success',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132757") %>',
                                showConfirmButton: false,
                                timer: 1500,
                                willClose: () => {
                                    window.location = "Main.aspx";
                                }
                            })
                        },
                        function (response) {
                            Swal.fire({
                                icon: 'error',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132758") %>',
                                //text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132756") %>',
                            })
                        }
                    );

                }
            });
        }

        function onLoad() {

            var dt = $('#lst-data').DataTable({
                "processing": true,
                //"serverSide": true,
                //"serverSide": true,
                "destroy": true,
                "info": false,
                paging: false,
                "pageLength": 20,
                searching: false,
                "lengthChange": false,
                //dom: 'Bfrtip',
                //"ajax": "Main.aspx/LoadData",
                //"ajax": {
                //    "url": "Main.aspx/LoadData",
                //    "type": "GET"
                //},
                ajax: function (data, callback, settings) {
                    $.ajax({
                        url: "Main.aspx/LoadData",
                        type: 'GET',
                        contentType: 'application/json',
                        //dataType: "json",
                        //data: JSON.stringify(data),
                        success: function (data) {
                            //$spinner.hide();
                            callback(data.d); // execute the callback function on the wrapped data
                        }
                    });
                },
                "columnDefs": [{
                    "targets": 2,
                    "orderable": false
                }],
                "columns": [
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>', data: 'index', "class": "text-center", "width": "10%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803020") %>", data: 'title', "class": "text-center", "width": "30%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103165") %>", data: 'editor', "class": "text-center", "width": "20%" },
                    { "title": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107053") %>", data: 'modify', "class": "text-center", "width": "20%" },
                    {
                        "title": "", data: '', "class": "text-center col-tool", "width": "20%", "mRender": function (data, type, row) {

                            var edit = '<a href="ModifyGroup.aspx?gid=' + row.id + '" style="color:green"><span class="material-icons">mode_edit</span></a>';

                            var del = '';
                            if (row.isEdit)
                                del = '<a href="#" onclick="onDelete(' + row.id + ')" style="color:red"><span class="material-icons">delete</span></a>';

                            return edit + del;
                        }
                    },
                ],
                "order": [[0, 'asc']]
            });

        }

        $(function () {
            onLoad();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
