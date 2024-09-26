<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="PersonalScreeningForm.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.PersonalScreeningForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/Content/VisitHouse/assets/css/material.css">
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

        #accordion textarea {
            font-size: 1rem !important;
        }

        .invisible {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content contents">
        <div class="title-bar" style="display: none;"></div>
        <div class="user">
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
        </div>
        <div id="accordion">
            <div class="card">
                <div class="card-header">
                    <a class="card-link" data-toggle="collapse" href="#collapse1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133408") %>
                    </a>
                </div>
                <div id="collapse1" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133408") %></p>
                        <div class="border-bottom-red">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoAcademic1" name="rdoAcademic" value="1" />
                                <label for="rdoAcademic1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                            </div>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoAcademic2" name="rdoAcademic" value="2" />
                                <label for="rdoAcademic2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicRisk1" name="chkAcademicRisk" value="1" />
                                <label for="chkAcademicRisk1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133402") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicRisk2" name="chkAcademicRisk" value="2" />
                                <label for="chkAcademicRisk2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133403") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicRisk3" name="chkAcademicRisk" value="3" />
                                <label for="chkAcademicRisk3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133404") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicRisk4" name="chkAcademicRisk" value="4" />
                                <label for="chkAcademicRisk4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133405") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicRisk5" name="chkAcademicRisk" value="5" />
                                <label for="chkAcademicRisk5" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133406") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicRisk99" name="chkAcademicRisk" value="99" data-element-other-id="tarAcademicRiskNote">
                                <label for="chkAcademicRisk99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarAcademicRiskNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoAcademic3" name="rdoAcademic" value="3" />
                                <label for="rdoAcademic3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem1" name="chkAcademicProblem" value="1" />
                                <label for="chkAcademicProblem1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133409") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem2" name="chkAcademicProblem" value="2" />
                                <label for="chkAcademicProblem2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133410") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem3" name="chkAcademicProblem" value="3" />
                                <label for="chkAcademicProblem3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133411") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem4" name="chkAcademicProblem" value="4" />
                                <label for="chkAcademicProblem4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133412") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem5" name="chkAcademicProblem" value="5" />
                                <label for="chkAcademicProblem5" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133413") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem6" name="chkAcademicProblem" value="6" />
                                <label for="chkAcademicProblem6" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133414") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkAcademicProblem99" name="chkAcademicProblem" value="99" data-element-other-id="tarAcademicProblemNote">
                                <label for="chkAcademicProblem99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarAcademicProblemNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133415") %>
                    </a>
                </div>
                <div id="collapse2" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <div class="accordion-choice">
                            <input type="radio" id="rdoAbilities1" name="rdoAbilities" value="1" data-element-other-id="tarAbilitiesNote">
                            <label for="rdoAbilities1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133416") %></label>
                        </div>
                        <textarea id="tarAbilitiesNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoAbilities2" name="rdoAbilities" value="2">
                            <label for="rdoAbilities2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133417") %></label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse3">3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307008") %>
                    </a>
                </div>
                <div id="collapse3" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307008") %></p>
                        <div class="border-bottom-red">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoHealth1" name="rdoHealth" value="1" />
                                <label for="rdoHealth1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                            </div>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoHealth2" name="rdoHealth" value="2" />
                                <label for="rdoHealth2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthRisk1" name="chkHealthRisk" value="1" />
                                <label for="chkHealthRisk1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133418") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthRisk2" name="chkHealthRisk" value="2" />
                                <label for="chkHealthRisk2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133419") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthRisk3" name="chkHealthRisk" value="3" />
                                <label for="chkHealthRisk3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305117") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthRisk4" name="chkHealthRisk" value="4" />
                                <label for="chkHealthRisk4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133420") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthRisk5" name="chkHealthRisk" value="5" />
                                <label for="chkHealthRisk5" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133421") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthRisk99" name="chkHealthRisk" value="99" data-element-other-id="tarHealthRiskNote">
                                <label for="chkHealthRisk99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarHealthRiskNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoHealth3" name="rdoHealth" value="3" />
                                <label for="rdoHealth3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthProblem1" name="chkHealthProblem" value="1" />
                                <label for="chkHealthProblem1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133422") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthProblem2" name="chkHealthProblem" value="2" />
                                <label for="chkHealthProblem2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133423") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthProblem3" name="chkHealthProblem" value="3" />
                                <label for="chkHealthProblem3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133424") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthProblem4" name="chkHealthProblem" value="4" />
                                <label for="chkHealthProblem4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133425") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkHealthProblem99" name="chkHealthProblem" value="99" data-element-other-id="tarHealthProblemNote">
                                <label for="chkHealthProblem99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarHealthProblemNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133426") %>
                    </a>
                </div>
                <div id="collapse4" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133427") %></p>
                        <span class="mt-3 text-dark d-block"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133428") %></span>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior11" name="rdoMentalHealthBehavior1" value="1">
                            <label for="rdoMentalHealthBehavior11" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior12" name="rdoMentalHealthBehavior1" value="2">
                            <label for="rdoMentalHealthBehavior12" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior13" name="rdoMentalHealthBehavior1" value="3">
                            <label for="rdoMentalHealthBehavior13" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                        <span class="mt-3 text-dark d-block"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133429") %></span>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior21" name="rdoMentalHealthBehavior2" value="1">
                            <label for="rdoMentalHealthBehavior21" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior22" name="rdoMentalHealthBehavior2" value="2">
                            <label for="rdoMentalHealthBehavior22" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior23" name="rdoMentalHealthBehavior2" value="3">
                            <label for="rdoMentalHealthBehavior23" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                        <span class="mt-3 text-dark d-block"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133430") %></span>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior31" name="rdoMentalHealthBehavior3" value="1">
                            <label for="rdoMentalHealthBehavior31" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior32" name="rdoMentalHealthBehavior3" value="2">
                            <label for="rdoMentalHealthBehavior32" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior33" name="rdoMentalHealthBehavior3" value="3">
                            <label for="rdoMentalHealthBehavior33" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                        <span class="mt-3 text-dark d-block"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133431") %></span>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior41" name="rdoMentalHealthBehavior4" value="1">
                            <label for="rdoMentalHealthBehavior41" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior42" name="rdoMentalHealthBehavior4" value="2">
                            <label for="rdoMentalHealthBehavior42" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior43" name="rdoMentalHealthBehavior4" value="3">
                            <label for="rdoMentalHealthBehavior43" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                        <span class="mt-3 text-dark d-block">4.5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304054") %></span>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior51" name="rdoMentalHealthBehavior5" value="1">
                            <label for="rdoMentalHealthBehavior51" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior52" name="rdoMentalHealthBehavior5" value="2">
                            <label for="rdoMentalHealthBehavior52" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoMentalHealthBehavior53" name="rdoMentalHealthBehavior5" value="3">
                            <label for="rdoMentalHealthBehavior53" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133432") %>
                    </a>
                </div>
                <div id="collapse5" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133432") %></p>
                        <div class="border-bottom-red">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoEconomic1" name="rdoEconomic" value="1" />
                                <label for="rdoEconomic1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                            </div>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoEconomic2" name="rdoEconomic" value="2" />
                                <label for="rdoEconomic2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicRisk1" name="chkEconomicRisk" value="1" />
                                <label for="chkEconomicRisk1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133433") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicRisk2" name="chkEconomicRisk" value="2" />
                                <label for="chkEconomicRisk2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133434") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicRisk3" name="chkEconomicRisk" value="3" />
                                <label for="chkEconomicRisk3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133435") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicRisk4" name="chkEconomicRisk" value="4" data-element-other-id="tarEconomicRiskMoney" />
                                <label for="chkEconomicRisk4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133436") %></label>
                            </div>
                            <textarea id="tarEconomicRiskMoney" rows="1" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicRisk99" name="chkEconomicRisk" value="99" data-element-other-id="tarEconomicRiskNote">
                                <label for="chkEconomicRisk99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarEconomicRiskNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoEconomic3" name="rdoEconomic" value="3" />
                                <label for="rdoEconomic3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicProblem1" name="chkEconomicProblem" value="1" />
                                <label for="chkEconomicProblem1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133437") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicProblem2" name="chkEconomicProblem" value="2" />
                                <label for="chkEconomicProblem2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133438") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicProblem3" name="chkEconomicProblem" value="3" />
                                <label for="chkEconomicProblem3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133439") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicProblem4" name="chkEconomicProblem" value="4" />
                                <label for="chkEconomicProblem4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133440") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicProblem5" name="chkEconomicProblem" value="5" />
                                <label for="chkEconomicProblem5" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133441") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkEconomicProblem99" name="chkEconomicProblem" value="99" data-element-other-id="tarEconomicProblemNote">
                                <label for="chkEconomicProblem99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarEconomicProblemNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133442") %>
                    </a>
                </div>
                <div id="collapse6" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133442") %></p>
                        <div class="border-bottom-red">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoProtection1" name="rdoProtection" value="1" />
                                <label for="rdoProtection1" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                            </div>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoProtection2" name="rdoProtection" value="2" />
                                <label for="rdoProtection2" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk1" name="chkProtectionRisk" value="1" />
                                <label for="chkProtectionRisk1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133443") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk2" name="chkProtectionRisk" value="2" />
                                <label for="chkProtectionRisk2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133444") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk3" name="chkProtectionRisk" value="3" />
                                <label for="chkProtectionRisk3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133445") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk4" name="chkProtectionRisk" value="4" />
                                <label for="chkProtectionRisk4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133446") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk5" name="chkProtectionRisk" value="5" />
                                <label for="chkProtectionRisk5" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133447") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk6" name="chkProtectionRisk" value="6" />
                                <label for="chkProtectionRisk6" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133448") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk7" name="chkProtectionRisk" value="7" />
                                <label for="chkProtectionRisk7" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133449") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionRisk8" name="chkProtectionRisk" value="8" />
                                <label for="chkProtectionRisk8" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133450") %></label>
                            </div>
                        </div>
                        <div class="border-bottom-red pt-2">
                            <div class="accordion-choice">
                                <input type="radio" id="rdoProtection3" name="rdoProtection" value="3" />
                                <label for="rdoProtection3" class="mb-0 ml-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem1" name="chkProtectionProblem" value="1" />
                                <label for="chkProtectionProblem1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305131") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem2" name="chkProtectionProblem" value="2" />
                                <label for="chkProtectionProblem2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133451") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem3" name="chkProtectionProblem" value="3" />
                                <label for="chkProtectionProblem3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133452") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem4" name="chkProtectionProblem" value="4" />
                                <label for="chkProtectionProblem4" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305132") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem5" name="chkProtectionProblem" value="5" />
                                <label for="chkProtectionProblem5" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133453") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem6" name="chkProtectionProblem" value="6" />
                                <label for="chkProtectionProblem6" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305205") %></label>
                            </div>
                            <div class="accordion-choice">
                                <input type="checkbox" id="chkProtectionProblem99" name="chkProtectionProblem" value="99" data-element-other-id="tarProtectionProblemNote">
                                <label for="chkProtectionProblem99" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133407") %></label>
                            </div>
                            <textarea id="tarProtectionProblemNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse7">7. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00618") %>
                    </a>
                </div>
                <div id="collapse7" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">7. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00618") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoSubstanceAbuse1" name="rdoSubstanceAbuse" value="1">
                            <label for="rdoSubstanceAbuse1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoSubstanceAbuse2" name="rdoSubstanceAbuse" value="2">
                            <label for="rdoSubstanceAbuse2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoSubstanceAbuse3" name="rdoSubstanceAbuse" value="3">
                            <label for="rdoSubstanceAbuse3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse8">8. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307013") %>
                    </a>
                </div>
                <div id="collapse8" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">8. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307013") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoSex1" name="rdoSex" value="1">
                            <label for="rdoSex1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoSex2" name="rdoSex" value="2">
                            <label for="rdoSex2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoSex3" name="rdoSex" value="3">
                            <label for="rdoSex3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapse9">9. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307014") %>
                    </a>
                </div>
                <div id="collapse9" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <p class="border-bottom-red pb-2 mb-3">9. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307014") %></p>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOtherSide1" name="rdoOtherSide" value="1">
                            <label for="rdoOtherSide1" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></label>
                        </div>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOtherSide2" name="rdoOtherSide" value="2" data-element-other-id="tarOtherSideRiskNote">
                            <label for="rdoOtherSide2" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></label>
                        </div>
                        <textarea id="tarOtherSideRiskNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                        <div class="accordion-choice">
                            <input type="radio" id="rdoOtherSide3" name="rdoOtherSide" value="3" data-element-other-id="tarOtherSideProblemNote">
                            <label for="rdoOtherSide3" class="mb-0 ml-2 h6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></label>
                        </div>
                        <textarea id="tarOtherSideProblemNote" rows="3" class="form-control mb-2" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %>" disabled></textarea>
                    </div>
                </div>
            </div>

        </div>
        <div class="card-body">
            <div class="align-items-center" style="text-align: center;">
                <button type="button" class="btn btn-success btn-round px-3 save-screening-btn <%=(VisibleSaveButton ? "" : "invisible")%>" style="width: 98%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/Content/VisitHouse/assets/js/homeVisit.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="../../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script>

        // Event Handler
        function SaveScreening(isComplete) {
            Swal.fire({
                title: 'Save!',
                text: 'Do you want to save?',
                reverseButtons: true,
                confirmButtonText: 'Save',
                cancelButtonText: 'Cancel',
                showCancelButton: true
            }).then(function (result) {
                if (result.isConfirmed) {

                    var data = {};

                    data.schoolId = '<%=Request.QueryString["schoolid"]%>';
                    data.roomId = '<%=Request.QueryString["roomid"]%>';
                    data.studentId = '<%=Request.QueryString["sid"]%>';

                    // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133408") %>
                    data.academic = $('input[name="rdoAcademic"]:checked').val();
                    switch (data.academic) {
                        case '2':
                            data.academicRisk = JSON.stringify($("input[name=chkAcademicRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.academicRisk.indexOf("99") != -1) {
                                data.academicRiskNote = $('#tarAcademicRiskNote').val();
                            }
                            break;
                        case '3':
                            data.academicProblem = JSON.stringify($("input[name=chkAcademicProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.academicProblem.indexOf("99") != -1) {
                                data.academicProblemNote = $('#tarAcademicProblemNote').val();
                            }
                            break;
                    }

                    // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133415") %>
                    data.abilities = $('input[name="rdoAbilities"]:checked').val();
                    if (data.abilities == '1') {
                        data.abilitiesNote = $('#tarAbilitiesNote').val();
                    }

                    // 3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307008") %>
                    data.health = $('input[name="rdoHealth"]:checked').val();
                    switch (data.health) {
                        case '2':
                            data.healthRisk = JSON.stringify($("input[name=chkHealthRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.healthRisk.indexOf("99") != -1) {
                                data.healthRiskNote = $('#tarHealthRiskNote').val();
                            }
                            break;
                        case '3':
                            data.healthProblem = JSON.stringify($("input[name=chkHealthProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.healthProblem.indexOf("99") != -1) {
                                data.healthProblemNote = $('#tarHealthProblemNote').val();
                            }
                            break;
                    }

                    // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133426") %>
                    data.mentalHealthBehavior1 = $('input[name="rdoMentalHealthBehavior1"]:checked').val();
                    data.mentalHealthBehavior2 = $('input[name="rdoMentalHealthBehavior2"]:checked').val();
                    data.mentalHealthBehavior3 = $('input[name="rdoMentalHealthBehavior3"]:checked').val();
                    data.mentalHealthBehavior4 = $('input[name="rdoMentalHealthBehavior4"]:checked').val();
                    data.mentalHealthBehavior5 = $('input[name="rdoMentalHealthBehavior5"]:checked').val();

                    // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133432") %>
                    data.economic = $('input[name="rdoEconomic"]:checked').val();
                    switch (data.economic) {
                        case '2':
                            data.economicRisk = JSON.stringify($("input[name=chkEconomicRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.economicRisk.indexOf("4") != -1) {
                                data.economicRiskMoney = $('#tarEconomicRiskMoney').val();
                            }
                            if (data.economicRisk.indexOf("99") != -1) {
                                data.economicRiskNote = $('#tarEconomicRiskNote').val();
                            }
                            break;
                        case '3':
                            data.economicProblem = JSON.stringify($("input[name=chkEconomicProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.economicProblem.indexOf("99") != -1) {
                                data.economicProblemNote = $('#tarEconomicProblemNote').val();
                            }
                            break;
                    }

                    // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133442") %>
                    data.protection = $('input[name="rdoProtection"]:checked').val();
                    switch (data.protection) {
                        case '2': data.protectionRisk = JSON.stringify($("input[name=chkProtectionRisk]:checked").map(function () { return parseInt($(this).val()); }).get()); break;
                        case '3':
                            data.protectionProblem = JSON.stringify($("input[name=chkProtectionProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
                            if (data.protectionProblem.indexOf("99") != -1) {
                                data.protectionProblemNote = $('#tarProtectionProblemNote').val();
                            }
                            break;
                    }

                    // 7. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00618") %>
                    data.substanceAbuse = $('input[name="rdoSubstanceAbuse"]:checked').val();

                    // 8. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307013") %>
                    data.sex = $('input[name="rdoSex"]:checked').val();

                    // 9. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307014") %>
                    data.otherSide = $('input[name="rdoOtherSide"]:checked').val();
                    switch (data.otherSide) {
                        case '2': data.otherSideRiskNote = $('#tarOtherSideRiskNote').val(); break;
                        case '3': data.otherSideProblemNote = $('#tarOtherSideProblemNote').val(); break;
                    }

                    data.status = isComplete;

                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "PersonalScreeningForm.aspx/SaveScreeningData",
                        data: "{schoolId: <%=Request.QueryString["schoolid"]%>, studentId: <%=Request.QueryString["sid"]%>, screeningData: " + JSON.stringify(data) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var r = JSON.parse(result.d);
                            if (r.success) {
                                Swal.fire('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>', r.message, 'success')
                                    .then(function () {
                                        if ('<%=Request.QueryString["client"]%>' == 'teacher') {
                                            location.href = "Index.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&roomid=<%=Request.QueryString["roomid"]%>";
                                        }
                                    });
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

        $(".save-screening-btn").click(function () {
            // Check request
            var invalid = false;
            var articleNo = '';

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133408") %>
            var academic = $('input[name="rdoAcademic"]:checked').val();
            var academicRisk = JSON.stringify($("input[name=chkAcademicRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
            var academicProblem = JSON.stringify($("input[name=chkAcademicProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
            if (academic === undefined || (academic == 2 && academicRisk == '[]') || (academic == 3 && academicProblem == '[]')) {
                articleNo += ', 1';
                invalid = true;
            }

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133415") %>
            var abilities = $('input[name="rdoAbilities"]:checked').val();
            if (abilities === undefined) {
                articleNo += ', 2';
                invalid = true;
            }

            // 3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307008") %>
            var health = $('input[name="rdoHealth"]:checked').val();
            var healthRisk = JSON.stringify($("input[name=chkHealthRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
            var healthProblem = JSON.stringify($("input[name=chkHealthProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
            if (health === undefined || (health == 2 && healthRisk == '[]') || (health == 3 && healthProblem == '[]')) {
                articleNo += ', 3';
                invalid = true;
            }

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133426") %>
            var mentalHealthBehavior1 = $('input[name="rdoMentalHealthBehavior1"]:checked').val();
            var mentalHealthBehavior2 = $('input[name="rdoMentalHealthBehavior2"]:checked').val();
            var mentalHealthBehavior3 = $('input[name="rdoMentalHealthBehavior3"]:checked').val();
            var mentalHealthBehavior4 = $('input[name="rdoMentalHealthBehavior4"]:checked').val();
            var mentalHealthBehavior5 = $('input[name="rdoMentalHealthBehavior5"]:checked').val();
            if (mentalHealthBehavior1 === undefined || mentalHealthBehavior2 === undefined || mentalHealthBehavior3 === undefined || mentalHealthBehavior4 === undefined || mentalHealthBehavior5 === undefined) {
                articleNo += ', 4';
                invalid = true;
            }

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133432") %>
            var economic = $('input[name="rdoEconomic"]:checked').val();
            var economicRisk = JSON.stringify($("input[name=chkEconomicRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
            var economicProblem = JSON.stringify($("input[name=chkEconomicProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
            if (economic === undefined || (economic == 2 && economicRisk == '[]') || (economic == 3 && economicProblem == '[]')) {
                articleNo += ', 5';
                invalid = true;
            }

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133442") %>
            var protection = $('input[name="rdoProtection"]:checked').val();
            var protectionRisk = JSON.stringify($("input[name=chkProtectionRisk]:checked").map(function () { return parseInt($(this).val()); }).get());
            var protectionProblem = JSON.stringify($("input[name=chkProtectionProblem]:checked").map(function () { return parseInt($(this).val()); }).get());
            if (protection === undefined || (protection == 2 && protectionRisk == '[]') || (protection == 3 && protectionProblem == '[]')) {
                articleNo += ', 6';
                invalid = true;
            }

            // 7. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00618") %>
            var substanceAbuse = $('input[name="rdoSubstanceAbuse"]:checked').val();
            if (substanceAbuse === undefined) {
                articleNo += ', 7';
                invalid = true;
            }

            // 8. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307013") %>
            var sex = $('input[name="rdoSex"]:checked').val();
            if (sex === undefined) {
                articleNo += ', 8';
                invalid = true;
            }

            // 9. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307014") %>
            var otherSide = $('input[name="rdoOtherSide"]:checked').val();
            if (otherSide === undefined) {
                articleNo += ', 9';
                invalid = true;
            }

            var alertMessage = '';
            if (invalid) {
                alertMessage = `<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133454") %> `+ articleNo.substring(2) + `
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133455") %>
และกด "บันทึก" เพื่อส่งข้อมูลอีกครั้ง`;

                Swal.fire({
                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133456") %>',
                    text: alertMessage,
                    icon: 'info',
                    confirmButtonText: 'OK'
                }).then(function (result) {
                    if (result.isConfirmed) {
                        SaveScreening(0);
                    }
                });
            }
            else {
                SaveScreening(1);
            }
        });

        $(".accordion-choice input[type='checkbox'][data-element-other-id]").click(function () {
            var otherId = $(this).data('element-other-id');
            if ($(this).is(":checked")) {
                $('#' + otherId).prop('disabled', false);
            }
            else {
                $('#' + otherId).val('');
                $('#' + otherId).prop('disabled', true);
            }
        });

        $(".accordion-choice input[type='radio']").change(function () {
            var otherId = $(this).data('element-other-id');

            $(this).closest('.card-body').find($("textarea[id='" + otherId + "']")).prop('disabled', false);

            $(this).closest('.card-body').find($("textarea[id!='" + otherId + "']")).val('');
            $(this).closest('.card-body').find($("textarea[id!='" + otherId + "']")).prop('disabled', true);
        });

        $(".accordion-choice input[type='radio']").change(function () {
            if ($(this).attr('name') == 'rdoAcademic' || $(this).attr('name') == 'rdoHealth' || $(this).attr('name') == 'rdoEconomic' || $(this).attr('name') == 'rdoProtection') {

                var checkboxName = $(this).attr('name').replace('rdo', 'chk');

                $(this).closest('.card-body').find($(".accordion-choice input[name!='" + checkboxName + ($(this).val() == '2' ? 'Risk' : ($(this).val() == '3' ? 'Problem' : 'Well')) + "']")).prop("disabled", true);
                $(this).closest('.card-body').find($(".accordion-choice input[name!='" + checkboxName + ($(this).val() == '2' ? 'Risk' : ($(this).val() == '3' ? 'Problem' : 'Well')) + "']")).prop("checked", false);
                $(this).closest('.card-body').find($(".accordion-choice input[name!='" + checkboxName + ($(this).val() == '2' ? 'Risk' : ($(this).val() == '3' ? 'Problem' : 'Well')) + "'][data-element-other-id]")).each(function (index) {
                    var otherId = $(this).data('element-other-id');
                    $('#' + otherId).val('');
                    $('#' + otherId).prop('disabled', true);
                });

                $(this).closest('.card-body').find($(".accordion-choice input[name='" + checkboxName + ($(this).val() == '2' ? 'Risk' : ($(this).val() == '3' ? 'Problem' : 'Well')) + "']")).prop("disabled", false);
                //$(this).closest('.card-body').find($(".accordion-choice input[name='" + checkboxName + ($(this).val() == '2' ? 'Risk' : ($(this).val() == '3' ? 'Problem' : 'Well')) + "']")).prop("checked", true);

                $(".accordion-choice input[name='" + $(this).attr('name') + "']").prop("checked", false);
                $(".accordion-choice input[name='" + $(this).attr('name') + "']").prop("disabled", false);
                $(this).prop("checked", true);
                $(this).prop("disabled", false);
            }

            if ($(this).attr('name') == 'rdoAbilities') {
                if ($(this).val() == '2') {
                    $(this).closest('.card-body').find($(".accordion-choice input[name='" + $(this).attr('name') + "'][data-element-other-id]")).each(function (index) {
                        var otherId = $(this).data('element-other-id');
                        $('#' + otherId).val('');
                        $('#' + otherId).prop('disabled', true);
                    });
                }

                $(".accordion-choice input[name='" + $(this).attr('name') + "']").prop("checked", false);
                $(".accordion-choice input[name='" + $(this).attr('name') + "']").prop("disabled", false);
                $(this).prop("checked", true);
                $(this).prop("disabled", false);
            }
        });

        function LoadScreeningData(schoolId, studentId) {
            $.ajax({
                type: "POST",
                url: "PersonalScreeningForm.aspx/GetScreeningData",
                data: '{schoolID: ' + schoolId + ', studentID: ' + studentId + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var r = JSON.parse(result.d); console.log(r);
                    if (r.success && r.data) {
                        // Show/hide save date data
                        $('.title-bar').html(r.data.updateDate ?? r.data.saveDate).show();

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133408") %>
                        $('input:radio[name=rdoAcademic][value=' + r.data.academic + ']').prop('checked', true);
                        $.each(JSON.parse(r.data.academicRisk), function (index, value) {
                            $('input:checkbox[name=chkAcademicRisk][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarAcademicRiskNote').val(r.data.academicRiskNote);
                        $.each(JSON.parse(r.data.academicProblem), function (index, value) {
                            $('input:checkbox[name=chkAcademicProblem][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarAcademicProblemNote').val(r.data.academicProblemNote);
                        if (r.data.academic == null) {
                            $('#rdoAcademic1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("disabled", true);
                            $('#rdoAcademic1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("checked", false);
                        }
                        else {
                            $('#rdoAcademic1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkAcademic" + (r.data.academic == '2' ? 'Risk' : (r.data.academic == '3' ? 'Problem' : 'Well')) + "']")).prop("disabled", true);
                            $('#rdoAcademic1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkAcademic" + (r.data.academic == '2' ? 'Risk' : (r.data.academic == '3' ? 'Problem' : 'Well')) + "']")).prop("checked", false);
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133415") %>
                        $('input:radio[name=rdoAbilities][value=' + r.data.abilities + ']').prop('checked', true);
                        $('#tarAbilitiesNote').val(r.data.abilitiesNote);

                        // 3. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307008") %>
                        $('input:radio[name=rdoHealth][value=' + r.data.health + ']').prop('checked', true);
                        $.each(JSON.parse(r.data.healthRisk), function (index, value) {
                            $('input:checkbox[name=chkHealthRisk][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarHealthRiskNote').val(r.data.healthRiskNote);
                        $.each(JSON.parse(r.data.healthProblem), function (index, value) {
                            $('input:checkbox[name=chkHealthProblem][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarHealthProblemNote').val(r.data.healthProblemNote);
                        if (r.data.health == null) {
                            $('#rdoHealth1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("disabled", true);
                            $('#rdoHealth1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("checked", false);
                        }
                        else {
                            $('#rdoHealth1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkHealth" + (r.data.health == '2' ? 'Risk' : (r.data.health == '3' ? 'Problem' : 'Well')) + "']")).prop("disabled", true);
                            $('#rdoHealth1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkHealth" + (r.data.health == '2' ? 'Risk' : (r.data.health == '3' ? 'Problem' : 'Well')) + "']")).prop("checked", false);
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133426") %>
                        $('input:radio[name=rdoMentalHealthBehavior1][value=' + r.data.mentalHealthBehavior1 + ']').prop('checked', true)
                        $('input:radio[name=rdoMentalHealthBehavior2][value=' + r.data.mentalHealthBehavior2 + ']').prop('checked', true)
                        $('input:radio[name=rdoMentalHealthBehavior3][value=' + r.data.mentalHealthBehavior3 + ']').prop('checked', true)
                        $('input:radio[name=rdoMentalHealthBehavior4][value=' + r.data.mentalHealthBehavior4 + ']').prop('checked', true)
                        $('input:radio[name=rdoMentalHealthBehavior5][value=' + r.data.mentalHealthBehavior5 + ']').prop('checked', true)
                        // SB-11188
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133457") %>
                        //$('input:radio[name^="rdoMentalHealthBehavior"]').prop("disabled", true);

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133432") %>
                        $('input:radio[name=rdoEconomic][value=' + r.data.economic + ']').prop('checked', true);
                        $.each(JSON.parse(r.data.economicRisk), function (index, value) {
                            $('input:checkbox[name=chkEconomicRisk][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarEconomicRiskMoney').val(r.data.economicRiskMoney);
                        $('#tarEconomicRiskNote').val(r.data.economicRiskNote);
                        $.each(JSON.parse(r.data.economicProblem), function (index, value) {
                            $('input:checkbox[name=chkEconomicProblem][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarEconomicProblemNote').val(r.data.economicProblemNote);
                        if (r.data.economic == null) {
                            $('#rdoEconomic1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("disabled", true);
                            $('#rdoEconomic1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("checked", false);
                        }
                        else {
                            $('#rdoEconomic1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkEconomic" + (r.data.economic == '2' ? 'Risk' : (r.data.economic == '3' ? 'Problem' : 'Well')) + "']")).prop("disabled", true);
                            $('#rdoEconomic1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkEconomic" + (r.data.economic == '2' ? 'Risk' : (r.data.economic == '3' ? 'Problem' : 'Well')) + "']")).prop("checked", false);
                        }

                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133442") %>
                        $('input:radio[name=rdoProtection][value=' + r.data.protection + ']').prop('checked', true);
                        $.each(JSON.parse(r.data.protectionRisk), function (index, value) {
                            $('input:checkbox[name=chkProtectionRisk][value=' + value + ']').prop('checked', true);
                        });
                        $.each(JSON.parse(r.data.protectionProblem), function (index, value) {
                            $('input:checkbox[name=chkProtectionProblem][value=' + value + ']').prop('checked', true);
                        });
                        $('#tarProtectionProblemNote').val(r.data.protectionProblemNote);
                        if (r.data.protection == null) {
                            $('#rdoProtection1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("disabled", true);
                            $('#rdoProtection1').closest('.card-body').find($(".accordion-choice input[type=checkbox]")).prop("checked", false);
                        }
                        else {
                            $('#rdoProtection1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkProtection" + (r.data.protection == '2' ? 'Risk' : (r.data.protection == '3' ? 'Problem' : 'Well')) + "']")).prop("disabled", true);
                            $('#rdoProtection1').closest('.card-body').find($(".accordion-choice input[type=checkbox][name!='chkProtection" + (r.data.protection == '2' ? 'Risk' : (r.data.protection == '3' ? 'Problem' : 'Well')) + "']")).prop("checked", false);
                        }

                        // 7. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00618") %>
                        $('input:radio[name=rdoSubstanceAbuse][value=' + r.data.substanceAbuse + ']').prop('checked', true);

                        // 8. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307013") %>
                        $('input:radio[name=rdoSex][value=' + r.data.sex + ']').prop('checked', true);

                        // 9. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M307014") %>
                        $('input:radio[name=rdoOtherSide][value=' + r.data.otherSide + ']').prop('checked', true);
                        $('#tarOtherSideRiskNote').val(r.data.otherSideRiskNote);
                        $('#tarOtherSideProblemNote').val(r.data.otherSideProblemNote);

                        // Enable element other id at radiobox and checkbox checked
                        $(".accordion-choice input[type='radio'][data-element-other-id]:checked, .accordion-choice input[type='checkbox'][data-element-other-id]:checked").each(function () {
                            $('#' + $(this).data('element-other-id')).prop('disabled', false);
                        });

                        <%if (!VisibleSaveButton) {%>
                        // Disabled all eleme
                        $('#accordion input, #accordion textarea').each(function (i) { $(this).attr("disabled", "disabled"); });
                        <%}%>
                    }
                    else {
                        $('#accordion input[type=checkbox]').each(function (i) { $(this).attr("disabled", "disabled"); });
                        // SB-11188
                        // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133457") %>
                        //$('input:radio[name^="rdoMentalHealthBehavior"]').prop("disabled", true);
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

        $(document).ready(function () {

            LoadScreeningData('<%=Request.QueryString["schoolid"]%>', '<%=Request.QueryString["sid"]%>');

        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
