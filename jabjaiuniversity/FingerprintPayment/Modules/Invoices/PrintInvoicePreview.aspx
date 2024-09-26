<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp_notFrom.Master"
    CodeBehind="PrintInvoicePreview.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.PrintInvoicePreview" %>

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
    </style>
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <link href="/Content/custom/custom-sticky.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />

    <%--<ul class="jabjai-sticky-toolbar" style="margin-top: 30px;">
        <li class="jabjai-sticky-toolbar__item jabjaisticky-toolbar__item--demo-toggle" id="jabjai_panel_toggle" data-toggle="jabjaitooltip" title="" data-placement="right" data-original-title="Check out more demos">
            <a href="#" id="btnPrint" class="jabjai-toolbar-link" style="font-size: 20px; font-weight: bolder"><span class="glyphicon glyphicon-print" aria-hidden="true"></span>
                <br />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></a>
        </li>
        <li>
        </li>
    </ul>--%>
    <ul class="jabjai-sticky-toolbar" style="margin-top: 30px;">
        <li class="jabjai-sticky-toolbar__item jabjaisticky-toolbar__item--demo-toggle" id="jabjai_panel_toggle" data-toggle="jabjaitooltip" title="" data-placement="right" data-original-title="Check out more demos">
            <a href="#" class="jabjai-toolbar-link" style="font-size: 20px; font-weight: bolder"><span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                <br />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603032") %></a>
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
                    <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %></label>
                    <select id="ddl-option" class="form-control" style="font-size: 20px">
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></option>
                    </select>
                </div>
                <div class="form-group">
                    <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132534") %></label>
                    <select id="ddl-print-type" class="form-control" style="font-size: 20px">
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105026") %>แ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206574") %>ี่ 1</option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105026") %>แ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206574") %>ี่ 2</option>
                    </select>
                </div>
                <div class="form-group" style="padding-top: 20px; text-align: center">
                    <button type="button" class="btn btn-primary btn-lg btnPrint" id="btnPrint">
                        <span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                </div>

            </div>
        </div>
    </div>

    <page size="A4" id="page1">
        <style>
            @media print {
                .blue-font {
                    color: #3498db !important;
                }

                .col-md-1 {
                    width: 8.33333333%;
                }

                .col-md-2 {
                    width: 16.66666667%;
                }

                .col-md-3 {
                    width: 25%;
                }

                .col-md-4 {
                    width: 33.33333333%;
                }

                .col-md-5 {
                    width: 41.66666667%;
                }

                .col-md-6 {
                    width: 50%;
                }

                .col-md-7 {
                    width: 58.33333333%;
                }

                .col-md-8 {
                    width: 66.66666667%;
                }

                .col-md-11 {
                    width: 91.66666667%;
                }

                .col-md-12 {
                    width: 100%;
                }

                .col-md-1, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-10, .col-md-11, .col-md-12 {
                    float: left;
                }

                .col-xs-1, .col-sm-1, .col-md-1, .col-lg-1, .col-xs-2, .col-sm-2, .col-md-2, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-xs-4, .col-sm-4, .col-md-4, .col-lg-4, .col-xs-5, .col-sm-5, .col-md-5, .col-lg-5, .col-xs-6, .col-sm-6, .col-md-6, .col-lg-6, .col-xs-7, .col-sm-7, .col-md-7, .col-lg-7, .col-xs-8, .col-sm-8, .col-md-8, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-xs-10, .col-sm-10, .col-md-10, .col-lg-10, .col-xs-11, .col-sm-11, .col-md-11, .col-lg-11, .col-xs-12, .col-sm-12, .col-md-12, .col-lg-12 {
                    position: relative;
                    min-height: 1px;
                    padding-right: 15px;
                    padding-left: 15px;
                }

                .form-group {
                    margin-bottom: 15px;
                }

                table tbody tr {
                    line-height: 14px;
                }
            }

            .Barcode {
                /*      width: 304px;
                height: 50px;*/
            }

            .QRCode {
                width: 40px;
                height: 40px;
            }
        </style>

        <div class="print_type_0" style="display: none;">
            <div class="form-group">
                <div class="text-right" style="padding: 10px" id="full-text"></div>
                <div class="col-md-8">
                    <div class="col-md-12">
                        <img src="<%=  schoolData.sImage %>" style="width: 70px; height: 70px; position: absolute" />
                        <ul style="padding: 0 0 0 30px; margin-bottom: 20px;">
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 50px">
                                <p style="margin: 0; font-size: 18px"><%=  schoolData.sCompany %></p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
                                <p style="margin: 0; font-size: 14px"><%=  schoolData.sNameEN %></p>
                            </li>
                            <li style="list-style: none; height: 28px; padding: 0 0 0 50px">
                                <p style="font-size: 14px">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                    <br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                    <br />
                                    <%= schoolData.sPost %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132537") %> <%= schoolData.sPhoneOne %>
                                </p>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="form-group" style="padding-top: 20px">
                <p class="text-center col-md-12" style="font-size: 20px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132521") %>/Invoice</b></p>
            </div>
            <div class="form-group" style="padding: 0px 0 0 18px">
                <div class="col-md-5" style="padding: 0">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>/ Name-Surname : <span class=""><%=Model.StudentName %></span></b></p>
                </div>
                <div class="col-md-5" style="padding: 0; width: 39%">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>/Student Number : <span class=""><%=Model.StudentCode %></span></b></p>
                </div>
                <div class="col-md-2" style="padding: 0; width: 18%">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b>REF.1 <%= Model.StudentCode + "-" + Model.nYear + "-" + Model.Term %></b></p>

                </div>
            </div>
            <div class="form-group" style="padding: 35px 0 0 18px">
                <div class="col-md-5" style="padding: 0">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107037") %> : <span class=""><%=Model.nYear %>/<%= Model.Term %></span></b></p>
                </div>
                <div class="col-md-5" style="padding: 0; width: 39%">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 20px;">
                        <b class=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <span class=""><%=Model.SubLevel+"/"+Model.SubLevel2 %></span></b>
                    </p>
                </div>
                <div class="col-md-2" style="padding: 0; width: 18%">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b>REF.2 <%= Model.SubLevel + Model.SubLevel2 + Model.Term + Model.TermYear %></b></p>
                </div>
            </div>


            <div class="form-group print_type_1" style="padding-top: 20px">
                <div class="col-md-5" style="font-size: 20px; margin: 0; height: 24.6px;">
                    <b class="labelDueDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132538") %>/Issue Date : <%=Issue %></b>
                </div>
                <div class="col-md-5" style="font-size: 20px; margin: 0; height: 24.6px;">
                    <b class="labelDueDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%=DueDate %></b>
                </div>
                <div class="col-md-2" style="padding: 0; width: 18%">
                </div>
            </div>

            <div class="form-group" style="padding-top: 20px">
                <p class="text-left col-md-12" style="font-size: 20px; margin: 0; height: 24.6px;">
                    <b class="labelDueDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%=DueDate %></b>
                </p>
            </div>

            <div class="form-group" style="padding: 30px 40px">
                <table style="width: 100%" id="tb-products-1">
                    <tr style="border: 1px solid black; border-collapse: collapse">
                        <th style="border: 1px solid black; border-collapse: collapse; width: 7%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                        </th>
                        <th style="border: 1px solid black; border-collapse: collapse; width: 70%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>
                        </th>
                        <th style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 20%; font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503030") %>
                        </th>
                    </tr>
                </table>
            </div>
            <div style="border: 1px dotted black; margin: 0 50px">
                <img src="/Assets/images/cut-icon.png" width="20" style="position: absolute; margin-top: -8px" />
            </div>
            <div class="form-group col-md-12">
                <div class="text-right" style="padding: 10px"></div>
                <div class="col-md-6">
                    <div class="col-md-12">
                        <img src="<%=  schoolData.sImage %>" style="width: 70px; height: 70px; position: absolute" />
                        <ul style="padding: 0 0 0 30px">
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 50px">
                                <p style="margin: 0; font-size: 18px"><%=  schoolData.sCompany %></p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
                                <p style="margin: 0; font-size: 14px"><%=  schoolData.sNameEN %></p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
                                <p style="font-size: 14px; white-space: nowrap;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                    <br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                </p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px" class="">
                                <br />
                                <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132518") %> : <%=  schoolData.TaxId %></p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-6" style="font-size: 18px">
                    <ul>
                        <li style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 23px"><b style="text-decoration: underline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132524") %>/Pay In Slip</b></p>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 10px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132525") %>________________________</p>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 10px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>_______________________________</p>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-7">
                    <ul style="padding: 0">
                        <% foreach (var paymentMethodDTO in paymentMethod.Where(w => !string.IsNullOrEmpty(w.BankName) && w.IsDelete == false).OrderBy(o => o.PaymentMethodTypeId).Take(3).ToList())
                            {%>
                        <li style="list-style: none; height: 52px; padding-left: 20px">
                            <p style="margin: 0; font-size: 18px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132527") %> : <%= paymentMethodDTO.PaymentMethodName %>
                                <br />
                                <span style="font-size: 12px;">&#x26AA;</span> <%= paymentMethodDTO.BankName %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02779") %>  <%= paymentMethodDTO.BankNumber %>
                            </p>
                        </li>
                        <%}%>
                    </ul>
                </div>
                <div class="col-md-5" style="font-size: 18px; padding-right: 40px">
                    <ul style="padding: 5px; border: solid 1px black;">
                        <li style="list-style: none; height: 22px;">
                            <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> : <%=Model.StudentName %></p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %> : <%=Model.StudentCode %> </p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <%=Model.SubLevel+"/"+Model.SubLevel2 %> </p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="font-size: 17px" class="">REF.1 <%= Model.StudentCode + "-" + Model.nYear + "-" + Model.Term %> <b>REF.2 <%=Model.SubLevel + Model.SubLevel2 + Model.Term + Model.TermYear %></b></p>
                        </li>
                        <%--       <li style="list-style:none;height:22px;">
                            <p style="font-size:18px">REF.2 <%=REF2 %> </p>
                        </li>--%>
                    </ul>
                </div>
            </div>

            <div class="form-group" style="padding: 30px 40px 0px 40px">
                <table style="width: 100%;" class="">
                    <tr style="">
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132528") %>
                        </td>
                        <td class="col-md-6" style="font-size: 18px;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132529") %><%-- (60%)--%>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                        </td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=FirstPaid.ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                    <tr>
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;"></td>
                        <td class="col-md-6" style="font-size: 18px;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132530") %> <%--(40%)--%>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                        </td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=SecondPaid.ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                    <tr>
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132531") %>
                        </td>
                        <td class="col-md-6" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132523") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                        </td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=(FirstPaid+SecondPaid).ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr style="border: 1px solid black; border-collapse: collapse; height: 23px">
                        <td class="col-md-3" style="border: 1px solid black; border-collapse: collapse; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132532") %>
                        </td>
                        <td class="col-md-9" style="text-align: center; border: 1px solid black; border-collapse: collapse; font-size: 18px;"></td>
                    </tr>
                </table>
            </div>

            <div style="border: 0px solid black; border-collapse: collapse; padding: 0px 40px 0px 40px; margin-bottom: 0px;" class="print_type_1 form-group col-md-12">
                <div class="col-md-3" style="border: 0px solid black; border-collapse: collapse; padding: 5px;">
                    <img src="<%= invoiceImage.QRCode %>" alt="Barcode" class="text-center QRCode" />
                </div>
                <div class="col-md-9" style="text-align: center; border: 0px solid black; border-collapse: collapse;">
                    <img src="<%= invoiceImage.BarCode %>" alt="Barcode" class="Barcode" style="text-align: center;" />
                </div>
            </div>

            <div class="form-group col-md-12" style="margin-bottom: 0px;">
                <div class="col-md-7">
                    <div class="col-md-12">
                        <ul style="padding: 0">
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 0">
                                <p style="margin: 0; font-size: 18px">**<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132533") %>**</p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 0; height: 24.6px;">
                                <p style="margin: 0; font-size: 18px" class="labelDueDate">**<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%=DueDate %>**</p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-5 text-right" style="font-size: 18px">
                    <ul>
                        <li class="text-center" style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132519") %></b></p>
                        </li>
                        <li class="text-center" style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132520") %></b></p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="print_type_1" <%--style="display: none;"--%>>
            <div class="form-group">
                <div class="text-right" style="padding: 10px" id="full-text"></div>
                <div class="col-md-8">
                    <div class="col-md-12">
                        <img src="<%=  schoolData.sImage %>" style="width: 70px; height: 70px; position: absolute" />
                        <ul style="padding: 0 0 0 30px; margin-bottom: 20px;">
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 50px">
                                <p style="margin: 0; font-size: 18px"><%=  schoolData.sCompany %></p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
                                <p style="margin: 0; font-size: 14px"><%=  schoolData.sNameEN %></p>
                            </li>
                            <li style="list-style: none; height: 28px; padding: 0 0 0 50px">
                                <p style="font-size: 14px">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                    <br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                    <br />
                                    <%= schoolData.sPost %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132537") %> <%= schoolData.sPhoneOne %>
                                </p>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="form-group" style="padding-top: 20px">
                <p class="text-center col-md-12" style="font-size: 20px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132521") %>/Invoice</b></p>
            </div>
            <div class="form-group" style="padding: 0px 0 0 18px">
                <div class="col-md-5" style="padding: 0">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>/ Name-Surname : <span class=""><%=Model.StudentName %></span></b></p>
                </div>
                <div class="col-md-5" style="padding: 0; width: 39%">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b class=""><%= NameEng %></b></p>
                </div>
                <div class="col-md-2" style="padding: 0; width: 18%">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b>REF.1 <%=REF %></b></p>
                </div>
            </div>
            <div class="form-group" style="padding: 0px 0 0 18px">
                <div class="col-md-5" style="padding: 0">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>/Student Number : <span class=""><%=Model.StudentCode %></span></b></p>
                </div>
                <div class="col-md-5" style="padding: 0; width: 39%">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0;">
                        <b class=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <span class=""><%=Model.SubLevel+"/"+Model.SubLevel2 %></span></b>
                    </p>
                </div>
                <div class="col-md-2" style="padding: 0; width: 18%">
                    <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b>REF.2 <%= REF2 %></b></p>
                </div>
            </div>

            <div class="form-group" style="padding: 0px 0 0 18px">
                <div class="col-md-5" style="padding: 0">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107037") %> : <span class=""><%=Model.nYear  %>/<%= Model.Term %></span></b></p>
                </div>
                <div class="col-md-7" style="padding: 0;">
                    <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 20px;">
                        <b class=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <span class=""><%= ClassLabel %></span></b>
                    </p>
                </div>
            </div>


            <div class="form-group " style="padding: 0px 0 0 18px">
                <div class="col-md-10" style="font-size: 20px; margin: 0; height: 24.6px; padding: 0px;">
                    <b class="labelDueDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date :  <%=Issue %> - <%=DueDate %> </b>
                </div>
                <div class="col-md-2" style="padding: 0; width: 18%">
                </div>
            </div>

            <div class="form-group print_type_0" style="padding-top: 20px">
                <p class="text-left col-md-12" style="font-size: 20px; margin: 0; height: 24.6px;">
                    <b class="labelDueDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%=DueDate %></b>
                </p>
            </div>

            <div class="form-group" style="padding: 30px 40px; padding-bottom: 0px; margin-bottom: 5px;">
                <table style="width: 100%" id="tb-products-2">
                    <tr style="border: 1px solid black; border-collapse: collapse">
                        <th style="border: 1px solid black; border-collapse: collapse; width: 7%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                        </th>
                        <th style="border: 1px solid black; border-collapse: collapse; width: 70%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>
                        </th>
                        <th style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 20%; font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503030") %>
                        </th>
                    </tr>
                </table>
                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br />
                    - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132535") %> / Failure to remit payment will result in late payment charges.<br />
                    - <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132536") %> / Tuition Fees are not Refundable</span>
            </div>
            <div style="border: 1px dotted black; margin: 0px 50px">
                <img src="/Assets/images/cut-icon.png" width="20" style="position: absolute; margin-top: -8px" />
            </div>
            <div class="form-group col-md-12">
                <div class="text-right" style="padding: 5px"></div>
                <div class="col-md-6">
                    <div class="col-md-12">
                        <img src="<%=  schoolData.sImage %>" style="width: 70px; height: 70px; position: absolute" />
                        <ul style="padding: 0 0 0 30px">
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 50px">
                                <p style="margin: 0; font-size: 18px"><%=  schoolData.sCompany %></p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
                                <p style="margin: 0; font-size: 14px"><%=  schoolData.sNameEN %></p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
                                <p style="font-size: 14px; white-space: nowrap;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                    <br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                                </p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-6" style="font-size: 18px">
                    <ul>
                        <li style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 23px"><b style="text-decoration: underline"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132524") %>/Pay In Slip</b></p>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 10px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132525") %>________________________</p>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 10px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>_______________________________</p>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-7">
                    <ul style="padding: 0">
                        <% foreach (var paymentMethodDTO in paymentMethod.Where(w => !string.IsNullOrEmpty(w.BankName) && w.IsDelete == false).OrderBy(o => o.PaymentMethodTypeId).Take(3).ToList())
                            {%>
                        <li style="list-style: none; height: 52px; padding-left: 20px">
                            <p style="margin: 0; font-size: 18px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132527") %> : <%= paymentMethodDTO.PaymentMethodName %>
                                <br />
                                <span style="font-size: 12px;">&#x26AA;</span> <%= paymentMethodDTO.BankName %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02779") %>  <%= paymentMethodDTO.BankNumber %>
                            </p>
                        </li>
                        <%}%>
                    </ul>
                </div>
                <div class="col-md-5" style="font-size: 18px; padding-right: 40px">
                    <ul style="padding: 5px; border: solid 1px black;">
                        <li style="list-style: none; height: 22px;">
                            <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> : <%=Model.StudentName %></p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="margin: 0; font-size: 18px"><%= NameEng %></p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %> : <%=Model.StudentCode %> </p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <%=Model.SubLevel+"/"+Model.SubLevel2 %> </p>
                        </li>
                        <li style="list-style: none; height: 22px;">
                            <p style="font-size: 17px" class="print_type_0">REF.1 <%= Model.StudentCode + "-" + Model.nYear + "-" + Model.Term %> <b>REF.2 <%=Model.SubLevel + Model.SubLevel2 + Model.Term + Model.TermYear %></b></p>
                            <p style="font-size: 17px" class="print_type_1">REF.1 <%=REF %> <b>REF.2 <%=REF2 %></b></p>
                        </li>
                        <%--       <li style="list-style:none;height:22px;">
                            <p style="font-size:18px">REF.2 <%=REF2 %> </p>
                        </li>--%>
                    </ul>
                </div>
            </div>

            <div class="form-group" style="padding: 30px 40px 0px 40px; margin-bottom: 5px;">
                <table style="width: 100%; display: none;" class="print_type_0">
                    <tr style="">
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132528") %>
                        </td>
                        <td class="col-md-6" style="font-size: 18px;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132529") %><%-- (60%)--%>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                        </td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=FirstPaid.ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                    <tr>
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;"></td>
                        <td class="col-md-6" style="font-size: 18px;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132530") %> <%--(40%)--%>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                        </td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=SecondPaid.ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                    <tr>
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132531") %>
                        </td>
                        <td class="col-md-6" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132523") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                        </td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=(FirstPaid+SecondPaid).ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                </table>
                <table style="width: 100%" class="print_type_1">
                    <tr>
                        <td class="col-md-2" style="font-size: 18px; padding: 7px 0;"></td>
                        <td class="col-md-6" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132523") %></td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></td>
                        <td class="col-md-2 text-right" style="font-size: 18px;">
                            <%=(FirstPaid+SecondPaid).ToString("N2") %>
                        </td>
                        <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %> 
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr style="border: 1px solid black; border-collapse: collapse; height: 23px">
                        <td class="col-md-3" style="border: 1px solid black; border-collapse: collapse; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132532") %>
                        </td>
                        <td class="col-md-9" style="text-align: left; border: 1px solid black; border-collapse: collapse; font-size: 18px;" name="textTH"></td>
                    </tr>
                </table>
            </div>

            <div style="border: 0px solid black; border-collapse: collapse; padding: 0px 40px 0px 40px; margin-bottom: 0px;" class="print_type_1 form-group col-md-12">
                <div class="col-md-3" style="border: 0px solid black; border-collapse: collapse; padding: 5px;">
                    <img src="<%= invoiceImage.QRCode %>" alt="Barcode" class="text-center QRCode" />
                </div>
                <div class="col-md-9" style="text-align: center; border: 0px solid black; border-collapse: collapse;">
                    <img src="<%= invoiceImage.BarCode %>" alt="Barcode" class="Barcode" style="text-align: center;" />
                </div>
            </div>

            <div class="form-group col-md-12" style="margin-bottom: 0px;">
                <div class="col-md-7">
                    <div class="col-md-12">
                        <ul style="padding: 0">
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 0">
                                <p style="margin: 0; font-size: 18px">**<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132533") %>**</p>
                            </li>
                            <li style="list-style: none; height: 22px; padding: 5px 0 0 0; height: 24.6px;">
                                <p style="margin: 0; font-size: 18px" class="labelDueDate">**<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%= Issue %> - <%=DueDate %></p>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-5 text-right" style="font-size: 18px">
                    <ul>
                        <li class="text-center" style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132519") %></b></p>
                        </li>
                        <li class="text-center" style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 18px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132520") %></b></p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </page>

    <div id="pageTemp" class="hide">
    </div>

    <script type="text/javascript">
        var _domain = "<%=HttpContext.Current.Request.Url.Authority%>/";
        var _invoice = <%=ModelJson%>;
    </script>
    <script type="text/javascript" src="/Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-confirm.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.serializeObject.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.serializejson.js"></script>
    <script type="text/javascript" src="/Scripts/pages/invoice-invoice-preview.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>"></script>
    <script type="text/javascript" src="../../Scripts/thaibath.js"></script>

    <script type="text/javascript">
        $(function () {

            /// COOKIE PRINT TYPE
            if (getCookie("print_type") == undefined) {
                document.cookie = "print_type = 0; expires = 2100/01/01";
            }

            $("#ddl-print-type").val(getCookie("print_type"));
            OptionPrintType($("#ddl-print-type").val())
            console.log(getCookie("print_type"));

            $("#ddl-print-type").change(function (e) {
                document.cookie = "print_type = " + $(this).val() + "; expires = 2100/01/01";
                OptionPrintType($(this).val())
            });

            /// COOKIE OPTION
            if (getCookie("option") == undefined) {
                document.cookie = "option = 0; expires = 2100/01/01";
            }

            $("#ddl-option").val(getCookie("option"));
            if (getCookie("option") == "0") {
                $(".labelDueDate").show();
            } else if (getCookie("option") == "1") {
                $(".labelDueDate").hide();
            }

            $("#ddl-option").change(function (e) {
                document.cookie = "option = " + $(this).val() + "; expires = 2100/01/01";
            });

            $("[name=textTH]").html(ArabicNumberToText(<%=(FirstPaid+SecondPaid) %>));
            $("[name=textENG]").html("( " + ArabicNumberToTextENG(<%=(FirstPaid+SecondPaid) %>) + ")");
        })

        function OptionPrintType(print_type) {
            $(".print_type_0").hide();
            $(".print_type_1").hide();
            if (print_type == "0") {
                $(".print_type_0").show();
            } else if (print_type == "1") {
                $(".print_type_1").show();
            } else {
                $(".print_type_0").show();
            }
        }

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
