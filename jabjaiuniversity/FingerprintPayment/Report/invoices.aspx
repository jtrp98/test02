<%@ Page Title="" Language="C#" MasterPageFile="~/mp_notFrom.Master" AutoEventWireup="true"
    CodeBehind="invoices.aspx.cs" Inherits="FingerprintPayment.Report.invoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
                font-size: 18px;
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
    </style>
    <link href="/Content/select2/select2.min.css" rel="stylesheet" />
    <link href="/Content/custom/custom-sticky.css" rel="stylesheet" />
    <script src="/Scripts/select2/select2.full.min.js" type="text/javascript"></script>
    <script src="/Scripts/moment.js" type="text/javascript"></script>
    <link href="/Content/custom/custom-datatable.css" rel="stylesheet" />
    <link href="/Content/jquery-confirm.css" rel="stylesheet" />

    <script src="Script/invoices.js" type="text/javascript"></script>

    <script>
        $(function () {
            $('#studentname1').on('change', function () {
                document.getElementById("studentname1").defaultValue = $('#studentname1').val();
            });
            $('#studentCode1').on('change', function () {
                document.getElementById("studentCode1").defaultValue = $('#studentCode1').val();
            });
            $('#className1').on('change', function () {
                document.getElementById("className1").defaultValue = $('#className1').val();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <ul class="jabjai-sticky-toolbar" style="margin-top: 30px;">
        <li class="jabjai-sticky-toolbar__item jabjaisticky-toolbar__item--demo-toggle" id="jabjai_panel_toggle" data-toggle="jabjaitooltip" title="" data-placement="right" data-original-title="Check out more demos">
            <a href="#" class="jabjai-toolbar-link" style="font-size: 20px; font-weight: bolder"><span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                <br />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603032") %></a>
            <button type="button" class="btn btn-primary btn-lg  btnPrint" onclick="print();"><span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
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
                        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></option>
                    </select>
                </div>
                <div class="form-group" style="padding-top: 20px; text-align: center">
                    <button type="button" class="btn btn-primary btn-lg btnPrint" onclick="print();"><span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                </div>
            </div>
        </div>
    </div>

    <page size="A4" id="page1">
    <div id="page2">
        <div class="row" id="header-page">
            <div class="col-sm-12 col-md-12">
                <ul style="padding-top: 10px">
                    <li style="list-style: none; height: 22px">
                        <h3 style="margin: 0">
                            <img src="<%= schoolData.sImage %>" style="width: 50px; height: 50px" /><b> <%=  schoolData.sCompany %></b></h3>
                    </li>
                    <li style="list-style: none; height: 22px; padding: 10px 0 0 50px">
                        <p style="font-size: 14px">
                            &nbsp;
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133155") %> <%=string.IsNullOrEmpty(schoolData.TaxId) ? "-" : schoolData.TaxId %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> <%=string.IsNullOrEmpty(schoolData.sPhoneOne) ? "-" : schoolData.sPhoneOne %>
                        </p>
                    </li>
                </ul>
            </div>
           
        </div>

        <div class="row" style="padding-top: 10px; margin: 0">
            <p class="text-center" style="font-size: 18px; margin: 0"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603028") %></b></p>
        </div>

        <div class="row">
            <div class="col-md-8">&nbsp;</div>
            <div class="col-md-4">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %>&nbsp;</label>
                <span style="text-align: left; width: 78%; border-bottom: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.invoiceCode %></span>
            </div>
        </div>
    </div>

    <div class="row" style="padding: 0px 0 0 15px">
       
          <div class="col-md-4">
            <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %>&nbsp;&nbsp;</b><input  id="className1" type="text" style="border:0;text-align: left; width: 88%; border-bottom: solid 1px black !important; padding: 0 5px 0 5px; display: inline-block;" value="<%=Model.ClassName %>" />
        </div>
        <div class="col-md-4">
            <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>&nbsp;&nbsp;</b><input   id="studentCode1" type="text" style="border:0;text-align: left; width: 65%; border-bottom: solid 1px black !important; padding: 0 5px 0 5px; display: inline-block;" value="<%=Model.studentCode %>"  />
        </div>
   
      
        <div class="col-md-4">
            <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>&nbsp;&nbsp;</b><span style="text-align: left; width: 80%; border-bottom: solid 1px black; padding: 0 5px 0 5px; display: inline-block;"><%=Model.invoiceDay %></span>
        </div>
    </div>

    <div class="row" style="padding: 0px 0 0 15px">
        <div class="col-md-12">
            <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>&nbsp;&nbsp;</b>
            <input type="text"  id="studentname1" style="border:0;text-align: left; width: 91%; border-bottom: solid 1px black !important; padding: 0 5px 0 5px; display: inline-block;" value="<%=(Model.studentName+"").Trim() %>" />
        </div>
    </div>

    <div class="row" style="padding: 25px 20px 0 20px">
        <table style="width: 100%;" id="tb-products-2" class="table" >
            <thead>
                <tr>
                    <th class="center" style="border:solid 1px black;font-size:18px;width:10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %></th>
                    <th class="center" style="border:solid 1px black;font-size:18px;width:10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></th>
                    <th class="center" style="border:solid 1px black;font-size:18px;width:50%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></th>
                    <th class="center" style="border:solid 1px black;font-size:18px;width:15%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603029") %></th>
                    <th class="center" style="border:solid 1px black;font-size:18px;width:15%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501008") %></th>
                </tr>
            </thead>
            <tbody>
                  <%
                      decimal priceTotal = 0;
                      int dataIndex = 1;
                      foreach (var productData in Model.products)
                      {
                          priceTotal += productData.Amount * productData.Price;
                          %>
                <tr style="height:40px;">
                    <td class="center" style="border:solid 1px black;font-size:18px;"><%= dataIndex++ %></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"><%= productData.Amount %></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"><%= productData.Name %></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"><%= productData.Price%></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"><b><%=  productData.Amount * productData.Price%></b></td>
                </tr>
                <%}%>
                <% for (int i = Model.products.Count(); i < 12; i++)
                    {
                        if (Model.products.Count() == 0 && i == 0)
                        {
                            priceTotal = Model.TotalMoney ?? 0;
                            %>
                            <tr style="height:40px;">
                                <td class="center" style="border:solid 1px black;font-size:18px;">1</td>
                                <td class="center" style="border:solid 1px black;font-size:18px;">1</td>
                                <td class="center" style="border:solid 1px black;font-size:18px;">-</td>
                                <td class="center" style="border:solid 1px black;font-size:18px;"><%= priceTotal %></td>
                                <td class="center" style="border:solid 1px black;font-size:18px;"><%= priceTotal %></td>
                            </tr>
                <%}
                    else
                    { %>
                <tr style="height:40px;">
                    <td class="center" style="border:solid 1px black;font-size:18px;"></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"></td>
                    <td class="center" style="border:solid 1px black;font-size:18px;"></td>
                </tr>
                <% }%>
  
                <%}%>
                  <tr style="height:30px;">
                    <td class="right" style="font-size:14px;" colspan="4"><b style="font-size:18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603030") %></b></td>
                    <td class="center" style="border:solid 1px black;font-size:14px;"><b style="font-size:18px;"><%= priceTotal %></b></td>
                </tr>
            </tbody>
        </table>
    </div>

    <div class="row" style="padding: 5px 0 0 0">
        <div class="col-md-2 text-right" style="font-size: 18px">
            &nbsp;
        </div>
        <div class="col-md-8 text-center" style="font-size: 18px">
            <span style="margin: 0; font-size: 18px; border-bottom: 1px solid black; width:70%;display:inline-block;"><%= Model.employeesName %></span>
            <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603031") %></b>
        </div>
         <div class="col-md-2 text-right" style="font-size: 18px">
            &nbsp;
        </div>
    </div>
    </page>

    <div id="pageTemp" class="hide">
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
