<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp_notFrom.Master"
    CodeBehind="PrintBillPreview.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.PrintBillPreview" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
        }

            page[size="A4"] {
                width: 21cm;
                height: 29.7cm;
            }

                page[size="A4"][layout="landscape"] {
                    width: 29.7cm;
                    height: 21cm;
                }

            page[size="A3"] {
                width: 29.7cm;
                height: 42cm;
            }

                page[size="A3"][layout="landscape"] {
                    width: 42cm;
                    height: 29.7cm;
                }

            page[size="A5"] {
                width: 14.8cm;
                height: 21cm;
            }

                page[size="A5"][layout="landscape"] {
                    width: 21cm;
                    height: 14.8cm;
                }

        .blue-font {
            color: #3498db
        }

        table tbody tr {
            line-height: 12px;
        }

        @media print {
            body, page {
                margin: 0;
                box-shadow: 0;
            }
        }

        .jabjai-sticky-toolbar {
            width: 120px !important;
        }

        .Class-label-1 {
            font-size:16px;
        }
    </style>
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <link href="/Content/custom/custom-sticky.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />
    <script src="../../Scripts/thaibath.js"></script>

    <ul class="jabjai-sticky-toolbar" style="margin-top: 30px;">
        <li class="jabjai-sticky-toolbar__item jabjaisticky-toolbar__item--demo-toggle" id="jabjai_panel_toggle" data-toggle="jabjaitooltip" title="" data-placement="right" data-original-title="Check out more demos">
            <a href="#" class="jabjai-toolbar-link" style="font-size: 20px; font-weight: bolder"><span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                <br />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603032") %></a>
            <button type="button" class="btn btn-primary btn-lg  btnPrint"><span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
        </li>
    </ul>

    <div id="jabjai_panel" class="jabjai-right-panel">
        <div class="jabjai-panel__head" style="">
            <h3 class="jabjai-panel__title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %>
		
               

                <!--<small>5</small>-->
            </h3>
            <a href="#" class="jabjai-panel__close" id="jabjai_panel_close"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a>
        </div>
        <div class="jabjai-panel__body jabjai-scroll ps ps--active-y" style="height: 317px; overflow: hidden;">
            <div class="jabjai-panel__item ">
                <div class="form-group">
                    <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %></label>
                    <select id="ddl-option" class="form-control" style="font-size: 20px">
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132511") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132512") %></option>
                    </select>
                </div>
                <div class="form-group">
                    <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132510") %></label>
                    <select id="ddl-show-name" class="form-control" style="font-size: 20px">
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132513") %></option>
                    </select>
                </div>
                <div class="form-group">
                    <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132514") %></label>
                    <select id="ddl-show-tax" class="form-control" style="font-size: 20px">
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></option>
                    </select>
                </div>
                <div class="form-group">
                    <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></label>
                    <select id="ddl-show-class" class="form-control" style="font-size: 20px">
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132516") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201034") %></option>
                    </select>
                </div>
                <div class="form-group" style="padding-top: 20px; text-align: center">
                    <button type="button" class="btn btn-primary btn-lg btnPrint" id="btnPrint"><span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                </div>
            </div>
        </div>
    </div>

    <page size="A4" id="page1">

        <div class="form-group">
            <div class="text-right" style="padding: 15px 15px 0 0" id="full-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132053") %></div>
            <div class="col-md-8">
                <div class="col-md-12">
                    <ul style="padding: 0">
                        <li style="list-style: none; height: 22px">
                            <h3 style="margin: 0">
                                <img src="<%=  schoolData.sImage %>" style="width: 50px; height: 50px" />
                                <b style="vertical-align: top;"><%=  schoolData.sCompany %></b></h3>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 0px 0 0 50px">
                            <p style="font-size: 14px">
                                &nbsp;
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                <br />
                                &nbsp;  <%= schoolData.sPost %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132537") %> <%= schoolData.sPhoneOne %>
                                <br />
                                &nbsp;<span class="Tax-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132514") %> :  <%=  schoolData.TaxId %></span>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-4 text-right" style="font-size: 18px">
                <ul>
                    <li style="list-style: none; height: 27px">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>&nbsp;</label><span style="text-align: center; width: 150px; border: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.ReceiptNo %></span></li>
                    <li style="list-style: none; height: 27px">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>&nbsp;</label><span style="text-align: center; width: 150px; border: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.Date.ToString("dd/MM/yyyy") %></span></li>
                </ul>
            </div>
        </div>
        <div class="form-group" style="padding-top: 10px; margin: 0">
            <p class="text-center" style="font-size: 18px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603028") %></b></p>
        </div>
        <div class="form-group" style="padding: 0px 0 0 15px">
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;<span><%=Model.StudentName %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Name-Surname</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %>&nbsp;&nbsp;<span><%=Model.StudentIdCard %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Identification Number</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>&nbsp;&nbsp;<span><%=Model.StudentCode %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Student Number</b></p>
            </div>
        </div>
        <div class="form-group" style="padding: 20px 0 0 15px">
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>&nbsp;&nbsp;<span><%=Model.nYear %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Academic Year</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>&nbsp;&nbsp;<span><%=Model.Term %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Semester</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;">
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>&nbsp;&nbsp;
                        <span class="Class-label-0"><%=( Model.Fd_NewTermSubLevel ?? Model.SubLevel ) + "/" + ( Model.Fd_NewTermSubLevel2 ?? Model.SubLevel2 )%></span>
                        <span class="Class-label-1"><%= ClassLabel %></span>
                    </b>
                </p>
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Class</b></p>
            </div>
        </div>

        <div class="form-group" style="padding: 35px 20px 0 20px">
            <table style="width: 100%" id="tb-products-1">
                <thead></thead>
                <tbody></tbody>
            </table>
        </div>
        <div class="form-group col-md-12">
            <div class="col-md-7" style="font-size: 18px">
                <ul>
                    <li class="text-left" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;">____________________________________</p>
                    </li>
                    <li class="text-left" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;" class="employees-name-3">(<span style="color: white;">___________________________________</span>)</p>
                    </li>
                </ul>
            </div>
            <div class="col-md-5 text-right" style="font-size: 22px; margin: 0 0 0 0;">
                <ul>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>____________________________________</p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;" class="employees-name-2"></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_1">
                        <p style="margin: 0; font-size: 16px; border-bottom: 1px solid black;" class="employees-name-1"></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_1">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                    </li>
                </ul>
            </div>
        </div>

        <div class="form-group col-md-12" style="padding-top: 10px; padding-left: 0px;">
            <hr style="width: 21cm; margin: 0; border-top: 2px dotted #a9a9a9;" size="10" />
        </div>
        <div id="page2">
            <div class="text-right" style="padding: 10px 15px 0 0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132515") %></div>
            <div class="form-group">
                <div class="col-md-8">
                    <div class="col-md-12">
                        <ul style="padding: 0">
                            <li style="list-style: none; height: 22px">
                                <h3 style="margin: 0">
                                    <img src="<%=  schoolData.sImage %>" style="width: 50px; height: 50px" />
                                    <b style="vertical-align: top;"><%=  schoolData.sCompany %></b>
                                </h3>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0px 0 0 50px">
                                <p style="font-size: 14px">
                                    &nbsp;
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                    <br />
                                    &nbsp;  <%= schoolData.sPost %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132537") %> <%= schoolData.sPhoneOne %>
                                    <br />
                                    &nbsp; <span class="Tax-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132514") %> : <%=  schoolData.TaxId %></span>
                                </p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-4 text-right" style="font-size: 18px">
                    <ul>
                        <li style="list-style: none; height: 27px">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>&nbsp;</label><span style="text-align: center; width: 150px; border: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.ReceiptNo %></span></li>
                        <li style="list-style: none; height: 27px">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>&nbsp;</label><span style="text-align: center; width: 150px; border: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.Date.ToString("dd/MM/yyyy") %></span></li>
                    </ul>
                </div>
            </div>

            <div class="form-group" style="padding-top: 10px; margin: 0">
                <p class="text-center" style="font-size: 18px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603028") %></b></p>
            </div>
            <div class="form-group" style="padding: 0px 0 0 15px">
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;<span><%=Model.StudentName %></span></b></p>
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Name-Surname</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %>&nbsp;&nbsp;<span><%=Model.StudentIdCard %></span></b></p>
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Identification Number</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>&nbsp;&nbsp;<span><%=Model.StudentCode %></span></b></p>
                    <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Student Number</b></p>
                </div>
            </div>
            <div class="form-group" style="padding: 20px 0 0 15px">
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>&nbsp;&nbsp;<span><%=Model.nYear %></span></b></p>
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Academic Year</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>&nbsp;&nbsp;<span><%=Model.Term %></span></b></p>
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Semester</b></p>
                </div>
                <div class="col-md-4">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;">
                        <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>&nbsp;&nbsp;
                            <span class="Class-label-0"><%=( Model.Fd_NewTermSubLevel ?? Model.SubLevel ) + "/" + ( Model.Fd_NewTermSubLevel2 ?? Model.SubLevel2 )%></span>
                            <span class="Class-label-1"><%= ClassLabel %></span></b>
                    </p>
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Class</b></p>
                </div>
            </div>
            <div class="form-group" style="padding: 25px 20px 0 20px">
                <table style="width: 100%" id="tb-products-2">
                    <thead></thead>
                    <tbody></tbody>
                </table>
            </div>

            <div class="form-group col-md-12">
                <div class="col-md-7" style="font-size: 18px">
                    <ul>
                        <li class="text-left" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;">____________________________________</p>
                        </li>
                        <li class="text-left" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;" class="employees-name-3">(<span style="color: white;">___________________________________</span>)</p>
                        </li>
                    </ul>
                </div>
                <div class="col-md-5 text-right" style="font-size: 22px; margin: 0 0 0 0;">
                    <ul>
                        <li class="text-center" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>____________________________________</p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px;" class="employees-name-2"></p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_2">
                            <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_1">
                            <p style="margin: 0; font-size: 16px; border-bottom: 1px solid black;" class="employees-name-1"></p>
                        </li>
                        <li class="text-center" style="list-style: none;" name="show_type_1">
                            <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </page>

    <page size="A4" id="page3" style="display: none;">
        <div class="form-group">
            <div class="text-right" style="padding: 15px 15px 0 0"></div>
            <div class="col-md-8">
                <div class="col-md-12">
                    <ul style="padding: 0">
                        <li style="list-style: none; height: 22px">
                            <h3 style="margin: 0">
                                <img src="<%=  schoolData.sImage %>" style="width: 50px; height: 50px" /><b> <%= schoolData.sCompany %></b>
                            </h3>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 10px 0 0 50px;">
                            <p style="font-size: 14px">
                                &nbsp;
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%= string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%= string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%= string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                &nbsp; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%= schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                <br />
                                <%= schoolData.sPost %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132537") %> <%= schoolData.sPhoneOne %>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-4 text-right" style="font-size: 18px">
                <ul>
                    <li style="list-style: none; height: 27px">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>&nbsp;</label><span style="text-align: center; width: 150px; border: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.ReceiptNo %></span></li>
                    <li style="list-style: none; height: 27px">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>&nbsp;</label><span style="text-align: center; width: 150px; border: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.Date.ToString("dd/MM/yyyy") %></span></li>
                </ul>
            </div>
        </div>
        <div class="form-group" style="padding-top: 10px; margin: 0">
            <p class="text-center" style="font-size: 18px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603028") %></b></p>
        </div>
        <div class="form-group" style="padding: 0px 0 0 15px">
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;<span><%=Model.StudentName %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Name-Surname</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %>&nbsp;&nbsp;<span><%=Model.StudentIdCard %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Identification Number</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>&nbsp;&nbsp;<span><%=Model.StudentCode %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; height: 15px;"><b>Student Number</b></p>
            </div>
        </div>
        <div class="form-group" style="padding: 20px 0 0 15px">
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>&nbsp;&nbsp;<span><%=Model.nYear %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Academic Year</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>&nbsp;&nbsp;<span><%=Model.Term %></span></b></p>
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Semester</b></p>
            </div>
            <div class="col-md-4">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;">
                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>&nbsp;&nbsp;
                        <span class="Class-label-0"><%=( Model.Fd_NewTermSubLevel ?? Model.SubLevel ) + "/" + ( Model.Fd_NewTermSubLevel2 ?? Model.SubLevel2 )%></span>
                        <span class="Class-label-1"><%= ClassLabel %></span>
                    </b>
                </p>
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 15px;"><b>Class</b></p>
            </div>
        </div>
        <br />
        <div class="form-group" style="padding: 25px 20px 0 20px">
            <table style="width: 100%; font-size: 20px;" id="tb-products-3">
                <thead></thead>
                <tbody></tbody>
            </table>
        </div>

        <div class="form-group col-md-12">
            <div class="col-md-7" style="font-size: 18px">
                <ul>
                    <li class="text-left" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;">____________________________________</p>
                    </li>
                    <li class="text-left" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;" class="employees-name-3">(<span style="color: white;">___________________________________</span>)</p>
                    </li>
                </ul>
            </div>
            <div class="col-md-5 text-right" style="font-size: 22px; margin: 0 0 0 0;">
                <ul>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>____________________________________</p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px;" class="employees-name-2"></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_2">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_1">
                        <p style="margin: 0; font-size: 16px; border-bottom: 1px solid black;" class="employees-name-1"></p>
                    </li>
                    <li class="text-center" style="list-style: none;" name="show_type_1">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b></p>
                    </li>
                </ul>
            </div>
        </div>
    </page>

    <div id="pageTemp" class="hide">
    </div>

    <script>
        var _domain = "<%=HttpContext.Current.Request.Url.Authority%>/";
        var _invoice = <%=ModelJson%>;
        var _hideCheckbox = <%=HideCheckBox.ToString().ToLower()%>;
        var _paidPeriod = <%=PaidPeriod%>;
        var _totalAllPaid = <%=TotalAllPaid%>;
        var employees_name = '<%= Session["Emp_Name"].ToString() %>';
    </script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/pages/invoice-bill-preview.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHHmmss") %>"></script>

    <script type="text/javascript">
        $(function () {
            /// COOKIE Tax Option
            if (getCookie("TaxOption") == undefined) {
                document.cookie = "TaxOption = 0; expires = 2100/01/01";
            }

            $("#ddl-show-tax").val(getCookie("TaxOption"));
            if (getCookie("TaxOption") == "0") {
                $(".Tax-label").show();
            } else if (getCookie("TaxOption") == "1") {
                $(".Tax-label").hide();
            }

            $("#ddl-show-tax").change(function (e) {
                document.cookie = "TaxOption = " + $(this).val() + "; expires = 2100/01/01";
                if (getCookie("TaxOption") == "0") {
                    $(".Tax-label").show();
                } else if (getCookie("TaxOption") == "1") {
                    $(".Tax-label").hide();
                }
            });

            /// COOKIE Class Option
            if (getCookie("ClassOption") == undefined) {
                document.cookie = "ClassOption = 0; expires = 2100/01/01";
            }

            $("#ddl-show-class").val(getCookie("ClassOption"));
            if (getCookie("ClassOption") == "0") {
                $(".Class-label-0").show();
                $(".Class-label-1").hide();
            } else if (getCookie("ClassOption") == "1") {
                $(".Class-label-0").hide();
                $(".Class-label-1").show();
            }

            $("#ddl-show-class").change(function (e) {
                document.cookie = "ClassOption = " + $(this).val() + "; expires = 2100/01/01";
                $(".Class-label-0").hide();
                $(".Class-label-1").hide();
                if (getCookie("ClassOption") == "0") {
                    $(".Class-label-0").show();
                } else if (getCookie("ClassOption") == "1") {
                    $(".Class-label-1").show();
                }
            });
        })

        function getCookie(cookieName) {
            let cookie = {};
            document.cookie.split(';').forEach(function (el) {
                let [key, value] = el.split('=');
                cookie[key.trim()] = value;
            })
            return cookie[cookieName];
        }
    </script>
</asp:Content>
