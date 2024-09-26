<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="schoolprofile.aspx.cs" Inherits="FingerprintPayment.schoolprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>

    <%--fontawesome icon--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />


    <style type="text/css">
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
            background-color: white;
            border: 1px solid white;
            font-size: 20px;
            padding: 4px;
        }

        .contentBox {
            margin: 0 auto;
            width: 120%;
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

        img.center {
            display: block;
            margin: 0 auto;
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

        .signImg {
            width: 250px;
        }
    </style>

    <script type="text/javascript">
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#blah').attr('src', e.target.result);
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class=" full-card box-content" style="margin-top: 10px; padding-top: 10px;">

        <div class="row">
            <div class="contentBox">
                <h1 class="page-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701029") %></h1>
                <div class="column30">
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="text-center">
                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801042") %></h3>
                            <img id="schoolpicture" runat="server" alt="" class="avatar img-responsive center" />
                        </div>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="text-center">
                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801004") %></h3>
                            <h4>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801005") %>)</h4>
                            <img id="schoolcover" runat="server" alt="" class="avatar img-responsive" />
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801047") %>
                        </div>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <div class="text-center">
                            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801048") %></h3>
                        </div>
                        <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBlXrha-w3Nc6LbnmPn3s16be4TP59T0Os&libraries=places&callback=initMap"
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
                                    position: latlng,
                                    map: map

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
                                    document.getElementById('txtlat').value = place.geometry.location.lat();
                                    document.getElementById('txtlon').value = place.geometry.location.lng();
                                });
                            }
                        </script>
                        <input id="searchInput" class="controls" type="text" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801049") %>" />
                        <div id="map"></div>
                        <ul id="geoData" class="hidden">
                            <li>Latitude: <span id="lat"></span></li>

                            <li>Longitude: <span id="lon"></span></li>
                        </ul>
                    </div>


                    <input type="hidden" id="txtlat" runat="server" />
                    <input type="hidden" id="txtlon" runat="server" />

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
                                                    <asp:TextBox ID="schCode" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131060") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103053") %></sub></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schNameTH" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131060") %> <sub><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103054") %></sub></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schNameEN" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--############################################## new 3/05/19 ##############################################--%>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801051") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="shortSchoolName" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--##############################################end new 3/05/19 ##############################################--%>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801015") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schOwner" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801052") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schArea" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801053") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schPhoneOne" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801054") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schPhoneTwo" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801055") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schMobilePhone" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801056") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schFax" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801057") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schEmailOne" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801058") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schEmailTwo" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801023") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schWebsite" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801024") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="txtTaxId" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
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
                                                    <asp:TextBox ID="schHomeNumber" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schMuu" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schRoad" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schSoy" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schTumbon" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schAumpher" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schProvince" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row student ">
                                            <div class="col-md-12 col-sm-12">
                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %></label>
                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                    <asp:TextBox ID="schPost" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
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
                                            <asp:TextBox ID="txtSchoolHistory" TextMode="multiline" class="form-control" Rows="6" runat="server" Enabled="False" />
                                        </div>
                                        <div class="form-group">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801030") %></label>
                                            <asp:TextBox ID="txtSchoolVision" TextMode="multiline" class="form-control" Rows="3" runat="server" Enabled="False" />
                                        </div>
                                        <div class="form-group">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801063") %></label>
                                            <asp:TextBox ID="txtSchoolMission" TextMode="multiline" class="form-control" Rows="4" runat="server" Enabled="False" />
                                        </div>
                                    </div>


                                    <div class="tab-pane fade in" id="tab3">


                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801064") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="schoolHeadPicture" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="schoolHeadName" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="schoolHeadLastname" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="schoolHeadPhone" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignSchoolHeadImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801034") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%;">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="Img1" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="regis1" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="regis2" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="regis3" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignRegistraDirectorImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801066") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="Img2" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="aca1" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="aca2" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="aca3" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignAcademicImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="hidden">
                                            <div class="col-xs-12">
                                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01525") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01133") %></label>
                                            </div>
                                            <div class="col-xs-4" style="width: 30%;">
                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                    <div class="text-center">
                                                        <img id="Img3" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-8">
                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                            <asp:TextBox ID="sub1" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                            <asp:TextBox ID="sub2" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                            <asp:TextBox ID="sub3" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row student ">
                                                    <div class="col-md-12 col-sm-12">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                            <img id="SignSubAcademicImg" class="signImg" runat="server" style="background-color: white" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801067") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%;">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="Img4" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="acc1" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="acc2" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="acc3" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignAccountingDirectorImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <%--############################################## new 2/05/19 ##############################################--%>
                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801068") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%;">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="imgPersonnel" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="namePersonnel" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="lastnamePersonnel" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="telPersonnel" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignPersonnelImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--############################################## end new 2/05/19 ##############################################--%>



                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801069") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="Img5" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="std1" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="std2" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="std3" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignStudentDevelopmentDirectorImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <%--############################################## new 2/05/19 ##############################################--%>
                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801070") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%;">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="imgGM" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="nameGM" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="lastnameGM" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="telGM" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignGMImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--############################################## end new 2/05/19 ##############################################--%>


                                        <div class="col-xs-12">
                                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801040") %></label>
                                        </div>
                                        <div class="col-xs-4" style="width: 30%;">
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="text-center">
                                                    <img id="Img6" runat="server" class="avatar img-thumbnail" alt="avatar" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xs-8">
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="admin1" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="admin2" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <asp:TextBox ID="admin3" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row student ">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801065") %></label>
                                                    <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                        <img id="SignAdminImg" class="signImg" runat="server" style="background-color: white" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                    <div class="form-group row student hid">
                                        <div class="col-md-12 col-sm-12">
                                            <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label righttext">
                                                hidden</label>
                                            <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 control-input">
                                                <asp:TextBox ID="TextBox24" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
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

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
                            <asp:Button ID="btnEdit" CssClass="btn-warning btn global-btn pull-right" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %>" />
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
                <asp:TextBox ID="TextBox26" runat="server" CssClass='form-control' class="input--mid" Enabled="False"></asp:TextBox>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
