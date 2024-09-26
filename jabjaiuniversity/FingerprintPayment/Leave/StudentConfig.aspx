<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="StudentConfig.aspx.cs" Inherits="FingerprintPayment.Leave.StudentConfig" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--<link rel="stylesheet" href="/assets/plugins/datatables/jquery.dataTables.min.css" />--%>
    <%--    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Content/Material/assets/css/toggle.css" rel="stylesheet" />

    <style>
        /*  .dropdown.bootstrap-select {
            width: 99% !important;
        }*/

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
        input.readonly {
            pointer-events: none !important;
            background-image: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script>    

        $(function () {

            //md.initFormExtendedDatetimepickers();
            onSearch();

            $('#text1').on('keyup change', function (e) {
                var v = +($(this).val());
                if (v > 3
                    && e.keyCode !== 46 // keycode for delete
                    && e.keyCode !== 8 // keycode for backspace
                ) {
                    e.preventDefault();
                    $(this).val(3);
                }

                if (v < 1
                    && e.keyCode !== 46 // keycode for delete
                    && e.keyCode !== 8 // keycode for backspace
                ) {
                    e.preventDefault();
                    $(this).val(1);
                }
            });

            $('#CollapseDiv1').on('show.bs.collapse', function () {
                //if ($('#logHistory').data('loaded') == '0') {

                $.ajax({
                    type: "POST",
                    url: "StudentConfig.aspx/GetLog",
                    data: '{type: "1"}',
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

        function onEdit1(obj) {
            $(obj).hide();
            $('#text1').removeClass('readonly');
            $('#btnSave1').show();
        }

        function onEdit3() {
            $('#myModal3').modal('show');
        }

        function onSave1() {
            var no = $('#text1').val();
            $("body").mLoading('show');
            PageMethods.SaveData1(no, function (response) {

                if (response.text == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                    });

                    onSearch();
                    $('#text1').addClass('readonly');
                    $('#btnSave1').hide();
                    $('#btnEdit1').show();
                    $("body").mLoading('hide');
                }
                else {
                    //Swal.fire({
                    //    type: 'error',
                    //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    //});
                }
            });
        }

        function onSave3() {
            var num = $('#leaveNum').val();
            var type = $('#leaveType').val();
            $("body").mLoading('show');
            PageMethods.SaveData3(num, type, function (response) {

                if (response.text == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                    });

                    onSearch();
                    $('#myModal3').modal('hide');
                    $("body").mLoading('hide');
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

            PageMethods.GetData(function (response) {

                if (response.text == 'success') {

                    if (response.data) {
                        $('#text1').val(response.data.approveNum);
                        $('#text31').text(response.data.day);
                        $('#text32').text(response.data.type == 'Y' ? '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301055") %>' : '1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %>');
                        $('#leaveNum').val(response.data.day);
                        $('#leaveType').val(response.data.type);
                        $('#leaveType').selectpicker('refresh');
                        // $('#logHistory').html(response.logs);
                    }

                }
                else {


                }
            });
        }



    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301046") %>
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
                            <i class="material-icons">settings</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132310") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102190") %></label>
                            <div class="col-md-2 "></div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301048") %>
                                <br />
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102193") %>
                            </label>
                            <div class="col-md-1 text-right">
                                <input type="number" id="text1" class="form-control text-center readonly" value="" />
                            </div>
                            <div class="col-md-1 text-left p-1">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                            </div>
                            <div class="col-md-4">
                                <button type="button" id="btnEdit1" onclick="onEdit1(this)" class="btn  btn-warning btn-sm">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                </button>
                                <button type="button" id="btnSave1" onclick="onSave1()" class="btn btn-sm btn-success" style="display: none">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>
                            </div>
                        </div>

                        <div class=" row mt-2">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301051") %></label>
                            <div class="col-md-2 text-right">
                            </div>
                            <div class="col-md-4">
                                <a href="/classmember/classmember.aspx" target="_blank" class="btn btn-warning btn-sm">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301052") %>
                                </a>
                            </div>
                        </div>
                        <div class=" row mt-2">
                            <div class="col-md-1"></div>
                            <div class="col-md-5  col-form-label text-left">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301053") %></label>
                                <label class="col-form-label" id="text31"></label>
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301054") %></label>
                                <label class="col-form-label" id="text32"></label>
                            </div>
                            <div class="col-md-2 text-right">
                            </div>
                            <div class="col-md-4">
                                <button type="button" onclick="onEdit3()" class="btn btn-sm btn-warning">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301056") %>
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
                                <div class="col-md-12" id="logHistory" data-loaded="0">
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
    <div id="myModal3" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="display: block !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132311") %></h4>
                </div>
                <div class="modal-body" style="">

                    <div class="row">
                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132312") %></label>
                        <div class="col-md-6 ">
                            <input type="text" id="leaveNum" class="form-control" value="" />
                        </div>
                        <div class="col-md-1 "></div>

                        <div class="col-md-1"></div>
                        <label class="col-md-4  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132313") %></label>
                        <div class="col-md-6 ">
                            <select id="leaveType" class="selectpicker" data-style="select-with-transition" data-width="100%">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132314") %></option>
                                <option value="T"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405023") %></option>
                                <option value="Y"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105012") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 "></div>
                    </div>

                </div>
                <div class="modal-footer" style="justify-content: center">
                    <button type="button" onclick="onSave3()" class="btn  btn-success">
                        <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                    </button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
