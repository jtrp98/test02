<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="schoolprofile-edit.aspx.cs" Inherits="FingerprintPayment.schoolprofile_edit" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <%--fontawesome icon--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />


    <style>
        /* USER PROFILE PAGE */

        .card {
            margin-top: 20px;
            padding: 30px;
            background-color: rgba(214, 224, 226, 0.2);
            -webkit-border-top-left-radius: 5px;
            -moz-border-top-left-radius: 5px;
            border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-top-right-radius: 5px;
            border-top-right-radius: 5px;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .select2-selection__rendered {
            line-height: 41px !important;
        }

        .select2-container .select2-selection--single {
            height: 41px !important;
        }

        .select2-selection__arrow {
            height: 41px !important;
        }

            .select2-selection__arrow b {
                border-color: black transparent transparent transparent !important;
            }

        [class^='select2'] {
            border-radius: 1px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
        }

        .mar10 {
            margin-bottom: 10px;
        }

        .mb-30 {
            margin-bottom: 30px;
        }

        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .card.hovercard {
            position: relative;
            padding-top: 0;
            overflow: hidden;
            text-align: center;
            background-color: #fff;
            background-color: rgba(255, 255, 255, 1);
        }

            .card.hovercard .card-background {
                height: 130px;
            }

        .card-background img {
            -webkit-filter: blur(25px);
            -moz-filter: blur(25px);
            -o-filter: blur(25px);
            -ms-filter: blur(25px);
            filter: blur(25px);
            margin-left: -100px;
            margin-top: -200px;
            min-width: 130%;
        }

        .card.hovercard .useravatar {
            position: absolute;
            top: 15px;
            left: 0;
            right: 0;
        }

            .card.hovercard .useravatar img {
                width: 100px;
                height: 100px;
                max-width: 100px;
                max-height: 100px;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
                border-radius: 50%;
                border: 5px solid rgba(255, 255, 255, 0.5);
            }

        .card.hovercard .card-info {
            position: absolute;
            bottom: 14px;
            left: 0;
            right: 0;
        }

            .card.hovercard .card-info .card-title {
                padding: 0 5px;
                font-size: 20px;
                line-height: 1;
                color: #262626;
                background-color: rgba(255, 255, 255, 0.1);
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

        .card.hovercard .card-info {
            overflow: hidden;
            font-size: 12px;
            line-height: 20px;
            color: #737373;
            text-overflow: ellipsis;
        }

        .card.hovercard .bottom {
            padding: 0 20px;
            margin-bottom: 17px;
        }

        .btn-pref .btn {
            -webkit-border-radius: 0 !important;
        }


        body {
            background: #EAEAEA;
        }

        .user-details {
            position: relative;
            padding: 0;
        }

            .user-details .user-image {
                position: relative;
                z-index: 1;
                width: 100%;
                text-align: center;
            }

        .user-image img {
            clear: both;
            margin: auto;
            position: relative;
        }

        .user-details .user-info-block {
            width: 100%;
            position: absolute;
            top: 55px;
            background: rgb(255, 255, 255);
            z-index: 0;
            padding-top: 35px;
        }

        .user-info-block .user-heading {
            width: 100%;
            text-align: center;
            margin: 10px 0 0;
        }

        .user-info-block .navigation {
            float: left;
            width: 100%;
            margin: 0;
            padding: 0;
            list-style: none;
            border-bottom: 1px solid #428BCA;
            border-top: 1px solid #428BCA;
        }

        .navigation li {
            float: left;
            margin: 0;
            padding: 0;
        }

            .navigation li a {
                padding: 20px 30px;
                float: left;
            }

            .navigation li.active a {
                background: #428BCA;
                color: #fff;
            }

        .user-info-block .user-body {
            float: left;
            padding: 5%;
            width: 90%;
        }

        .user-body .tab-content > div {
            float: left;
            width: 100%;
        }

        .user-body .tab-content h4 {
            width: 100%;
            margin: 10px 0;
            color: #333;
        }

        .hid {
            visibility: hidden;
        }

        .upload2 {
            display: inline-block;
            font-size: 20px;
            padding: 4px;
        }

        .contentBox {
            margin: 0 auto;
            width: 120%;
        }

        .width50 {
            font-size: 80%;
            width: 100%;
        }

        .width50right {
            font-size: 80%;
            float: right;
            width: 100%;
        }

        .oneline {
            white-space: nowrap;
        }

        .circle-cropper {
            background-repeat: no-repeat;
            background-position: 50%;
            border-radius: 50%;
            width: 100px;
            height: 100px;
        }

        html, body {
            width: 100%;
            margin: 0px;
            padding: 0px;
            overflow-x: hidden;
        }

        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }

        .contentBox .column30 {
            float: left;
            margin: 0;
            width: 45%;
        }

        .contentBox .column20 {
            float: left;
            margin: 0;
            width: 25%;
        }

        .contentBox .column60 {
            float: left;
            margin: 0;
            width: 65%;
        }

        #map {
            width: 100%;
            height: 400px;
        }

        .controls {
            margin-top: 10px;
            border: 1px solid transparent;
            border-radius: 2px 0 0 2px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            height: 32px;
            outline: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        }

        #searchInput {
            background-color: #fff;
            font-family: Roboto;
            font-size: 15px;
            font-weight: 300;
            margin-left: 12px;
            padding: 0 11px 0 13px;
            text-overflow: ellipsis;
            width: 80%;
        }

            #searchInput:focus {
                border-color: #4d90fe;
            }

        #block_container {
            text-align: center;
        }

        img.center {
            display: block;
            margin: 0 auto;
        }

        #lat, #lon, #lat2 {
            display: inline;
        }

        .select2-results__option {
            font-size: 24px
        }
    </style>

    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#schoolpicture').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#filePhoto").change(function () {
            readURL(this);
        });
        $(document).ready(function () {
            $(".btn-pref .btn").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                // $(".tab").addClass("active"); // instead of this do the below 
                $(this).removeClass("btn-default").addClass("btn-primary");
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            $('.js-example-basic-multiple2').select2({
                allowClear: true,
                placeholder: ''
            });
       
            $('.js-example-basic-multiple3').select2({
                allowClear: true,
                placeholder: ''
            });
      
            $('.js-example-basic-multiple4').select2({
                allowClear: true,
                placeholder: ''
            });
      
            $('.js-example-basic-multiple5').select2({
                allowClear: true,
                placeholder: ''
            });
        
            $('.js-example-basic-multiple6').select2({
                allowClear: true,
                placeholder: ''
            });
       
            $('.js-example-basic-multiple7').select2({
                allowClear: true,
                placeholder: ''
            });
       
            $('.js-example-basic-multiple8').select2({
                allowClear: true,
                placeholder: ''
            });

            $('#Ddlprovince').on('change', function () {
                var provId = $('#Ddlprovince').val();
                var $aumper = $('#ddlAumpher');
                $.get("/App_Logic/dataJSONArray.ashx?mode=getaumper&sid=" + provId, "", function (result) {
                    $aumper.empty();
                    $.each(result, function (index) {
                        $aumper.append($("<option></option>")
                            .attr("value", result[index].AMPHUR_ID)
                            .text(result[index].AMPHUR_NAME));
                    });
                });
            });

            $('#ddlAumpher').on('change', function () {
                var aumpId = $('#ddlAumpher').val();
                $('#schAumpher').val($('#ddlAumpher :selected').text());
                var $tumbon = $('#ddlTumbon');
                $.get("/App_Logic/dataJSONArray.ashx?mode=gettumbon&sid=" + aumpId, "", function (result) {
                    $tumbon.empty();
                    $.each(result, function (index) {
                        $tumbon.append($("<option></option>")
                            .attr("value", result[index].DISTRICT_ID)
                            .text(result[index].DISTRICT_NAME));
                    });
                })

            });

            $('#ddlTumbon').on('change', function () {              
                $('#schTumbon').val($('#ddlTumbon :selected').text());
            });
        });

        $(document).ready(function () {
            var configid = document.getElementsByClassName("configid");

            $('#ctl00_MainContent_ddlschoolHead').val(configid[0].value).trigger('change');
            $('#ctl00_MainContent_ddlRegis').val(configid[1].value).trigger('change');
            $('#ctl00_MainContent_ddlAcademic').val(configid[2].value).trigger('change');
            $('#ctl00_MainContent_ddlSubAcademic').val(configid[3].value).trigger('change');
            $('#ctl00_MainContent_ddlAcc').val(configid[4].value).trigger('change');
            $('#ctl00_MainContent_ddlStd').val(configid[5].value).trigger('change');
            $('#ctl00_MainContent_ddladmin').val(configid[6].value).trigger('change');

            $('#ctl00_MainContent_dropdownPersonnel').val(configid[7].value).trigger('change');
            $('#ctl00_MainContent_dropdownGM').val(configid[8].value).trigger('change');
        });

        function comboclick() {
            var data = $('.js-example-basic-multiple2').select2('data');
            data.prop('selectedIndex', 1).change();
            $('#fieldId').select2("val", $('#fieldId option:eq(1)').val());
        }

        function readURL2(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#schoolHeadPicture').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        function changeddl() {
            var configid2 = document.getElementsByClassName("configid2");
            configid2[0].value = $('#ctl00_MainContent_ddlschoolHead').val();
            configid2[1].value = $('#ctl00_MainContent_ddlRegis').val();
            configid2[2].value = $('#ctl00_MainContent_ddlAcademic').val();
            configid2[3].value = $('#ctl00_MainContent_ddlSubAcademic').val();
            configid2[4].value = $('#ctl00_MainContent_ddlAcc').val();
            configid2[5].value = $('#ctl00_MainContent_ddlStd').val();
            configid2[6].value = $('#ctl00_MainContent_ddladmin').val();

            configid2[7].value = $('#ctl00_MainContent_dropdownPersonnel').val();
            configid2[8].value = $('#ctl00_MainContent_dropdownGM').val();
        }

        $("#filePhoto").change(function () {
            readURL(this);
        });
        $(document).ready(function () {
            $(".btn-pref .btn").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                // $(".tab").addClass("active"); // instead of this do the below 
                $(this).removeClass("btn-default").addClass("btn-primary");
            });
        });
    </script>

    <script>
        function readURL3(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#serverAdminPicture').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#filePhoto").change(function () {
            readURL(this);
        });
        $(document).ready(function () {
            $(".btn-pref .btn").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                // $(".tab").addClass("active"); // instead of this do the below 
                $(this).removeClass("btn-default").addClass("btn-primary");
            });
        });
    </script>

    <script>
        function readURL4(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#schoolcover').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#filePhoto").change(function () {
            readURL2(this);
        });
        $(document).ready(function () {
            $(".btn-pref .btn").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                // $(".tab").addClass("active"); // instead of this do the below 
                $(this).removeClass("btn-default").addClass("btn-primary");
            });
        });
    </script>

    <script>

        //$(document).ready(function () {
        //    var imageLoader = document.getElementById('ctl00_MainContent_SignSchoolHead');
        //    imageLoader.addEventListener('change', handleImage, false);
        //});


        //function handleImage(e) {
        //    var reader = new FileReader();
        //    reader.onload = function (event) {

        //        $('.uploader img').attr('src', event.target.result);
        //    }
        //    reader.readAsDataURL(e.target.files[0]);
        //}

        function funSignatureImgHandle(eleId) {

            var reader = new FileReader();
            reader.onload = function (event) {
                //console.log(event.target.result);
                document.getElementById(eleId + 'Img').src = event.target.result;
                $('button[id$=' + eleId + 'Btn]').show();
            }
            reader.readAsDataURL(event.target.files[0]);
        }

        function onRemoveSignature(id) {
            $('input[id$=' + id + ']').val('');
            $('input[id$=' + id + 'Status]').val('DEL');
            $('img[id$=' + id + 'Img]').attr('src', '../images/select-img-bg.jpg');
            $('button[id$=' + id + 'Btn]').hide();
        }


    </script>

    <style>
        .uploader {
            position: relative;
            overflow: hidden;
            /*width: 345px;*/
            height: 100px;
            background: #f3f3f3;
            border: 2px dashed #e8e8e8;
        }

        .signature_filePhoto {
            position: absolute;
            /*width: 345px;*/
            height: 100px;
            top: -1px;
            left: 0;
            z-index: 2;
            opacity: 0;
            cursor: pointer;
        }

        .uploader img {
            position: absolute;
            width: -webkit-fill-available;
            height: 100px;
            top: -1px;
            left: -1px;
            z-index: 1;
            border: none;
        }
    </style>






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class=" full-card box-content" style="margin-top: 10px; padding-top: 10px;">

        <div class="row">
            <div class="contentBox">
                <h1 class="page-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701029") %></h1>
                <div class="column30">
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="text-center">
                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801042") %></h3>
                            <img id="schoolpicture" runat="server" alt="" class="avatar img-responsive center" />

                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="upload2" onchange="readURL(this)" />

                        </div>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="text-center">
                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801004") %></h3>
                            <h4>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801005") %>)</h4>
                            <img id="schoolcover" runat="server" alt="" class="avatar img-responsive" />
                            <asp:FileUpload ID="FileUpload4" runat="server" CssClass="upload2" onchange="readURL4(this)" />
                        </div>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="text-center">
                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801048") %></h3>
                        </div>
                        <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA32MXWA3eBA7KeowjyGrgFFpHVyqXV0hw&libraries=places&callback=initMap"
                            type="text/javascript"></script>
                        <script>
                            function initMap() {
                                var latlng = new google.maps.LatLng(<%= _lat %>, <%= _lng %>);
                                map = new google.maps.Map(document.getElementById('map'), {
                                    center: latlng,
                                    zoom: 16,
                                    mapTypeControl: false,
                                    streetViewControl: false
                                });
                                var input = document.getElementById('searchInput');
                                map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

                                var autocomplete = new google.maps.places.Autocomplete(input);
                                autocomplete.bindTo('bounds', map);

                                var infowindow = new google.maps.InfoWindow();
                                var marker = new google.maps.Marker({
                                    map: map,
                                    position: latlng
                                });

                                autocomplete.addListener('place_changed', function () {
                                    infowindow.close();
                                    marker.setVisible(false);
                                    var place = autocomplete.getPlace();
                                    if (!place.geometry) {
                                        window.alert("Autocomplete's returned place contains no geometry");
                                        return;
                                    }

                                    // If the place has a geometry, then present it on a map.
                                    if (place.geometry.viewport) {
                                        map.fitBounds(place.geometry.viewport);
                                    } else {
                                        map.setCenter(place.geometry.location);
                                        map.setZoom(17);
                                    }
                                    marker.setIcon(({
                                        url: place.icon,
                                        size: new google.maps.Size(71, 71),
                                        origin: new google.maps.Point(0, 0),
                                        anchor: new google.maps.Point(17, 34),
                                        scaledSize: new google.maps.Size(35, 35)
                                    }));
                                    marker.setPosition(place.geometry.location);
                                    marker.setVisible(true);

                                    var address = '';
                                    if (place.address_components) {
                                        address = [
                                            (place.address_components[0] && place.address_components[0].short_name || ''),
                                            (place.address_components[1] && place.address_components[1].short_name || ''),
                                            (place.address_components[2] && place.address_components[2].short_name || '')
                                        ].join(' ');
                                    }

                                    infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
                                    infowindow.open(map, marker);

                                    //Location details

                                    document.getElementById('lat').innerHTML = place.geometry.location.lat();
                                    document.getElementById('lon').innerHTML = place.geometry.location.lng();
                                    document.getElementById("<%=SendA.ClientID%>").value = place.geometry.location.lat();
                                    document.getElementById("<%=SendB.ClientID%>").value = place.geometry.location.lng();
                                    document.getElementById("<%=lat2.ClientID%>").value = place.geometry.location.lat();
                                    document.getElementById("<%=lon2.ClientID%>").value = place.geometry.location.lng();

                                });
                            }
                        </script>
                        <input id="searchInput" class="controls" type="text" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801049") %>">
                        <div id="map"></div>



                        <ul id="geoData">
                            <div id="block_container">

                                <div id="lat" class="hidden"></div>
                                <div id="lon" class="hidden"></div>

                            </div>



                        </ul>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="col-md-6 col-sm-6 col-xs-6">
                            <asp:TextBox runat="server" ID="lat2" Value="" CssClass="width50right form-control" Enabled="False" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103057") %>" />
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-6">
                            <asp:TextBox runat="server" ID="lon2" Value="" CssClass="width50 form-control" Enabled="False" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103058") %>" />
                        </div>





                    </div>
                    <asp:HiddenField runat="server" ID="SendA" Value="" />

                    <asp:HiddenField runat="server" ID="SendB" Value="" />

                    <!--70-->
                </div>
                <div class="column30">
                    <div class="form-group row student " style="margin-left: -15%; padding-left: -15%;">
                        <div class="col-md-12 col-sm-12" style="margin-left: -15%; padding-left: -15%;">

                            <div class="btn-pref btn-group btn-group-justified btn-group-lg" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <button type="button" id="stars" class="btn btn-primary" href="#tab1" data-toggle="tab">
                                        <span class="glyphicon glyphicon-home" aria-hidden="true"></span>
                                        <div class="hidden-xs"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801010") %></div>
                                    </button>
                                </div>
                                <div class="btn-group" role="group">
                                    <button type="button" id="favorites" class="btn btn-default" href="#tab2" data-toggle="tab">
                                        <span class="glyphicon glyphicon-flag" aria-hidden="true"></span>
                                        <div class="hidden-xs"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801026") %></div>
                                    </button>
                                </div>
                                <div class="btn-group" role="group">
                                    <button type="button" id="schoolHistory" class="btn btn-default" href="#schoolHistorytab" data-toggle="tab">
                                        <span class="fas fa-school" aria-hidden="true"></span>
                                        <div class="hidden-xs"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801029") %></div>
                                    </button>
                                </div>
                                <div class="btn-group" role="group">
                                    <button type="button" id="following" class="btn btn-default" href="#tab3" data-toggle="tab">
                                        <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
                                        <div class="hidden-xs"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102003") %></div>
                                    </button>
                                </div>
                            </div>

                            <div class="well">
                                <div class="tab-content">
                                    <div class="tab-pane fade in active" id="tab1">

                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801050") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schCode" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131060") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schNameTH" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131060") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schNameEN" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--############################################## new 3/05/19 ##############################################--%>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801051") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="shortSchoolName" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--##############################################end new 3/05/19 ##############################################--%>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801015") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:DropDownList ID="schOwner" runat="server" CssClass="width100 form-control">
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133224") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133224") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132102") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132102") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133225") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133225") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133226") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133226") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133227") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133227") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133228") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133228") %>"></asp:ListItem>
                                                        <asp:ListItem Text="สทศ." Value="สทศ."></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133229") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133229") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133230") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133230") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133231") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133231") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133232") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133232") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133234") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133234") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133235") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133235") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133236") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133236") %>"></asp:ListItem>
                                                        <asp:ListItem Text="กระทรวงวัฒนธรรม" Value="กระทรวงวัฒนธรรม"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133237") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133237") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133238") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133238") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133239") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133239") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133240") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133240") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133241") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133241") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133242") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133242") %>"></asp:ListItem>
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133243") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133243") %>"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801052") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:DropDownList ID="schArea" runat="server" CssClass="width100 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801053") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schPhoneOne" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801054") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schPhoneTwo" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801055") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schMobilePhone" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801056") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schFax" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801057") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schEmailOne" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801058") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schEmailTwo" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801023") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schWebsite" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801024") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="txtTaxId" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade in" id="tab2">
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schHomeNumber" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schMuu" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schRoad" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schSoy" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:DropDownList ID="Ddlprovince" ClientIDMode="Static" runat="server" CssClass="width100 form-control">
                                                        <%--  <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133002") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133002") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133003") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133003") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133004") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133004") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133005") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133005") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133006") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133006") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133007") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133007") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133008") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133008") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133010") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133010") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133011") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133011") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133012") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133012") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133009") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133009") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133014") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133014") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133013") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133013") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133015") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133015") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133016") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133016") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133017") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133017") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133020") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133020") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133022") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133022") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133024") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133024") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133027") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133027") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133028") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133028") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133029") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133029") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133032") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133032") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ะเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132942") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ะเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132942") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133040") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133040") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133045") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133045") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133061") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133061") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133059") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133059") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133064") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133064") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133069") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133069") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133070") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133070") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133076") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133076") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133072") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133072") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133073") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133073") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133074") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133074") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133075") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133075") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %>"></asp:ListItem>
                 <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schAumpher" ClientIDMode="Static" runat="server" CssClass='form-control ' style="display:none" class="input--mid"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlAumpher" ClientIDMode="Static" runat="server" CssClass="width100 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schTumbon" ClientIDMode="Static" runat="server" CssClass='form-control' style="display:none" class="input--mid"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlTumbon" ClientIDMode="Static" runat="server" CssClass="width100 form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schPost" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103057") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schlat" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103058") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schlon" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="tab-pane fade in" id="schoolHistorytab">
                                        <div class="form-group">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801029") %></label>
                                            <asp:TextBox ID="txtSchoolHistory" TextMode="multiline" class="form-control" Rows="6" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801030") %></label>
                                            <asp:TextBox ID="txtSchoolVision" TextMode="multiline" class="form-control" Rows="3" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801063") %></label>
                                            <asp:TextBox ID="txtSchoolMission" TextMode="multiline" class="form-control" Rows="4" runat="server" />
                                        </div>
                                    </div>


                                    <div class="tab-pane fade in" id="tab3">

                                        <div class="contentBox">
                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801064") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="ddlschoolHead" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple2" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input" <%--style="" onclick="$('#ctl00_MainContent_filePhoto').click()"--%>>
                                                    <div class="uploader">
                                                        <img id="SignSchoolHeadImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignSchoolHead" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignSchoolHeadStatus" />
                                                    <button runat="server" id="SignSchoolHeadBtn" style="display: none" type="button" onclick="onRemoveSignature('SignSchoolHead')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>

                                            </div>

                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801034") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="ddlRegis" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple3" name="classchoice2[]"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignRegistraDirectorImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignRegistraDirector" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignRegistraDirectorStatus" />
                                                    <button runat="server" id="SignRegistraDirectorBtn" style="display: none" type="button" onclick="onRemoveSignature('SignRegistraDirector')" class="btn  btn-danger">
                                                        ลบลายเซ็น                                                        
                                                    </button>
                                                </div>
                                            </div>

                                            <!--------------------------------------------------------------------------------------------------------------------->
                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801066") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="ddlAcademic" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple4" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignAcademicImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignAcademic" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignAcademicStatus" />
                                                    <button runat="server" id="SignAcademicBtn" style="display: none" type="button" onclick="onRemoveSignature('SignAcademic')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>
                                            </div>
                                            <!--------------------------------------------------------------------------------------------------------------------->

                                            <div class="hidden">
                                                <div class="col-xs-12 mar10">
                                                    <label class="col-xs-5 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01525") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01133") %></label>
                                                    <div class="col-xs-5 control-input" onclick="comboclick()">
                                                        <asp:DropDownList ID="ddlSubAcademic" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple5" name="classchoice2[]">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 mb-30">
                                                    <label class="col-xs-5 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-xs-5  control-input">
                                                        <div class="uploader">
                                                            <img id="SignSubAcademicImg" runat="server" style="background-color: white" />
                                                            <asp:FileUpload ID="SignSubAcademic" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                        </div>
                                                        <input type="hidden" runat="server" id="SignSubAcademicStatus" />
                                                        <button runat="server" id="SignSubAcademicBtn" style="display: none" type="button" onclick="onRemoveSignature('SignSubAcademic')" class="btn  btn-danger">
                                                            ลบลายเซ็น
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801067") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="ddlAcc" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple6" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignAccountingDirectorImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignAccountingDirector" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignAccountingDirectorStatus" />
                                                    <button runat="server" id="SignAccountingDirectorBtn" style="display: none" type="button" onclick="onRemoveSignature('SignAccountingDirector')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>
                                            </div>

                                            <%--############################################## new 2/05/19 ##############################################--%>
                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801068") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="dropdownPersonnel" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple6" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--##############################################end new 2/05/19 ##############################################--%>


                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignPersonnelImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignPersonnel" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignPersonnelStatus" />
                                                    <button runat="server" id="SignPersonnelBtn" type="button" style="display: none" onclick="onRemoveSignature('SignPersonnel')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>
                                            </div>


                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801069") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="ddlStd" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple7" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignStudentDevelopmentDirectorImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignStudentDevelopmentDirector" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignStudentDevelopmentDirectorStatus" />
                                                    <button runat="server" id="SignStudentDevelopmentDirectorBtn" style="display: none" type="button" onclick="onRemoveSignature('SignStudentDevelopmentDirector')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>
                                            </div>

                                            <%--############################################## new 2/05/19 ##############################################--%>
                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801070") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="dropdownGM" runat="server" class="form-control" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple6" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--############################################## end new 2/05/19 ##############################################--%>

                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignGMImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignGM" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignGMStatus" />
                                                    <button runat="server" id="SignGMBtn" style="display: none" type="button" onclick="onRemoveSignature('SignGM')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>
                                            </div>

                                            <div class="col-xs-12 mar10">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801040") %></label>
                                                <div class="col-xs-5 control-input" onclick="comboclick()">
                                                    <asp:DropDownList ID="ddladmin" runat="server" class="form-control ddlvalue" onchange="changeddl()" Width="100%" CssClass="js-example-basic-multiple8" name="classchoice2[]">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 mb-30">
                                                <label class="col-xs-5 control-label">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                <div class="col-xs-5  control-input">
                                                    <div class="uploader">
                                                        <img id="SignAdminImg" runat="server" style="background-color: white" />
                                                        <asp:FileUpload ID="SignAdmin" name="userprofile_picture" CssClass="signature_filePhoto" runat="server" onchange="funSignatureImgHandle(this.id)" />
                                                    </div>
                                                    <input type="hidden" runat="server" id="SignAdminStatus" />
                                                    <button runat="server" id="SignAdminBtn" style="display: none" type="button" onclick="onRemoveSignature('SignAdmin')" class="btn  btn-danger">
                                                        ลบลายเซ็น
                                                    </button>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <div class="hidden">
                                                    <asp:TextBox ID="schoolheadtxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="registxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="academictxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="subacademictxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="acctxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="stdtxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="webtxt" runat="server" CssClass="configid"></asp:TextBox>

                                                    <%--############################################## new 2/05/19 ##############################################--%>
                                                    <asp:TextBox ID="personnelTxt" runat="server" CssClass="configid"></asp:TextBox>
                                                    <asp:TextBox ID="gmTxT" runat="server" CssClass="configid"></asp:TextBox>
                                                    <%--############################################## end new 2/05/19 ##############################################--%>

                                                    <asp:TextBox ID="editHead" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editRegis" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editAca" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editSub" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editAcc" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editStd" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editAdmin" runat="server" CssClass="configid2"></asp:TextBox>

                                                    <%--############################################## new 2/05/19 ##############################################--%>
                                                    <asp:TextBox ID="editpersonnel" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <asp:TextBox ID="editGM" runat="server" CssClass="configid2"></asp:TextBox>
                                                    <%--############################################## end new 2/05/19 ##############################################--%>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-group row student ">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                <asp:Button ID="btnSave" CssClass="btn-warning btn global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105042") %>" />
                                <asp:Button ID="btnBack" CssClass="btn-info btn global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
        <div class="form-group row student ">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-5 col-md-5 col-sm-5 col-xs-5 control-input">
                </div>
                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                </div>
            </div>
        </div>
        <div class="form-group row student hid">
            <div class="col-md-12 col-sm-12">
                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                    hidden</label>
                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                    <asp:TextBox ID="TextBox26" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
