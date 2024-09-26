<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="SDQForm.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.SDQForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!-- Fonts and icons -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />

    <style>
        .material-icons {
            font-family: 'Material Icons';
            font-weight: normal;
            font-style: normal;
            font-size: 24px;
            line-height: 1;
            letter-spacing: normal;
            text-transform: none;
            display: inline-block;
            white-space: nowrap;
            word-wrap: normal;
            direction: ltr;
            -webkit-font-feature-settings: 'liga';
            -webkit-font-smoothing: antialiased;
        }

        .btn.btn-default {
            color: #fff;
            background-color: #999999;
            border-color: #999999;
            box-shadow: 0 2px 2px 0 rgb(153 153 153 / 14%), 0 3px 1px -2px rgb(153 153 153 / 20%), 0 1px 5px 0 rgb(153 153 153 / 12%);
        }

        .question-numbers ul {
            background-color: #fff;
        }

        .answer-choice.complete {
            color: white;
            background-color: var(--bg-green);
        }

        footer {
            position: fixed;
            bottom: 0;
            width: 100%;
            height: 59px;
            border-top: 3px solid var(--black);
            padding: 0 40px;
        }

            footer .prev {
                /*padding: 19px 15px 17px 15px;
                background-color: #fff;*/
                padding: 9px 1px 9px 2px;
                background-color: #fff;
                font-size: 38px;
            }

            footer .next {
                /*padding: 19px 13px 17px 15px;
                background-color: #fff;*/
                padding: 9px 1px 9px 1px;
                background-color: #fff;
                font-size: 38px;
            }

        .disabled {
            pointer-events: none;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="SectionView1" runat="server">
            <div class="main-content contents">
                <div class="title-bar" style="display: none;">
                    28 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %> 2565
                </div>
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
                    <% if (VisibleSaveButton)
                        {%>
                    <div class="px-4 ml-auto">
                        <button type="button" class="btn btn-success btn-round px-3 save-draft-btn" style="width: 110px; height: 39px; margin-bottom: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %></button><br />
                        <button type="button" class="btn <%=StyleSaveButton1%> btn-round px-3 save-btn" style="width: 110px; height: 39px;" <%=DisabledSaveButton1%>><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    </div>
                    <% }%>
                </div>
                <div class="SDQ-form">
                    <span class="ques-num">1</span> /<span class="ques-all">25</span>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00969") %>
                </div>
                <div class="sdq-content" style="padding-bottom: 90px;">
                    <p class="mt-3 ques-title">?</p>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %></div>
                </div>
            </div>
            <footer class="question-numbers">
                <%--<img src="/Content/VisitHouse/assets/img/prev.png" class="prev">--%>
                <span class="material-icons prev">navigate_before</span>
                <ul></ul>
                <%--<img src="/Content/VisitHouse/assets/img/next.png" class="next">--%>
                <span class="material-icons next">navigate_next</span>
            </footer>
        </asp:View>
        <asp:View ID="SectionView2" runat="server">
            <style>
                .sdq-content {
                    display: none;
                }

                    .sdq-content.active {
                        display: block;
                        padding-bottom: 90px;
                    }

                    .sdq-content .sub-ques-title {
                        padding-bottom: 0px;
                        border-bottom: 0px solid var(--red);
                    }

                select {
                    border: 1px solid var(--light);
                    border-radius: 7px;
                    width: 100%;
                    color: var(--black);
                    box-shadow: 0 2px 8px 0 rgb(0 0 0 / 12%), 0 0px 0px 0 rgb(0 0 0 / 12%);
                    text-align: center;
                    padding: 5px;
                    line-height: 5;
                    overflow-y: hidden;
                }

                    select:focus {
                        border-color: gray;
                        outline: none;
                    }

                option:checked {
                    background-color: var(--bg-green);
                    color: #fff;
                }
            </style>
            <div class="main-content contents">
                <div class="title-bar" style="display: none;">
                    28 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %> 2565
                </div>
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
                    <% if (VisibleSaveButton)
                        {%>
                    <div class="px-4 ml-auto">
                        <button type="button" class="btn btn-success btn-round px-3 save-draft-btn" style="width: 110px; height: 39px; margin-bottom: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %></button><br />
                        <button type="button" class="btn <%=StyleSaveButton2%> btn-round px-3 save-btn" style="width: 110px; height: 39px;" <%=DisabledSaveButton2%>><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    </div>
                    <% }%>
                </div>
                <div class="SDQ-form">
                    <span class="ques-num">1</span>. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00969") %>
                </div>
                <div class="sdq-content active" data-no="1" data-id="26" data-name="Question26" data-group="6">
                    <p class="mt-3 ques-title"><%= (QuestionsForStudent ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133458") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133459") %>") %></p>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="1" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="2" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133100") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="3" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133101") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="4" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133102") %></div>
                </div>
                <div class="sdq-content" data-no="2" data-id="27" data-name="Question27" data-group="6">
                    <p class="mt-3 ques-title">ปัญหานี้เกิดขึ้นมานานเท่าไรแล้ว</p>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="1" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133104") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="2" data-no="2">1-5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="3" data-no="3">6–12 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="4" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133107") %></div>
                </div>
                <div class="sdq-content" data-no="3" data-id="28" data-name="Question28" data-group="6">
                    <p class="mt-3 ques-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133460") %></p>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></div>
                </div>
                <% if (UserType == 1)
                    {%>
                <div class="sdq-content" data-no="4" data-group="6">
                    <p class="mt-3 ques-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133461") %></p>
                    <p class="mt-3 sub-ques-title">4.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133113") %></p>
                    <select data-no="4.1" data-id="29" data-name="Question29" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                    <p class="mt-3 sub-ques-title">4.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133114") %></p>
                    <select data-no="4.2" data-id="30" data-name="Question30" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                    <p class="mt-3 sub-ques-title">4.3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133115") %></p>
                    <select data-no="4.3" data-id="31" data-name="Question31" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                    <p class="mt-3 sub-ques-title">4.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133116") %></p>
                    <select data-no="4.4" data-id="32" data-name="Question32" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                </div>
                <% }
                    else if (UserType == 2)
                    {%>
                <div class="sdq-content" data-no="4" data-group="6">
                    <p class="mt-3 ques-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133461") %></p>
                    <p class="mt-3 sub-ques-title">4.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133114") %></p>
                    <select data-no="4.2" data-id="30" data-name="Question30" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                    <p class="mt-3 sub-ques-title">4.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133115") %></p>
                    <select data-no="4.3" data-id="31" data-name="Question31" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                </div>
                <% }
                    else if (UserType == 3)
                    {%>
                <div class="sdq-content" data-no="4" data-group="6">
                    <p class="mt-3 ques-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133462") %></p>
                    <p class="mt-3 sub-ques-title">4.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133113") %></p>
                    <select data-no="4.1" data-id="29" data-name="Question29" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                    <p class="mt-3 sub-ques-title">4.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133114") %></p>
                    <select data-no="4.2" data-id="30" data-name="Question30" <%=(VisibleSaveButton ? "" : "disabled")%>>
                        <option value="" disabled selected><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %></option>
                        <option value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></option>
                        <option value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></option>
                        <option value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></option>
                        <option value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></option>
                    </select>
                </div>
                <% } %>
                <div class="sdq-content" data-no="5" data-id="33" data-name="Question33" data-group="6">
                    <p class="mt-3 ques-title"><%=(UserType == 1 ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133464") %>" : (UserType == 2 ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133463") %>" : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133462") %>")) %></p>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="0" data-no="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="0" data-no="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="1" data-no="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></div>
                    <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>" data-value="2" data-no="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></div>
                </div>
            </div>
            <footer class="question-numbers <%=DisableNavigator%>">
                <%--<img src="/Content/VisitHouse/assets/img/prev.png" class="prev">--%>
                <span class="material-icons prev">navigate_before</span>
                <ul></ul>
                <%--<img src="/Content/VisitHouse/assets/img/next.png" class="next">--%>
                <span class="material-icons next">navigate_next</span>
            </footer>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <asp:MultiView ID="MvScript" runat="server">
        <asp:View ID="SectionScript1" runat="server">
            <script>

                // SDQ question data
                var choiceLabel = ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %>']; // choices.no: 1 = <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %>, 2 = <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %>, 3 = <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %>
                <% if (QuestionsForStudent)
                {%>
                var questions = [
                    { no: 1, id: 1, name: 'Question1', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133073") %>', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 2, id: 2, name: 'Question2', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133074") %>', choices: [{ no: 1, value: 0, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 2, group: 3 }] },
                    { no: 3, id: 3, name: 'Question3', title: 'ฉันปวดศีรษะ ปวดท้อง หรือไม่สบายบ่อยๆ', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 4, id: 4, name: 'Question4', title: 'ฉันเต็มใจแบ่งปันสิ่งของให้คนอื่น (ของกิน เกม ปากกา เป็นต้น)', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 5, id: 5, name: 'Question5', title: 'ฉันโกรธแรง และมักอารมณ์เสีย', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 6, id: 6, name: 'Question6', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133078") %>', choices: [{ no: 1, value: 0, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 2, group: 4 }] },
                    { no: 7, id: 7, name: 'Question7', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133079") %>', choices: [{ no: 1, value: 2, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 0, group: 2 }] },
                    { no: 8, id: 8, name: 'Question8', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133080") %>', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 9, id: 9, name: 'Question9', title: 'ใคร ๆ ก็พึ่งฉันได้ถ้าเขาเสียใจ อารมณ์ไม่ดีหรือไม่สบายใจ', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 10, id: 10, name: 'Question10', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133082") %>', choices: [{ no: 1, value: 0, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 2, group: 3 }] },
                    { no: 11, id: 11, name: 'Question11', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133083") %>', choices: [{ no: 1, value: 2, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 0, group: 4 }] },
                    { no: 12, id: 12, name: 'Question12', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133084") %>', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 13, id: 13, name: 'Question13', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133085") %>', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 14, id: 14, name: 'Question14', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133086") %>', choices: [{ no: 1, value: 2, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 0, group: 4 }] },
                    { no: 15, id: 15, name: 'Question15', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133087") %>', choices: [{ no: 1, value: 0, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 2, group: 3 }] },
                    { no: 16, id: 16, name: 'Question16', title: 'ฉันกังวลเวลาอยู่ในสถานการณ์ที่ไม่คุ้น และเสียความเชื่อมั่นในตนเองง่าย', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 17, id: 17, name: 'Question17', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133089") %>', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 18, id: 18, name: 'Question18', title: 'มีคนว่าฉันโกหก หรือขี้โกงบ่อย ๆ', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 19, id: 19, name: 'Question19', title: 'เด็ก ๆ คนอื่น ล้อเลียนหรือรังแกฉัน', choices: [{ no: 1, value: 0, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 2, group: 4 }] },
                    { no: 20, id: 20, name: 'Question20', title: 'ฉันมักจะอาสาช่วยเหลือคนอื่น (พ่อแม่ ครู เด็กคนอื่น)', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 21, id: 21, name: 'Question21', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133093") %>', choices: [{ no: 1, value: 2, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 0, group: 3 }] },
                    { no: 22, id: 22, name: 'Question22', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133094") %> ๆ', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 23, id: 23, name: 'Question23', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133095") %>', choices: [{ no: 1, value: 0, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 2, group: 4 }] },
                    { no: 24, id: 24, name: 'Question24', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133096") %>', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 25, id: 25, name: 'Question25', title: 'ฉันทำงานได้จนสำเร็จ ความตั้งใจในการทำงานของฉันดี', choices: [{ no: 1, value: 2, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 0, group: 3 }] }
                ];
                <% }
                else
                {%>
                var questions = [
                    { no: 1, id: 1, name: 'Question1', title: 'ห่วงใยความ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ู้สึก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่น', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 2, id: 2, name: 'Question2', title: 'อยู่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>สุ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %> เคลื่อนไหว<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>สามา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ถอยู่นิ่ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %>นาน', choices: [{ no: 1, value: 0, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 2, group: 3 }] },
                    { no: 3, id: 3, name: 'Question3', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>วดศี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ษะ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>วดท้อง <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>สบายบ่อย ๆ', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 4, id: 4, name: 'Question4', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132248") %>ใจแบ่ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>ันสิ่ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>องให้เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื่อน (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>นม <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>องเล่น ดินสอ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %>ต้น)', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 5, id: 5, name: 'Question5', title: 'มักจะ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>ละวาด <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>โมโห<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>้าย', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 6, id: 6, name: 'Question6', title: 'ค่อน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>้างอยู่โดดเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>่ยว มักเล่น<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %>มลำ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ัง', choices: [{ no: 1, value: 0, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 2, group: 4 }] },
                    { no: 7, id: 7, name: 'Question7', title: 'เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>ฟัง มักจะทำ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %>มที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>ู้<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106110") %>ต้องกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>', choices: [{ no: 1, value: 2, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 0, group: 2 }] },
                    { no: 8, id: 8, name: 'Question8', title: 'กังวลใจห<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105016") %>ย<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105069") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404030") %>วิตกกังวลเสมอ', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 9, id: 9, name: 'Question9', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ึ่ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่นเสียใจ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02323") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>สบายใจ', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 10, id: 10, name: 'Question10', title: 'อยู่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>สุ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %> วุ่นวายอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01457") %>ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %>', choices: [{ no: 1, value: 0, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 2, group: 3 }] },
                    { no: 11, id: 11, name: 'Question11', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %>เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื่อนสนิท', choices: [{ no: 1, value: 2, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 0, group: 4 }] },
                    { no: 12, id: 12, name: 'Question12', title: 'มัก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105069") %>ทะเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105016") %>ะวิวาท<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132955") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่น <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ังแก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่น', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 13, id: 13, name: 'Question13', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306025") %> เศ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>้า <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>้องไห้บ่อย ๆ', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 14, id: 14, name: 'Question14', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %>ที่ชื่นชอบ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>องเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื่อน', choices: [{ no: 1, value: 2, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 0, group: 4 }] },
                    { no: 15, id: 15, name: 'Question15', title: 'วอกแวก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204042") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02108") %>', choices: [{ no: 1, value: 0, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 2, group: 3 }] },
                    { no: 16, id: 16, name: 'Question16', title: 'วิตกกังวล เมื่ออยู่ในสถานกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ณ์ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>คุ้น และ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>ความเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>มั่นในตนเอง', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 17, id: 17, name: 'Question17', title: 'ใจ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132955") %>เด็กที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111037") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305106") %>กว่า', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 18, id: 18, name: 'Question18', title: 'ชอบโกหก <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>ี้โกง', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 19, id: 19, name: 'Question19', title: 'เด็ก ๆ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่น ล้อเลียน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ังแกฉัน', choices: [{ no: 1, value: 0, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 2, group: 4 }] },
                    { no: 20, id: 20, name: 'Question20', title: 'ชอบ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>สาช่วยเหลือ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่น (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210024") %> เด็ก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>อื่น)', choices: [{ no: 1, value: 0, group: 5 }, { no: 2, value: 1, group: 5 }, { no: 3, value: 2, group: 5 }] },
                    { no: 21, id: 21, name: 'Question21', title: 'คิดก่อนทำ', choices: [{ no: 1, value: 2, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 0, group: 3 }] },
                    { no: 22, id: 22, name: 'Question22', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>โมย<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>องที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %> ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>ที่อื่น', choices: [{ no: 1, value: 0, group: 2 }, { no: 2, value: 1, group: 2 }, { no: 3, value: 2, group: 2 }] },
                    { no: 23, id: 23, name: 'Question23', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132955") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>ู้<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106110") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>กว่าเด็กวัยเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>ยวกัน', choices: [{ no: 1, value: 0, group: 4 }, { no: 2, value: 1, group: 4 }, { no: 3, value: 2, group: 4 }] },
                    { no: 24, id: 24, name: 'Question24', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %>ควา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206417") %>ลัวห<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105016") %>ย<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105069") %> หวาดกลัว<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204042") %>', choices: [{ no: 1, value: 0, group: 1 }, { no: 2, value: 1, group: 1 }, { no: 3, value: 2, group: 1 }] },
                    { no: 25, id: 25, name: 'Question25', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102096") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %>จนเส<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>็จ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %>ความตั้งใจในกา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102096") %>', choices: [{ no: 1, value: 2, group: 3 }, { no: 2, value: 1, group: 3 }, { no: 3, value: 0, group: 3 }] }
                ];
                <% } %>

                var sdqChooseData = [<%=SDQChooseData1%>];

                // Generate question numbers
                for (var i = 1; i <= questions.length; i++) {
                    var li = "<li>" + i + "</li>";
                    $(".question-numbers ul").append(li);
                }

                // Init first
                var questionObj = questions[0];
                $('.ques-num').text(1);
                $('.ques-all').text(questions.length);
                $('.ques-title').text(questionObj.title);
                $.each(questionObj.choices, function (i, obj) {
                    $('.answer-choice:eq(' + i + ')')
                        .data('question-id', questionObj.id)
                        .data('question-name', questionObj.name)
                        .data('choice-no', obj.no)
                        .data('choice-value', obj.value)
                        .data('choice-group', obj.group)
                        .text(choiceLabel[i]);

                    // Init set have already save data
                    if (sdqChooseData.length > 0) {
                        var objIndex = sdqChooseData.findIndex((o => o.questionId == questionObj.id && o.choiceNo == obj.no));
                        if (objIndex != -1 && !$('.answer-choice:eq(' + i + ')').hasClass('complete')) {
                            $('.answer-choice:eq(' + i + ')').addClass('complete')
                        }
                    }
                });
                $('.question-numbers ul li:nth-child(1)').addClass("active");

                // Init set have already save data
                if (sdqChooseData.length > 0) {
                    $('.question-numbers ul li').each(function (i) {
                        var objIndex = sdqChooseData.findIndex((obj => obj.questionId == questions[i].id));
                        if (objIndex != -1) {
                            $('.question-numbers ul li:nth-child(' + (i + 1) + ')').addClass("complete");
                        }
                    });
                }

                // Click question numbers of footer
                $(".question-numbers ul li").click(function () {
                    var thisNumber = parseInt($(this).text());
                    var currentNumber = parseInt($(".ques-num").text());

                    moveQuestion(thisNumber - currentNumber);
                });


                // Click next icon of footer
                $(".next").click(function () {
                    moveQuestion(1);
                });

                $(".prev").click(function () {
                    moveQuestion(-1);
                });

                // Click true or false button
                $(".answer-choice").click(function () {
                    SaveSDQChooseData($(this));
                    moveQuestion(1);

                    // Check all complete sdq
                    if (sdqChooseData.length == questions.length) {
                        $('.save-btn').removeClass('btn-default').addClass('btn-success').prop("disabled", false);
                    }
                });

                function moveQuestion(nextStep) {
                    var currentNumber = parseInt($(".ques-num").text());

                    if (currentNumber == questions.length && nextStep > 0) nextStep = 0;

                    var toNumber = currentNumber + nextStep;

                    if (toNumber < 1 || questions.length < toNumber) return;

                    var questionObj = questions[toNumber - 1];

                    $(".ques-num").text(toNumber);
                    $(".ques-title").text(questionObj.title);

                    $.each(questionObj.choices, function (i, obj) {
                        $('.answer-choice:eq(' + i + ')')
                            .data('question-id', questionObj.id)
                            .data('question-name', questionObj.name)
                            .data('choice-no', obj.no)
                            .data('choice-value', obj.value)
                            .data('choice-group', obj.group)
                            .text(choiceLabel[i])
                            .removeClass('complete');

                        var objIndex = sdqChooseData.findIndex((o => o.questionId == questionObj.id && o.choiceNo == obj.no));
                        if (objIndex != -1 && !$('.answer-choice:eq(' + i + ')').hasClass('complete')) {
                            $('.answer-choice:eq(' + i + ')').addClass('complete')
                        }
                    });

                    $('.question-numbers ul li:nth-child(' + currentNumber + ')').removeClass("active");
                    if (!$('.question-numbers ul li:nth-child(' + currentNumber + ')').hasClass('complete')) {
                        var objIndex = sdqChooseData.findIndex((obj => obj.questionId == questions[currentNumber - 1].id));
                        if (objIndex != -1) {
                            $('.question-numbers ul li:nth-child(' + currentNumber + ')').addClass("complete");
                        }
                    }

                    $('.question-numbers ul li:nth-child(' + toNumber + ')').addClass("active");

                    $('li.active')[0].scrollIntoView();
                }

                function SaveSDQChooseData(answer) {
                    var objIndex = sdqChooseData.findIndex((obj => obj.questionId == answer.data('question-id')));
                    if (objIndex == -1) {
                        sdqChooseData.push({ questionId: answer.data('question-id'), questionName: answer.data('question-name'), choiceNo: answer.data('choice-no'), choiceValue: answer.data('choice-value'), choiceGroup: answer.data('choice-group') });
                    }
                    else {
                        sdqChooseData[objIndex].questionId = answer.data('question-id');
                        sdqChooseData[objIndex].questionName = answer.data('question-name');
                        sdqChooseData[objIndex].choiceNo = answer.data('choice-no');
                        sdqChooseData[objIndex].choiceValue = answer.data('choice-value');
                        sdqChooseData[objIndex].choiceGroup = answer.data('choice-group');
                    }
                }

                function SaveSDQDataToDatabase() {
                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "SDQForm.aspx/SaveSDQDataSection1",
                        data: "{schoolId: <%=Request.QueryString["schoolid"]%>, studentId: <%=Request.QueryString["sid"]%>, typeId: <%=Request.QueryString["type"]%>, sdqDatas: " + JSON.stringify(sdqChooseData) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var r = JSON.parse(result.d);
                            if (r.success) {
                                Swal.fire('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>', r.message, 'success')
                                    .then(function () {
                                        location.href = "SDQSection.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=<%=Request.QueryString["type"]%>&client=<%=Request.QueryString["client"]%>";
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

                $(".save-draft-btn").click(function () {
                    if (sdqChooseData.length > 0) {
                        // Check all complete sdq
                        var answer = [];
                        $.each(questions, function (i, obj) {
                            var objIndex = sdqChooseData.findIndex((o => o.questionId == obj.id));
                            if (objIndex == -1) {
                                answer.push(obj.id);
                            }
                        });

                        if (answer.length > 0) {
                            Swal.fire({
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210025") %>',
                                html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133465") %> ' + answer.join(', ') + '<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133455") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133466") %>',
                                reverseButtons: true,
                                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                                showCancelButton: true
                            }).then(function (confirm) {
                                if (confirm.isConfirmed) {
                                    SaveSDQDataToDatabase();
                                }
                            });
                        }
                        else {
                            Swal.fire({
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %>',
                                html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133468") %>',
                                reverseButtons: true,
                                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                                showCancelButton: true
                            }).then(function (confirm) {
                                if (confirm.isConfirmed) {
                                    SaveSDQDataToDatabase();
                                }
                            });
                        }
                    }
                    else {
                        Swal.fire('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133469") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133470") %>', 'info');
                    }
                });

                $(".save-btn").click(function () {
                    Swal.fire({
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %>',
                        html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133468") %>',
                        reverseButtons: true,
                        confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                        cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                        showCancelButton: true
                    }).then(function (confirm) {
                        if (confirm.isConfirmed) {
                            SaveSDQDataToDatabase();
                        }
                    });
                });

            </script>
        </asp:View>
        <asp:View ID="SectionScript2" runat="server">
            <script>

                // SDQ get saved data
                var sdqChooseData = [<%=SDQChooseData2%>];

                // Generate question numbers
                for (var i = 1; i <= $('.sdq-content').length; i++) {
                    var li = "<li class='" + (i == 1 ? "active" : "") + "'>" + i + "</li>";
                    $(".question-numbers ul").append(li);
                }

                // Init set have already save data
                if (sdqChooseData.length > 0) {
                    $('.question-numbers ul li').each(function (i) {
                        var choiceNo = parseInt($(this).text());
                        if (choiceNo == 4) {
                            var question4All = $('.sdq-content[data-no=' + choiceNo + '] select').length;
                            var question4Complete = 0;
                            $('.sdq-content[data-no=' + choiceNo + '] select').each(function (eIndex) {
                                var objIndex = sdqChooseData.findIndex((obj => obj.questionId == $(this).data('id')));
                                if (objIndex != -1) {
                                    question4Complete++;
                                }
                            });
                            if (question4Complete == question4All) {
                                $('.question-numbers ul li:nth-child(' + (i + 1) + ')').addClass("complete");
                            }
                        }
                        else {
                            var objIndex = sdqChooseData.findIndex((obj => obj.questionId == $('.sdq-content[data-no=' + choiceNo + ']').data('id')));
                            if (objIndex != -1) {
                                $('.question-numbers ul li:nth-child(' + (i + 1) + ')').addClass("complete");
                            }
                        }
                    });
                }

                // Init first question
                var firstQuestionObj = $('.sdq-content[data-no=1]');
                var firstObjIndex = sdqChooseData.findIndex((obj => obj.questionId == firstQuestionObj.data('id')));
                if (firstObjIndex != -1 && $('.sdq-content[data-no=1] .answer-choice.complete').length == 0) {
                    $('.sdq-content[data-no=1] .answer-choice[data-no=' + sdqChooseData[firstObjIndex].choiceNo + ']').addClass('complete');
                }


                // Click question numbers of footer
                $(".question-numbers ul li").click(function () {
                    var thisNumber = parseInt($(this).text());
                    var currentNumber = parseInt($(".ques-num").text());

                    moveQuestion(thisNumber - currentNumber);
                });


                // Click next icon of footer
                $(".next").click(function () {
                    moveQuestion(1);
                });

                $(".prev").click(function () {
                    moveQuestion(-1);
                });

                // Click true or false button
                $(".answer-choice").click(function () {
                    SaveSDQChooseData($(this));
                    if ($(this).data('no') == 1 && $(this).parent().data('no') == 1) {
                        Swal.fire({
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133471") %>',
                            html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133472") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133473") %>',
                            reverseButtons: true,
                            confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>',
                            cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                            showCancelButton: false
                        }).then(function (confirm) {
                            if (confirm.isConfirmed) {

                            }
                        });

                        $('.save-btn').removeClass('btn-default').addClass('btn-success').prop("disabled", false);

                        var currentNumber = parseInt($(".ques-num").text());
                        if ($('.sdq-content[data-no=' + currentNumber + '] .answer-choice.complete').length > 0) {
                            $('.sdq-content[data-no=' + currentNumber + '] .answer-choice.complete').removeClass('complete');
                        }
                        $('.sdq-content[data-no=' + currentNumber + '] .answer-choice[data-no=' + $(this).data('no') + ']').addClass('complete');

                        if (!$('.question-numbers ul li:nth-child(' + currentNumber + ')').hasClass('complete')) {
                            $('.question-numbers ul li:nth-child(' + currentNumber + ')').addClass("complete");
                        }

                        // Disable Navigator
                        $('.question-numbers').addClass("disabled");
                    }
                    else {
                        if ($(this).parent().data('no') == 1) {
                            // Enable Navigator
                            $('.question-numbers').removeClass("disabled");
                        }

                        moveQuestion(1);

                        // Check all complete sdq
                        var objIndex = sdqChooseData.findIndex((obj => obj.questionId == 26 && obj.choiceNo == 1));
                        if (objIndex != -1 || sdqChooseData.length == ($('.sdq-content').length + $('.sdq-content select').length - 1)) {
                            $('.save-btn').removeClass('btn-default').addClass('btn-success').prop("disabled", false);
                        }
                        else {
                            $('.save-btn').addClass('btn-default').removeClass('btn-success').prop("disabled", true);
                        }
                    }
                });

                function moveQuestion(nextStep) {
                    var currentNumber = parseInt($(".ques-num").text());
                    var questionCount = $('.sdq-content').length;

                    if (currentNumber == questionCount && nextStep > 0) nextStep = 0;

                    var toNumber = currentNumber + nextStep;

                    if (toNumber < 1 || questionCount < toNumber) return;

                    var currentQuestionObj = $('.sdq-content[data-no=' + currentNumber + ']');
                    var toQuestionObj = $('.sdq-content[data-no=' + toNumber + ']');

                    $(".ques-num").text(toNumber);
                    if ($('.sdq-content[data-no=' + currentNumber + ']').hasClass('active')) {
                        $('.sdq-content[data-no=' + currentNumber + ']').removeClass("active");
                    }
                    if (!$('.sdq-content[data-no=' + toNumber + ']').hasClass('active')) {
                        $('.sdq-content[data-no=' + toNumber + ']').addClass("active");
                    }

                    if (currentNumber != 4) {
                        var objIndex = sdqChooseData.findIndex((obj => obj.questionId == currentQuestionObj.data('id')));
                        if (objIndex != -1) {
                            if ($('.sdq-content[data-no=' + currentNumber + '] .answer-choice.complete').length > 0) {
                                $('.sdq-content[data-no=' + currentNumber + '] .answer-choice.complete').removeClass('complete');
                            }
                            $('.sdq-content[data-no=' + currentNumber + '] .answer-choice[data-no=' + sdqChooseData[objIndex].choiceNo + ']').addClass('complete');
                        }
                    }

                    if (toNumber == 4) {
                        $('.sdq-content[data-no=4] select').each(function (eIndex) {
                            var objIndex = sdqChooseData.findIndex((obj => obj.questionId == $(this).data('id')));
                            if (objIndex != -1) {
                                $('.sdq-content[data-no=' + toNumber + '] select[data-id=' + sdqChooseData[objIndex].questionId + '] option[data-no=' + sdqChooseData[objIndex].choiceNo + ']').attr('selected', 'selected');
                            }
                        });
                    }
                    else {
                        objIndex = sdqChooseData.findIndex((obj => obj.questionId == toQuestionObj.data('id')));
                        if (objIndex != -1 && $('.sdq-content[data-no=' + toNumber + '] .answer-choice.complete').length == 0) {
                            $('.sdq-content[data-no=' + toNumber + '] .answer-choice[data-no=' + sdqChooseData[objIndex].choiceNo + ']').addClass('complete');
                        }
                    }


                    // Number Navigator
                    $('.question-numbers ul li:nth-child(' + currentNumber + ')').removeClass("active");

                    if (!$('.question-numbers ul li:nth-child(' + currentNumber + ')').hasClass('complete')) {
                        if (currentNumber == 4) {
                            var question4All = $('.sdq-content[data-no=4] select').length;
                            var question4Complete = 0;
                            $('.sdq-content[data-no=4] select').each(function (eIndex) {
                                var objIndex = sdqChooseData.findIndex((obj => obj.questionId == $(this).data('id')));
                                if (objIndex != -1) {
                                    question4Complete++;
                                }
                            });
                            if (question4Complete == question4All) {
                                $('.question-numbers ul li:nth-child(' + currentNumber + ')').addClass("complete");
                            }
                        }
                        else {
                            var objIndex = sdqChooseData.findIndex((obj => obj.questionId == currentQuestionObj.data('id')));
                            if (objIndex != -1) {
                                $('.question-numbers ul li:nth-child(' + currentNumber + ')').addClass("complete");
                            }
                        }
                    }

                    $('.question-numbers ul li:nth-child(' + toNumber + ')').addClass("active");

                    $('li.active')[0].scrollIntoView();
                }

                function SaveSDQChooseData(answer) {
                    var objIndex = sdqChooseData.findIndex((obj => obj.questionId == answer.parent().data('id')));
                    if (objIndex == -1) {
                        sdqChooseData.push({ questionId: answer.parent().data('id'), questionName: answer.parent().data('name'), choiceNo: answer.data('no'), choiceValue: answer.data('value'), choiceGroup: answer.parent().data('group') });
                    }
                    else {
                        sdqChooseData[objIndex].questionId = answer.parent().data('id');
                        sdqChooseData[objIndex].questionName = answer.parent().data('name');
                        sdqChooseData[objIndex].choiceNo = answer.data('no');
                        sdqChooseData[objIndex].choiceValue = answer.data('value');
                        sdqChooseData[objIndex].choiceGroup = answer.parent().data('group');
                    }
                }

                function SaveSDQChooseData2(answer) {
                    var objIndex = sdqChooseData.findIndex((obj => obj.questionId == answer.data('id')));
                    if (objIndex == -1) {
                        sdqChooseData.push({ questionId: answer.data('id'), questionName: answer.data('name'), choiceNo: answer.find(":selected").data('no'), choiceValue: answer.find(":selected").val(), choiceGroup: answer.parent().data('group') });
                    }
                    else {
                        sdqChooseData[objIndex].questionId = answer.data('id');
                        sdqChooseData[objIndex].questionName = answer.data('name');
                        sdqChooseData[objIndex].choiceNo = answer.find(":selected").data('no');
                        sdqChooseData[objIndex].choiceValue = answer.find(":selected").val();
                        sdqChooseData[objIndex].choiceGroup = answer.parent().data('group');
                    }
                }

                function SaveSDQDataToDatabase() {
                    $.ajax({
                        async: true,
                        type: "POST",
                        url: "SDQForm.aspx/SaveSDQDataSection2",
                        data: "{schoolId: <%=Request.QueryString["schoolid"]%>, studentId: <%=Request.QueryString["sid"]%>, typeId: <%=Request.QueryString["type"]%>, sdqDatas: " + JSON.stringify(sdqChooseData) + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var r = JSON.parse(result.d);
                            if (r.success) {
                                Swal.fire('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101266") %>', r.message, 'success')
                                    .then(function () {
                                        <%--if ('<%=Request.QueryString["client"]%>' == 'teacher') {
                                            location.href = "SDQSection.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=<%=Request.QueryString["type"]%>";
                                        }
                                        else if ('<%=Request.QueryString["client"]%>' == 'student') {
                                            location.href = "SDQSection.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=<%=Request.QueryString["type"]%>";
                                        }--%>
                                        location.href = "SDQSection.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=<%=Request.QueryString["type"]%>&client=<%=Request.QueryString["client"]%>";
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

                $('.sdq-content[data-no=4] select').change(function () {
                    SaveSDQChooseData2($(this));

                    if ($('.sdq-content[data-no=4] select option[value=""]:selected').length == 0) {
                        moveQuestion(1);

                        // Check all complete sdq
                        if (sdqChooseData.length == ($('.sdq-content').length + $('.sdq-content select').length - 1)) {
                            $('.save-btn').removeClass('btn-default').addClass('btn-success').prop("disabled", false);
                        }
                    }
                });

                $(".save-draft-btn").click(function () {
                    if (sdqChooseData.length > 0) {
                        // Check all complete sdq
                        var answer = [];
                        $('.sdq-content').each(function (index) {
                            if ($(this).data('no') == 4) {
                                $('.sdq-content[data-no=4] select').each(function (eIndex) {
                                    var objIndex = sdqChooseData.findIndex((obj => obj.questionId == $(this).data('id')));
                                    if (objIndex == -1) {
                                        answer.push($(this).data('no'));
                                    }
                                });
                            }
                            else {
                                var objIndex = sdqChooseData.findIndex((obj => obj.questionId == $(this).data('id')));
                                if (objIndex == -1) {
                                    answer.push($(this).data('no'));
                                }
                            }
                        });

                        if (answer.length > 0) {
                            var objIndex = sdqChooseData.findIndex((obj => obj.questionId == 26 && obj.choiceNo == 1));
                            if (objIndex != -1) {
                                Swal.fire({
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %>',
                                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133468") %>',
                                    reverseButtons: true,
                                    confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                                    cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                                    showCancelButton: true
                                }).then(function (confirm) {
                                    if (confirm.isConfirmed) {
                                        SaveSDQDataToDatabase();
                                    }
                                });
                            }
                            else {
                                Swal.fire({
                                    title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210025") %>',
                                    html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133465") %> ' + answer.join(', ') + '<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133455") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133466") %>',
                                    reverseButtons: true,
                                    confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                                    cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                                    showCancelButton: true
                                }).then(function (confirm) {
                                    if (confirm.isConfirmed) {
                                        SaveSDQDataToDatabase();
                                    }
                                });
                            }
                        }
                        else {
                            Swal.fire({
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %>',
                                html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133468") %>',
                                reverseButtons: true,
                                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                                showCancelButton: true
                            }).then(function (confirm) {
                                if (confirm.isConfirmed) {
                                    SaveSDQDataToDatabase();
                                }
                            });
                        }
                    }
                    else {
                        Swal.fire('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133469") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133470") %>', 'info');
                    }
                });

                $(".save-btn").click(function () {
                    Swal.fire({
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %>',
                        html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133468") %>',
                        reverseButtons: true,
                        confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                        cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                        showCancelButton: true
                    }).then(function (confirm) {
                        if (confirm.isConfirmed) {
                            SaveSDQDataToDatabase();
                        }
                    });
                });

            </script>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
