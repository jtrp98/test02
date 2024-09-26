<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="ProfileImage.aspx.cs" Inherits="FingerprintPayment.StudentInfo.ProfileImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
     <link rel="stylesheet" href="../assets/plugins/datatables/jquery.dataTables.min.css" />
    <link href="../Assets/plugins/datatables/rowGroup.dataTables.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/datatables/material-components-web.min.css" />
    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-9">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101288") %>:</label>
                                    <div class="col-md-8">
                                        <div class="form-group ">
                                           
                                            <select id="ddlImportStudentPictureMode" 
                                                class="selectpicker" data-width="700px" data-style="select-with-transition">
                                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101289") %></option>
                                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101290") %></option>
                                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101291") %></option>
                                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101292") %></option>
                                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101293") %></option>
                                            </select>
                                            <input type="hidden" id="schoolid" clientidmode="Static" runat ="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-9">
                                 <form enctype="multipart/form-data">
                                <div class="row">
                                    <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %></label>
                                    <%--<label id="lblStudentName" class="col-md-8 col-form-label" style="text-align: left; padding-left: 5px" />--%>
                                    <div class="col-md-8 control-input">
                                        <div class="form-group">
                                            <div class="input-group input-file" name="Fichier1">
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default btn-choose" type="button">Choose</button>
                                                </span>
                                                <input type="text" class="form-control col-lg-12" style="height: 47.6px" placeholder='Choose a file...' />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-warning btn-reset" type="button">Reset</button>
                                                </span>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                     </form>
                            </div>
                            
                            <div class="col-md-12 text-center">
                                <br />
                                <%--<button type="button" onclick="onSearch()" id="btnSearch" disabled class="btn btn-fill btn-info">
                                    <span class="material-icons">search</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">history</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101028") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row" id="rowProfileImage">
                            <div class="col-md-12">
                             <%--   Main Table Content--%>
                               <table id="CroppedImage" class="display" cellspacing="0" width="100%;height:200px">
                                <thead>
                                    <tr>
                                        <th style="text-align: left; min-width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101294") %>
                                        </th>
                                        <th style="text-align: left; min-width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101227") %></th>
                                        <th style="text-align: left; min-width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101296") %></th>
                                        <th style="text-align: left; min-width: 30%"></th>
                                        <th style="text-align: left; min-width: 30%">File Name</th>
                                        <th style="text-align: left; min-width: 30%">File Path</th>
                                        <th style="text-align: left; min-width: 30%">Local Server Path</th>
                                    </tr>
                                </thead>
                            </table>
                            </div>
                            <div class="col-md-12" style="text-align:center">
                               <button id="btnSaveProfileImage" class="btn btn-success">
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.js"></script>
    <script type='text/javascript' src="../assets/plugins/bootstrap-datetimepicker/moment-with-locales.js"></script>
    <script type='text/javascript' src="../assets/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.th.min.js"></script>
    <script type='text/javascript' src="../Content/Material/assets/js/plugins/bootstrap-selectpicker.js"></script>
    <script src="../Assets/plugins/datatables/dataTables.rowsGroup.js"></script>
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script type="text/javascript">
        var table;
        //var hostURL = "https://localhost:44396";
        var hostURL = "https://api-grade.schoolbright.co";
        var croppedImageResponse;
        $(window).on('load', $("body").mLoading());
       
        $(document).ready(function () {
            $('#rowProfileImage').hide();
            table = $('#CroppedImage').DataTable();
            $(".selectpicker").selectpicker();
            $(".input-file").before(
                function () {
                    if (!$(this).prev().hasClass('input-ghost')) {
                        var element = $("<input type='file' id='ImportFile' accept='.jpg,.jpeg,.png' multiple class='input-ghost' style='visibility:hidden; height:0'>");
                        element.attr("name", $(this).attr("name"));
                        element.change(function () {
                            element.next(element).find('input').val((element.val()).split('\\').pop());
                        });
                        $(this).find("button.btn-choose").click(function () {
                            element.click();
                        });
                        $(this).find("button.btn-reset").click(function () {
                            element.val(null);
                            $(this).parents(".input-file").find('input').val('');
                            $("#InValidData").html("");
                            $('#rowProfileImage').hide();
                        });
                        $(this).find('input').css("cursor", "pointer");
                        $(this).find('input').mousedown(function () {
                            $(this).parents('.input-file').prev().click();
                            return false;
                        });
                        return element;
                    }
                }
            );

            $('#ImportFile:file').change(function () {
                $("body").mLoading();
                /* $("body").mLoading();*/
                console.log("File Changed");
                // Checking whether FormData is available in browser
                if (window.FormData !== undefined) {

                    var files = this.files;

                    // Create FormData object
                    var fileData = new FormData();

                   
                    var isImageFile = false;
                    var imageFileCount = 0;
                    // Looping over all files and add it to FormData object
                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                        console.log(files[i].name.toLowerCase());
                        if (files[i].name.toLowerCase().indexOf(".jpg") != -1 || files[i].name.toLowerCase().indexOf(".jpeg") != -1) {
                            isImageFile = true;
                            imageFileCount++;
                        }
                        else if (files[i].name.toLowerCase().indexOf(".png") != -1) {
                            isImageFile = true;
                            imageFileCount++;
                        }

                    }
                    if (imageFileCount != files.length)
                        isImageFile = false;

                    console.log("Image File" + isImageFile);
                    if (isImageFile) {
                        $.ajax({
                            url: hostURL +'/api/Profile/ImportStudentProfile?schoolId=' + $("#schoolid").val() + '&mode=' + $("#ddlImportStudentPictureMode").val(),
                            type: "POST",
                            contentType: false, // Not to set any content header
                            processData: false, // Not to process data
                            crossDomain: true,
                            data: fileData,
                            success: function (result) {
                                ShowCroppedImages(result);
                            },
                            error: function (err) {
                                console.log(err.statusText);
                            },

                        });
                    }
                    else {
                        console.log("Wrong File");
                       
                        return false;
                    }
                } else {
                    console.log("FormData is not supported.");
                }
            });

            $("body").mLoading('hide');
        });

        function ShowCroppedImages(response) {
            $("#btnSaveProfileImage").show();
            croppedImageResponse = response;
            $('#rowProfileImage').show();
            if (table != undefined) {
                table.destroy();
                table.clear().draw();
            }
            console.log(response);
            if (response != null)
                addKeyValue(response, 'delete', 'delete');
            table = $('#CroppedImage').DataTable({
                bFilter: false,
                bSort: false,
                bPaginate: false,
                //scrollY: '500px',
                //scrollCollapse: true,
                "sDom": 'Rlfrtlip',

                'data': response,
                'columns': [
                    { 'data': 'rootURL' },
                    { 'data': 'userName' },
                    { 'data': 'isValidImage' },
                    { 'data': 'delete' },
                    { 'data': 'fileName', 'visible': false },
                    { 'data': 'fileURL', 'visible': false },
                    { 'data': 'localServerPath', 'visible': false },

                ],
                'columnDefs': [
                    {
                        'targets': 0, //Image
                        'searchable': false,
                        'orderable': false,
                        'render': function (data, type, row, meta) {
                            return '<img width="150" height="150" src= "' + data + '" />';
                        }
                    },
                    {
                        'targets': 2, //match/not match
                        'searchable': false,
                        'orderable': false,
                        'render': function (data, type, row, meta) {

                            return (data) ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101297") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101298") %>";

                        }
                    },
                    {
                        'targets': 3, //Delete Image
                        'searchable': false,
                        'orderable': false,
                        'render': function (data, type, row, meta) {
                            
                            return ' <i class="material-icons" style="cursor:pointer;" onclick="DeleteProfile(' + meta.row + ',\'' + row["huaweiObjectKey"] + '\'' + ',\'' + row["localServerPath"] + '\'' + ',\'' + row["fileURL"] + '\')">delete</i>';

                        }
                    },
                   ], 
                fnInitComplete: function () {
                  
                    $("body").mLoading('hide');
                    console.log("fnInitComplete");
                   
                }

            });
        }
        function DeleteProfile(rowIndex, huaweiObjectKey, localServerPath, fileURL) {
            $("body").mLoading();
            console.log(croppedImageResponse[rowIndex].fileURL);
            console.log(croppedImageResponse[rowIndex].localServerPath);
            table.row(rowIndex).remove().draw(true);
            table.rows().invalidate('data')
                .draw(false);
            var profileImage = { LocalServerPath: croppedImageResponse[rowIndex].localServerPath, FileURL: croppedImageResponse[rowIndex].fileURL };
            $.ajax({
                url: hostURL + '/api/Profile/DeleteStudentProfile',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(profileImage),
                success: function (result) {
                    $("body").mLoading('hide');
                   /* ShowCroppedImages(result);*/
                    if (!table.data().count()) {
                        $("#btnSaveProfileImage").hide();
                    }
                },
                error: function (err) {
                    console.log(err.statusText);
                },

            });
        }
        function addKeyValue(obj, key, data) {
            obj[key] = data;
        }
        $("#btnSaveProfileImage").click(function () {
            $("#btnSaveProfileImage").hide();
            console.log("btnSaveProfileImage");
            $("body").mLoading();
            var profileImage = [];
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();
                profileImage.push(data);
            });
            console.log(profileImage);
            $("body").mLoading();
           
            if (profileImage.length > 0) {
                $.ajax({
                    url: hostURL + '/api/Profile/SaveStudentProfile',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(profileImage),
                    success: function (result) {
                        $.ajax({
                            url: '/api/common/StudentProfileUpdateToMemory',
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify(profileImage),
                            success: function (response) {
                                console.log(result);
                                table.clear().draw();
                                $("body").mLoading('hide');
                                Swal.fire({
                                    icon: 'success',
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                                    //text: 'Something went wrong!',                      
                                });

                            },
                            error: function (err) {
                                console.log(err.statusText);
                            },
                        });
                    },
                    error: function (err) {
                        console.log(err.statusText);
                    },
                });
            }
            else {

            }
            return false;
        });
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
