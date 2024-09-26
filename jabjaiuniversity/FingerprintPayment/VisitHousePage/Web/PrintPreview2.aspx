<%@ Page Title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01117") %>" Language="C#" AutoEventWireup="true" CodeBehind="PrintPreview2.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.PrintPreview2" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133550") %></title>
    <link rel="stylesheet" href="/styles/style-visithouse-print.css?v=<%=DateTime.Now.Ticks%>" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link href="/Content/Material/layout.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />
    <style>
        .print-btn {
            position: fixed;
            top: 50%;
            right: 10px;
            z-index: 4;
            border: 1px solid black;
            padding: 10px 15px;
            color: #fff !important;
            background-color: #2196F3 !important;
            text-align: center;
            cursor: pointer;
        }

        .photo {
            padding: 1px;
        }

        .input-with-dot {
            /* text-decoration:underline;
            text-decoration-style:dotted;*/
            border: 0px;
            border-bottom: 1px dotted;
            text-align: center;
        }

        @media print {
            .print-btn {
                display: none !important;
            }
        }

        .sub-page {
            height: 270mm !important;
        }
    </style>

</head>
<body>
    <div class="print-btn " onclick="window.print();">
        <span class="glyphicon glyphicon-print"></span>
        <br>
        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>      
    </div>
    <div class="book">
        <% foreach (var DataForm in DataForms)
            {  %>
        <div class="page">
            <div class="sub-page">
                <span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 1/5</span>
                <div class="photo">
                    <%if (!string.IsNullOrWhiteSpace(DataForm.Profile.Picture))
                        {  %>
                    <img src="<%=DataForm.Profile.Picture%>" style="width: 66px; border: 0px solid #000; height: 82px;" />
                    <% }
                        else
                        { %>
                    <p class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131283") %></p>
                    <p class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></p>
                    <% }  %>
                </div>
                <div class="text-center">
                    <span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></span>
                    <hr style="margin: 2px; border-top: 1px solid #000;">
                </div>
                <div style="padding: 10px 0 0 0;">
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %></span><input value="<%=SCHOOL_NAME %>" class="input-bottom-dot" style="width: 300px;"><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132163") %></span><input value="<%=SCHOOL_AREA %>" class="input-bottom-dot" style="width: 270px;">
                </div>
                <div style="padding: 10px 0 0 0;">
                    <span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00350") %></span>
                </div>
                <div>
                    <span class="tab-1" style="padding-right: 10px;">&#9726;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></span><span style="padding-left: 5px; padding-right: 5px;">&#9711;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02228") %></span>
                </div>
                <div style="margin-top: -8px;">
                    <span class="tab-1" style="padding-right: 10px;">&#9726;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></span><span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133477") %></span>
                </div>
                <div style="padding: 8px 0 0 0;">
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %></span><input class="input-bottom-dot" value="<%=DataForm.Profile.FullName%>" readonly style="width: 200px;">
                    <%--<span class="normal-text"></span><input class="input-bottom-dot" value="" readonly>--%>
                    <span class="normal-text "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></span><input class="input-bottom-dot" style="width: 80px;" value="<%=DataForm.Profile.NickName%>" readonly>
                    <span class="normal-text "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></span><input class="input-bottom-dot" style="width: 75px;" value="<%=DataForm.Profile.Level%>" readonly>
                    <span class="normal-text "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></span><input class="input-bottom-dot" style="width: 75px;" value="<%=DataForm.Profile.StudentNo%>" readonly>
                   
                </div>
                <div>
                  
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %></span><input class="input-bottom-dot" style="width: 120px;" value="<%=DataForm.Profile.Mobile%>" readonly>
                    <span class="normal-text ">Line</span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.LINE%>" readonly>
                    <span class="normal-text ">Facebook</span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.Facebook%>" readonly>                   
                </div>
                <div style="margin-top:1px ">
                    <% var cardid = DataForm.Profile.CID.Replace("-", ""); %>                  
                    <span class="normal-text  tab-1" style="padding-right: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131288") %></span><div class="card-number"><%= cardid.ElementAtOrDefault(0)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=cardid.ElementAtOrDefault(1)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(2)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(3)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(4)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=cardid.ElementAtOrDefault(5)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(6)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(7)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(8)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(9)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=cardid.ElementAtOrDefault(10)%></div>
                    <div class="card-number"><%=cardid.ElementAtOrDefault(11)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=cardid.ElementAtOrDefault(12)%></div>
                </div>
                <div style="padding-top: 5px;">
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133555") %></span><input class="input-bottom-dot" style="width: 185px;" value="<%=DataForm.Visit.Fullname%>" readonly><%--<span class="normal-text"></span><input class="input-bottom-dot" style="width: 125px;" value="" readonly>--%><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></span><input class="input-bottom-dot" style="width: 105px;" value="<%=DataForm.Visit.PhoneNumber%>" readonly><div class="container-radio">
                        <div class="radio-b">&#9711;</div>
                        <div class="radio-check-<%=(!DataForm.Visit.HaveParents == true ? ACTIVE : DEACTIVE)%>">&#10004;</div>
                    </div>
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></span>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131286") %></span><input class="input-bottom-dot" style="width: 95px;" value="<%= GetVisitRelation(DataForm.Visit.Relationship, DataForm.Visit.RelationshipOther)%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></span><input class="input-bottom-dot" style="width: 112px;" value="<%=GetVisitOccupation(DataForm.Visit.Occupation, DataForm.Visit.OccupationOther)%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131287") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=GetVisitHighestEducation(DataForm.Visit.HighestEducation)%>" readonly>
                </div>
                <%--  <div>
                    <% var parentId = (DataForm.Visit.IDCardNumber + "").Replace("-", ""); %>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131288") %></span>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(0)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=parentId.ElementAtOrDefault(1)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(2)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(3)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(4)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=parentId.ElementAtOrDefault(5)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(6)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(7)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(8)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(9)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=parentId.ElementAtOrDefault(10)%></div>
                    <div class="card-number"><%=parentId.ElementAtOrDefault(11)%></div>
                    <span class="card-number-s">-</span><div class="card-number"><%=parentId.ElementAtOrDefault(12)%></div>
                    <div class="container-radio">
                        <div class="radio-b">&#9711;</div>
                        <div class="radio-check-<%=(string.IsNullOrEmpty(DataForm.Visit.IDCardNumber) ? ACTIVE : DEACTIVE)%>">&#10004;</div>
                    </div>
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132140") %></span>
                </div>--%>
                <div class="tab-1" style="margin-top: -5px;">
                    <div class="container-radio">
                        <div class="radio-b">&#9711;</div>
                        <div class="radio-check-<%=(DataForm.Visit.WelfareRegistersPoor == true ? ACTIVE : DEACTIVE)%>">&#10004;</div>
                    </div>
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00376") %></span>
                </div>
                <div style="padding: 6px 0 0 0;">
                    <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133486") %></span>
                </div>
                <div>
                    <span class="normal-text tab-1">2.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305028") %> </span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 18%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.ResidentialHouse == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305029") %></span>
                            </td>
                            <td style="width: 15%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.ResidentialHouse == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.ResidentialHouse == 3 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305030") %></span>
                            </td>
                            <td style="width: 15%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.ResidentialHouse == 4 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %></span>
                            </td>
                            <td style="width: 35%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.ResidentialHouse == 5 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305031") %></span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.DormitoryLivingWith%>" readonly>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 56%;" colspan="5">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.ResidentialHouse == 99 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.ResidentialHouseOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305032") %> </span>
                </div>
                <div>
                    <span class="normal-text tab-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133488") %></span>
                </div>
                <div class="no-border tab-3">
                    <table>
                        <tr>
                            <td style="width: 18%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.OwnHome == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %></span>
                            </td>
                            <td style="width: 15%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.OwnHome == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206543") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.OwnHome == 3 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305036") %></span>
                            </td>
                            <td style="width: 15%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.OwnHome == 4 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305037") %></span>
                            </td>
                            <td style="width: 35%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.OwnHome == 5 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305038") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <span class="normal-text tab-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133489") %></span>
                </div>
                <div class="no-border tab-3">
                    <table>
                        <tr>
                            <td style="width: 20%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305041") %></span>
                            </td>
                            <td style="width: 20%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305042") %></span>
                            </td>
                            <td style="width: 20%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 3 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305043") %></span>
                            </td>
                            <td style="width: 40%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 99 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span>
                                <input class="input-bottom-dot" style="width: 120px;" value="<%=DataForm.Visit.CleanlinessOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <span class="normal-text tab-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133489") %></span>
                </div>
                <div class="no-border tab-3">
                    <table>
                        <tr>
                            <td style="width: 18%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305041") %></span>
                            </td>
                            <td style="width: 15%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305042") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 3 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305043") %></span>
                            </td>
                            <td style="width: 35%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Cleanliness == 99 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span>
                                <input class="input-bottom-dot" style="width: 120px;" value="<%=DataForm.Visit.CleanlinessOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133490") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.UtilitiesElectricity == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.UtilitiesElectricity == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></span>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133491") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.WaterForConsumption == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.WaterForConsumption == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></span>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133492") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Toilet == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.Toilet == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></span>
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <span class="normal-text tab-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133493") %></span>
                </div>
                <div class="tab-2">
                    <input class="input-bottom-dot" style="width: 98%; text-align: left;" value="<%=DataForm.Visit.LivingEnvironment %>" readonly>
                </div>

                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133494") %> </span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 40%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133495") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.StudentFamilyMembersAmount%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.StudentFamilyMembersMale%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.StudentFamilyMembersFemale%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 40%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133496") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.SiblingsBornSameParentsAmount%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.SiblingsBornSameParentsMale%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.SiblingsBornSameParentsFemale%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 40%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133497") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.SiblingsBornDifferentParentsAmount%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.SiblingsBornDifferentParentsMale%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.SiblingsBornDifferentParentsFemale%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 62%;">
                                <span class="normal-text">2.3.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00024") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.FamiliesNeedSpecialAssistance%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></span>
                            </td>
                            <td style="width: 22%;">
                                <input class="input-bottom-dot" style="width: 70px;" value="<%=DataForm.Visit.FamiliesNeedSpecialAssistanceTotal%>" readonly>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                            </td>

                        </tr>
                    </table>
                </div>

                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133499") %> </span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.FamilyRelationship == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305053") %></span>
                            </td>
                            <td style="width: 25%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.FamilyRelationship == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305054") %></span>
                            </td>
                            <td style="width: 25%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.FamilyRelationship == 3 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305055") %></span>
                            </td>
                            <td style="width: 25%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.FamilyRelationship == 4 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></span>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.FamilyRelationship == 5 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305057") %></span>
                            </td>
                            <td colspan="3">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.FamilyRelationship == 99 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.FamilyRelationshipOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133500") %></span>
                </div>
                <div class="tab-1">
                    <table>
                        <tr>
                            <th style="width: 50%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131292") %></th>
                            <th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></th>
                            <th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></th>
                            <th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></th>
                            <th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></th>
                            <th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></th>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></td>
                            <td class="text-center"><span><%=DataForm.visitHouseFatherRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseFatherRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseFatherRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseFatherRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseFatherRelationsLevel[4]%></span></td>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></td>
                            <td class="text-center"><span><%=DataForm.visitHouseMotherRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseMotherRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseMotherRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseMotherRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseMotherRelationsLevel[4]%></span></td>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305063") %></td>
                            <td class="text-center"><span><%=DataForm.visitHouseBrotherRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseBrotherRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseBrotherRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseBrotherRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseBrotherRelationsLevel[4]%></span></td>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305064") %></td>
                            <td class="text-center"><span><%=DataForm.visitHouseSistersRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseSistersRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseSistersRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseSistersRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseSistersRelationsLevel[4]%></span></td>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305066") %></td>
                            <td class="text-center"><span><%=DataForm.visitHouseGrandparentsRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseGrandparentsRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseGrandparentsRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseGrandparentsRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseGrandparentsRelationsLevel[4]%></span></td>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></td>
                            <td class="text-center"><span><%=DataForm.visitHouseRelativeRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseRelativeRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseRelativeRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseRelativeRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseRelativeRelationsLevel[4]%></span></td>
                        </tr>
                        <tr>
                            <td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %><input class="input-bottom-dot" value="<%=DataForm.visitHouseOtherRelationsLevelSpecify%>" readonly></td>
                            <td class="text-center"><span><%=DataForm.visitHouseOtherRelationsLevel[0]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseOtherRelationsLevel[1]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseOtherRelationsLevel[2]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseOtherRelationsLevel[3]%></span></td>
                            <td class="text-center"><span><%=DataForm.visitHouseOtherRelationsLevel[4]%></span></td>
                        </tr>
                    </table>

                </div>


            </div>
        </div>
        <div class="page">
            <div class="sub-page">
                <span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 2/5</span>
                <div class="text-center">
                    <span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></span>
                    <hr style="margin: 2px; border-top: 1px solid #000;">
                </div>
                <br />
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133501") %></span><input class="input-bottom-dot" value="<%=(DataForm.Visit.SpendTimeWithFamily.HasValue ? DataForm.Visit.SpendTimeWithFamily.Value.ToString("#") : "")%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131291") %></span>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133502") %>
                    </span>
                    <input class="input-bottom-dot" value="<%=(DataForm.Visit.WorkloadTheirFamilies)%>" readonly><span class="normal-text"></span>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133503") %>
                    </span>
                    <input class="input-bottom-dot" value="<%=(DataForm.Visit.LeisureActivities)%>" readonly><span class="normal-text"></span>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133504") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133522") %> </span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.LeaveStudent == 1 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.LeaveStudent == 2 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305077") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.LeaveStudent == 3 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131294") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.Visit.LeaveStudent == 99 ? ACTIVE : DEACTIVE%>">&#10004;</div>
                                </div>
                                <span class="normal-text">อื่น ๆ ระบุ</span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.LeaveStudentOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <span class="normal-text tab-1">2.10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01730") %></span><input class="input-bottom-dot" value="<%=GetVisitMedianHouseholdIncome(DataForm.Visit.MedianHouseholdIncome)%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305086") %></span><input class="input-bottom-dot" value="<%=GetVisitReceiveExpensesFrom(DataForm.Visit.ReceiveExpensesFrom, DataForm.Visit.ReceiveExpensesFromOther)%>" readonly>
                            </td>
                            <td style="width: 50%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131236") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=GetVisitStudentWorkIncome(DataForm.Visit.StudentWorkIncome, DataForm.Visit.StudentWorkIncomeOther)%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <span class="normal-text" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01734") %></span><input class="input-bottom-dot" value="<%=GetVisitDayIncome(DataForm.Visit.DailyIncome)%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                            </td>
                            <td style="width: 50%;">
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00816") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=GetVisitPaidComeDay(DataForm.Visit.PaidComeDay)%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133506") %></span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 16%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseParentWantAgencyHelp2[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305103") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseParentWantAgencyHelp2[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseParentWantAgencyHelp2[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204043") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseParentWantAgencyHelp2[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305106") %></span>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseParentWantAgencyHelp2[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133541") %></span><input class="input-bottom-dot" style="width: 275px;" value="<%=DataForm.Visit.ParentWantAgencyHelp2Other%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133507") %></span>
                </div>
                <div class="tab-1">
                    <input class="input-bottom-dot" style="width: 98%; text-align: left;" value="<%=DataForm.Visit.ParentConcerns %>" readonly>
                </div>

                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133508") %></span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 16%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHelpFromSchool[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></span>
                            </td>
                            <td style="width: 28%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHelpFromSchool[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00629") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHelpFromSchool[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305112") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHelpFromSchool[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span><input class="input-bottom-dot" style="width: 90px;" value="<%=DataForm.Visit.ParentWantSchoolsHelpOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div>
                    <span class="normal-text">4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01176") %></span>
                </div>
                <div>
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133509") %></span>
                </div>
                <div class="no-border tab-1">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHealth[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305117") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHealth[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305118") %></span>
                            </td>
                            <td style="width: 36%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHealth[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305119") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHealth[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305120") %></span>
                            </td>
                            <td style="width: 70%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHealth[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305121") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133510") %></span>
                </div>
                <div class="no-border tab-1" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01180") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00796") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01335") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305128") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305129") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305130") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[6]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305131") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[7]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305130") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[8]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305133") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[9]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305134") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseWelfare[10]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305135") %></span>
                            </td>
                            <td style="width: 50%;"></td>
                        </tr>
                    </table>
                </div>

                <div class="between-row">
                    <span class="normal-text tab-1">3.3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305137") %></span><input class="input-bottom-dot" style="width: 140px;" value="<%=GetVisitDistanceHomeToSchool(DataForm.Visit.DistanceHomeToSchool)%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305143") %></span>
                    <input class="input-bottom-dot" style="width: 160px;" value="<%=GetVisitTravelTime(DataForm.Visit.TravelTime)%>" readonly><%--<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131296") %> </span><input class="input-bottom-dot" style="width: 55px;" value="<%=DataForm.visitHouseTimeSchoolMinute%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></span>--%>
                </div>
                <%-- <div>
                    <span class="normal-text tab-1" style="padding-left: 35px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305143") %> </span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 25%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelTime[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305144") %></span>
                            </td>
                            <td style="width: 25%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelTime[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305145") %></span>
                            </td>
                            <td style="width: 25%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelTime[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305146") %></span>
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <div>
                    <span class="normal-text tab-1" style="padding-left: 35px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00091") %> </span>
                </div>
                <div class="no-border tab-2">
                    <table>
                        <tr>
                            <td style="width: 20%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131250") %></span>
                            </td>
                            <td style="width: 20%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131249") %></span>
                            </td>
                            <td style="width: 24%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305151") %></span>
                            </td>
                            <td style="width: 36%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305152") %></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305153") %></span>
                            </td>
                            <td>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305154") %></span>
                            </td>
                            <td>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[6]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305155") %></span>
                            </td>
                            <td>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseTravelMethod[7]%>">&#10004;</div>
                                </div>
                                <span class="normal-text">อื่น ๆ </span>
                                <input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.StudentTravelOther%>" readonly>
                            </td>
                        </tr>

                    </table>
                </div>

                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133512") %></span>
                </div>

                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseLivingConditions[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305158") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseLivingConditions[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305159") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseLivingConditions[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305160") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseLivingConditions[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02247") %></span>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseLivingConditions[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00302") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseLivingConditions[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>
                                    <input class="input-bottom-dot" value="<%=DataForm.Visit.LivingConditionsOther%>" readonly></span>
                            </td>
                        </tr>
                    </table>
                </div>


                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133513") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseStudentWorkFamily[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305165") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseStudentWorkFamily[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131238") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseStudentWorkFamily[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305167") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseStudentWorkFamily[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305168") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseStudentWorkFamily[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305169") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseStudentWorkFamily[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text">อื่น ระบุ</span><input class="input-bottom-dot" style="width: 190px;" value="<%=DataForm.Visit.StudentResponsibilitiesOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </div>
        <div class="page">
            <div class="sub-page">
                <span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 3/5</span>
                <div class="text-center">
                    <span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></span>
                    <hr style="margin: 2px; border-top: 1px solid #000;">
                </div>
                <br />
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133514") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305172") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305173") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305174") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01876") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305176") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305177") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[6]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305178") %></span>
                            </td>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[7]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305179") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 50%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseHobby[8]%>">&#10004;</div>
                                </div>
                                <span class="normal-text">อื่น ๆ ระบุ</span><input class="input-bottom-dot" style="width: 190px;" value="<%=DataForm.Visit.HobbiesOther%>" readonly>
                            </td>
                            <td style="width: 50%;"></td>
                        </tr>
                    </table>
                </div>
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133515") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSubstanceAbuseBehavior[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305184") %></span>
                            </td>
                            <td style="width: 36%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSubstanceAbuseBehavior[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131302") %></span>
                            </td>
                            <td style="width: 30%;"></td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSubstanceAbuseBehavior[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text">อยู่ในสภาพแวดดล้อมที่ใช้สารเสพติด</span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSubstanceAbuseBehavior[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305187") %></span>
                            </td>
                            <td style="width: 36%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSubstanceAbuseBehavior[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131299") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133516") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseViolenceBehavior[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305192") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseViolenceBehavior[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305193") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseViolenceBehavior[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305194") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseViolenceBehavior[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305195") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseViolenceBehavior[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305196") %></span>
                            </td>
                            <td style="width: 30%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseViolenceBehavior[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text">อื่น ๆ </span>
                                <input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.ViolentBehaviorOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133517") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 22%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSexualBehavior[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305200") %></span>
                            </td>
                            <td style="width: 55%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSexualBehavior[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305201") %></span>
                            </td>
                            <td style="width: 23%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSexualBehavior[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305202") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 22%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSexualBehavior[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305203") %></span>
                            </td>
                            <td style="width: 55%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSexualBehavior[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305204") %></span>
                            </td>
                            <td style="width: 23%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseSexualBehavior[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305205") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133518") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305209") %></span>
                            </td>
                            <td style="width: 39%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305210") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305211") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305212") %></span>
                            </td>
                            <td style="width: 39%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01609") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305213") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[6]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305214") %></span>
                            </td>
                            <td style="width: 39%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[7]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305215") %></span>
                            </td>
                            <td style="width: 33%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseGameAddiction[8]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span><input class="input-bottom-dot" style="width: 100px;" value="<%=DataForm.Visit.GameAddictionOther%>" readonly>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="between-row">
                    <span class="normal-text tab-1">3.11 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305218") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 40%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseInternetAccess[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305219") %></span>
                            </td>
                            <td style="width: 60%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseInternetAccess[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305220") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="between-row">
                    <span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133520") %></span>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 80%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseUsingElectronicTools[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305223") %></span>
                            </td>
                            <td style="width: 20%;"></td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-2" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 80%;">
                                <div class="container-checkbox">
                                    <div class="checkbox-b">&#9744;</div>
                                    <div class="checkbox-check-<%=DataForm.visitHouseUsingElectronicTools[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305224") %></span>
                            </td>
                            <td style="width: 20%;"></td>
                        </tr>
                    </table>
                </div>
                <%--<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 80%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=DataForm.visitHouseUsingElectronicTools[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131305") %></span>
							</td>
							<td style="width: 20%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 80%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=DataForm.visitHouseUsingElectronicTools[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133552") %></span>
							</td>
							<td style="width: 20%;">
							</td>
						</tr>
					</table>
				</div>--%>
                <br />
                <div style="padding: 10px 0 0 0;">
                    <span class="header-sub-title-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01158") %></span>
                </div>
                <div class="no-border tab-1" style="margin-top: -2px;">
                    <table>
                        <tr>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01221") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01225") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[4]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00835") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[5]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %></span>
                            </td>
                            <td style="width: 14%;"></td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -2px;">
                    <table>
                        <tr>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[6]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01058") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[7]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01862") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[8]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131272") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[9]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01457") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[10]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[11]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01458") %></span>
                            </td>
                            <td style="width: 14%;"></td>
                        </tr>
                    </table>
                </div>
                <div class="no-border tab-1" style="margin-top: -2px;">
                    <table>
                        <tr>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[12]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00771") %></span>
                            </td>
                            <td style="width: 14%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[13]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00935") %></span>
                            </td>
                            <td style="width: 16%;">
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseInformant[14]%>">&#10004;</div>
                                </div>
                                <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01325") %></span>
                            </td>
                            <td style="width: 14%;"></td>
                            <td style="width: 14%;"></td>
                            <td style="width: 14%;"></td>
                            <td style="width: 14%;"></td>
                        </tr>
                    </table>
                </div>
                <%--   <div class="no-border tab-1" style="margin-top: 18px;">
                    <table>
                        <tr>
                            <td style="width: 45%;"></td>
                            <td style="text-align: center;">
                                <span class="license-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131306") %></span>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td></td>
                            <td style="text-align: center; vertical-align: bottom;">
                                <span class="license-text">...................................................</span>
                            </td>
                        </tr>
                        <tr>
                            <td style=""></td>
                            <td style="text-align: center;">
                                <span class="license-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131307") %></span>
                            </td>
                        </tr>
                    </table>
                </div>--%>
            </div>
        </div>
        <div class="page">
            <div class="sub-page">
                <span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 4/5</span>

                <div class="text-center" style="margin-top: 22px;">
                    <span class="header-title-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131308") %> </span>
                </div>
                <div style="margin-top: 22px;">
                    <span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132139") %></span><input class="input-bottom-dot" style="width: 370px;" value="<%=DataForm.Profile.FullName%>" readonly>
                </div>
                <div class="no-border" style="margin-top: 5px;">
                    <table>
                        <tr>
                            <td style="width: 30%;">
                                <span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131309") %></span>
                            </td>
                            <td style="width: 70%;">
                                <%--<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span>--%>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseStyle[0]%>">&#10004;</div>
                                </div>
                                <span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131240") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 30%;"></td>
                            <td style="width: 70%;">
                                <%--<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span>--%>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseStyle[1]%>">&#10004;</div>
                                </div>
                                <span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 30%;"></td>
                            <td style="width: 70%;">
                                <%--<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span>--%>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseStyle[2]%>">&#10004;</div>
                                </div>
                                <span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131276") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border" style="margin-top: -5px;">
                    <table>
                        <tr>
                            <td style="width: 30%;"></td>
                            <td style="width: 70%;">
                                <%--<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span>--%>
                                <div class="container-radio">
                                    <div class="radio-b">&#9711;</div>
                                    <div class="radio-check-<%=DataForm.visitHouseStyle[3]%>">&#10004;</div>
                                </div>
                                <span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132141") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="no-border" style="margin-top: 2px;">
                    <table>
                        <tr>
                            <td style="width: 30%;"></td>
                            <td style="width: 70%;">
                                <span class="normal-text-2" style="padding-left: 24px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132142") %></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 30px; text-align: center;">
                    <span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131277") %></span>
                </div>
                <div style="margin-top: 2px; text-align: center;">
                    <div style="width: 645px; height: 320px; border: 1px solid #000; margin: 0 auto; display: inline-flex;">
                        <% if (DataForm.VisitFiles.Where(o => o.Type == 1).Count() > 1)
                            {  %>
                        <% foreach (var f in DataForm.VisitFiles.Where(o => o.Type == 1).Take(2))
                            { %>
                        <img src="<%=f.Path%>" style="height: 317px; width: 321px;" />
                        <% } %>
                        <% }
                            else if (DataForm.VisitFiles.Where(o => o.Type == 1).Count() > 0)
                            {  %>
                        <img src="<%=DataForm.VisitFiles.Where(o => o.Type == 1).FirstOrDefault().Path%>" style="height: 317px; width: 640px;" />
                        <% }
                            else
                            { %>
                        <img src="<%=DataForm.visitHousePhotosInsideHome%>" style="width: 94%; height: 315px; width: 640px;" />
                        <% }%>
                    </div>
                </div>
                <div style="margin-top: 18px; margin-bottom: 10px; text-align: center;">
                    <span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01753") %> 2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01304") %></span>
                </div>
                <div style="margin-top: 2px; text-align: center;">
                    <div style="width: 645px; height: 320px; border: 1px solid #000; margin: 0 auto; display: inline-flex;">
                        <% if (DataForm.VisitFiles.Where(o => o.Type == 2).Count() > 1)
                            {  %>
                        <% foreach (var f in DataForm.VisitFiles.Where(o => o.Type == 2).Take(2))
                            { %>
                        <img src="<%=f.Path%>" style="height: 317px; width: 321px;" />
                        <% } %>
                        <% }
                            else if (DataForm.VisitFiles.Where(o => o.Type == 2).Count() > 0)
                            {  %>
                        <img src="<%=DataForm.VisitFiles.Where(o => o.Type == 2).FirstOrDefault().Path%>" style="height: 317px; width: 640px;" />
                        <% }
                            else
                            { %>
                        <img src="<%=DataForm.visitHousePhotosInsideHome%>" style="width: 94%; height: 315px; width: 640px;" />
                        <% }
                        %>
                    </div>
                </div>
            </div>
        </div>
        <div class="page">
            <div class="sub-page">
                <span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 5/5</span>

                <%
                    var date = (DataForm.Visit.UpdatedDate ?? DateTime.Today)
                        .ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("th-TH")).Split(' ');
                %>
                <%--student--%>
                <div style="margin-top: 10px; text-align: center;">
                    <div style="width: 94%; height: 165px; border: 1px solid #000; display: inline-block; margin-top: 20px;">
                        <div class="no-border" style="margin-top: 12px; margin-left: 0px; text-align: center;">
                            <span class="normal-text-2" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00205") %></span>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <% if (!string.IsNullOrEmpty(DataForm.Visit.StudentSignature))
                                            {  %>
                                        <span style="position: relative; bottom: -10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133553") %></span>
                                        <img src="<%= DataForm.Visit.StudentSignature+"?v="+ DateTime.Now.Ticks %>" style="height: 50px; margin-left: 30px; image-renderingx: -webkit-optimize-contrast;" />
                                        <% }
                                            else
                                            {  %>
                                        <span class="normal-text-2" style="padding-left: 0;">
                                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133553") %></span>
                                            <input type="text" class="input-with-dot" style="width: 250px;" />
                                        </span>
                                        <% } %>
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;">(  
                                            <input type="text" class="input-with-dot" style="width: 285px;" value="<%= DataForm.Profile.FullName%>" />
                                            )</span>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>
                                            <input type="text" class="input-with-dot" style="width: 45px;" value="<%=date[0] %>" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>
                                            <input type="text" class="input-with-dot" style="width: 90px;" value="<%=date[1] %>" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>ศ
                                            <input type="text" class="input-with-dot" style="width: 60px;" value="<%=date[2] %>" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <%--parent--%>
                <div style="margin-top: 10px; text-align: center;">
                    <div style="width: 94%; height: 165px; border: 1px solid #000; display: inline-block;">
                        <div class="no-border" style="margin-top: 12px; margin-left: 0px; text-align: center;">
                            <span class="normal-text-2" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00205") %></span>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <% if (!string.IsNullOrEmpty(DataForm.Visit.ParentSignature))
                                            {  %>
                                        <span style="position: relative; bottom: -10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133554") %></span>
                                        <img src="<%= DataForm.Visit.ParentSignature+"?v="+ DateTime.Now.Ticks %>" style="height: 50px; margin-left: 25px; image-renderingx: -webkit-optimize-contrast;" />
                                        <% }
                                            else
                                            {  %>
                                        <span class="normal-text-2" style="padding-left: 0;">
                                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133554") %></span>
                                            <input type="text" class="input-with-dot" style="width: 250px;" />
                                        </span>
                                        <% } %>
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;">(  
                                            <input type="text" class="input-with-dot" style="width: 285px;" value="<%= DataForm.Visit.Fullname %>" />
                                            )</span>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>
                                             <input type="text" class="input-with-dot" style="width: 45px;" value="<%=date[0] %>" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>
                                            <input type="text" class="input-with-dot" style="width: 90px;" value="<%=date[1] %>" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>ศ
                                            <input type="text" class="input-with-dot" style="width: 60px;" value="<%=date[2] %>" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <%--teacher--%>
                <div style="margin-top: 10px; text-align: center;">
                    <div style="width: 94%; height: 190px; border: 1px solid #000; display: inline-block;">
                        <div class="no-border" style="margin-top: 12px; margin-left: 0px; text-align: center;">
                            <span class="normal-text-2" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00205") %></span>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <% if (!string.IsNullOrEmpty(DataForm.Visit.TeacherSignature))
                                            {  %>
                                        <span style="position: relative; bottom: -10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></span>
                                        <img src="<%= DataForm.Visit.TeacherSignature+"?v="+ DateTime.Now.Ticks %>" style="height: 50px; margin-left: 35px; image-renderingx: -webkit-optimize-contrast;" />
                                        <% }
                                            else
                                            {  %>
                                        <span class="normal-text-2" style="padding-left: 0;">
                                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></span>
                                            <input type="text" id="license1" class="input-with-dot" style="width: 250px;" />
                                        </span>
                                        <% } %>
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;">(  
                                            <input type="text" id="license2" class="input-with-dot" style="width: 285px;" value="<%= DataForm.TeacherName %>" />
                                            )</span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;">
                                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %> </span>
                                            <input type="text" id="position" class="input-with-dot" style="width: 242px;" valuex="<%= DataForm.TeacherPosition %>" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %>" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="no-border" style="margin-top: 7px; text-align: left;">
                            <table>
                                <tr>
                                    <td style="width: 22%;"></td>
                                    <td style="width: 70%;">
                                        <span class="normal-text-2" style="padding-left: 0;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>
                                            <input type="text" class="input-with-dot" style="width: 45px;" value="<%=date[0] %>" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>
                                            <input type="text" class="input-with-dot" style="width: 90px;" value="<%=date[1] %>" />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131008") %>ศ
                                            <input type="text" class="input-with-dot" style="width: 60px;" value="<%=date[2] %>" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div style="margin-top: 15px;">
                    <span class="normal-text" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131314") %></span>
                </div>
            </div>
        </div>
        <% }  %>
    </div>
</body>
</html>
