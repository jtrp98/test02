<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mp_notFrom.Master"
    CodeBehind="PrintInvoiceGroupPreview.aspx.cs" Inherits="FingerprintPayment.Modules.Invoices.PrintInvoiceGroupPreview" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#btnPrint").click(function () {
                $("#pageTemp").html('');
                $.each($("page"), function (index, item) {
                    $(item).clone().appendTo("#pageTemp");
                });
                var contents = $("#pageTemp").html();
                var frame1 = document.createElement('iframe');
                frame1.name = "frame1";
                frame1.style.position = "absolute";
                frame1.style.top = "-1000000px";
                document.body.appendChild(frame1);
                var frameDoc = (frame1.contentWindow) ? frame1.contentWindow : (frame1.contentDocument.document) ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.document.open();
                frameDoc.document.write("<html><meta http-equiv='cache-control' content='no-cache'><head><title></title>");
                frameDoc.document.write("<link rel=\"stylesheet\" href=\"/bootstrap%20SB2/bower_components/bootstrap/dist/css/bootstrap.css\" type=\"text/css\"/>");
                frameDoc.document.write("<link rel=\"stylesheet\" href=\"/Content/print.css?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>\" type=\"text/css\"/>");
                frameDoc.document.write('<script type=\"text/javascript\" src=\"/Scripts/jquery-1.1<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>.min.js\" /><\/script>');
                frameDoc.document.write("</head><body>");
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    document.body.removeChild(frame1);
                }, 500);
                return false;
            });

            $(".jabjai-toolbar-link").click(function () {
                $(".jabjai-right-panel").addClass("jabjai-panel--on");
                return false;
            });

            $(".jabjai-panel__close").click(function () {
                $(".jabjai-right-panel").removeClass("jabjai-panel--on");
                return false;
            });

            $("#ddl-option").change(function () {
                if ($(this).val() === "0") {
                    $(".labelDueDate").show();
                } else {
                    $(".labelDueDate").hide();
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

</script>
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

    <ul class="jabjai-sticky-toolbar" style="margin-top: 30px;">
        <li class="jabjai-sticky-toolbar__item jabjaisticky-toolbar__item--demo-toggle" id="jabjai_panel_toggle" data-toggle="jabjaitooltip" title="" data-placement="right" data-original-title="Check out more demos">
            <a href="#" class="jabjai-toolbar-link" style="font-size: 20px; font-weight: bolder">
                <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                <br />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603032") %></a>
        </li>
    </ul>

    <%-- <ul class="jabjai-sticky-toolbar" style="margin-top: 30px;">
        <li class="jabjai-sticky-toolbar__item jabjaisticky-toolbar__item--demo-toggle" id="jabjai_panel_toggle" data-toggle="jabjaitooltip" title="" data-placement="right" data-original-title="Check out more demos">
            <a href="#" class="jabjai-toolbar-link" style="font-size: 20px; font-weight: bolder">
                <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                <br />
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603032") %></a>
        </li>
    </ul>--%>

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
                <div class="form-group" style="padding-top: 20px; text-align: center">
                    <button type="button" class="btn btn-primary btn-lg btnPrint" id="btnPrint">
                        <span class="glyphicon glyphicon-print"></span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                </div>
            </div>
        </div>
    </div>
    <% 
        var term = termLogic.GetAll().Result;
        foreach (var invoiceData in Model.Where(w => w.InvoiceStatus.ToLower() != "void" && w.OutstandingAmount > 0).OrderBy(o => o.StudentCode))
        {
            var f_term = term.FirstOrDefault(f => f.TermId.Trim() == invoiceData.TermId.Trim());
            invoiceData.nYear = f_term.Year.Number.Value;
            //FirstPaid = Convert.ToInt32(Math.Round(((decimal)invoiceData.GrandTotal * 60 / 100)));
            //SecondPaid = Convert.ToInt32(Math.Round(((decimal)invoiceData.GrandTotal * 40 / 100)));

            FirstPaid = ((decimal)invoiceData.GrandTotal * 60 / 100);
            SecondPaid = ((decimal)invoiceData.GrandTotal * 40 / 100);

            DueDate = dateTimeFormat(invoiceData.DueDate);

            var paidPayments = invoiceData.PaidPayments.Where(w => string.IsNullOrEmpty(w.Status)).Sum(s => s.Payments.Sum(s1 => s1.Amount)) ?? 0;
            if (FirstPaid >= paidPayments) FirstPaid -= paidPayments;
            else
            {
                SecondPaid -= (paidPayments - FirstPaid);
                FirstPaid = 0;
            }
    %>

    <page size="A4" class="group" style="width: 21cm; height: 29.7cm;">
        <div class="form-group">
            <div class="text-right" style="padding: 10px" id="full-text"></div>
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
                            <p style="font-size: 14px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <br />
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
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
                <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>/ Name-Surname : <span class=""><%=invoiceData.StudentName %></span></b></p>
            </div>
            <div class="col-md-5" style="padding: 0; width: 39%">
                <p class="" style="font-size: 18px; margin: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %>/Student Number : <span class=""><%=invoiceData.StudentCode %></span></b></p>
            </div>
            <div class="col-md-2" style="padding: 0; width: 18%">
                <p class="" style="font-size: 18px; margin: 0; height: 20px;">
                    <b>REF.1 <%= invoiceData.StudentCode + "-" + invoiceData.nYear + "-" + invoiceData.Term %></b>
                </p>
            </div>
        </div>
        <div class="form-group" style="padding: 35px 0 0 18px">
            <div class="col-md-5" style="padding: 0">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107037") %> : <span class=""><%=invoiceData.nYear %></span></b></p>
            </div>
            <div class="col-md-5" style="padding: 0; width: 39%">
                <p class="" style="font-size: 18px; margin: 0; padding: 0; height: 20px;"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <span class=""><%=invoiceData.SubLevel+"/"+invoiceData.SubLevel2 %></span></b></p>
            </div>
            <div class="col-md-2" style="padding: 0; width: 18%">
                <p class="" style="font-size: 18px; margin: 0; height: 20px;">
                    <b>REF.2 <%= invoiceData.SubLevel + invoiceData.SubLevel2 + invoiceData.Term + invoiceData.TermYear %></b>
                </p>
            </div>
        </div>

        <div class="form-group" style="padding-top: 20px">
            <p class="text-center col-md-12" style="font-size: 20px; margin: 0; height: 24.6px;">
                <b class="labelDueDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%=DueDate %></b>
            </p>
        </div>
        <div class="form-group" style="padding: 30px 40px">
            <table style="width: 100%" id="tb-products-1">
                <tr style="border: 1px solid black; border-collapse: collapse">
                    <th style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 7%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                    </th>
                    <th style="border: 1px solid black; border-collapse: collapse; width: 70%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %>
                    </th>
                    <th style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 20%; font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503030") %>
                    </th>

                </tr>
                <% 
                    int indexItme = 1;
                    decimal TotalAmount = 0;
                    foreach (var productData in invoiceData.InvoiceItems.Where(w => w.IsDelete == false))
                    {
                        TotalAmount += productData.GrandTotal ?? 0;
                %>
                <tr>
                    <td style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 7%; font-size: 18px; padding: 5px;">
                        <%=indexItme++ %>
                    </td>
                    <td style="border: 1px solid black; border-collapse: collapse; width: 70%; font-size: 18px; padding: 5px;">
                        <%=productData.ProductName %>
                    </td>
                    <td style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 20%; font-size: 18px;">
                        <%=string.Format("{0:#,#0.00}", productData.GrandTotal) %>
                    </td>
                </tr>

                <%}
                    if (invoiceData.ManualDiscount > 0)
                    {%>
                <tr>
                    <td style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 7%; font-size: 18px; padding: 5px;">
                        <%=indexItme++ %>
                    </td>
                    <td style="border: 1px solid black; border-collapse: collapse; width: 70%; font-size: 18px; padding: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503017") %>
                    </td>
                    <td style="text-align: center; border: 1px solid black; border-collapse: collapse; width: 20%; font-size: 18px;">
                        <%=string.Format("{0:#,#0.00}", invoiceData.ManualDiscount * -1) %>
                    </td>
                </tr>
                <%
                        TotalAmount -= invoiceData.ManualDiscount ?? 0;
                    }

                    for (int i = invoiceData.InvoiceItems.Count(); i <= 12; i++)
                    {%>
                <tr style="border: 1px solid black; border-collapse: collapse; height: 23px">
                    <td class="drag-icon ui-sortable-handle" style="font-size: 18px; padding: 5px;"></td>
                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: right;"></td>
                    <td style="border: 1px solid black; border-right: 1px solid black; border-collapse: collapse; padding: 5px; text-align: right;"></td>
                </tr>
                <%
                    }%>
                <tr style="border: 1px solid black; border-collapse: collapse">
                    <td class="drag-icon ui-sortable-handle" style="font-size: 18px; padding: 5px; text-align: right; text-align: right;"></td>
                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: right;"><span style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></span></td>
                    <td style="border: 1px solid black; border-right: 1px solid black; border-collapse: collapse; padding: 5px; text-align: center;">
                        <span class="product_grand_total" style="font-size: 18px;"><%=  string.Format("{0:#,#0.00}", TotalAmount) %></span></td>
                </tr>
                <tr style="border: 1px solid black; border-collapse: collapse">
                    <td class="drag-icon ui-sortable-handle" style="font-size: 18px; padding: 5px; text-align: right; text-align: right;"></td>
                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: right;"><span style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132522") %></span></td>
                    <td style="border: 1px solid black; border-right: 1px solid black; border-collapse: collapse; padding: 5px; text-align: center;">
                        <span class="product_grand_total" style="font-size: 18px;"><%= string.Format("{0:#,#0.00}", invoiceData.PaidPayments.Where(w=>(w.Status??"").ToLower() != "void").Sum(s=> s.Payments.Sum(s1=>s1.Amount))) %></span></td>
                </tr>
                <tr style="border: 1px solid black; border-collapse: collapse">
                    <td class="drag-icon ui-sortable-handle" style="font-size: 18px; padding: 5px; text-align: right; text-align: right;"></td>
                    <td style="border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: right;"><span style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132523") %></span></td>
                    <td style="border: 1px solid black; border-right: 1px solid black; border-collapse: collapse; padding: 5px; text-align: center;">
                        <span class="product_grand_total" style="font-size: 18px;"><%= string.Format("{0:#,#0.00}",invoiceData.OutstandingAmount) %></span></td>
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
                            <p style="font-size: 14px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> <%=string.IsNullOrEmpty(schoolData.sHomeNumber) ? "-" : schoolData.sHomeNumber %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> <%=string.IsNullOrEmpty(schoolData.sMuu) ? "-" : schoolData.sMuu %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> <%=string.IsNullOrEmpty(schoolData.sRoad) ? "-" : schoolData.sRoad %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> <%= string.IsNullOrEmpty(schoolData.sSoy) ? "-" :  schoolData.sSoy %>
                                <br />
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %> <%=  schoolData.sTumbon %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %> <%=  schoolData.sAumpher %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> <%=  schoolData.sProvince %>
                            </p>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 0 0 0 50px">
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
                    <li style="list-style: none; height: 52px; padding: 0 0 0 20px">
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
                        <p style="margin: 0; font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> : <%=invoiceData.StudentName %></p>
                    </li>
                    <li style="list-style: none; height: 22px;">
                        <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %> : <%=invoiceData.StudentCode %> </p>
                    </li>
                    <li style="list-style: none; height: 22px;">
                        <p style="font-size: 18px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>/Class : <%=invoiceData.SubLevel+"/"+invoiceData.SubLevel2 %> </p>
                    </li>
                    <li style="list-style: none; height: 22px;">
                        <p style="font-size: 18px">
                            <b>REF.1</b> <%= invoiceData.StudentCode + "-" + invoiceData.nYear + "-" + invoiceData.Term %>
                            <b>REF.2</b> <%= invoiceData.SubLevel + invoiceData.SubLevel2 + invoiceData.Term + invoiceData.TermYear %>
                        </p>
                    </li>
                </ul>
            </div>
        </div>

        <div class="form-group" style="padding: 30px 40px">
            <table style="width: 100%">
                <tr style="">
                    <td class="col-md-2" style="font-size: 18px; padding: 7px 0;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132528") %>
                    </td>
                    <td class="col-md-6" style="font-size: 18px;">☐ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132529") %> <%--(60%)--%>
                    </td>
                    <td class="col-md-1" style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> 
                    </td>
                    <td class="col-md-2 text-right" style="font-size: 18px;">
                        <%= FirstPaid.ToString("N2") %>
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
        <div class="form-group col-md-12">
            <div class="col-md-7">
                <div class="col-md-12">
                    <ul style="padding: 0">
                        <li style="list-style: none; height: 22px; padding: 5px 0 0 0">
                            <p style="margin: 0; font-size: 16px">**<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132533") %>**</p>
                        </li>
                        <li style="list-style: none; height: 22px; padding: 5px 0 0 0; height: 24.6px;">
                            <p style="margin: 0; font-size: 16px" class="labelDueDate">**<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132517") %>/Payment Due Date : <%=DueDate %>**</p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-5 text-right" style="font-size: 18px">
                <ul>
                    <li class="text-center" style="list-style: none; height: 22px; padding: 5px 0 0 0">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132519") %></b></p>
                    </li>
                    <li class="text-center" style="list-style: none; height: 22px; padding: 5px 0 0 0">
                        <p style="margin: 0; font-size: 16px"><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132520") %></b></p>
                    </li>
                </ul>
            </div>
        </div>

    </page>

    <%}%>

    <div id="pageTemp" class="">
    </div>

    <script>
        var _domain = "<%=HttpContext.Current.Request.Url.Authority%>/";
        var _invoice = <%=ModelJson%>;
    </script>
    <script src="/Scripts/jquery.blockUI.js"></script>
    <script src="/Scripts/jquery-confirm.js"></script>
    <script src="/Scripts/jquery.serializeObject.js"></script>
    <script src="/Scripts/jquery.serializejson.js"></script>
    <%--<script src="/Scripts/pages/invoice-invoice-preview.js"></script>--%>
</asp:Content>
