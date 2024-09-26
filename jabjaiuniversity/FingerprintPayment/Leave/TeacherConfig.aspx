<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="TeacherConfig.aspx.cs" Inherits="FingerprintPayment.Leave.TeacherConfig" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <%--<link rel="stylesheet" href="/assets/plugins/datatables/jquery.dataTables.min.css" />--%>
    <%--    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="/Content/Material/assets/css/toggle.css" rel="stylesheet" />

    <style>
        /* .dropdown.bootstrap-select {
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

        tr.readonly {
            pointer-events: none;
        }

            tr.readonly button.dropdown-toggle {
                background-image: none !important;
            }

                tr.readonly button.dropdown-toggle::after {
                    display: none;
                }

            tr.readonly.--edit button.dropdown-toggle {
                background-image: linear-gradient(to top, #9c27b0 2px, rgba(156, 39, 176, 0) 2px), linear-gradient(to top, #d2d2d2 1px, rgba(210, 210, 210, 0) 1px) !important;
            }

                tr.readonly.--edit button.dropdown-toggle::after {
                    display: inline-block;
                }

            tr.readonly .btn-edit {
                display: inline-block;
            }

            tr.readonly.--edit .btn-edit {
                display: none;
            }

            tr.readonly .btn-save {
                display: none;
            }

            tr.readonly.--edit .btn-save {
                display: inline-block;
            }

            tr.readonly.--edit {
                pointer-events: all !important;
            }

            tr.readonly button.btn-edit,
            tr.readonly button.btn-save {
                pointer-events: auto !important;
            }

        input.readonly {
            pointer-events: none !important;
            background-image: none;
        }

        #wrapper2 {
            pointer-events: none;
        }

            #wrapper2 input {
                background-image: none !important;
            }

            #wrapper2.--edit {
                pointer-events: all !important;
            }

                #wrapper2.--edit input {
                    background-image: linear-gradient(to top, #9c27b0 2px, rgba(156, 39, 176, 0) 2px), linear-gradient(to top, #d2d2d2 1px, rgba(210, 210, 210, 0) 1px) !important;
                }

        #temp2 {
            display: none
        }

        #wrapper3.readonly input {
            background-image: none !important;
            pointer-events: none;
        }

        #wrapper3 input {
            background-image: linear-gradient(to top, #9c27b0 2px, rgba(156, 39, 176, 0) 2px), linear-gradient(to top, #d2d2d2 1px, rgba(210, 210, 210, 0) 1px) !important;
        }

        #wrapper3.readonly .form-control-feedback {
            display: none;
        }

        #wrapper3 .form-control-feedback {
            display: block;
        }

        hr {
            display: block;
            height: 1px;
            border: 0;
            border-top: 1px solid #ccc;
            margin: 1em 0;
            padding: 0;
        }

        .row41 .btn, .row42 .btn, .row43 .btn {
            display: inline-block;
        }

        .row41:first-child .btn, .row42:first-child .btn, .row43:first-child .btn {
            display: none;
        }

        #wrapper41, #wrapper42, #wrapper43, #wrapper44 {
            pointer-events: none;
        }

            #wrapper41.--edit, #wrapper42.--edit, #wrapper43.--edit, #wrapper44.--edit {
                pointer-events: all !important;
            }

            #wrapper44 button.dropdown-toggle {
                background-image: none !important;
            }

                #wrapper44 button.dropdown-toggle::after {
                    display: none;
                }

            #wrapper44.--edit button.dropdown-toggle {
                background-image: linear-gradient(to top, #9c27b0 2px, rgba(156, 39, 176, 0) 2px), linear-gradient(to top, #d2d2d2 1px, rgba(210, 210, 210, 0) 1px) !important;
            }

                #wrapper44.--edit button.dropdown-toggle::after {
                    display: inline-block;
                }

        .btn-tool4 {
            display: none;
        }

            .btn-tool4.--edit {
                display: flex;
            }

        .input-y1 {
            background-color: #eee;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script>    
        var _empList = <%= Newtonsoft.Json.JsonConvert.SerializeObject(EmpJobList) %>;
        $(function () {

            //$('.selectpicker').selectpicker();

            $('.datepicker').datetimepicker({
                format: 'DD/MM',
                locale: 'th',
                debug: false,
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });

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
                    data: '{type: "2"}',
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

                //var $select = $('#selectEmpList');

                //}
            });

            $('#wrapper2').on('keyup change', 'input.dayinput', function (e) {
                var v = +($(this).val());

                if (v < 0
                    && e.keyCode !== 46
                    && e.keyCode !== 8
                ) {
                    e.preventDefault();
                    $(this).val(0);
                }

            });
            $('#wrapper2').on('keyup change', 'input.input-y2', function (e) {
                var v = +($(this).val());

                if (v < 0
                    && e.keyCode !== 46
                    && e.keyCode !== 8
                ) {
                    e.preventDefault();
                    $(this).val(0);
                }

            });
            onSearch();

        });

        function onValidate2() {
            var isValid = true;

            $('#wrapper2 > .row-item2').each(function (i) {
                var $this = $(this);
                var valueInputs = $this.find('input.dayinput').filter(function () {
                    var _val = +$(this).val();
                    return _val > 0;
                });

                var y1 = +$this.find('#year1').val();
                var y2 = +$this.find('#year2').val();

                if (y1 >= y2)
                    isValid = false;

                if (valueInputs.length == 0) {
                    isValid = false;
                }

                if (!isValid)
                    return false;
            });

            return isValid;
        }

        function onEditYear2(obj) {
            $(obj).parents('.row-item2').next().find('.input-y1').val(obj.value);
        }

        function onAddNew2() {
            if (onValidate2()) {
                var $o = $('#temp2').clone();
                var index = $('#wrapper2 .year-title').length + 1;
                $o.attr('id', 'row' + index)
                $o.find('.year-title').text(`2.${index} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132315") %>`);
                $o.find('#year1').val($('input[id=year2]:last').val() || '0');
                $('#wrapper2').append($o);
            }
            else {
                Swal.fire({
                    type: 'warning',
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105029") %>',
                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132316") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132317") %>',
                });
            }
        }

        function onSave2() {
            if (onValidate2()) {
                var data = [];
                $('#wrapper2 > .row-item2').each(function (i) {
                    var $this = $(this);
                    var dayId = $this.find('#rowId').val() || '0';
                    var list = [];
                    $this.find('.dayinput').each(function (j) {
                        list.push({
                            TypeID: $(this).data('typeid'),
                            Day: $(this).val(),
                        });
                    });
                    var d = {
                        PKID: dayId,
                        YearStart: $this.find('#year1').val(),
                        YearEnd: $this.find('#year2').val(),
                        List: list,
                        //Sick: $this.find('#sick').val(),
                        //Personal: $this.find('#personal').val(),
                        //Vacation: $this.find('#vacation').val(),
                        //Maternity: $this.find('#maternity').val(),
                        //Ordination: $this.find('#ordination').val(),
                        //Military: $this.find('#military').val(),
                        //Goverment: $this.find('#goverment').val(),
                        //Training: $this.find('#training').val(),
                    };
                    data.push(d);
                });

                PageMethods.SaveData2(data, $('#delete2').val(), function (response) {

                    if (response.text == 'success') {
                        //$('#btnOpen2').hide();
                        //$('#btnClose2').show();
                        Swal.fire({
                            type: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                            timer: 1500
                        });

                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                        });
                    }
                });
            }
            else {
                Swal.fire({
                    type: 'warning',
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105029") %>',
                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132316") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132317") %>',
                });
            }
        }

        function onEdit2(obj) {
            $('#wrapper2').addClass('--edit');
            $('#btn-tool2').show();
            $(obj).hide();
        }

        function onRemove2(t) {

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
                    var $row = $(t).parents('.row-item2');
                    var id = $row.find('#rowId').val();
                    if (id) {
                        $('#delete2').val($('#delete2').val() + ',' + id);
                    }
                    $row.remove();

                    return true;
                }
            });


        }
        function onSwitch2(obj) {
            //var $t = $(obj);
            PageMethods.OnEnableOrDisable(obj.checked, function (response) {

                if (response.text == 'success') {
                    //$('#btnOpen2').hide();
                    //$('#btnClose2').show();
                    if (obj.checked) {
                        $('#btnEdit2').show();
                        $('#wrapper2').show();
                    }
                    else {
                        $('#btnEdit2').hide();
                        $('#wrapper2').hide();
                    }

                    if (response.employeeList) {
                        Swal.fire({
                            type: 'warning',
                            //title: (obj.checked ? '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132324") %>' : '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132325") %>'),
                            title:"<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102198") %>",
                            html: "<strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102199") %></strong><div class='text-left' style='padding-left: 25px;'>" + response.employeeList + "</div>",
                        });
                        if(obj.checked)
                            $('#btnSwitch2').prop('checked', false);
                    }
                    else {
                        Swal.fire({
                            type: 'success',
                            title: (obj.checked ? '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132324") %>' : '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132325") %>'),
                            timer: 1500
                        });
                    }


                    // onSearch();
                }
                else {
                    //Swal.fire({
                    //    type: 'error',
                    //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    //});
                }
            });
        }

        //function onOpen2() {
        //    PageMethods.OnEnableOrDisable(true, function (response) {

        //        if (response.text == 'success') {
        //            $('#btnOpen2').hide();
        //            $('#btnClose2').show();
        //            $('#btnEdit2').show();
        //            Swal.fire({
        //                type: 'success',
        //                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
        //                timer: 1500
        //            });

        //            // onSearch();
        //        }
        //        else {
        //            //Swal.fire({
        //            //    type: 'error',
        //            //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
        //            //});
        //        }
        //    });
        //}

        //function onClose2() {
        //    PageMethods.OnEnableOrDisable(false, function (response) {

        //        if (response.text == 'success') {
        //            $('#btnOpen2').show();
        //            $('#btnClose2').hide();
        //            $('#btnEdit2').hide();
        //            Swal.fire({
        //                type: 'success',
        //                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
        //                timer: 1500
        //            });


        //            // onSearch();
        //        }
        //        else {
        //            //Swal.fire({
        //            //    type: 'error',
        //            //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
        //            //});
        //        }
        //    });
        //}

        function onEdit12(index) {
            $('#lst-data12 #row' + index).addClass('--edit');
        }

        function onEdit11(obj) {
            $(obj).hide();
            $('#text1').removeClass('readonly');
            $('#btnSave11').show();
        }

        function onSave11() {
            var no = $('#text1').val();
            PageMethods.SaveData11(no, function (response) {

                if (response.text == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                        timer: 1500
                    });

                    $('#text1').addClass('readonly');
                    $('#btnSave11').hide();
                    $('#btnEdit11').show();
                    // onSearch();
                }
                else {
                    //Swal.fire({
                    //    type: 'error',
                    //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    //});
                }
            });
        }

        function onSave12(index) {
            var $tr = $('#lst-data12 #row' + index);
            var depId = $tr.data('dept');
            var user1 = $tr.find(`#${index}user1`).val();
            var name1 = $tr.find(`#${index}user1 :selected`).text();
            var user2 = $tr.find(`#${index}user2`).val();
            var name2 = $tr.find(`#${index}user2 :selected`).text();
            var user3 = $tr.find(`#${index}user3`).val();
            var name3 = $tr.find(`#${index}user3 :selected`).text();
            //console.log(`${depId} ${user1} ${user2} ${user3}`);

            PageMethods.SaveData12(depId, user1, name1, user2, name2, user3, name3, function (response) {

                if (response.text == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                        timer: 1500
                    });

                    //onSearch();
                }
                else {
                    //Swal.fire({
                    //    type: 'error',
                    //    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                    //});
                }
            });

            $tr.removeClass('--edit');
        }

        function onEdit3(obj) {
            $(obj).hide();
            $('#wrapper3').removeClass('readonly');
            $('#btnSave3').show();

        }

        function onSave3() {
            PageMethods.SaveCutOffDay($('#text3').data("DateTimePicker").date().format('MM/DD') + '/2000', function (response) {

                if (response.text == 'success') {
                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                        timer: 1500
                    });

                    $('#wrapper3').addClass('readonly');
                    $('#btnSave3').hide();
                    $('#btnEdit3').show();
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
            $("body").mLoading();
            PageMethods.GetData(function (response) {

                if (response.text == 'success') {

                    if (response.data1) {
                        $('#text1').val(response.data1.approveNum);
                        if (response.data1.isEnable) {
                            //$('#btnOpen2').hide();
                            //$('#btnClose2').show();
                            $('#btnSwitch2').attr('checked', 'checked');
                            $('#btnEdit2').show();
                            $('#wrapper2').show();
                        }
                        else {
                            //$('#btnOpen2').show();
                            //$('#btnClose2').hide();
                            $('#btnEdit2').hide();
                            $('#wrapper2').hide();
                        }
                        $('#text3').val(response.data1.cutOffDay);
                        //$('#logHistory').html(response.logs);
                    }
                    else {
                        $('#btnOpen2').show();
                        $('#btnClose2').hide();
                    }

                    if (response.data12) {
                        var $tbl = $('#lst-data12 tbody');
                        var $select = $('#selectEmpList');
                        $select.addClass('selectpicker');
                        $select.attr('name', '');
                        var index = 1;

                        response.data12.forEach(function (d) {
                            var $select1 = $select.clone();
                            $select1.attr('id', index + 'user1');
                            //$select1.val(d.user1);
                            var $select2 = $select.clone();
                            $select2.attr('id', index + 'user2');
                            //$select2.val(d.user2);
                            var $select3 = $select.clone();
                            $select3.attr('id', index + 'user3');
                            //$select3.val(d.user3);

                            var $row = $('<tr>').attr('id', 'row' + index).attr('class', 'readonly').attr('data-dept', d.DepID);
                            $row.append($('<td>').text(index));
                            $row.append($('<td>').text(d.departmentName));
                            $row.append($('<td>').append($select1));
                            $row.append($('<td>').append($select2));
                            $row.append($('<td>').append($select3));
                            $row.append($('<td>').html(`
                            <button type="button" onclick="onEdit12(${index})" class="btn btn-sm btn-warning btn-edit">
                                 <span class="material-icons">edit</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                             </button>
                             <button type="button" onclick="onSave12(${index})" class="btn btn-sm btn-success  btn-save">
                                 <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                             </button>`));
                            $tbl.append($row);

                            $('#lst-data12 tr#row' + index + ' .selectpicker').selectpicker();
                            $('#lst-data12 tr#row' + index + '').find("#" + index + 'user1').val(d.user1);
                            $('#lst-data12 tr#row' + index + '').find("#" + index + 'user2').val(d.user2);
                            $('#lst-data12 tr#row' + index + '').find("#" + index + 'user3').val(d.user3);
                            $('#lst-data12 tr#row' + index + ' .selectpicker').selectpicker('refresh');
                            index++;
                        });


                        //$('#lst-data12 .selectpicker').selectpicker('refresh');
                    }

                    if (response.data2) {
                        response.data2.forEach(function (d) {
                            var $this = $('#temp2').clone();
                            var index = $('#wrapper2 .year-title').length + 1;
                            $this.attr('id', 'row' + index);
                            $this.find('.year-title').text(`2.${index} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132315") %>`);

                            $this.find('#rowId').val(d.id) || '0';
                            $this.find('#year1').val(d.year1);
                            $this.find('#year2').val(d.year2);

                            d.list.forEach(function (dd) {
                                $this.find('[data-typeid=' + dd.typeId + ']').val(dd.day);
                            });

                            //$this.find('#sick').val(d.sick);
                            //$this.find('#personal').val(d.personal);
                            //$this.find('#vacation').val(d.vacation);
                            //$this.find('#maternity').val(d.maternity);
                            //$this.find('#ordination').val(d.ordination);
                            //$this.find('#military').val(d.military);
                            //$this.find('#goverment').val(d.goverment);
                            //$this.find('#training').val(d.training);

                            $('#wrapper2').append($this);
                        });
                    }

                    //if (response.emp) {
                    //    var _html = "";
                    //    response.emp.forEach(function (d) {
                    //        _html += ` 
                    //        <div class="col-md-1"></div>
                    //        <span class="col-md-2 col-form-label text-left"><strong>คนที่</strong> ${d.Order} ${d.Name}</span>
                    //        <span class="col-md-5 col-form-label text-left"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></strong> ${d.Position}</span>
                    //        <div class="col-md-4"></div>`;
                    //    });
                    //    $('#emp-wrapper').html(_html);
                    //}

                    if (response.data41) {
                        var _html = "";
                        response.data41.forEach(function (d) {
                            _html += ` 
                              <div  class=" row row41">
                                  <div class="col-md-1"><input class="id d-none"  value="${d.id}" /><input class="type d-none"  value="1" /></div>
                                  <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                                  <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"  value="${d.no}" /></span>
                                  <span class="col-md-3 form-inline" style="padding-left: 85px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><input class="form-control time1 text-center" value="${(d.time1 || 0)}" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %></span>
                                  <span class="col-md-2">
                                    <button type="button" onclick="onRemove4(1,this)" class="btn btn-sm btn-danger btn-link">
                                       <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                     </button>
                                  </span>
                                  <div class="col-md-3"> 
                                     
                                  </div>
                              </div>`;
                        });

                        $('#wrapper41').html(_html);
                    }

                    if (response.data42) {
                        var _html = "";
                        response.data42.forEach(function (d) {
                            _html += ` 
                          <div  class=" row row42">
                              <div class="col-md-1"><input class="id d-none"  value="${d.id}" /><input class="type d-none"  value="2" /></div>
                              <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                              <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"  value="${d.no}" /></span>
                              <span class="col-md-3 form-inline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102215") %> <input class="form-control time1 text-center" value="${(d.time1 || 0)}" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                              <div class="col-md-5">
                                  <button type="button" onclick="onRemove4(2,this)" class="btn btn-sm btn-danger btn-link">
                                    <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                  </button>
                              </div>
                          </div>`;
                        });

                        $('#wrapper42').html(_html);
                    }

                    if (response.data43) {
                        var _html = "";
                        response.data43.forEach(function (d) {
                            _html += ` 
                            <div  class=" row row43">
                                <div class="col-md-1"><input class="id d-none"  value="${d.id}" /><input class="type d-none"  value="3" /></div>
                                <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                                <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"  value="${d.no}" /></span>
                                <span class="col-md-3 form-inline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102215") %> <input class="form-control time1 text-center" value="${(d.time1 || 0)}" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                                <div class="col-md-5">
                                    <button type="button" onclick="onRemove4(3,this)" class="btn btn-sm btn-danger btn-link">
                                      <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                    </button>
                                </div>
                            </div>`;
                        });

                        $('#wrapper43').html(_html);
                    }

                    if (response.data44) {
                        var _html = "";
                        response.data44.forEach(function (d) {
                            _html += ` 
                             <div  class=" row row44">
                                 <div class="col-md-1"><input class="id d-none"  value="${d.id}" /><input class="type d-none"  value="4" /></div>
                                 <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                                 <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"  value="${d.no}" /></span>
                                 <span class="col-md-3 form-inline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102215") %> <input class="form-control time1 text-center" value="${(d.time1 || 0)}" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                                 <div class="col-md-5">
                                     <button type="button" onclick="onRemove4(4,this)" class="btn btn-sm btn-danger btn-link">
                                       <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                     </button>
                                 </div>
                             </div>`;
                        });

                        $('#wrapper44').html(_html);
                    }

                    if (response.data45) {
                        response.data45.forEach(function (d) {
                            $('#lst-data45 tbody').append(`
                                <tr id='row${d.Id}'>
                                    <td class='text-center'>${$('#lst-data45 tbody tr').length + 1}</td>
                                    <td class='text-center'>${d.Job}</td>
                                    <td class='text-center'>${d.Name}</td>
                                    <td class='text-center'>
                                       <button type="button" onclick="onRemove45(${d.Id},this)" class="btn btn-sm btn-danger btn-link">
                                          <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                       </button>
                                    </td>
                                </tr>
                              `);
                        });
                    }
                }
                else {


                }
                $("body").mLoading('hide');
            });
        }

        function onAdd45() {
            var emp = $('#ddlNotiEmp').val();

            var selectedEmp = _empList.filter(function (obj) {
                return obj.Id == emp;
            });

            if (selectedEmp.length > 0) {
                var isAdded = $('#lst-data45 tbody tr#row' + selectedEmp[0].Id).length;

                if (isAdded == 0) {
                    $('#lst-data45 tbody').append(`
                       <tr id='row${selectedEmp[0].Id}'>
                           <td class='text-center'>${$('#lst-data45 tbody tr').length + 1}</td>
                           <td class='text-center'>${selectedEmp[0].Job}</td>
                           <td class='text-center'>${selectedEmp[0].Name}</td>
                           <td class='text-center'>
                              <button type="button" onclick="onRemove45(${selectedEmp[0].Id},this)" class="btn btn-sm btn-danger btn-link">
                                 <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                              </button>
                           </td>
                       </tr>
                     `);

                    $('#ddlNotiEmp').val('');
                    $('#ddlNotiEmp').selectpicker('refresh');

                    $('#new45').val($('#new45').val() + ',' + selectedEmp[0].Id);

                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132318") %>',
                        timer: 1500
                    });
                }
            }
        }

        function onRemove45(id, obj) {

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
                    var $this = $(obj);
                    var $parent = $this.parents('tr');

                    $('#delete45').val($('#delete45').val() + ',' + id);

                    $parent.remove();
                }
            });

        }

        function onValidate4() {
            var isValid = true;

            $('#wrapper41 .row,#wrapper42 .row,#wrapper43 .row').each(function (i) {
                var $this = $(this);
                var emptyInputs = $this.find('input.time1').filter(function () {
                    var _val = $(this).val();
                    return _val === '' || +_val < 0;
                });

                if (emptyInputs.length > 0) {
                    isValid = false;
                }

                if (!isValid)
                    return false;
            });

            return isValid;
        }

        function onSave4() {
            if (onValidate4()) {

                var data = [];
                $('#wrapper41 .row41,#wrapper42 .row42,#wrapper43 .row43,#wrapper44 .row44').each(function (i) {
                    var $this = $(this);

                    var d = {
                        NotiID: $this.find('.id').val() || '0',
                        NotiType: $this.find('.type').val(),
                        No: $this.find('.no').val(),
                        Time1: $this.find('.time1').val(),
                        Time2: $this.find('.time2').val()
                    };
                    data.push(d);
                });

                PageMethods.SaveData4(data, $('#delete4').val(), $('#new45').val(), $('#delete45').val(), function (response) {

                    if (response.text == 'success') {

                        Swal.fire({
                            type: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                            timer: 1500
                        });

                        $('#wrapper41').removeClass('--edit');
                        $('#wrapper42').removeClass('--edit');
                        $('#wrapper43').removeClass('--edit');
                        $('#wrapper44').removeClass('--edit');
                        $('#wrapper45').removeClass('--edit');
                        $('.btn-tool4').removeClass('--edit');
                        $("#btnEdit4").show();
                        $('#btnSave4').hide();

                    }
                    else {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                        });
                    }
                });
            }
            else {
                Swal.fire({
                    type: 'warning',
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105029") %>',
                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132316") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132317") %>',
                });
            }
        }

        function onEdit4(obj) {
            $('#wrapper41').addClass('--edit');
            $('#wrapper42').addClass('--edit');
            $('#wrapper43').addClass('--edit');
            $('#wrapper44').addClass('--edit');
            $('#wrapper45').addClass('--edit');
            $('.btn-tool4').addClass('--edit');
            $(obj).hide();
            $('#btnSave4').show();

        }

        function onNew41() {
            $('#wrapper41').append(` 
               <div  class=" row row41">
                   <div class="col-md-1"><input class="id d-none"  value="" /><input class="type d-none"  value="1" /></div>
                   <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                   <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"  value="${$("#wrapper41 .no").length + 1}" /></span>
                   <span class="col-md-3 form-inline"  style="padding-left: 85px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><input class="form-control time1 text-center" value="" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %></span>
                   <span class="col-md-2">
                    <button type="button" onclick="onRemove4(1,this)" class="btn btn-sm btn-danger btn-link">
                       <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                    </button>
                   </span>
                   <div class="col-md-3">
                      
                   </div>
               </div>`);
        }

        function onRemove4(type, obj) {
            var $row = $(obj).parents('.row4' + type);
            var $wrap = $(obj).parents('#wrapper4' + type);
            var id = $row.find('.id').val();
            if (id) {
                $('#delete4').val($('#delete4').val() + ',' + id);
            }
            $row.remove();

            //re order
            $wrap.find('.no').each(function (i) {
                $(this).val(i + 1);
            });
        }

        function onNew42() {
            $('#wrapper42').append(` 
                <div  class=" row row42">
                    <div class="col-md-1"><input class="id d-none"  value="" /><input class="type d-none"  value="2" /></div>
                    <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                    <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"   value="${$("#wrapper42 .no").length + 1}" /></span>
                    <span class="col-md-3 form-inline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102215") %> <input class="form-control time1 text-center" value="" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                    <div class="col-md-5">
                        <button type="button" onclick="onRemove4(2,this)" class="btn btn-sm btn-danger btn-link">
                        <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                        </button>
                    </div>
                </div>`);
        }

        function onNew43() {
            $('#wrapper43').append(` 
                    <div  class=" row row43">
                        <div class="col-md-1"><input class="id d-none"  value="" /><input class="type d-none"  value="3" /></div>
                        <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                        <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"   value="${$("#wrapper43 .no").length + 1}" /></span>
                        <span class="col-md-3 form-inline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102215") %> <input class="form-control time1 text-center" value="" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                        <div class="col-md-5">
                            <button type="button" onclick="onRemove4(3,this)" class="btn btn-sm btn-danger btn-link">
                                <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                            </button>
                        </div>
                    </div>`);
        }

        function onNew44() {
            $('#wrapper44').append(` 
            <div  class=" row row44">
                <div class="col-md-1"><input class="id d-none"  value="" /><input class="type d-none"  value="4" /></div>
                <span class="col-md-1 col-form-label form-inline text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102212") %> </span>
                <span class="col-md-2 form-inline"><input class="form-control no text-center readonly"   value="${$("#wrapper44 .no").length + 1}" /></span>
                <span class="col-md-3 form-inline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102215") %> <input class="form-control time1 text-center" value="" /> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                <div class="col-md-5">
                    <button type="button" onclick="onRemove4(4,this)" class="btn btn-sm btn-danger btn-link">
                        <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                    </button>
                </div>
            </div>`);
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102188") %>
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
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102189") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row d-none">
                            <asp:DropDownList ID="selectEmpList" runat="server"
                                CssClass="" data-width="100%" data-size="7" data-style="select-with-transition"
                                data-live-search="true" ClientIDMode="Static"
                                DataTextField="Text"
                                DataValueField="Value"
                                title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203071") %>">
                            </asp:DropDownList>
                        </div>

                        <div class="row">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102190") %></label>
                            <div class="col-md-2 "></div>
                            <div class="col-md-4"></div>
                        </div>
                        <div class=" row ">
                            <label class="col-md-1 col-form-label text-right " style="padding-right: 15px;">1.</label>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102191") %>
                            </label>
                            <div class="col-md-1 text-right">
                            </div>
                            <div class="col-md-1 text-left p-1">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102192") %>
                                <br />
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102193") %></span>
                            </label>
                            <div class="col-md-1 text-right">
                                <input type="number" id="text1" class="form-control text-center readonly" value="" />
                            </div>
                            <div class="col-md-1 text-left p-1">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                            </div>
                            <div class="col-md-4">
                                <button type="button" id="btnEdit11" onclick="onEdit11(this)" class="btn  btn-warning btn-sm">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                </button>
                                <button type="button" id="btnSave11" onclick="onSave11()" class="btn  btn-success btn-sm" style="display: none">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>
                            </div>
                        </div>
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102194") %>
                            </label>
                            <div class="col-md-1 text-right">
                            </div>
                            <div class="col-md-1 text-left p-1">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <table id="lst-data12" class=" table-hover dataTable" width="100%">
                                    <thead>
                                        <tr>
                                            <th width="5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                            <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102170") %></th>
                                            <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132326") %></th>
                                            <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>ู้อนมัติ 2</th>
                                            <th width="20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>ู้อนมัติ 3</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <hr />
                        <div class=" row mt-2">
                            <label class="col-md-1 col-form-label text-right " style="padding-top: 25px; padding-right: 15px;">2.</label>
                            <div class="col-md-5  col-form-label text-left">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102197") %></label>
                                <input type="text" id="delete2" class="d-none" value="" />
                            </div>
                            <div class="col-md-6 mt-2 text-right">
                                <label class="el-switch el-switch-md ">
                                    <input id="btnSwitch2" type="checkbox" onclick="onSwitch2(this)" class="switch-button" hidden>
                                    <span class="el-switch-style"></span>
                                </label>
                                <br />
                                <%--<button type="button" id="btnOpen2" onclick="onOpen2()" class="btn btn-sm  btn-success">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>
                                </button>
                                <button type="button" id="btnClose2" onclick="onClose2()" class="btn btn-sm btn-danger" style="display: none">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>
                                </button>--%>
                                <button type="button" id="btnEdit2" onclick="onEdit2(this)" class="btn btn-sm btn-warning" style="display: none">
                                    <span class="material-icons">edit</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                </button>
                            </div>
                        </div>
                        <div class="row ">
                            <div id="temp2" class=" row-item2 row mt-2 ">
                                <div class="col-md-1"></div>
                                <label class="year-title col-md-4 col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102200") %>
                                </label>
                                <div class="col-md-1">
                                    <input type="text" id="rowId" class="d-none" value="" />
                                    <input type="number" id="year1" class="form-control text-center input-y1 readonly" value="" />
                                </div>
                                <div class="col-md-2  p-1">
                                    <div class=" text-center col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102201") %></div>
                                    <%-- <div class="row">
                                        <div class="col-md-12 text-left"></div>
                                        <div class="col-md-6 text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102201") %></div>
                                    </div>--%>
                                </div>
                                <div class="col-md-1">
                                    <input type="number" id="year2" class="form-control text-center input-y2" onkeyup="onEditYear2(this)" value="" />
                                </div>
                                <div class="col-md-1 p-1">
                                    <div class=" col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %></div>
                                </div>
                                <div class="col-md-2 text-right">
                                    <button type="button" onclick="onRemove2(this)" class="btn btn-sm btn-danger btn-link">
                                        <span class="material-icons">delete</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                    </button>
                                </div>

                                <div class="col-md-1"></div>
                                <label class="col-md-5  col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102203") %>  : 
                                </label>
                                <div class="col-md-6 text-left"></div>

                                <div class="col-md-1"></div>
                                <div class="col-md-11">
                                    <div class="row">
                                        <% var c = 1;
                                            foreach (var t in TypeList)
                                            { %>
                                        <div class="col-md-2 p-1"><%=c++ %>. <%=t.TypeName %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" data-typeid="<%=t.TypeID %>" class="form-control text-center dayinput" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <% } %>
                                        <%--   <div class="col-md-2 p-1">1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="sick" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <div class="col-md-2 p-1">2.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132328") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="personal" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <div class="col-md-2 p-1">3.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132329") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="vacation" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>

                                        <div class="col-md-2 p-1">4.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01844") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="maternity" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <div class="col-md-2 p-1">5.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132330") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="ordination" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <div class="col-md-2 p-1">6.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132319") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="military" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>

                                        <div class="col-md-2 p-1">7.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132320") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="goverment" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <div class="col-md-2 p-1">8.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132331") %> :</div>
                                        <div class="col-md-1 ">
                                            <input type="number" id="training" class="form-control text-center" value="" />
                                        </div>
                                        <div class="col-md-1 p-1 "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132327") %></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-1"></div>--%>
                                    </div>
                                </div>
                            </div>
                            <div id="wrapper2" class="col-12">
                            </div>
                        </div>
                        <div class="row mt-4" id="btn-tool2" style="display: none">
                            <div class="col-md-12 text-center">
                                <button type="button" onclick="onAddNew2()" class="btn btn-sm  btn-success">
                                    <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106153") %>
                                </button>
                                <button type="button" onclick="onSave2()" class="btn btn-sm btn-success">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-md-1"></div>
                            <div class="col-md-11 col-form-label text-left">
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102205") %></span>
                            </div>
                        </div>
                        <hr />

                        <div class=" row ">
                            <label class="col-md-1 col-form-label text-right " style="padding-right: 15px;">3.</label>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102206") %>
                            </label>
                            <div class="col-md-1 text-right">
                            </div>
                            <div class="col-md-1 text-left p-1">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102207") %>                               
                            </label>
                            <div id="wrapper3" class="col-md-2 text-right readonly">
                                <input type="text" id="text3" class="form-control text-center datepicker" value="" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                            <div class="col-md-4">
                                <button type="button" id="btnEdit3" onclick="onEdit3(this)" class="btn  btn-warning btn-sm">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                </button>
                                <button type="button" id="btnSave3" onclick="onSave3()" class="btn  btn-success btn-sm" style="display: none">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11 col-form-label text-left">
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102208") %></span>
                            </div>
                        </div>
                        <hr />

                        <div class=" row ">
                            <label class="col-md-1 col-form-label text-right " style="padding-right: 15px;">4.</label>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102209") %>
                            </label>
                            <div class="col-md-1 text-right">
                                <input class="d-none" id="delete4" value="" />
                            </div>
                            <div class="col-md-1 text-left p-1">
                            </div>
                            <div class="col-md-4  text-right">
                                <button type="button" id="btnEdit4" onclick="onEdit4(this)" class="btn  btn-warning btn-sm">
                                    <span class="material-icons">settings</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>
                                </button>
                                <button type="button" id="btnSave4" onclick="onSave4()" class="btn  btn-success btn-sm" style="display: none">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>
                            </div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102210") %>                             
                            </label>
                            <div class="col-md-2 text-right">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div id="wrapper41">
                        </div>
                        <div class=" row btn-tool4">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <button type="button" onclick="onNew41()" class="btn btn-sm btn-success ">
                                    <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132321") %>
                                </button>
                            </div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102214") %>
                            </label>
                            <div class="col-md-2 text-right">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div id="wrapper42">
                        </div>
                        <div class=" row btn-tool4 ">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <button type="button" onclick="onNew42()" class="btn btn-sm btn-success ">
                                    <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132321") %>
                                </button>
                            </div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102217") %>
                            </label>
                            <div class="col-md-2 text-right">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div id="wrapper43">
                        </div>
                        <div class=" row btn-tool4">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <button type="button" onclick="onNew43()" class="btn btn-sm btn-success ">
                                    <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132321") %>
                                </button>
                            </div>
                        </div>

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-5  col-form-label text-left">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102218") %>
                            </label>
                            <div class="col-md-2 text-right">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                        <div id="wrapper44">
                        </div>
                        <div class=" row btn-tool4">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <button type="button" onclick="onNew44()" class="btn btn-sm btn-success ">
                                    <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132321") %>
                                </button>
                            </div>
                        </div>
                        <div id="wrapper45">
                            <div class=" row ">
                                <div class="col-md-1"></div>
                                <label class="col-md-5  col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102219") %>
                                    <input type="hidden" id="delete45" value="" />
                                    <input type="hidden" id="new45" value="" />
                                </label>
                                <div class="col-md-2 text-right">
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                            <div class=" row  btn-tool4">
                                <div class="col-md-1"></div>
                                <label class="col-md-2  col-form-label text-left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlNotiEmp" runat="server"
                                        CssClass="selectpicker" data-width="100%" data-size="7" data-style="select-with-transition "
                                        data-live-search="true" ClientIDMode="Static"
                                        DataTextField="Text"
                                        DataValueField="Value"
                                        title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203071") %>">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <button type="button" onclick="onAdd45()" class="btn btn-sm btn-info ">
                                        <span class="material-icons">add</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132322") %>
                                    </button>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class=" row ">
                                <div class="col-md-1"></div>
                                <div class="col-md-11 p-0">
                                    <table id="lst-data45" class=" table-hover dataTable" width="100%">
                                        <thead>
                                            <tr>
                                                <th class='text-center' width="10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                <th class='text-center' width="30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></th>
                                                <th class='text-center' width="40%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>
                                                <th class='text-center'></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <%-- <div id="emp-wrapper" class="row">
                        </div>--%>

                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11 col-form-label text-left">
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %> :</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11 col-form-label text-left">
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102221") %></span>
                                <br />
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102222") %><br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102223") %><br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102224") %>
                                </span>
                                <br />
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102225") %></span>
                            </div>


                        </div>
                        <%-- <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-11 col-form-label text-left">
                                <span class="text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132323") %></span>
                            </div>
                        </div>--%>
                        <hr />
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
                            <select id="leaveType" class="form-control">
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
