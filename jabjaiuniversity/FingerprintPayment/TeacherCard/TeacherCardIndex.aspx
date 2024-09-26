<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherCardIndex.aspx.cs" MasterPageFile="~/Material2.Master" Inherits="FingerprintPayment.TeacherCard.TeacherCardIndex" %>

<%@ Register Src="~/UserControls/TeacherAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%-- <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>

    <%--<script src="../bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>--%>

    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <%-- <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            label {
                font-weight: normal;
                font-size: 18px;
            }

            legend {
                padding-left: 30px;
                font-size: 18px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }

            label {
                font-weight: normal;
                font-size: 22px;
            }

            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }

            .table-show-result {
                font-size: 22px;
            }
        }

        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }

            label {
                font-weight: normal;
                font-size: 26px;
            }

            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 26px;
            }
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        @media (min-width: 1300px) {
            #page-wrapper {
                position: inherit;
                margin: 0 0 0 250px;
                padding: 0 30px;
                border-left: 1px solid #e7e7e7;
                background-color: #eee;
                padding-top: 30px;
                padding-bottom: 30px;
                min-height: 900px;
            }
        }

        .header_01 {
            min-width: 100px;
        }

        .header_02 {
            min-width: 50px;
        }

        /*#example {
            background-color: white;
        }*/
        /*.sort_type {
            display: none;
        }*/

        .btn_red.active {
            background-color: #337AB7;
        }

        .btn_red {
            min-width: 70px;
        }

        .pad0 {
            padding: 0px;
        }
    </style>--%>

    <style>
        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr td {
            border-bottom: 1px solid #000;
        }
    </style>
    <% if (!IsReportBuilder)
        {%>
    <style>
        .reportbuilder {
            display: none !important;
        }

        .td-reportbuilder {
            width: 1% !important;
        }
    </style>
    <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106105") %>
            </p>
        </div>
    </div>

    <form id="aspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Release" />

        <asp:HiddenField ID="hdfschoolname" runat="server" />

        <div class="row ">
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
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %></label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="eMp_Type" runat="server" ClientIDMode="Static"
                                    DataTextField="Text"
                                    DataValueField="Value" CssClass="selectpicker  col-sm-12" data-style="select-with-transition">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                            <div class="col-sm-3">
                                <uc1:TeacherAutocomplete runat="server" ID="TeacherAutocomplete" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <button type="button" class="btn btn-info" onclick="searchData()">
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
        </div>


        <div class="row ">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning  card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                    </div>
                    <div class="card-body ">
                        <table id="example" class="table-hover dataTable table-show-result" style="width: 100%"></table>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="exampleModal">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class=" modal-header">
                        <h3 class="modal-title"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106093") %></strong></h3>
                    </div>

                    <div class="modal-body">
                        <div class="row">


                            <div class=" col-sm-12">
                                <div class="row">
                                    <label class="col-md-5 col-form-label col-sm-6 text-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106026") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="select_tYpe" runat="server" class="selectpicker col-sm-12" onchange="CardType()" data-style="select-with-transition">
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106117") %>"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106118") %>"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106119") %>"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106120") %>"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106121") %>"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106122") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class=" col-sm-12" id="divShowImg" style="display: none;">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106112") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="ShowImgProfile" ClientIDMode="Static" runat="server" CssClass="selectpicker col-sm-12"
                                            onchange="PropShowImgProfile('ShowImgProfile',this.value)" data-style="select-with-transition">
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class=" col-sm-12" id="FcNameEng" style="display: none;">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106106") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="ShowNameEng" runat="server" CssClass="selectpicker col-sm-12" onchange="fcShowNameEng()" data-style="select-with-transition">
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-12" id="FcPositionEmp" style="display: none;">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="ShowPosition" runat="server" CssClass="selectpicker col-sm-12" onchange="fcShowPosition()" data-style="select-with-transition">
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12" id="FcEmpID" style="display: none;">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106107") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="ShowEmpID" runat="server" CssClass="selectpicker col-sm-12" onchange="fcShowEmpID()" data-style="select-with-transition">
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-12" id="divPictureSize" style="display: none;">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106108") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="ddlPictureSize" runat="server" CssClass="selectpicker col-sm-12" onchange="PropShowImgProfile('PictureSize',this.value)" data-style="select-with-transition">
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106109") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106110") %>" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12" id="FcEmpIdentity" style="display: none;">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %> :</label>
                                    <div class="col-md-6 col-sm-6">
                                        <asp:DropDownList ID="ShowIdentity" runat="server" CssClass="selectpicker col-sm-12" onchange="fcShowIdentity()" data-style="select-with-transition">
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="row">
                                    <label class="col-md-5 col-sm-6 text-right col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %> :</label>
                                    <div class="col-md-7 col-sm-6">
                                        <iframe scrolling="no" src="../TeacherCard/UploadPictureTeacher.aspx" style="border: 0px none; height: 200px; width: 300px;"></iframe>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12 pad0 settingdetail d-none">

                                <div class="col-sm-12 pad0 QRCode">

                                    <div class=" col-sm-12">
                                        <div class="row">
                                            <label class="col-md-5 col-sm-6 text-right  col-form-label pad0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106073") %> :</label>
                                            <div class="col-md-6 col-sm-6">
                                                <asp:DropDownList CssClass="selectpicker col-sm-12 positiontest" runat="server" ID="select_UpDown" onchange="ScrollPositionSetting(0)" data-style="select-with-transition">
                                                    <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                                    <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                                    <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                                    <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                                    <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                                    <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                                    <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                                    <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                                    <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                                    <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class=" col-sm-12">
                                        <div class="row">
                                            <label class="col-md-5 col-sm-6 text-right  col-form-label pad0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106074") %> :</label>
                                            <div class="col-md-6 col-sm-6">
                                                <asp:DropDownList CssClass="selectpicker col-sm-12 positiontest" runat="server" ID="select_LeftRight" onchange="ScrollPositionSetting(1)" data-style="select-with-transition">
                                                    <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                                    <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                                    <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                                    <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                                    <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                                    <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                                    <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                                    <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                                    <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                                    <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div class="col-sm-12 text-center">
                            <button type="button" class="btn btn-info" style="" onclick="Setting()"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106113") %></strong></button>
                            <button type="button" class="btn btn-success" data-dismiss="modal"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></strong></button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106116") %></h4>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; height: 600px;">


                        <div class="row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106026") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlCardForm2" runat="server" CssClass="width100x  selectpicker col-md-12 typevalue" data-style="select-with-transition" data-size="10"
                                    onchange="PropShowImgProfile('NewCardType',this.value)">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>" Selected="True"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>

                        <%--  <div class=" row" style="padding: 0 4px;">
                      <div class="col-md-5">
                          <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106029") %></label>
                      </div>
                      <div class="col-md-7">
                          <div class="form-groupx col-md-12" style="padding-left: 1px;">
                              <asp:TextBox ID="txtPrintDate2" runat="server" onchange="" ClientIDMode="static" CssClass="form-control datepicker linkfrom " Style="padding-left: 10px;" />
                              <span class="form-control-feedback" style="color: #000; opacity: 1; right: 15px;">
                                  <i class="material-icons">event</i>
                              </span>
                          </div>
                      </div>
                  </div>

                  <div class=" row" style="padding: 0 4px;">
                      <div class="col-md-5">
                          <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106030") %></label>
                      </div>
                      <div class="col-md-7">
                          <div class="form-groupx col-md-12" style="padding-left: 1px;">
                              <asp:TextBox ID="startDay2" runat="server" onchange="" ClientIDMode="static" CssClass="form-control datepicker linkfrom " Style="padding-left: 10px;" />
                              <span class="form-control-feedback" style="color: #000; opacity: 1; right: 15px;">
                                  <i class="material-icons">event</i>
                              </span>
                          </div>
                      </div>
                  </div>--%>

                        <div class=" ">
                            <div class="row" style="padding: 0 4px;">
                                <div class="col-md-5">
                                    <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106051") %></label>
                                </div>
                                <div class="col-md-7">
                                    <iframe scrolling="no" src="../TeacherCard/UploadPictureTeacher.aspx" style="border: 0px none; height: 200px; width: 300px;"></iframe>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSubmit2" class="btn btn-success" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>

    <script src="../Report/ScriptReport.js" type="text/javascript"></script>
    <script src="Script/TeacherCard.js?v=2" type="text/javascript"></script>

    <script type="text/javascript">

        var TecherCard = new Techer_Card();

        var Emp_Type = "";
        var EmpName = "";

        var OptionUpDown = "";
        var OptionLeftRight = "";


        $(document).ready(function () {
            $('select[id*=select_tYpe]').trigger('change');
        });

        function searchData() {
            Emp_Type = $('select[id*=eMp_Type]').val();
            EmpName = TAC.GetUserName();//$('#txtEmpName').val();

            var data = {
                "eMp_Type": Emp_Type,
                "Emp_Name": EmpName
            };

            //$("body").mLoading();
            PageMethods.List_DataEmp(data, function (e) {
                console.log(e.data);
                TecherCard.List_DataEmp = e;
                TecherCard.RenderHtml_ListDataEmp("example", false);
            });

        }

        function Modelsetting() {
            $("#exampleModal").modal()
        }


        function PrintID(EmpID) {

            OptionUpDown = $('select[id*=select_UpDown]').val();
            OptionLeftRight = $('select[id*=select_LeftRight]').val();

            var CardTYpe = $('select[id*=select_tYpe]').val();
            var ShowEmpID = $('select[id*=ShowEmpID]').val();

            var ShowNameEng = $('select[id*=ShowNameEng]').val();
            var ShowPosition = $('select[id*=ShowPosition]').val();

            var ShowIdentity = $('select[id*=ShowIdentity]').val();

            if (CardTYpe == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>") {
                alert("กรุณาเลือกรูปแบบบัตร");
            } else {
                var queryString = "?EmpID=" + EmpID + "&OUpDown=" + OptionUpDown + "&OLeftRight=" + OptionLeftRight + "&showEmpid=" + ShowEmpID + "&SHnameen=" + ShowNameEng + "&SHposition=" + ShowPosition + "&SHidenti=" + ShowIdentity;
                window.open('TeacherCardPrint.aspx' + queryString);
            }

        }

        function PrintID2(EmpID) {

            var CardTYpe = $('select[id*=ddlCardForm2]').val();
          
            if (CardTYpe == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>") {
                alert("กรุณาเลือกรูปแบบบัตร");
            } else {
               
                window.open('TeacherCardPrint2.aspx?id=' + EmpID);
            }

        }

        function PrintALL() {
            var Type = $('select[id*=eMp_Type]').val();

            OptionUpDown = $('select[id*=select_UpDown]').val();
            OptionLeftRight = $('select[id*=select_LeftRight]').val();

            var CardTYpe = $('select[id*=select_tYpe]').val();
            var ShowEmpID = $('select[id*=ShowEmpID]').val();

            var ShowNameEng = $('select[id*=ShowNameEng]').val();
            var ShowPosition = $('select[id*=ShowPosition]').val();

            var ShowIdentity = $('select[id*=ShowIdentity]').val();

            if (CardTYpe == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>") {
                alert("กรุณาเลือกรูปแบบบัตร");
            } else {
                var queryString = "?type=" + Type + "&OUpDown=" + OptionUpDown + "&OLeftRight=" + OptionLeftRight + "&showEmpid=" + ShowEmpID + "&SHnameen=" + ShowNameEng + "&SHposition=" + ShowPosition + "&SHidenti=" + ShowIdentity;
                window.open('TeacherCardPrint.aspx' + queryString);
            }

        }


        function Setting() {
            var settingdetail = document.getElementsByClassName("settingdetail");

            if (settingdetail[0].classList.contains('d-none') == true) {
                settingdetail[0].classList.remove('d-none');
            } else {
                settingdetail[0].classList.add('d-none');
            }
        }

        function ScrollPositionSetting(index) {
            var PositionName = "";

            //var ClassPosition = document.getElementsByClassName("positiontest");
            var PositionValue = 0;////ClassPosition[Number(index)].value;


            if (index == 0) {
                PositionName = "ContentAlignmentTopBottom";
                PositionValue = $('select[id*=select_UpDown]').val();
            }
            else if (index == 1) {
                PositionName = "ContentAlignmentLeftRight";
                PositionValue = $('select[id*=select_LeftRight]').val();
            }

            //console.log("/TeacherCard/Ashx/EditTeacherCardScrollPosition.ashx?position=" + PositionName + "&value=" + PositionValue);

            $.ajax({
                url: "/TeacherCard/Ashx/EditTeacherCardScrollPosition.ashx?position=" + PositionName + "&value=" + PositionValue,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });

        }

        function CardType() {
            var CardTYpe = $('select[id*=select_tYpe]').val();

            switch (CardTYpe) {
                case "1":
                    document.getElementById('FcNameEng').style.display = 'none';
                    document.getElementById('FcPositionEmp').style.display = 'block';
                    document.getElementById('FcEmpID').style.display = 'block';
                    document.getElementById('FcEmpIdentity').style.display = 'none';
                    document.getElementById('divShowImg').style.display = 'none';
                    document.getElementById('divPictureSize').style.display = 'none';
                    break;
                case "2":
                    document.getElementById('FcNameEng').style.display = 'block';
                    document.getElementById('FcPositionEmp').style.display = 'block';
                    document.getElementById('FcEmpID').style.display = 'block';
                    document.getElementById('FcEmpIdentity').style.display = 'none';
                    document.getElementById('divShowImg').style.display = 'none';
                    document.getElementById('divPictureSize').style.display = 'none';
                    break;
                case "3":
                    document.getElementById('FcNameEng').style.display = 'block';
                    document.getElementById('FcPositionEmp').style.display = 'block';
                    document.getElementById('FcEmpID').style.display = 'block';
                    document.getElementById('FcEmpIdentity').style.display = 'none';
                    document.getElementById('divShowImg').style.display = 'none';
                    document.getElementById('divPictureSize').style.display = 'block';
                    break;
                case "4":
                    document.getElementById('FcNameEng').style.display = 'none';
                    document.getElementById('FcPositionEmp').style.display = 'block';
                    document.getElementById('FcEmpID').style.display = 'block';
                    document.getElementById('FcEmpIdentity').style.display = 'block';
                    document.getElementById('divShowImg').style.display = 'none';
                    document.getElementById('divPictureSize').style.display = 'none';
                    break;
                case "5":
                    document.getElementById('FcNameEng').style.display = 'block';
                    document.getElementById('FcPositionEmp').style.display = 'block';
                    document.getElementById('FcEmpID').style.display = 'none';
                    document.getElementById('FcEmpIdentity').style.display = 'block';
                    document.getElementById('divShowImg').style.display = 'block';
                    document.getElementById('divPictureSize').style.display = 'none';
                    break;
                case "6":
                    document.getElementById('FcNameEng').style.display = 'none';
                    document.getElementById('FcPositionEmp').style.display = 'none';
                    document.getElementById('FcEmpID').style.display = 'none';
                    document.getElementById('FcEmpIdentity').style.display = 'none';
                    document.getElementById('divShowImg').style.display = 'none';
                    document.getElementById('divPictureSize').style.display = 'none';
                    break;
            }

            $.ajax({
                url: "/TeacherCard/Ashx/EditTeacherCardType.ashx?CardType=" + CardTYpe,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }

        function fcShowEmpID() {
            var value = $('select[id*=ShowEmpID]').val();
            $.ajax({
                url: "/TeacherCard/Ashx/fcShowEmpID.ashx?ShowEmpID=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }

        function fcShowNameEng() {
            var value = $('select[id*=ShowNameEng]').val();
            $.ajax({
                url: "/TeacherCard/Ashx/fcShowNameEng.ashx?ShowNameEng=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }
        function PropShowImgProfile(prop, value) {
            $.ajax({
                url: `/TeacherCard/Ashx/TeacherCardProp.ashx?prop=${prop}&value=${value}`,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }
        function fcShowPosition() {
            var value = $('select[id*=ShowPosition]').val();
            $.ajax({
                url: "/TeacherCard/Ashx/fcShowPosition.ashx?ShowPosition=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }

        function fcShowIdentity() {
            var value = $('select[id*=ShowIdentity]').val();
            $.ajax({
                url: "/TeacherCard/Ashx/fcShowIdentity.ashx?ShowIdentity=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }

        function PrintAll2() {
            window.open('TeacherCardPrint2.aspx');
        }

    </script>
</asp:Content>
