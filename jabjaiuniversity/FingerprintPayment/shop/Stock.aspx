<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="FingerprintPayment.shop.Stock" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--<link rel="stylesheet" href="/assets/plugins/datatables/jquery.dataTables.min.css" />--%>
    <%--    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Content/Material/assets/css/toggle.css" rel="stylesheet" />

    <style>
        .dropdown.bootstrap-select {
            width: 99% !important;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }


        .el-switch-style {
            top: 5px !important;
        }
        /*table.dataTable thead .sorting_asc,
        table.dataTable thead .sorting_desc,
        table.dataTable thead .sorting {
            background-image: url('') !important;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
            background: #fff !important;
            border: 0px !important;
        }*/
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script>    

        $(function () {

            //md.initFormExtendedDatetimepickers();
            onSearch();

            //$('#btnCollapDiv1').click(function () { //you can give id or class name here for $('button')
            //    $(this).text(function (i, old) {
            //        return old == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132308") %>' ? '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132309") %>' : '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132308") %>';
            //    });
            //});


            $('#CollapseDiv1').on('show.bs.collapse', function () {
                //if ($('#logHistory').data('loaded') == '0') {

                $.ajax({
                    type: "POST",
                    url: "StudentConfig.aspx/GetLog",
                    data: '{type: "3"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $('#logHistory').html(response.d.logs);
                        //$('#logHistory').data('loaded', '1');
                    },
                    failure: function (response) {

                    },
                    error: function (response) {

                    }
                });


                //}

            });
        });

        function onCreate() {
            $('#myModal').modal('show');
        }

        function onEdit(id) {
            $("body").mLoading('show');
            PageMethods.GetByID(id, function (response) {
                $("body").mLoading('hide');
                if (response.text == 'success') {

                    $("#TypeID").val(response.data.id);
                    $("#nameTH2").val(response.data.name);
                    $("#nameEN2").val(response.data.nameEn);
                    $("#nameAbbrTH2").val(response.data.abbrTH);
                    $("#nameAbbrEN2").val(response.data.abbrEN);
                    $('#myModal2').modal('show');
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    });
                }
            });

        }

        function onUpdate() {
            let _saveData = {
                "ID": $("#TypeID").val(),
                "NameTH": $("#nameTH2").val(),
                "NameEN": $("#nameEN2").val(),
                "AbbrTH": $("#nameAbbrTH2").val(),
                "AbbrEN": $("#nameAbbrEN2").val(),
            };

            $("body").mLoading('show');
            PageMethods.SaveData(_saveData, function (response) {
                $("body").mLoading('hide');
                if (response.text == 'success') {

                    onSearch();

                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                    });

                    $("#TypeID").val('');
                    $("#nameTH2").val('');
                    $("#nameEN2").val('');
                    $("#nameAbbrTH2").val('');
                    $("#nameAbbrEN2").val('');

                    $('#myModal2').modal('hide');
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    });
                }
            });
        }

        function onSave(btn) {
            $('#btnSave').attr("disabled", "disabled");

            let _saveData = {
                "NameTH": $("#nameTH1").val(),
                "NameEN": $("#nameEN1").val(),
                "AbbrTH": $("#nameAbbrTH1").val(),
                "AbbrEN": $("#nameAbbrEN1").val(),
            };
            $("body").mLoading('show');
            PageMethods.SaveData(_saveData, function (response) {
                $("body").mLoading('hide');
                $('#btnSave').removeAttr("disabled");

                if (response.text == 'success') {
                    onSearch();

                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                    });

                    $("#nameTH1").val('');
                    $("#nameEN1").val('');
                    $("#nameAbbrTH1").val('');
                    $("#nameAbbrEN1").val('');

                    $('#myModal').modal('hide');
                }
                else {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    });
                }

            });
        }

        function onRemove(id) {

            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>',
                //text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
            }).then((result) => {
                if (result.value) {
                    $("body").mLoading('show');
                    PageMethods.RemoveByID(id,
                        function (result) {
                            $("body").mLoading('hide');
                            if (result.text == "success") {

                                onSearch();

                                Swal.fire({
                                    type: 'success',
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                                });


                            }
                        },
                        function (result) {
                            console.log(result);
                        })

                    return true;
                }
            });

            return false;
        }

        function onSwitch(id, obj) {
            $("body").mLoading('show');
            PageMethods.SwitchByID(id, obj.checked, function (response) {
                $("body").mLoading('hide');
                if (response.text == 'success') {
                    //Swal.fire({
                    //    type: 'success',
                    //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                    //});
                }
                else {
                    //Swal.fire({
                    //    type: 'error',
                    //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    //});
                }
            });
        }

        function onSearch() {

            var dt = $('#lst-data').DataTable({
                "processing": true,
                "destroy": true,
                "info": false,
                paging: true,
                "pageLength": 50,
                "bLengthChange": false,
                searching: false,

                ajax: {
                    url: "LeaveType.aspx/GetData",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    "dataSrc": function (r) {
                        return r.d.data;
                    },
                    'data': function (d) {

                        d.search = {
                            'text': $('#searchText').val(),
                        };

                        return JSON.stringify(d);
                    },
                },

                columns: [
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><br>/No', "data": "index", "width": "5%", "class": "text-center" },
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %><br>/Type of Leave', "data": "name", "width": "12%", "class": "text-center" },
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102179") %><br>/Abbrevation TH', "data": "abbrTH", "width": "7%", "class": "text-center" },
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102180") %><br>/Type of Leave(EN)', "data": "nameEn", "width": "12%", "class": "text-center" },
                    { "title": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102181") %><br>/Abbrevation EN', "data": "abbrEN", "width": "7%", "class": "text-center" },
                    {
                        "title": `<button type="button" onclick="onCreate()" class="btn btn-fill btn-info">
                                     <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                                  </button>`, "width": "8%", "class": "text-center", "orderable": false,
                        "mRender": function (data, type, row) {
                            if (row.isDefault) {
                                return `
<label class="el-switch el-switch-sm ">
    <input type="checkbox" onclick="onSwitch(${row.id} , this)" class="switch-button" data-id="${row.id}" ${(row.active ? "checked" : "")} hidden>
    <span class="el-switch-style"></span>
</label>
<a style="width: 24px;display: inline-block;">&nbsp;</a>
<a style="width: 24px;display: inline-block;">&nbsp;</a>
`;
                            }
                            else {
                                return `
<label class="el-switch el-switch-sm ">
    <input type="checkbox" onclick="onSwitch(${row.id} , this)" class="switch-button" data-id="${row.id}" ${(row.active ? "checked" : "")} hidden>
    <span class="el-switch-style"></span>
</label>
<a class="" style="color:#00bcd4;cursor:pointer;" href="#" onclick="onEdit(${row.id})"> <span class="material-icons">edit</span></a>
<a class="" style="color:#f44336;cursor:pointer;" onclick="onRemove(${row.id})"> <span class="material-icons">delete</span></a>
`;
                            }
                        }
                    },
                ],
                "order": [[0, 'asc']],
                "fnInitComplete": function (oSettings, json) {
                    //$('#logHistory').html(json.d.logs);
                },
            });

        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102177") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

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
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %><br />
                                Leave type</label>
                            <div class="col-md-3 ">
                                <input type="text" id="searchText" class="form-control" value="" />
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"></label>
                            <div class="col-md-3 ">
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" onclick="onSearch()" class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">delete</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301040") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
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
                        <h5 class="card-title">
                            <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102185") %></strong>
                            <button id="btnCollapDiv1" style="margin-top: -8px;" class="btn btn-success btn-sm float-right" type="button" data-toggle="collapse" data-target="#CollapseDiv1" aria-expanded="false" aria-controls="CollapseDiv1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102186") %></button>
                        </h5>

                    </div>
                    <div class="collapse showx multi-collapse" id="CollapseDiv1">
                        <div class="card-body ">
                            <div class="row">
                                <div class="col-md-12" id="logHistory">
                                    <div class="col-md-12 text-center">
                                        <img src="/images/SBLoading.gif?v=1" width="70px" alt="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="display: block !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102182") %></h4>
                </div>
                <div class="modal-body" style="">

                    <div class="row">
                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %><br />
                            Leave type</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameTH1" class="form-control" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102179") %><br />
                            Abbrevation</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameAbbrTH1" class="form-control" maxlength="6" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102180") %><br />
                            Leave type(EN)</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameEN1" class="form-control" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102179") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131001") %>)<br />
                            Abbrevation(EN)</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameAbbrEN1" class="form-control" maxlength="6" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                    </div>

                </div>
                <div class="modal-footer" style="justify-content: center">
                    <button id="btnSave" type="button" onclick="onSave()" class="btn  btn-success">
                        <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="display: block !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102184") %></h4>
                </div>
                <div class="modal-body" style="">

                    <div class="row">
                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %><br />
                            Leave type</label>
                        <div class="col-md-6 ">
                            <input type="text" id="TypeID" class="d-none" value="" />
                            <input type="text" id="nameTH2" class="form-control" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102179") %><br />
                            Abbrevation</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameAbbrTH2" class="form-control" maxlength="6" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102180") %><br />
                            Leave type(EN)</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameEN2" class="form-control" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102179") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131001") %>)<br />
                            Abbrevation(EN)</label>
                        <div class="col-md-6 ">
                            <input type="text" id="nameAbbrEN2" class="form-control" maxlength="2" value="" />
                        </div>
                        <div class="col-md-1 "></div>
                    </div>

                </div>
                <div class="modal-footer" style="justify-content: center">
                    <button type="button" onclick="onUpdate()" class="btn  btn-success">
                        <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
