<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp_notFrom.Master"
    CodeBehind="PrintPreview.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.PrintPreview" %>

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
    <script src="/Scripts/select2/select2.full.min.js"></script>
    <script src="/Scripts/moment.js"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />

    <div style="border: 1px solid black; padding: 10px; position: absolute; background: white; border-radius: 5px; right: 70px; width: 280px">
        <p class="text-center" style="font-size: 22px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %></b></p>
        <div class="form-group" style="padding-top: 20px">
            <label style="font-size: 20px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %></label>
            <select id="ddl-option" class="form-control" style="font-size: 20px">
                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132511") %></option>
                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132512") %></option>
            </select>
        </div>
        <div class="form-group" style="padding-top: 20px; text-align: center">
            <button type="button" class="btn btn-primary btn-lg" id="btnPrint"><span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
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

                .col-md-3 {
                    width: 25%;
                }

                .col-md-4 {
                    width: 33.33333333%;
                }


                .col-md-6 {
                    width: 50%;
                }

                .col-md-8 {
                    width: 66.66666667%;
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

                .page-break {
                    page-break-after: always;
                }

                table tbody tr {
                    line-height: 14px;
                }
            }
        </style>

        <div class="form-group">
            <div class="text-right" style="padding:10px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132053") %></div>
            <div class="col-md-6">
                <div class="col-md-12">
                    <ul style="padding:0">
                        <li style="list-style:none;height:22px">
                            <h3 style="margin:0"><img src="<%=  schoolData.sImage %>" style="width:50px;height:50px" /><b><%=  schoolData.sCompany %></b></h3>
                        </li>
                        <li style="list-style:none;height:22px;padding:5px 0 0 50px">
                            <p style="font-size:18px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-6 text-right" style="font-size:18px">
                <ul>
                    <li style="list-style:none;height:27px"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>&nbsp;</label><span style="text-align:center;width:150px;border:solid 1px black;padding:0 5px 0 5px;display: inline-block;"><%=Model.Code %></span></li>
                    <li style="list-style:none;height:27px"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>&nbsp;</label><span style="text-align:center;width:150px;border:solid 1px black;padding:0 5px 0 5px;display: inline-block;"><%=Model.Date.ToString("dd/MM/yyyy") %></span></li>
                </ul>
            </div>
        </div>
        <div class="form-group" style="padding-top:20px">
            <p class="text-center" style="font-size:18px;margin:0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504050") %></b></p>
            <p class="text-center" style="font-size:18px;margin:0"><b>RECEIPT</b></p>
        </div>
        <div class="form-group" style="padding:0px 0 0 15px">
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.StudentName %></span></b></p>
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b>Name-Surname</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.StudentIdCard %></span></b></p>
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b>Identification Number</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.StudentCode %></span></b></p>
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b>Student Number</b></p>
            </div>
        </div>
        <div class="form-group" style="padding:35px 0 0 15px">
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.nYear %></span></b></p>
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b>Academic Year</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.Term %></span></b></p>
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b>Semester</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.SubLevel+"/"+Model.SubLevel2 %></span></b></p>
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b>Class</b></p>
            </div>
        </div>
        
        <div class="form-group" style="padding:50px 5px 0 20px">
            <table style="width:100%" id="tb-products-1">
                <tr style="border:1px solid black;border-collapse: collapse">
                    <th style="border:1px solid black;border-collapse: collapse;width:50%;font-size:18px;padding:5px;">
                         <input type="checkbox"  class="chk-all" checked > 
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>
                    </th>
                    <th  style="text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503030") %>
                    </th>
                    <th  style="text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %>
                    </th>
                </tr>
            </table>
        </div>
        <hr />
<%--    </page>

    <page size="A4" id="page2">
          <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
        --%>
            <div class="text-right" style="padding:10px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132515") %></div>
        <div class="form-group">
            <div class="col-md-6">
                <div class="col-md-12">
                    <ul style="padding:0">
                        <li style="list-style:none;height:22px">
                            <h3 style="margin:0"><img src="<%=  schoolData.sImage %>" style="width:50px;height:50px" /><b><%=  schoolData.sCompany %></b></h3>
                        </li>
                        <li style="list-style:none;height:22px;padding:5px 0 0 50px">
                            <p style="font-size:18px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>                           
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-6 text-right" style="font-size:18px">
                <ul>
                    <li style="list-style:none;height:27px"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>&nbsp;</label><span style="text-align:center;width:150px;border:solid 1px black;padding:0 5px 0 5px;display: inline-block;"><%=Model.Code %></span></li>
                    <li style="list-style:none;height:27px"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>&nbsp;</label><span style="text-align:center;width:150px;border:solid 1px black;padding:0 5px 0 5px;display: inline-block;"><%=Model.Date.ToString("dd/MM/yyyy") %></span></li>
                </ul>
            </div>
        </div>
        
        <div class="form-group" style="padding-top:20px">
            <p class="text-center" style="font-size:18px;margin:0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504050") %></b></p>
            <p class="text-center" style="font-size:18px;margin:0"><b>RECEIPT</b></p>
        </div>
        <div class="form-group" style="padding:0px 0 0 15px">
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.StudentName %></span></b></p>
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b>Name-Surname</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106111") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.StudentIdCard %></span></b></p>
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b>Identification Number</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.StudentCode %></span></b></p>
            <p class="" style="font-size:18px;margin:0;height: 15px;"><b>Student Number</b></p>
            </div>
        </div>
        <div class="form-group" style="padding:35px 0 0 15px">
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.nYear %></span></b></p>
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b>Academic Year</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.Term %></span></b></p>
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b>Semester</b></p>
            </div>
            <div class="col-md-4">
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>&nbsp;&nbsp;<span class="blue-font"><%=Model.SubLevel+"/"+Model.SubLevel2 %></span></b></p>
            <p class="" style="font-size:18px;margin:0;padding:0;height: 15px;"><b>Class</b></p>
            </div>
        </div>
        <div class="form-group" style="padding:30px 20px 0 20px">
            <table style="width:100%" id="tb-products-2">
                <tr style="border:1px solid black;border-collapse: collapse">
                    <th style="border:1px solid black;border-collapse: collapse;width:50%;font-size:18px;padding:5px;">
                         <input type="checkbox"  class="chk-all" checked > 
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>
                    </th>
                    <th  style="text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503030") %>
                    </th>
                    <th  style="text-align:center;border:1px solid black;border-collapse: collapse;width:25%;font-size:18px;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501034") %>
                    </th>
                </tr>
            </table>
        </div>
    </page>

    <div id="pageTemp" class="hide">
    </div>

    <script>
        var _domain = "<%=HttpContext.Current.Request.Url.Authority%>/";
        var _invoice = <%=ModelJson%>;
    </script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <script src="/Scripts/pages/invoice-preview.js"></script>
</asp:Content>
