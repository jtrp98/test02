<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="VisitHouseForm.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.VisitHouseForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/Content/VisitHouse/assets/css/material.css">
    <link rel="stylesheet" href="/Content/VisitHouse/assets/css/image-uploader.css?v=<%=DateTime.Now.Ticks%>">
    <link rel="stylesheet" href="/Content/VisitHouse/assets/css/signature-pad.css">
    <link rel="stylesheet" href="/Content/VisitHouse/assets/css/sweetAlert.css">
    <style>
        input[type=checkbox], input[type=radio] {
            /* Double-sized Checkboxes */
            -ms-transform: scale(1.5); /* IE */
            -moz-transform: scale(1.5); /* FF */
            -webkit-transform: scale(1.5); /* Safari and Chrome */
            -o-transform: scale(1.5); /* Opera */
            transform: scale(1.5);
        }

        .accordion-choice label {
            width: fit-content;
        }

        .form-control::placeholder {
            opacity: 0.5;
            color: #808080;
            font-size: 18px;
        }

        .member-amount {
            width: 66% !important;
        }
            .member-amount .span-label {
                flex-basis: 125px;
                text-align: right;
            }
            .member-amount input {
                margin: -12px 7px 0px 7px;
                padding: 9px 0px 1px 0px;
                height: 38px;
            }
                .member-amount input::placeholder {
                    opacity: 0.5;
                    color: #808080;
                    font-size: 14px;
                }

        .special-assistance {
            margin: -12px 7px 0px 7px;
            padding: 9px 0px 1px 0px;
            height: 38px;
        }
            .special-assistance::placeholder {
                opacity: 0.5;
                color: #808080;
                font-size: 14px;
            }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content contents">
        <div class="title-bar" style="display: none;"></div>
        <div class="user profile">
            <div class="d-flex align-items-center justify-content-between">
                <div class="w-100 d-flex align-items-center px-4">
                    <img src="<%=StudentPicture%>" alt="user" style="width: 50px; height: 50px; border-radius: 50%;">
                    <div>
                        <p><%=StudentCode%></p>
                        <p><%=StudentName%></p>
                        <p><%=StudentClass%></p>
                    </div>
                </div>
            </div>
            <%--<button type="button" class="btn btn-success btn-round px-3 save-homevisit-btn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>--%>
            <div style="width: inherit;">
                <div class="w-100 d-flex px-4">
                    <div class="w-100" style="margin-left: 50px;">
                        <div class="user-social-media"><label style="width: 66px;">LINE</label><input id="iptLINE" name="iptLINE" style="width: 70%;" maxlength="100" /></div>
                        <div class="user-social-media"><label style="width: 66px;">Facebook</label><input id="iptFacebook" name="iptFacebook" style="width: 70%;" maxlength="200" /></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="d-flex align-items-center">
                <div class="accordion-choice">
                    <input type="radio" id="rdoStatusNotFound3" name="rdoStatusNotFound" value="3">
                    <label for="rdoStatusNotFound3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305005") %></label>
                </div>
            </div>
        </div>
        <div id="accordion">
            <div class="card">
                <div class="card-header">
                    <a class="card-link" data-toggle="collapse" href="#collapse1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133479") %>
                    </a>
                </div>
                <div id="collapse1" class="collapse" data-parent="#accordion">
                    <div class="card-body" style="box-shadow: none; padding-bottom: 7px;">
                        <h3 class="border-bottom w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00350") %></h3>
                        <div class="align-items-center mb-2">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></label>
                            <input type="checkbox" class="checkbox-round mx-2" disabled>
                            <label style="display: contents"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02228") %></label>
                        </div>
                        <div class="align-items-center">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></label>
                            <input type="checkbox" class="checkbox-square mx-2" disabled>
                            <label style="display: contents"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133477") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="d-flex align-items-center">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoHaveParents1" name="rdoHaveParents" value="1">
                                <label for="rdoHaveParents1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01338") %></label>
                            </div>
                            <div class="accordion-choice ml-3">
                                <input type="radio" id="rdoHaveParents0" name="rdoHaveParents" value="0">
                                <label for="rdoHaveParents0" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131285") %></label>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133480") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoRelationship1" name="rdoRelationship" value="1" />
                            <label for="rdoRelationship1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoRelationship2" name="rdoRelationship" value="2" />
                            <label for="rdoRelationship2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoRelationship3" name="rdoRelationship" value="3" />
                            <label for="rdoRelationship3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133478") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoRelationship4" name="rdoRelationship" value="4" />
                            <label for="rdoRelationship4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoRelationship99" name="rdoRelationship" value="99" data-element-other-id="tarRelationshipOther" />
                            <label for="rdoRelationship99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarRelationshipOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <div>
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133481") %></p>
                            <input id="iptFullname" name="iptFullname" type="text" class="form-control border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="100">
                        </div>
                        <div class="mt-3">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133482") %></p>
                            <input id="iptPhoneNumber" name="iptPhoneNumber" type="text" class="form-control border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="20">
                        </div>
                        <div class="mt-3 d-none">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133483") %></p>
                            <input id="iptIDCardNumber" type="text" class="form-control border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">1.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation1" name="rdoOccupation" value="1" />
                            <label for="rdoOccupation1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305090") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation2" name="rdoOccupation" value="2" />
                            <label for="rdoOccupation2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01168") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation3" name="rdoOccupation" value="3" />
                            <label for="rdoOccupation3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01169") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation4" name="rdoOccupation" value="4" />
                            <label for="rdoOccupation4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00128") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation5" name="rdoOccupation" value="5" />
                            <label for="rdoOccupation5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00228") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation6" name="rdoOccupation" value="6" />
                            <label for="rdoOccupation6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01594") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOccupation99" name="rdoOccupation" value="99" data-element-other-id="tarOccupationOther" />
                            <label for="rdoOccupation99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarOccupationOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">1.5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131287") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation1" name="rdoHighestEducation" value="1" />
                            <label for="rdoHighestEducation1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation2" name="rdoHighestEducation" value="2" />
                            <label for="rdoHighestEducation2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation3" name="rdoHighestEducation" value="3" />
                            <label for="rdoHighestEducation3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133524") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation4" name="rdoHighestEducation" value="4" />
                            <label for="rdoHighestEducation4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133526") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation5" name="rdoHighestEducation" value="5" />
                            <label for="rdoHighestEducation5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133525") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation6" name="rdoHighestEducation" value="6" />
                            <label for="rdoHighestEducation6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHighestEducation7" name="rdoHighestEducation" value="7" />
                            <label for="rdoHighestEducation7" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133527") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="d-flex align-items-center">
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkWelfareRegistersPoor">
                                <label for="chkWelfareRegistersPoor" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133528") %></label>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133486") %>
                    </a>
                </div>
                <div id="collapse2" class="collapse" data-parent="#accordion">
                    <%--2.1--%>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133487") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoResidentialHouse1" name="rdoResidentialHouse" value="1" />
                            <label for="rdoResidentialHouse1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305029") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoResidentialHouse2" name="rdoResidentialHouse" value="2" />
                            <label for="rdoResidentialHouse2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoResidentialHouse3" name="rdoResidentialHouse" value="3" />
                            <label for="rdoResidentialHouse3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305030") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoResidentialHouse4" name="rdoResidentialHouse" value="4" />
                            <label for="rdoResidentialHouse4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoResidentialHouse5" name="rdoResidentialHouse" value="5" data-element-other-id="tarDormitoryLivingWith" />
                            <label for="rdoResidentialHouse5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305031") %></label>
                        </div>
                        <textarea id="tarDormitoryLivingWith" rows="2" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                        <div class="accordion-choice mt-3">
                            <input type="radio" id="rdoResidentialHouse99" name="rdoResidentialHouse" value="99" data-element-other-id="tarResidentialHouseOther" />
                            <label for="rdoResidentialHouse99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarResidentialHouseOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <%--2.2--%>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305032") %></p>
                        <p class="mt-3 pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133488") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOwnHome1" name="rdoOwnHome" value="1" />
                            <label for="rdoOwnHome1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOwnHome2" name="rdoOwnHome" value="2" />
                            <label for="rdoOwnHome2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206543") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOwnHome3" name="rdoOwnHome" value="3" />
                            <label for="rdoOwnHome3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305036") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOwnHome4" name="rdoOwnHome" value="4" />
                            <label for="rdoOwnHome4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305037") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOwnHome5" name="rdoOwnHome" value="5" />
                            <label for="rdoOwnHome5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305038") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133489") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoCleanliness1" name="rdoCleanliness" value="1" />
                            <label for="rdoCleanliness1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305041") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoCleanliness2" name="rdoCleanliness" value="2" />
                            <label for="rdoCleanliness2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305042") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoCleanliness3" name="rdoCleanliness" value="3" />
                            <label for="rdoCleanliness3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305043") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoCleanliness99" name="rdoCleanliness" value="99" data-element-other-id="tarCleanlinessOther" />
                            <label for="rdoCleanliness99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarCleanlinessOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133490") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoUtilitiesElectricity1" name="rdoUtilitiesElectricity" value="1" />
                            <label for="rdoUtilitiesElectricity1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoUtilitiesElectricity2" name="rdoUtilitiesElectricity" value="0" />
                            <label for="rdoUtilitiesElectricity2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                        </div>
                        <p class="mt-3 pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133491") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoWaterForConsumption1" name="rdoWaterForConsumption" value="1" />
                            <label for="rdoWaterForConsumption1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoWaterForConsumption2" name="rdoWaterForConsumption" value="0" />
                            <label for="rdoWaterForConsumption2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                        </div>
                        <p class="mt-3 pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133492") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoToilet1" name="rdoToilet" value="1" />
                            <label for="rdoToilet1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoToilet2" name="rdoToilet" value="0" />
                            <label for="rdoToilet2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133493") %></p>
                        <textarea id="tarLivingEnvironment" name="tarLivingEnvironment" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="300"></textarea>
                    </div>
                    <%--2.3--%>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133494") %></p>
                        <p class="mt-3 pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133495") %></p>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-1">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                            <input type="text" id="iptStudentFamilyMembersAmount" name="iptStudentFamilyMembersAmount" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-1">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                            <input type="text" id="iptStudentFamilyMembersMale" name="iptStudentFamilyMembersMale" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-1">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                            <input type="text" id="iptStudentFamilyMembersFemale" name="iptStudentFamilyMembersFemale" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <p class="mt-3 pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133496") %></p>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-2">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                            <input type="text" id="iptSiblingsBornSameParentsAmount" name="iptSiblingsBornSameParentsAmount" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-2">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                            <input type="text" id="iptSiblingsBornSameParentsMale" name="iptSiblingsBornSameParentsMale" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-2">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                            <input type="text" id="iptSiblingsBornSameParentsFemale" name="iptSiblingsBornSameParentsFemale" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <p class="mt-3 pb-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133497") %></p>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-3">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></span>
                            <input type="text" id="iptSiblingsBornDifferentParentsAmount" name="iptSiblingsBornDifferentParentsAmount" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-3">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                            <input type="text" id="iptSiblingsBornDifferentParentsMale" name="iptSiblingsBornDifferentParentsMale" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount cal-group-3">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                            <input type="text" id="iptSiblingsBornDifferentParentsFemale" name="iptSiblingsBornDifferentParentsFemale" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                        <div class="d-flex mt-3 pb-2" style="flex-wrap: wrap;">
                            <span style="line-height: 2.1rem;">2.3.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00024") %> <input type="text" id="iptFamiliesNeedSpecialAssistance" name="iptFamiliesNeedSpecialAssistance" class="special-assistance text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>"></span>
                        </div>
                        <div class="d-flex mt-2 px-2 member-amount">
                            <span class="span-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></span>
                            <input type="text" id="iptFamiliesNeedSpecialAssistanceTotal" name="iptFamiliesNeedSpecialAssistanceTotal" class="form-control text-center border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>">
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></span>
                        </div>
                    </div>
                    <%--2.4--%>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133499") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship1" name="rdoFamilyRelationship" value="1" />
                            <label for="rdoFamilyRelationship1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305053") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship2" name="rdoFamilyRelationship" value="2" />
                            <label for="rdoFamilyRelationship2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305054") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship3" name="rdoFamilyRelationship" value="3" />
                            <label for="rdoFamilyRelationship3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305055") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship4" name="rdoFamilyRelationship" value="4" />
                            <label for="rdoFamilyRelationship4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship5" name="rdoFamilyRelationship" value="5" />
                            <label for="rdoFamilyRelationship5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305057") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship6" name="rdoFamilyRelationship" value="6" />
                            <label for="rdoFamilyRelationship6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133529") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoFamilyRelationship99" name="rdoFamilyRelationship" value="99" data-element-other-id="tarFamilyRelationshipOther" />
                            <label for="rdoFamilyRelationship99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarFamilyRelationshipOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="300"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133500") %></p>
                        <div class="match-block">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></p>
                            <button class="match-choose relation-member" data-member="1" data-relation-level="" data-toggle="modal" data-target=".relationLevelModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                        <div class="match-block">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></p>
                            <button class="match-choose relation-member" data-member="2" data-relation-level="" data-toggle="modal" data-target=".relationLevelModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                        <div class="match-block">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305063") %></p>
                            <button class="match-choose relation-member" data-member="3" data-relation-level="" data-toggle="modal" data-target=".relationLevelModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                        <div class="match-block">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305064") %></p>
                            <button class="match-choose relation-member" data-member="4" data-relation-level="" data-toggle="modal" data-target=".relationLevelModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                        <div class="match-block">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305066") %></p>
                            <button class="match-choose relation-member" data-member="5" data-relation-level="" data-toggle="modal" data-target=".relationLevelModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                        <div class="match-block">
                            <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></p>
                            <button class="match-choose relation-member" data-member="6" data-relation-level="" data-toggle="modal" data-target=".relationLevelModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133501") %></p>
                        <input type="text" id="iptSpendTimeWithFamily" class="form-control text-center mt-5 border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" style="display: none;">
                        <select id="sltSpendTimeWithFamily" name="sltSpendTimeWithFamily" style="margin-top: 10px; width: 100%; text-align: center; border: unset; border-bottom: 1px solid var(--yellow); outline-color: var(--yellow); color: var(--gray);">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                            <option value="0">0</option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                            <option value="13">13</option>
                            <option value="14">14</option>
                            <option value="15">15</option>
                            <option value="16">16</option>
                            <option value="17">17</option>
                            <option value="18">18</option>
                            <option value="19">19</option>
                            <option value="20">20</option>
                            <option value="21">21</option>
                            <option value="22">22</option>
                            <option value="23">23</option>
                            <option value="24">24</option>
                        </select>
                    </div>
                    <%--2.7--%>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133502") %></p>
                        <input type="text" id="iptWorkloadTheirFamilies" name="iptWorkloadTheirFamilies" class="form-control text-center mt-5 border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="300">
                    </div>
                    <%--2.8--%>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133503") %></p>
                        <input type="text" id="iptLeisureActivities" name="iptLeisureActivities" class="form-control text-center mt-5 border-0 border-bottom-red" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="300">
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133504") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133522") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoLeaveStudent1" name="rdoLeaveStudent" value="1" />
                            <label for="rdoLeaveStudent1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoLeaveStudent2" name="rdoLeaveStudent" value="2" />
                            <label for="rdoLeaveStudent2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305077") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoLeaveStudent3" name="rdoLeaveStudent" value="3" />
                            <label for="rdoLeaveStudent3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131294") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoLeaveStudent99" name="rdoLeaveStudent" value="99" data-element-other-id="tarLeaveStudentOther" />
                            <label for="rdoLeaveStudent99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarLeaveStudentOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133505") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMedianHouseholdIncome1" name="rdoMedianHouseholdIncome" value="1" />
                            <label for="rdoMedianHouseholdIncome1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 15,000</label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMedianHouseholdIncome2" name="rdoMedianHouseholdIncome" value="2" />
                            <label for="rdoMedianHouseholdIncome2" class="mb-0 ml-2">15,000 - 20,000</label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMedianHouseholdIncome3" name="rdoMedianHouseholdIncome" value="3" />
                            <label for="rdoMedianHouseholdIncome3" class="mb-0 ml-2">20,001 - 30,000</label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMedianHouseholdIncome4" name="rdoMedianHouseholdIncome" value="4" />
                            <label for="rdoMedianHouseholdIncome4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01319") %> 30,000</label>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305086") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoReceiveExpensesFrom1" name="rdoReceiveExpensesFrom" value="1" />
                            <label for="rdoReceiveExpensesFrom1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoReceiveExpensesFrom2" name="rdoReceiveExpensesFrom" value="2" />
                            <label for="rdoReceiveExpensesFrom2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoReceiveExpensesFrom3" name="rdoReceiveExpensesFrom" value="3" />
                            <label for="rdoReceiveExpensesFrom3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoReceiveExpensesFrom99" name="rdoReceiveExpensesFrom" value="99" data-element-other-id="tarReceiveExpensesFromOther" />
                            <label for="rdoReceiveExpensesFrom99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarReceiveExpensesFromOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                        <p class="border-bottom-red pb-2 mb-3 mt-4">นักเรียนทำงานหารายได้ อาชีพ</p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentWorkIncome1" name="rdoStudentWorkIncome" value="1" />
                            <label for="rdoStudentWorkIncome1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305088") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentWorkIncome2" name="rdoStudentWorkIncome" value="2" />
                            <label for="rdoStudentWorkIncome2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01191") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentWorkIncome3" name="rdoStudentWorkIncome" value="3" />
                            <label for="rdoStudentWorkIncome3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305090") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentWorkIncome4" name="rdoStudentWorkIncome" value="4" />
                            <label for="rdoStudentWorkIncome4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305091") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentWorkIncome99" name="rdoStudentWorkIncome" value="99" data-element-other-id="tarStudentWorkIncomeOther" />
                            <label for="rdoStudentWorkIncome99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarStudentWorkIncomeOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01734") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDailyIncome1" name="rdoDailyIncome" value="1" />
                            <label for="rdoDailyIncome1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305093") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDailyIncome2" name="rdoDailyIncome" value="2" />
                            <label for="rdoDailyIncome2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305094") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDailyIncome3" name="rdoDailyIncome" value="3" />
                            <label for="rdoDailyIncome3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305095") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDailyIncome4" name="rdoDailyIncome" value="4" />
                            <label for="rdoDailyIncome4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305096") %></label>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00816") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoPaidComeDay1" name="rdoPaidComeDay" value="1" />
                            <label for="rdoPaidComeDay1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305098") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoPaidComeDay2" name="rdoPaidComeDay" value="2" />
                            <label for="rdoPaidComeDay2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305099") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoPaidComeDay3" name="rdoPaidComeDay" value="3" />
                            <label for="rdoPaidComeDay3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305100") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133506") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantAgencyHelp1" name="chkParentWantAgencyHelp" value="1" />
                            <label for="chkParentWantAgencyHelp1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305103") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantAgencyHelp2" name="chkParentWantAgencyHelp" value="2" />
                            <label for="chkParentWantAgencyHelp2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantAgencyHelp3" name="chkParentWantAgencyHelp" value="3" />
                            <label for="chkParentWantAgencyHelp3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204043") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantAgencyHelp4" name="chkParentWantAgencyHelp" value="4" />
                            <label for="chkParentWantAgencyHelp4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305106") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantAgencyHelp5" name="chkParentWantAgencyHelp" value="5" />
                            <label for="chkParentWantAgencyHelp5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133541") %></label>
                        </div>
                        <%--<div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantAgencyHelp3" name="chkParentWantAgencyHelp" value="99" data-element-other-id="tarParentWantAgencyHelpOther" />
                            <label for="chkParentWantAgencyHelp3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>--%>
                        <textarea id="tarParentWantAgencyHelpOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133507") %></p>
                        <textarea id="tarParentConcerns" name="tarParentConcerns" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" maxlength="500"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133508") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantSchoolsHelp1" name="chkParentWantSchoolsHelp" value="1" />
                            <label for="chkParentWantSchoolsHelp1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantSchoolsHelp2" name="chkParentWantSchoolsHelp" value="2" />
                            <label for="chkParentWantSchoolsHelp2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00629") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantSchoolsHelp3" name="chkParentWantSchoolsHelp" value="3" />
                            <label for="chkParentWantSchoolsHelp3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305112") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkParentWantSchoolsHelp99" name="chkParentWantSchoolsHelp" value="99" data-element-other-id="tarParentWantSchoolsHelpOther" />
                            <label for="chkParentWantSchoolsHelp99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarParentWantSchoolsHelpOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>

                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse3">3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01176") %>
                    </a>
                </div>
                <div id="collapse3" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133509") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHealth1" name="chkHealth" value="1" />
                            <label for="chkHealth1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305117") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHealth2" name="chkHealth" value="2" />
                            <label for="chkHealth2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305118") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHealth3" name="chkHealth" value="3" />
                            <label for="chkHealth3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305119") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHealth4" name="chkHealth" value="4" />
                            <label for="chkHealth4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305120") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHealth5" name="chkHealth" value="5" />
                            <label for="chkHealth5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305121") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133510") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety1" name="chkWelfareSafety" value="1" />
                            <label for="chkWelfareSafety1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305125") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety2" name="chkWelfareSafety" value="2" />
                            <label for="chkWelfareSafety2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00796") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety3" name="chkWelfareSafety" value="3" />
                            <label for="chkWelfareSafety3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01335") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety4" name="chkWelfareSafety" value="4" />
                            <label for="chkWelfareSafety4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305128") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety5" name="chkWelfareSafety" value="5" />
                            <label for="chkWelfareSafety5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305129") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety6" name="chkWelfareSafety" value="6" />
                            <label for="chkWelfareSafety6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305130") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety7" name="chkWelfareSafety" value="7" />
                            <label for="chkWelfareSafety7" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305131") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety8" name="chkWelfareSafety" value="8" />
                            <label for="chkWelfareSafety8" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305132") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety9" name="chkWelfareSafety" value="9" />
                            <label for="chkWelfareSafety9" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305133") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety10" name="chkWelfareSafety" value="10" />
                            <label for="chkWelfareSafety10" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305134") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkWelfareSafety11" name="chkWelfareSafety" value="11" />
                            <label for="chkWelfareSafety11" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305135") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">3.3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305137") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDistanceHomeToSchool1" name="rdoDistanceHomeToSchool" value="1">
                            <label for="rdoDistanceHomeToSchool1" class="mb-0 ml-2">1-5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDistanceHomeToSchool2" name="rdoDistanceHomeToSchool" value="2">
                            <label for="rdoDistanceHomeToSchool2" class="mb-0 ml-2">6-10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDistanceHomeToSchool3" name="rdoDistanceHomeToSchool" value="3">
                            <label for="rdoDistanceHomeToSchool3" class="mb-0 ml-2">11-15 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDistanceHomeToSchool4" name="rdoDistanceHomeToSchool" value="4">
                            <label for="rdoDistanceHomeToSchool4" class="mb-0 ml-2">16-20 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoDistanceHomeToSchool5" name="rdoDistanceHomeToSchool" value="5">
                            <label for="rdoDistanceHomeToSchool5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305142") %></label>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305143") %>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00513") %>)</p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoTravelTime1" name="rdoTravelTime" value="1">
                            <label for="rdoTravelTime1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305144") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoTravelTime2" name="rdoTravelTime" value="2">
                            <label for="rdoTravelTime2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305145") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoTravelTime3" name="rdoTravelTime" value="3">
                            <label for="rdoTravelTime3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305146") %></label>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133530") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel1" name="rdoStudentTravel" value="1">
                            <label for="rdoStudentTravel1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305149") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel2" name="rdoStudentTravel" value="2">
                            <label for="rdoStudentTravel2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305150") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel3" name="rdoStudentTravel" value="3">
                            <label for="rdoStudentTravel3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305151") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel4" name="rdoStudentTravel" value="4">
                            <label for="rdoStudentTravel4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305152") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel5" name="rdoStudentTravel" value="5">
                            <label for="rdoStudentTravel5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305153") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel6" name="rdoStudentTravel" value="6">
                            <label for="rdoStudentTravel6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305154") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel7" name="rdoStudentTravel" value="7">
                            <label for="rdoStudentTravel7" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305155") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoStudentTravel99" name="rdoStudentTravel" value="99" data-element-other-id="tarStudentTravelOther">
                            <label for="rdoStudentTravel99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarStudentTravelOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">3.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305157") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkLivingConditions1" name="chkLivingConditions" value="1" />
                            <label for="chkLivingConditions1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305158") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkLivingConditions2" name="chkLivingConditions" value="2" />
                            <label for="chkLivingConditions2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305159") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkLivingConditions3" name="chkLivingConditions" value="3" />
                            <label for="chkLivingConditions3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305160") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkLivingConditions4" name="chkLivingConditions" value="4" />
                            <label for="chkLivingConditions4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305161") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkLivingConditions5" name="chkLivingConditions" value="5" />
                            <label for="chkLivingConditions5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133531") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkLivingConditions99" name="chkLivingConditions" value="99" data-element-other-id="tarLivingConditionsOther">
                            <label for="chkLivingConditions99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarLivingConditionsOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133513") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkStudentResponsibilities1" name="chkStudentResponsibilities" value="1" />
                            <label for="chkStudentResponsibilities1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305165") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkStudentResponsibilities2" name="chkStudentResponsibilities" value="2" />
                            <label for="chkStudentResponsibilities2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305166") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkStudentResponsibilities3" name="chkStudentResponsibilities" value="3" />
                            <label for="chkStudentResponsibilities3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305167") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkStudentResponsibilities4" name="chkStudentResponsibilities" value="4" />
                            <label for="chkStudentResponsibilities4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305168") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkStudentResponsibilities5" name="chkStudentResponsibilities" value="5" />
                            <label for="chkStudentResponsibilities5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305169") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkStudentResponsibilities99" name="chkStudentResponsibilities" value="99" data-element-other-id="tarStudentResponsibilitiesOther">
                            <label for="chkStudentResponsibilities99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarStudentResponsibilitiesOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133514") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies1" name="chkHobbies" value="1" />
                            <label for="chkHobbies1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305172") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies2" name="chkHobbies" value="2" />
                            <label for="chkHobbies2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305173") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies3" name="chkHobbies" value="3" />
                            <label for="chkHobbies3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305174") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies4" name="chkHobbies" value="4" />
                            <label for="chkHobbies4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305175") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies5" name="chkHobbies" value="5" />
                            <label for="chkHobbies5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305176") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies6" name="chkHobbies" value="6" />
                            <label for="chkHobbies6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305177") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies7" name="chkHobbies" value="7" />
                            <label for="chkHobbies7" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133537") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies8" name="chkHobbies" value="8" />
                            <label for="chkHobbies8" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305179") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkHobbies99" name="chkHobbies" value="99" data-element-other-id="tarHobbiesOther">
                            <label for="chkHobbies99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarHobbiesOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133515") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSubstanceAbuseBehavior1" name="chkSubstanceAbuseBehavior" value="1" />
                            <label for="chkSubstanceAbuseBehavior1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305184") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSubstanceAbuseBehavior2" name="chkSubstanceAbuseBehavior" value="2" />
                            <label for="chkSubstanceAbuseBehavior2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02105") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSubstanceAbuseBehavior3" name="chkSubstanceAbuseBehavior" value="3" />
                            <label for="chkSubstanceAbuseBehavior3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305186") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSubstanceAbuseBehavior4" name="chkSubstanceAbuseBehavior" value="4" />
                            <label for="chkSubstanceAbuseBehavior4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305187") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSubstanceAbuseBehavior5" name="chkSubstanceAbuseBehavior" value="5" />
                            <label for="chkSubstanceAbuseBehavior5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305188") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133516") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkViolentBehavior1" name="chkViolentBehavior" value="1" />
                            <label for="chkViolentBehavior1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305192") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkViolentBehavior2" name="chkViolentBehavior" value="2" />
                            <label for="chkViolentBehavior2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305193") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkViolentBehavior3" name="chkViolentBehavior" value="3" />
                            <label for="chkViolentBehavior3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305194") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkViolentBehavior4" name="chkViolentBehavior" value="4" />
                            <label for="chkViolentBehavior4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305195") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkViolentBehavior5" name="chkViolentBehavior" value="5" />
                            <label for="chkViolentBehavior5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305196") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkViolentBehavior99" name="chkViolentBehavior" value="99" data-element-other-id="tarViolentBehaviorOther">
                            <label for="chkViolentBehavior99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarViolentBehaviorOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133517") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSexualBehavior1" name="chkSexualBehavior" value="1" />
                            <label for="chkSexualBehavior1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305200") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSexualBehavior2" name="chkSexualBehavior" value="2" />
                            <label for="chkSexualBehavior2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305201") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSexualBehavior3" name="chkSexualBehavior" value="3" />
                            <label for="chkSexualBehavior3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305202") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSexualBehavior4" name="chkSexualBehavior" value="4" />
                            <label for="chkSexualBehavior4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305203") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSexualBehavior5" name="chkSexualBehavior" value="5" />
                            <label for="chkSexualBehavior5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305204") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkSexualBehavior6" name="chkSexualBehavior" value="6" />
                            <label for="chkSexualBehavior6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305205") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133518") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction1" name="chkGameAddiction" value="1" />
                            <label for="chkGameAddiction1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305209") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction2" name="chkGameAddiction" value="2" />
                            <label for="chkGameAddiction2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305210") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction3" name="chkGameAddiction" value="3" />
                            <label for="chkGameAddiction3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305211") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction4" name="chkGameAddiction" value="4" />
                            <label for="chkGameAddiction4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305212") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction5" name="chkGameAddiction" value="5" />
                            <label for="chkGameAddiction5" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01609") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction6" name="chkGameAddiction" value="6" />
                            <label for="chkGameAddiction6" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305213") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction7" name="chkGameAddiction" value="7" />
                            <label for="chkGameAddiction7" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305214") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction8" name="chkGameAddiction" value="8" />
                            <label for="chkGameAddiction8" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305215") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkGameAddiction99" name="chkGameAddiction" value="99" data-element-other-id="tarGameAddictionOther">
                            <label for="chkGameAddiction99" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %></label>
                        </div>
                        <textarea id="tarGameAddictionOther" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled maxlength="100"></textarea>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133519") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoAccessComputerInternet1" name="rdoAccessComputerInternet" value="1">
                            <label for="rdoAccessComputerInternet1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305219") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoAccessComputerInternet2" name="rdoAccessComputerInternet" value="2">
                            <label for="rdoAccessComputerInternet2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305220") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133520") %></p>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkUseElectronicTools1" name="chkUseElectronicTools" value="1">
                            <label for="chkUseElectronicTools1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00602") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="checkbox" id="chkUseElectronicTools2" name="chkUseElectronicTools" value="2">
                            <label for="chkUseElectronicTools2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00603") %></label>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="border-bottom-red pb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133521") %></p>
                        <div class="match-block" style="text-align: center; display: block;">
                            <button class="match-choose student-info-provider" data-student-info-provider="" data-toggle="modal" data-target=".studentInfoProviderModal" style="margin-top: 25px; width: 100%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></button>
                        </div>
                    </div>

                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse4">4. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131242") %>
                    </a>
                </div>
                <div id="collapse4" class="collapse" data-parent="#accordion">
                    <div class="card-body" style="box-shadow: none;">
                        <%--<p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></p>
                        <textarea id="tarNote" rows="3" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>"></textarea>--%>
                        <p class="border-bottom-red pb-2 mb-3 mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131309") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHouseStyle1" name="rdoHouseStyle" value="1">
                            <label for="rdoHouseStyle1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133532") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHouseStyle2" name="rdoHouseStyle" value="2">
                            <label for="rdoHouseStyle2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHouseStyle3" name="rdoHouseStyle" value="3">
                            <label for="rdoHouseStyle3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131276") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoHouseStyle4" name="rdoHouseStyle" value="4">
                            <label for="rdoHouseStyle4" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133549") %></label>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01306") %></p>
                        <div class="card-body">
                            <p class="border-bottom w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01306") %></p>
                            <form method="POST" enctype="multipart/form-data">
                                <div class="div-images-outside"></div>
                            </form>
                            <p style="margin: 10px 3px 3px 3px; color: var(--red); font-size: 0.7em;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133533") %></p>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01304") %></p>
                        <div class="card-body">
                            <p class="border-bottom w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133534") %></p>
                            <form method="POST" enctype="multipart/form-data">
                                <div class="div-images-inside"></div>
                            </form>
                            <p style="margin: 10px 3px 3px 3px; color: var(--red); font-size: 0.7em;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133533") %></p>
                        </div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133538") %></p>
                        <div class="sign-block sign-parent" data-sign-div=".sign-parent"></div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133539") %></p>
                        <div class="sign-block sign-student" data-sign-div=".sign-student"></div>
                        <p class="border-bottom-red pb-2 mb-3 mt-4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133540") %></p>
                        <div class="sign-block sign-teacher" data-sign-div=".sign-teacher"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="align-items-center" style="text-align: center;">
                <button type="button" class="btn btn-success btn-round px-3 save-homevisit-btn" style="width: 98%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
            </div>
        </div>
    </div>

    <div class="modal fade classModal relationLevelModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133535") %></h4>
                </div>
                <div class="modal-body" style="display: block; overflow-y: auto; height: 153px;">
                    <button class="choose-relation-level" data-level="1" data-ref-member="" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></button><br />
                    <button class="choose-relation-level" data-level="2" data-ref-member="" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></button><br />
                    <button class="choose-relation-level" data-level="3" data-ref-member="" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></button><br />
                    <button class="choose-relation-level" data-level="4" data-ref-member="" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></button><br />
                    <button class="choose-relation-level" data-level="5" data-ref-member="" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade classModal studentInfoProviderModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133535") %></h4>
                </div>
                <div class="modal-body" style="display: block; overflow-y: auto; height: 153px;">
                    <button class="choose-student-info-provider" data-member="1" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></button><br />
                    <button class="choose-student-info-provider" data-member="2" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></button><br />
                    <button class="choose-student-info-provider" data-member="3" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01221") %></button><br />
                    <button class="choose-student-info-provider" data-member="4" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01225") %></button><br />
                    <button class="choose-student-info-provider" data-member="5" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00835") %></button><br />
                    <button class="choose-student-info-provider" data-member="6" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %></button><br />
                    <button class="choose-student-info-provider" data-member="7" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01058") %></button><br />
                    <button class="choose-student-info-provider" data-member="8" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01862") %></button><br />
                    <button class="choose-student-info-provider" data-member="9" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131272") %></button><br />
                    <button class="choose-student-info-provider" data-member="10" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01457") %></button><br />
                    <button class="choose-student-info-provider" data-member="11" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %></button><br />
                    <button class="choose-student-info-provider" data-member="12" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01458") %></button><br />
                    <button class="choose-student-info-provider" data-member="13" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00771") %></button><br />
                    <button class="choose-student-info-provider" data-member="14" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00935") %></button><br />
                    <button class="choose-student-info-provider" data-member="15" style="padding: 12px; margin: 5px auto;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01325") %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="signature">
        <div id="signature-pad" class="signature-pad">
            <div class="signature-pad--header">
                <img src="/Content/VisitHouse/assets/img/close.png" alt="close" class="signature-close">
            </div>
            <div class="signature-pad--body">
                <canvas></canvas>
            </div>
            <div class="signature-pad--footer">
                <div class="signature-pad--actions p-3">
                    <button type="button" class="btn btn-default btn-round" data-action="clear">Clear</button>
                    <button type="button" class="btn btn-success btn-round save" data-action="save-png" data-from-class="">Save</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/Content/VisitHouse/assets/js/image-uploader.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="/Content/VisitHouse/assets/js/signature_pad.umd.js"></script>
    <script src="/Content/VisitHouse/assets/js/app.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="/Content/VisitHouse/assets/js/homeVisit.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="../../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script>

        var housePictureOutSideDB = [<%=HousePictureOutSideDB%>];
        var housePictureInSideDB = [<%=HousePictureInSideDB%>];

        //$("#aPrev").attr("href", "Index.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&roomid=<%=Request.QueryString["roomid"]%>");

        var requestElementName = ['iptLINE', 'iptFacebook'
            , 'rdoHaveParents', 'rdoRelationship', 'iptFullname', 'iptPhoneNumber', 'rdoOccupation', 'rdoHighestEducation'
            , 'rdoResidentialHouse', 'rdoOwnHome', 'rdoCleanliness', 'rdoUtilitiesElectricity', 'rdoWaterForConsumption', 'rdoToilet', 'tarLivingEnvironment'
            , 'iptStudentFamilyMembersAmount', 'iptStudentFamilyMembersMale', 'iptStudentFamilyMembersFemale', 'iptSiblingsBornSameParentsAmount', 'iptSiblingsBornSameParentsMale', 'iptSiblingsBornSameParentsFemale', 'iptSiblingsBornDifferentParentsAmount', 'iptSiblingsBornDifferentParentsMale', 'iptSiblingsBornDifferentParentsFemale', 'iptFamiliesNeedSpecialAssistance', 'iptFamiliesNeedSpecialAssistanceTotal'
            , 'rdoFamilyRelationship', '.relation-member', 'sltSpendTimeWithFamily', 'iptWorkloadTheirFamilies', 'iptLeisureActivities'
            , 'rdoLeaveStudent', 'rdoMedianHouseholdIncome', 'rdoReceiveExpensesFrom', 'rdoStudentWorkIncome', 'rdoDailyIncome', 'rdoPaidComeDay', 'chkParentWantAgencyHelp', 'tarParentConcerns', 'chkParentWantSchoolsHelp'
            , 'chkHealth', 'chkWelfareSafety', 'rdoDistanceHomeToSchool', 'rdoTravelTime', 'rdoStudentTravel', 'chkLivingConditions', 'chkStudentResponsibilities', 'chkHobbies', 'chkSubstanceAbuseBehavior', 'chkViolentBehavior', 'chkSexualBehavior', 'chkGameAddiction', 'rdoAccessComputerInternet', 'chkUseElectronicTools', '.student-info-provider'
            , 'rdoHouseStyle'];
        var requestMessage = ['LINE', 'Facebook'
            , '1', '1.1', '1.2', '1.3', '1.4', '1.5'
            , '2.1', '2.2.1', '2.2.2', '2.2.3', '2.2.4', '2.2.5', '2.2.6'
            , '2.3.1', '2.3.1', '2.3.1', '2.3.2', '2.3.2', '2.3.2', '2.3.3', '2.3.3', '2.3.3', '2.3.4', '2.3.4'
            , '2.4', '2.5', '2.6', '2.7', '2.8'
            , '2.9', '2.10', '2.10', '2.10', '2.10', '2.10', '2.11', '2.12', '2.13'
            , '3.1', '3.2', '3.3', '3.3', '3.3', '3.4', '3.5', '3.6', '3.7', '3.8', '3.9', '3.10', '3.11', '3.12', '3.13'
            , '4'];

        var radioState;

        // Image Uploader
        $('.div-images-outside').imageUploader({
            preloaded: [<%=HousePictureOutSidePreloaded%>],
            preloadedInputName: 'old-out',
            imagesInputName: 'images-out',
            label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133536") %>',
            maxSize: 5 * 1024 * 1024,
            maxFiles: 5
        });
        $('.div-images-inside').imageUploader({
            preloaded: [<%=HousePictureInSidePreloaded%>],
            preloadedInputName: 'old-in',
            imagesInputName: 'images-in',
            label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133536") %>',
            maxSize: 5 * 1024 * 1024,
            maxFiles: 5
        });


        // Signature
        $(".signature").hide();

        $(".sign-block").click(function () {
            $(".signature").css("display", "flex");
            savePNGButton.setAttribute('data-from-class', $(this).data('sign-div'))
        });

        $(".signature-close").click(function () {
            $(".signature").css("display", "none");
        });

        $('.relation-member').click(function () {
            $('.choose-relation-level').data('ref-member', $(this).data('member'));
        });

        $('.choose-relation-level').click(function () {
            $('.relation-member[data-member="' + $(this).data('ref-member') + '"]').data('relation-level', $(this).data('level')).text($(this).text());
            $(".relationLevelModal").modal('hide');
        });

        $('.choose-student-info-provider').click(function () {
            $('.student-info-provider').data('student-info-provider', $(this).data('member')).text($(this).text());
            $(".studentInfoProviderModal").modal('hide');
        });

        $('.delete-image').click(function () {
            if ($(this).parent().data('preloaded')) {
                var fileId = $(this).parent().children('input[name^="old"]').val();
                var fileType = $(this).parent().children('input[name^="old"]').attr("name").replace('old-', '').replace('[]', '');
                if (fileType == 'out') {
                    var objIndex = housePictureOutSideDB.findIndex((obj => obj.id == fileId));
                    housePictureOutSideDB[objIndex].status = 'delete';
                }
                else {
                    // in
                    var objIndex = housePictureInSideDB.findIndex((obj => obj.id == fileId));
                    housePictureInSideDB[objIndex].status = 'delete';
                }
            }
        });

        // Event Handler
        function SaveHomeVisit() {
            Swal.fire({
                title: 'Save!',
                text: 'Do you want to save?',
                reverseButtons: true,
                confirmButtonText: 'Save',
                cancelButtonText: 'Cancel',
                showCancelButton: true
            }).then(function (result2) {
                if (result2.isConfirmed) {

                    // Create form data object  
                    var formData = new FormData();
                    var data = {};

                    data.schoolId = '<%=Request.QueryString["schoolid"]%>';
                    data.roomId = '<%=Request.QueryString["roomid"]%>';
                    data.studentId = '<%=Request.QueryString["sid"]%>';

                    if ($('#rdoStatusNotFound3').is(':checked')) {
                        data.status = $('#rdoStatusNotFound3:checked').val();
                    }
                    else {
                        var housePictureOutSide = [];
                        var housePictureInSide = [];

                        // Looping over all files and add it to FormData object  
                        var filesOut = $('.div-images-outside input').get(0).files;
                        for (var i = 0; i < filesOut.length; i++) {
                            formData.append('out-' + (i + 1) + '-' + filesOut[i].name, filesOut[i]);
                            housePictureOutSide.push({ id: (i + 1), type: 1, contentType: filesOut[i].type, fileName: filesOut[i].name, indb: false, status: 'new' });
                        }
                        $.merge(housePictureOutSide, housePictureOutSideDB);

                        var filesIn = $('.div-images-inside input').get(0).files;
                        for (var i = 0; i < filesIn.length; i++) {
                            formData.append('in-' + (i + 1) + '-' + filesIn[i].name, filesIn[i]);
                            housePictureInSide.push({ id: (i + 1), type: 2, contentType: filesIn[i].type, fileName: filesIn[i].name, indb: false, status: 'new' });
                        }
                        $.merge(housePictureInSide, housePictureInSideDB);

                        var relationMember = [];
                        $(".relation-member").each(function () {
                            if ($(this).data('relation-level')) {
                                relationMember.push({ relation: $(this).data('member'), relationLevel: $(this).data('relation-level') });
                            }
                        });

                        data.status = 2;
                        data.line = $('#iptLINE').val();
                        data.facebook = $('#iptFacebook').val();

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133479") %>
                        data.haveParents = $('input[name="rdoHaveParents"]:checked').val() == 1;
                        data.relationship = $('input[name="rdoRelationship"]:checked').val();
                        data.relationshipOther = $('#tarRelationshipOther').val();
                        data.fullname = $('#iptFullname').val();
                        data.phoneNumber = $('#iptPhoneNumber').val();
                        data.idCardNumber = $('#iptIDCardNumber').val();
                        data.occupation = $('input[name="rdoOccupation"]:checked').val();
                        data.occupationOther = $('#tarOccupationOther').val();
                        data.highestEducation = $('input[name="rdoHighestEducation"]:checked').val();
                        data.welfareRegistersPoor = $('#chkWelfareRegistersPoor').is(":checked");

                        // 2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133545") %>
                        data.residentialHouse = $('input[name="rdoResidentialHouse"]:checked').val();
                        data.dormitoryLivingWith = $('#tarDormitoryLivingWith').val();
                        data.residentialHouseOther = $('#tarResidentialHouseOther').val();
                        data.ownHome = $('input[name="rdoOwnHome"]:checked').val();
                        data.cleanliness = $('input[name="rdoCleanliness"]:checked').val();
                        data.cleanlinessOther = $('#tarCleanlinessOther').val();
                        data.utilitiesElectricity = $('input[name="rdoUtilitiesElectricity"]:checked').val();
                        data.waterForConsumption = $('input[name="rdoWaterForConsumption"]:checked').val();
                        data.toilet = $('input[name="rdoToilet"]:checked').val();
                        data.livingEnvironment = $('#tarLivingEnvironment').val();
                        data.studentFamilyMembersAmount = $('#iptStudentFamilyMembersAmount').val();
                        data.studentFamilyMembersMale = $('#iptStudentFamilyMembersMale').val();
                        data.studentFamilyMembersFemale = $('#iptStudentFamilyMembersFemale').val();
                        data.siblingsBornSameParentsAmount = $('#iptSiblingsBornSameParentsAmount').val();
                        data.siblingsBornSameParentsMale = $('#iptSiblingsBornSameParentsMale').val();
                        data.siblingsBornSameParentsFemale = $('#iptSiblingsBornSameParentsFemale').val();
                        data.siblingsBornDifferentParentsAmount = $('#iptSiblingsBornDifferentParentsAmount').val();
                        data.siblingsBornDifferentParentsMale = $('#iptSiblingsBornDifferentParentsMale').val();
                        data.siblingsBornDifferentParentsFemale = $('#iptSiblingsBornDifferentParentsFemale').val();
                        data.familiesNeedSpecialAssistance = $('#iptFamiliesNeedSpecialAssistance').val();
                        data.familiesNeedSpecialAssistanceTotal = $('#iptFamiliesNeedSpecialAssistanceTotal').val();
                        data.familyRelationship = $('input[name="rdoFamilyRelationship"]:checked').val();
                        data.familyRelationshipOther = $('#tarFamilyRelationshipOther').val();
                        data.relationshipMember = JSON.stringify(relationMember);
                        data.spendTimeWithFamily = $('#sltSpendTimeWithFamily').val();
                        data.workloadTheirFamilies = $('#iptWorkloadTheirFamilies').val();
                        data.leisureActivities = $('#iptLeisureActivities').val();
                        data.leaveStudent = $('input[name="rdoLeaveStudent"]:checked').val();
                        data.leaveStudentOther = $('#tarLeaveStudentOther').val();
                        data.medianHouseholdIncome = $('input[name="rdoMedianHouseholdIncome"]:checked').val();
                        data.receiveExpensesFrom = $('input[name="rdoReceiveExpensesFrom"]:checked').val();
                        data.receiveExpensesFromOther = $('#tarReceiveExpensesFromOther').val();
                        data.studentWorkIncome = $('input[name="rdoStudentWorkIncome"]:checked').val();
                        data.studentWorkIncomeOther = $('#tarStudentWorkIncomeOther').val();
                        data.dailyIncome = $('input[name="rdoDailyIncome"]:checked').val();
                        data.paidComeDay = $('input[name="rdoPaidComeDay"]:checked').val();
                        data.parentWantAgencyHelp = JSON.stringify($("input[name=chkParentWantAgencyHelp]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.parentWantAgencyHelpOther = $('#tarParentWantAgencyHelpOther').val();
                        data.parentConcerns = $('#tarParentConcerns').val();
                        data.parentWantSchoolsHelp = JSON.stringify($("input[name=chkParentWantSchoolsHelp]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.parentWantSchoolsHelpOther = $('#tarParentWantSchoolsHelpOther').val();

                        // 3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01176") %>
                        data.health = JSON.stringify($("input[name=chkHealth]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.welfareSafety = JSON.stringify($("input[name=chkWelfareSafety]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.distanceHomeToSchool = $('input[name="rdoDistanceHomeToSchool"]:checked').val();
                        data.travelTime = $('input[name="rdoTravelTime"]:checked').val();
                        data.studentTravel = $('input[name="rdoStudentTravel"]:checked').val();
                        data.studentTravelOther = $('#tarStudentTravelOther').val();
                        data.livingConditions = JSON.stringify($("input[name=chkLivingConditions]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.livingConditionsOther = $('#tarLivingConditionsOther').val();
                        data.studentResponsibilities = JSON.stringify($("input[name=chkStudentResponsibilities]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.studentResponsibilitiesOther = $('#tarStudentResponsibilitiesOther').val();
                        data.hobbies = JSON.stringify($("input[name=chkHobbies]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.hobbiesOther = $('#tarHobbiesOther').val();
                        data.substanceAbuseBehavior = JSON.stringify($("input[name=chkSubstanceAbuseBehavior]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.violentBehavior = JSON.stringify($("input[name=chkViolentBehavior]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.violentBehaviorOther = $('#tarViolentBehaviorOther').val();
                        data.sexualBehavior = JSON.stringify($("input[name=chkSexualBehavior]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.gameAddiction = JSON.stringify($("input[name=chkGameAddiction]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.gameAddictionOther = $('#tarGameAddictionOther').val();
                        data.accessComputerInternet = $('input[name="rdoAccessComputerInternet"]:checked').val();
                        data.useElectronicTools = JSON.stringify($("input[name=chkUseElectronicTools]:checked").map(function () { return parseInt($(this).val()); }).get());
                        data.studentInfoProvider = $('.student-info-provider').data('student-info-provider');

                        // 4. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131242") %>
                        //data.note = $('#tarNote').val();
                        data.houseStyle = $('input[name="rdoHouseStyle"]:checked').val();
                        data.housePictureOutSide = housePictureOutSide;
                        data.housePictureInSide = housePictureInSide;
                        if ($('.sign-parent img').length > 0 && ~$('.sign-parent img').attr('src').indexOf("base64")) {
                            data.parentSignature = $('.sign-parent img').attr('src').split(';')[1].replace("base64,", "");
                        }
                        if ($('.sign-student img').length > 0 && ~$('.sign-student img').attr('src').indexOf("base64")) {
                            data.studentSignature = $('.sign-student img').attr('src').split(';')[1].replace("base64,", "");
                        }
                        if ($('.sign-teacher img').length > 0 && ~$('.sign-teacher img').attr('src').indexOf("base64")) {
                            data.teacherSignature = $('.sign-teacher img').attr('src').split(';')[1].replace("base64,", "");
                        }
                    }

                    formData.append("data", JSON.stringify(data));

                    $.ajax({
                        url: 'SaveHomeVisit.ashx',
                        type: 'POST',
                        data: formData,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (r) {
                            if (r.success) {
                                Swal.fire('Success', r.message, 'success');
                            }
                            else {
                                Swal.fire('Error', r.message, 'error');
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            Swal.fire({ icon: 'error', title: 'Oops...', text: 'Something went wrong![' + xhr.responseText + ']' });
                        },
                        failure: function (xhr, textStatus, errorThrown) {
                            Swal.fire({ icon: 'error', title: 'Oops...', text: 'Something went wrong![' + xhr.responseText + ']' });
                        }
                    });
                }
            });
        }

        $(".save-homevisit-btn").click(function () {
            // Check request
            var invalid = false;
            var articleNo = [];
            var articleNoText = '';
            $.each(requestElementName, function (index, elementName) {
                switch (elementName) {
                    case '.relation-member':
                        var relationMember = [];
                        $(".relation-member").each(function () {
                            if ($(this).data('relation-level')) {
                                relationMember.push({ relation: $(this).data('member'), relationLevel: $(this).data('relation-level') });
                            }
                        });
                        if (relationMember.length <= 0) {
                            articleNo.push(requestMessage[index]);
                        }
                        break;
                    case '.student-info-provider':
                        if (!$('.student-info-provider').data('student-info-provider')) {
                            articleNo.push(requestMessage[index]);
                        }
                        break;
                    default:
                        switch ($('[name=' + elementName + ']').prop("tagName").toLowerCase()) {
                            case 'input':
                                switch ($('input[name=' + elementName + ']').attr('type')) {
                                    case 'radio':
                                        if (!$('input[name=' + elementName + ']').is(':checked')) {
                                            articleNo.push(requestMessage[index]);
                                        }
                                        break;
                                    case 'checkbox':
                                        if ($('input[name=' + elementName + ']:checked').length <= 0) {
                                            articleNo.push(requestMessage[index]);
                                        }
                                        break;
                                    case 'text':
                                        if (!$('input[name=' + elementName + ']').val().trim()) {
                                            articleNo.push(requestMessage[index]);
                                        }
                                        break;
                                }
                                break;
                            case 'textarea':
                                if (!$('textarea[name=' + elementName + ']').val().trim()) {
                                    articleNo.push(requestMessage[index]);
                                }
                                break;
                            case 'select':
                                if (!$('select[name=' + elementName + ']').val()) {
                                    articleNo.push(requestMessage[index]);
                                }
                                break;
                        }
                        break;
                }
            });
            // unique article no
            if (articleNo.length > 0) {
                $.unique(articleNo);
                articleNoText = articleNo.join(", ");
                invalid = true;
            }

            var alertMessage = '';
            if (invalid) {
                alertMessage = `<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133454") %> `+ articleNoText + `
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133455") %>
และกด "บันทึก" เพื่อส่งข้อมูลอีกครั้ง`;

                Swal.fire({
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133456") %>',
                    text: alertMessage,
                    icon: 'info',
                    confirmButtonText: 'OK'
                }).then(function (result) {
                    if (result.isConfirmed) {
                        SaveHomeVisit();
                    }
                });
            }
            else {
                SaveHomeVisit();
            }
        });

        $(".accordion-choice input[type='radio']").click(function () {
            var rdVal = $("input[name='" + $(this).attr('name') + "']:checked").val();
            var rdOtherId = $("input[name='" + $(this).attr('name') + "'][value='99']").data('element-other-id');
            if (rdVal == 99) {
                $('#' + rdOtherId).prop('disabled', false);
            }
            else {
                $('#' + rdOtherId).val('');
                $('#' + rdOtherId).prop('disabled', true);
            }

            var rd5Id = $("input[name='" + $(this).attr('name') + "'][value='5']").data('element-other-id');
            if (rdVal == 5) {
                $('#' + rd5Id).prop('disabled', false);
            }
            else {
                $('#' + rd5Id).val('');
                $('#' + rd5Id).prop('disabled', true);
            }
        });

        $(".accordion-choice input[type='checkbox'][value='99']").click(function () {
            var rdOtherId = $(this).data('element-other-id');
            if (rdOtherId) {
                if ($(this).is(":checked")) {
                    $('#' + rdOtherId).prop('disabled', false);
                }
                else {
                    $('#' + rdOtherId).val('');
                    $('#' + rdOtherId).prop('disabled', true);
                }
            }
        });

        $("input[name='rdoStatusNotFound']:radio").on('click', function (e) {
            if (radioState === this) {
                // unchecked
                this.checked = false;
                radioState = null;
            } else {
                // checked
                radioState = this;
            }
        });

        $(".cal-group-1 input").on('change', function () {
            $('.cal-group-1 input:eq(0)').val(parseInt($('.cal-group-1 input:eq(1)').val() || '0') + parseInt($('.cal-group-1 input:eq(2)').val() || '0'));
            if (!$(this).val()) {
                $(this).val('0');
            }
        });

        $(".cal-group-2 input").on('change', function () {
            $('.cal-group-2 input:eq(0)').val(parseInt($('.cal-group-2 input:eq(1)').val() || '0') + parseInt($('.cal-group-2 input:eq(2)').val() || '0'));
            if (!$(this).val()) {
                $(this).val('0');
            }
        });

        $(".cal-group-3 input").on('change', function () {
            $('.cal-group-3 input:eq(0)').val(parseInt($('.cal-group-3 input:eq(1)').val() || '0') + parseInt($('.cal-group-3 input:eq(2)').val() || '0'));
            if (!$(this).val()) {
                $(this).val('0');
            }
        });

        $(".member-amount input[type='text'], #iptFamiliesNeedSpecialAssistance").on("click", function () {
            $(this).select();
        });

        $(".member-amount input[type='text'], #iptFamiliesNeedSpecialAssistance").on("keyup", function (e) {
            var key = event.keyCode || event.charCode;
            if (key == 8 && !$(this).val()) {
                $(this).val('0');
                return false;
            }
        });

        function LoadHomeVisitData(schoolId, roomId, studentId) {
            $.ajax({
                type: "POST",
                url: "VisitHouseForm.aspx/GetVisitHouseData",
                data: '{schoolID: ' + schoolId + ', roomID: ' + roomId + ', studentID: ' + studentId + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var r = JSON.parse(result.d);
                    if (r.success && r.data) {
                        // Show/hide save date data
                        $('.title-bar').html(r.data.updateDate ?? r.data.saveDate).show();

                        $('#iptLINE').val(r.data.line);
                        $('#iptFacebook').val(r.data.facebook);

                        $('input:radio[name=rdoStatusNotFound][value=' + r.data.status + ']').prop('checked', true);
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133479") %>
                        $('input:radio[name=rdoHaveParents][value=' + (r.data.haveParents ? '1' : '0') + ']').prop('checked', true);
                        $('input:radio[name=rdoRelationship][value=' + r.data.relationship + ']').prop('checked', true);
                        $('#tarRelationshipOther').val(r.data.relationshipOther);
                        $('#iptFullname').val(r.data.fullname);
                        $('#iptPhoneNumber').val(r.data.phoneNumber);
                        $('#iptIDCardNumber').val(r.data.idCardNumber);
                        $('input:radio[name=rdoOccupation][value=' + r.data.occupation + ']').prop('checked', true);
                        $('#tarOccupationOther').val(r.data.occupationOther);
                        $('input:radio[name=rdoHighestEducation][value=' + r.data.highestEducation + ']').prop('checked', true);
                        $('#chkWelfareRegistersPoor').prop('checked', r.data.welfareRegistersPoor);

                        // 2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133545") %>
                        $('input:radio[name=rdoResidentialHouse][value=' + r.data.residentialHouse + ']').prop('checked', true);
                        $('#tarDormitoryLivingWith').val(r.data.dormitoryLivingWith);
                        $('#tarResidentialHouseOther').val(r.data.residentialHouseOther);
                        $('input:radio[name=rdoOwnHome][value=' + r.data.ownHome + ']').prop('checked', true);
                        $('input:radio[name=rdoCleanliness][value=' + r.data.cleanliness + ']').prop('checked', true);
                        $('#tarCleanlinessOther').val(r.data.cleanlinessOther);
                        $('input:radio[name=rdoUtilitiesElectricity][value=' + r.data.utilitiesElectricity + ']').prop('checked', true);
                        $('input:radio[name=rdoWaterForConsumption][value=' + r.data.waterForConsumption + ']').prop('checked', true);
                        $('input:radio[name=rdoToilet][value=' + r.data.toilet + ']').prop('checked', true);
                        $('#tarLivingEnvironment').val(r.data.livingEnvironment);
                        $('#iptStudentFamilyMembersAmount').val(r.data.studentFamilyMembersAmount);
                        $('#iptStudentFamilyMembersMale').val(r.data.studentFamilyMembersMale);
                        $('#iptStudentFamilyMembersFemale').val(r.data.studentFamilyMembersFemale);
                        $('#iptSiblingsBornSameParentsAmount').val(r.data.siblingsBornSameParentsAmount);
                        $('#iptSiblingsBornSameParentsMale').val(r.data.siblingsBornSameParentsMale);
                        $('#iptSiblingsBornSameParentsFemale').val(r.data.siblingsBornSameParentsFemale);
                        $('#iptSiblingsBornDifferentParentsAmount').val(r.data.siblingsBornDifferentParentsAmount);
                        $('#iptSiblingsBornDifferentParentsMale').val(r.data.siblingsBornDifferentParentsMale);
                        $('#iptSiblingsBornDifferentParentsFemale').val(r.data.siblingsBornDifferentParentsFemale);
                        $('#iptFamiliesNeedSpecialAssistance').val(r.data.familiesNeedSpecialAssistance);
                        $('#iptFamiliesNeedSpecialAssistanceTotal').val(r.data.familiesNeedSpecialAssistanceTotal);
                        $('input:radio[name=rdoFamilyRelationship][value=' + r.data.familyRelationship + ']').prop('checked', true);
                        $('#tarFamilyRelationshipOther').val(r.data.familyRelationshipOther);
                        $.each(JSON.parse(r.data.relationshipMember), function (index, obj) {
                            $('button.relation-member[data-member="' + obj.relation + '"]').data('relation-level', obj.relationLevel).text($('button.choose-relation-level[data-level="' + obj.relationLevel + '"]').text());
                        });
                        $('#sltSpendTimeWithFamily').val(r.data.spendTimeWithFamily);
                        $('#iptWorkloadTheirFamilies').val(r.data.workloadTheirFamilies);
                        $('#iptLeisureActivities').val(r.data.leisureActivities);
                        $('input:radio[name=rdoLeaveStudent][value=' + r.data.leaveStudent + ']').prop('checked', true);
                        $('#tarLeaveStudentOther').val(r.data.leaveStudentOther);
                        $('input:radio[name=rdoMedianHouseholdIncome][value=' + r.data.medianHouseholdIncome + ']').prop('checked', true);
                        $('input:radio[name=rdoReceiveExpensesFrom][value=' + r.data.receiveExpensesFrom + ']').prop('checked', true);
                        $('#tarReceiveExpensesFromOther').val(r.data.receiveExpensesFromOther);
                        $('input:radio[name=rdoStudentWorkIncome][value=' + r.data.studentWorkIncome + ']').prop('checked', true);
                        $('#tarStudentWorkIncomeOther').val(r.data.studentWorkIncomeOther);
                        $('input:radio[name=rdoDailyIncome][value=' + r.data.dailyIncome + ']').prop('checked', true);
                        $('input:radio[name=rdoPaidComeDay][value=' + r.data.paidComeDay + ']').prop('checked', true);
                        $.each(JSON.parse(r.data.parentWantAgencyHelp), function (index, value) {
                            $('input:checkbox[name=chkParentWantAgencyHelp][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarParentWantAgencyHelpOther').val(r.data.parentWantAgencyHelpOther);
                        $('#tarParentConcerns').val(r.data.parentConcerns);
                        $.each(JSON.parse(r.data.parentWantSchoolsHelp), function (index, value) {
                            $('input:checkbox[name=chkParentWantSchoolsHelp][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarParentWantSchoolsHelpOther').val(r.data.parentWantSchoolsHelpOther);

                        // 3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01176") %>
                        $.each(JSON.parse(r.data.health), function (index, value) {
                            $('input:checkbox[name=chkHealth][value=' + value + ']').prop('checked', true);
                        });
                        $.each(JSON.parse(r.data.welfareSafety), function (index, value) {
                            $('input:checkbox[name=chkWelfareSafety][value=' + value + ']').prop('checked', true);
                        });
                        $('input:radio[name=rdoDistanceHomeToSchool][value=' + r.data.distanceHomeToSchool + ']').prop('checked', true);
                        $('input:radio[name=rdoTravelTime][value=' + r.data.travelTime + ']').prop('checked', true);
                        $('#tarParentConcerns').val(r.data.parentConcerns);
                        $('input:radio[name=rdoStudentTravel][value=' + r.data.studentTravel + ']').prop('checked', true);
                        $('#tarStudentTravelOther').val(r.data.studentTravelOther);
                        $.each(JSON.parse(r.data.livingConditions), function (index, value) {
                            $('input:checkbox[name=chkLivingConditions][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarLivingConditionsOther').val(r.data.livingConditionsOther);
                        $.each(JSON.parse(r.data.studentResponsibilities), function (index, value) {
                            $('input:checkbox[name=chkStudentResponsibilities][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarStudentResponsibilitiesOther').val(r.data.studentResponsibilitiesOther);
                        $.each(JSON.parse(r.data.hobbies), function (index, value) {
                            $('input:checkbox[name=chkHobbies][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarHobbiesOther').val(r.data.hobbiesOther);
                        $.each(JSON.parse(r.data.substanceAbuseBehavior), function (index, value) {
                            $('input:checkbox[name=chkSubstanceAbuseBehavior][value=' + value + ']').prop('checked', true);
                        });
                        $.each(JSON.parse(r.data.violentBehavior), function (index, value) {
                            $('input:checkbox[name=chkViolentBehavior][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarViolentBehaviorOther').val(r.data.violentBehaviorOther);
                        $.each(JSON.parse(r.data.sexualBehavior), function (index, value) {
                            $('input:checkbox[name=chkSexualBehavior][value=' + value + ']').prop('checked', true);
                        });
                        $.each(JSON.parse(r.data.gameAddiction), function (index, value) {
                            $('input:checkbox[name=chkGameAddiction][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarGameAddictionOther').val(r.data.gameAddictionOther);
                        $('input:radio[name=rdoAccessComputerInternet][value=' + r.data.accessComputerInternet + ']').prop('checked', true);
                        $.each(JSON.parse(r.data.useElectronicTools), function (index, value) {
                            $('input:checkbox[name=chkUseElectronicTools][value=' + value + ']').prop('checked', true);
                        });
                        $('button.student-info-provider').data('student-info-provider', r.data.studentInfoProvider).text($('button.choose-student-info-provider[data-member="' + r.data.studentInfoProvider + '"]').text());

                        // 4. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131242") %>
                        //$('#tarNote').val(r.data.note);
                        $('input:radio[name=rdoHouseStyle][value=' + r.data.houseStyle + ']').prop('checked', true);
                        if (r.data.parentSignature) {
                            $('.sign-parent').html('');
                            var img = $('<img style="width: 100%; height: 100%;">');
                            img.attr('src', r.data.parentSignature);
                            img.appendTo('.sign-parent');
                        }
                        if (r.data.studentSignature) {
                            $('.sign-student').html('');
                            var img = $('<img style="width: 100%; height: 100%;">');
                            img.attr('src', r.data.studentSignature);
                            img.appendTo('.sign-student');
                        }
                        if (r.data.teacherSignature) {
                            $('.sign-teacher').html('');
                            var img = $('<img style="width: 100%; height: 100%;">');
                            img.attr('src', r.data.teacherSignature);
                            img.appendTo('.sign-teacher');
                        }

                        // Init
                        $(".accordion-choice input[type='radio']:checked").each(function () {
                            var rdVal = $("input[name='" + $(this).attr('name') + "']:checked").val();
                            var rdOtherId = $("input[name='" + $(this).attr('name') + "'][value='99']").data('element-other-id');
                            if (rdVal == 99 && rdOtherId) {
                                $('#' + rdOtherId).prop('disabled', false);
                            }
                        });

                        $(".accordion-choice input[type='checkbox'][value='99']:checked").each(function () {
                            var rdOtherId = $(this).data('element-other-id');
                            if ($(this).is(":checked") && rdOtherId) {
                                $('#' + rdOtherId).prop('disabled', false);
                            }
                        });
                    }
                },
                failure: function (xhr, textStatus, errorThrown) {
                    Swal.fire({ icon: 'error', title: 'Oops...', text: 'Something went wrong![' + xhr.responseText + ']' });
                },
                error: function (xhr, textStatus, errorThrown) {
                    Swal.fire({ icon: 'error', title: 'Oops...', text: 'Something went wrong![' + xhr.responseText + ']' });
                }
            });
        }

        //$('#iptPhoneNumber').number(true, 0, '', '');
        $('#iptStudentFamilyMembersAmount, #iptStudentFamilyMembersMale, #iptStudentFamilyMembersFemale, #iptSiblingsBornSameParentsAmount, #iptSiblingsBornSameParentsMale, #iptSiblingsBornSameParentsFemale, #iptSiblingsBornDifferentParentsAmount, #iptSiblingsBornDifferentParentsMale, #iptSiblingsBornDifferentParentsFemale, #iptFamiliesNeedSpecialAssistance, #iptFamiliesNeedSpecialAssistanceTotal').number(true, 0, '', '');
        $('#iptStudentFamilyMembersAmount, #iptStudentFamilyMembersMale, #iptStudentFamilyMembersFemale, #iptSiblingsBornSameParentsAmount, #iptSiblingsBornSameParentsMale, #iptSiblingsBornSameParentsFemale, #iptSiblingsBornDifferentParentsAmount, #iptSiblingsBornDifferentParentsMale, #iptSiblingsBornDifferentParentsFemale, #iptFamiliesNeedSpecialAssistance, #iptFamiliesNeedSpecialAssistanceTotal').attr('maxlength', '2');

        $(document).ready(function () {

            LoadHomeVisitData('<%=Request.QueryString["schoolid"]%>', '<%=Request.QueryString["roomid"]%>', '<%=Request.QueryString["sid"]%>');

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
