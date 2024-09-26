<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="EditsEmpWorkStatus.aspx.cs" Inherits="FingerprintPayment.UpdateEmpStatus.EditsEmpWorkStatus" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style type="text/css">
        .text-responsive {
            font-weight: normal;
            font-size: calc(40% + 1vw + 1vh);
        }

        .set-text {
            font-size: 24px;
        }

        .output {
            font-weight: normal;
            padding-top: 12px;
        }

        .marginHandle {
            margin-top: 20px;
            margin-bottom: 20px;
        }

        .btn2 {
            width: 165px;
            margin-left: 10px;
            margin-right: 10px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .btn-secondary {
            color: #fff;
            background-color: #6c757d;
        }

        #loading {
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 100;
            width: 100vw;
            height: 100vh;
            background-color: rgba(192, 192, 192, 0.5);
            background-image: url("https://i.imgur.com/CgViPo0.gif");
            background-repeat: no-repeat;
            background-position: center;
        }

        /*  label.col-form-label{
            padding-top:0 !important;
        }*/
    </style>


    <%--fontawesome icon--%>
    <%--    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />--%>

    <%--  <script type="text/javascript" src="updateEmpStatus.js"></script>--%>

    <%--  <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <%--    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>

    <%--    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>--%>
    <%--    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script src="updateEmpStatus.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>

    <script>
        $(function () {
            if (jQuery.validator) {

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                //$("#aspnetForm").validate({  // initialize the plugin
                //    errorPlacement: function (error, element) {
                //        var _class = element.attr('class');

                //        //if (_class.includes('--date-validate')) {
                //        //    error.insertAfter(element.parent());
                //        //}
                //        //else {

                //        //}
                //        error.insertAfter(element);
                //    }

                //});


                //$('#mysearchform').on('submit', function (e) {
                //    if ($('#aspnetForm').valid() == false) {

                //        e.preventDefault();
                //        e.stopPropagation();
                //        return false;
                //    }
                //});
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--   <div id="loading" class="loading"></div>--%>

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105009") %>          
            </p>
        </div>
    </div>

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>--%>

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

                    <div class="row">
                        <label class="col-md-2 col-form-label xtext-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-md-4 ">
                            <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" />
                            <%-- <input class="form-control" id="txtname" placeholder=" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>" autofocus />
                            <input class="form-control" id="empid" placeholder=" id" style="display: none" />
                            <input class="form-control" id="txtPhone" placeholder=" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02940") %>" style="display: none" />--%>
                        </div>
                        <div class="col-md-2">
                            <button type="button" id="btnSearch" class="btn btn-info " onclick="GetEmpData()">
                                <span class="btn-label">
                                    <i class="material-icons">search</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
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
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                </div>
                <div class="card-body ">
                    <div class="row" style="margin: 30px 0">
                        <div class="col-sm-12 col-lg-3" id="Picture">
                            <div class="col-lg-12 text-center" style="padding: 10px;">
                                <i class="fa fa-user" style="font-size: 100px; background-color: #39c; width: 180px; height: 180px; padding: 30px; border-radius: 50%;" id="mokPic"></i>
                                <img id="empImg" src="" width="180" height="180" style="display: none; margin: auto">
                            </div>
                        </div>
                        <div class="col-xs-12 col-lg-9" style="padding: 0 30px">
                            <div class="row">
                                <label class="col-lg-4 col-xs-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> :</label>
                                <label class="col-lg-8 col-xs-6 output" id="txtNameDetail"></label>
                                <label class="col-lg-4 col-xs-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105032") %> :</label>
                                <label class="col-lg-8 col-xs-6 output" id="txtType"></label>
                            </div>
                            <div class="row">
                                <label class="col-lg-4 col-xs-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %> :</label>
                                <label class="col-lg-8 col-xs-6 output" id="txtJob"></label>
                                <label class="col-lg-4 col-xs-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %> :</label>
                                <label class="col-lg-8 col-xs-6 output" id="txtDepart"></label>
                            </div>
                            <div class="row">
                                <label class="col-lg-4 col-xs-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %> :</label>
                                <label class="col-lg-8 col-xs-6 output" id="txtTimeType"></label>
                                <label class="col-lg-4 col-xs-6 col-form-label text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105034") %> :</label>
                                <label class="col-lg-8 col-xs-6 output" id="txtBirthday"></label>
                            </div>

                        </div>
                    </div>
                    <div>

                        <div class="row" style="margin: 10px 0;">
                            <div class="col-md-2 col-xs-6 text-right">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105035") %> :</label>
                            </div>
                            <div class="col-md-10 col-xs-6">
                                <label class="radio-inline" style="margin-left: 10px">
                                    <input type="radio" name="typeScanTime" style="margin-top: 6px; height: 20px; width: 20px" value="0" checked />&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105036") %>
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="typeScanTime" style="margin-top: 6px; height: 20px; width: 20px" value="1" />&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105037") %>
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="typeScanTime" style="margin-top: 6px; height: 20px; width: 20px" value="2" />&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105038") %>
                                </label>
                            </div>

                        </div>

                        <div class="row" style="margin: 10px 0;">
                            <div class="col-md-2 col-xs-6 text-right">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105039") %> :</label>
                            </div>
                            <div class="col-md-3 col-xs-6">
                                <%-- <input type="text" class='form-control datepicker' id="txtdaystart" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301014") %>" readonly />--%>
                                <div class="form-group has-successx">
                                    <input type="text" name="txtdaystart" id="txtdaystart" class="form-control datepicker" value="" required="required">
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1 col-xs-6 text-right">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %> :</label>
                            </div>
                            <div class="col-md-3 col-xs-6">
                                <%--  <input type="text" class='form-control datepicker' id="txtdayend" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301014") %>" readonly />--%>
                                <div class="form-group has-successx">
                                    <input type="text" name="txtdayend" id="txtdayend" class="form-control datepicker" value="" required="required">
                                    <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>

                        </div>

                        <div class="row" style="margin: 20px 0;">
                            <div class="col-md-2 col-xs-6 text-right">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105041") %> :</label>
                            </div>

                            <div class="col-md-3 col-xs-6">
                                <select class=" selectpicker" id="ddlStatus" data-style="select-with-transition" data-width="100%">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></option>
                                    <% foreach (var i in LeaveData){%>
                                        <option value="<%=i.Value %>"><%=i.Text %></option>
                                    <% } %>
                                   <%-- <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></option>
                                    <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></option>
                                    <option value="26"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01108") %></option>
                                    <option value="25"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01852") %></option>
                                    <option value="21"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01844") %></option>
                                    <option value="23"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01854") %></option>
                                    <option value="24"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01843") %></option>
                                    <option value="22"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01850") %></option>--%>
                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701032") %></option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 10px 0;">
                        <div class="col-xs-12 center">
                            <button type="button" class="btn btn-success" onclick="EmpUpdateStatus()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105042") %></button>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
    <%--            <div class="row">
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" id="btnStatus" onclick="EmpUpdateStatus(0)" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(1) " disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(3) " disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(10) " disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(11) " disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(26) " disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01108") %></button>
                </div>

            </div>

            <div class="row">
                <div class="col-md-1">
                   
                </div>
                
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(25) " disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01852") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(21)" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01844") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(23)" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01854") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(24)" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01843") %></button>
                </div>
                <div class="col-md-2 col-xs-6">
                    <button class="btn btn-secondary btn2" onclick="EmpUpdateStatus(22)" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133393") %></button>
                </div>
                <div class="col-md-1">
                   
                </div>

            </div>--%>

    <%--            <div class="row">
            <h2><b>ประวัติการมาทำงานประจำเดือน ปัจจุบัน ปีการศึกษา ปีปจบ</b></h2>
            <div class=" col-sm-12 col-lg-8">
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #33cc66; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_0">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_0">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #ff9966; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_1">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_1">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #ff6666; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_2">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_2">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #3399cc; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_3">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_3">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #9933cc; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_4">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_4">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #cc66cc; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01108") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_5">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_5">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #e8e8e8; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102257") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_6">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_6">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #e8e8e8; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01844") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_7">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_7">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #e8e8e8; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01854") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_8">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_8">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #e8e8e8; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132319") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_9">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_9">0</span> % )
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <i class="fa fa-users" style="background-color: #e8e8e8; padding: 8px; border-radius: 25px; color: #fff;"></i>
                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01850") %></span>
                    </div>
                    <div class="col-lg-4">
                        <span id="Status_10">0</span>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>
                    </div>
                    <div class="col-lg-4">
                        ( <span id="percent_10">0</span> % )
                    </div>
                </div>
            </div>
        </div>
    </form>
    <%-- <div class="row set-text">
        <div class="col-md-12 content-container" style="background: #ffffff; padding: 50px 50px 30px 50px">
        </div>
    </div>--%>
</asp:Content>


