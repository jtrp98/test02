<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="EQForm.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.EQForm" %>

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
                <button type="button" class="btn <%=StyleSaveButton%> btn-round px-3 save-btn" style="width: 110px; height: 39px;" <%=DisabledSaveButton%>><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
            </div>
            <% }%>
        </div>
        <div class="EQ-form">
            <span class="ques-num">1</span> /<span class="ques-all">52</span>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00968") %>
        </div>
        <div class="eq-content" style="padding-bottom: 90px;">
            <p class="mt-3 ques-title">?</p>
            <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %></div>
            <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132976") %></div>
            <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %></div>
            <div class="answer-choice <%=(VisibleSaveButton ? "" : "disabled")%>">จริงมาก</div>
        </div>
    </div>
    <footer class="question-numbers">
        <%--<img src="/Content/VisitHouse/assets/img/prev.png" class="prev">--%>
        <span class="material-icons prev">navigate_before</span>
        <ul></ul>
        <%--<img src="/Content/VisitHouse/assets/img/next.png" class="next">--%>
        <span class="material-icons next">navigate_next</span>
    </footer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script>

        // EQ question data
        var choiceLabel = ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132976") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %>', 'จริงมาก']; // choices.no: 1 = <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %>, 2 = <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132976") %>, 3 = <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %>, 4 = จริงมาก
                <% if (QuestionsForStudent)
        {%>
        var questions = [
            { no: 1, id: 1, name: 'Question1', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %>', choices: [{ no: 1, value: 1, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 1, largeGroup: 1 }] },
            { no: 2, id: 2, name: 'Question2', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %>', choices: [{ no: 1, value: 4, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 1, largeGroup: 1 }] },
            { no: 3, id: 3, name: 'Question3', title: 'เมื่อถูกขัดใจฉันมักรู้สึกหงุดหงิดจนควบคุมอารมณ์ไม่ได้', choices: [{ no: 1, value: 4, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 1, largeGroup: 1 }] },
            { no: 4, id: 4, name: 'Question4', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %>', choices: [{ no: 1, value: 1, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 1, largeGroup: 1 }] },
            { no: 5, id: 5, name: 'Question5', title: 'ฉันมักมีปฏิกิริยาโต้ตอบปัญหาเพียงเล็กน้อย', choices: [{ no: 1, value: 4, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 1, largeGroup: 1 }] },
            { no: 6, id: 6, name: 'Question6', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %>', choices: [{ no: 1, value: 1, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 1, largeGroup: 1 }] },

            { no: 7, id: 7, name: 'Question7', title: 'ฉันสังเกตได้ เมื่อคนใกล้ชิดมีอารมณ์เปลี่ยนแปลง', choices: [{ no: 1, value: 1, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 2, largeGroup: 1 }] },
            { no: 8, id: 8, name: 'Question8', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %>', choices: [{ no: 1, value: 4, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 2, largeGroup: 1 }] },
            { no: 9, id: 9, name: 'Question9', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %>', choices: [{ no: 1, value: 4, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 2, largeGroup: 1 }] },
            { no: 10, id: 10, name: 'Question10', title: 'ฉันยอมรับได้ว่าผู้อื่นก็มีเหตุผลที่จะไม่พอใจในการกระทำของฉัน', choices: [{ no: 1, value: 1, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 2, largeGroup: 1 }] },
            { no: 11, id: 11, name: 'Question11', title: 'ฉันรู้สึกว่าผู้อื่นเรียกร้องความสนใจมากเกินไป', choices: [{ no: 1, value: 4, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 2, largeGroup: 1 }] },
            { no: 12, id: 12, name: 'Question12', title: 'แม้จะมีภาระที่ต้องทำก็ยินดีรับฟังความทุกข์ของผู้อื่นที่ต้องการความช่วยเหลือ', choices: [{ no: 1, value: 1, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 2, largeGroup: 1 }] },

            { no: 13, id: 13, name: 'Question13', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %>', choices: [{ no: 1, value: 4, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 3, largeGroup: 1 }] },
            { no: 14, id: 14, name: 'Question14', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %>', choices: [{ no: 1, value: 1, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 3, largeGroup: 1 }] },
            { no: 15, id: 15, name: 'Question15', title: 'เมื่อทำผิดฉันสามารถกล่าวคำ "ขอโทษ" ผู้อื่นได้', choices: [{ no: 1, value: 1, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 3, largeGroup: 1 }] },
            { no: 16, id: 16, name: 'Question16', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %>', choices: [{ no: 1, value: 4, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 3, largeGroup: 1 }] },
            { no: 17, id: 17, name: 'Question17', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %>', choices: [{ no: 1, value: 1, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 3, largeGroup: 1 }] },
            { no: 18, id: 18, name: 'Question18', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %>', choices: [{ no: 1, value: 4, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 3, largeGroup: 1 }] },

            { no: 19, id: 19, name: 'Question19', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %>', choices: [{ no: 1, value: 4, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 4, largeGroup: 2 }] },
            { no: 20, id: 20, name: 'Question20', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %>', choices: [{ no: 1, value: 1, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 4, largeGroup: 2 }] },
            { no: 21, id: 21, name: 'Question21', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %>', choices: [{ no: 1, value: 4, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 4, largeGroup: 2 }] },
            { no: 22, id: 22, name: 'Question22', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %>', choices: [{ no: 1, value: 1, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 4, largeGroup: 2 }] },
            { no: 23, id: 23, name: 'Question23', title: 'เมื่อต้องเผชิญกับอุปสรรคและความผิดหวัง ฉันจะไม่ยอมแพ้', choices: [{ no: 1, value: 1, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 4, largeGroup: 2 }] },
            { no: 24, id: 24, name: 'Question24', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %>', choices: [{ no: 1, value: 4, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 4, largeGroup: 2 }] },

            { no: 25, id: 25, name: 'Question25', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %>', choices: [{ no: 1, value: 1, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 5, largeGroup: 2 }] },
            { no: 26, id: 26, name: 'Question26', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },
            { no: 27, id: 27, name: 'Question27', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },
            { no: 28, id: 28, name: 'Question28', title: 'เมื่อต้องทำอะไรหลายอย่างในเวลาเดียวกัน ฉันตัดสินใจได้ว่าจะทำอะไรก่อนหลัง', choices: [{ no: 1, value: 1, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 5, largeGroup: 2 }] },
            { no: 29, id: 29, name: 'Question29', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },
            { no: 30, id: 30, name: 'Question30', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },

            { no: 31, id: 31, name: 'Question31', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },
            { no: 32, id: 32, name: 'Question32', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },
            { no: 33, id: 33, name: 'Question33', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %>', choices: [{ no: 1, value: 4, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 6, largeGroup: 2 }] },
            { no: 34, id: 34, name: 'Question34', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },
            { no: 35, id: 35, name: 'Question35', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %>', choices: [{ no: 1, value: 4, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 6, largeGroup: 2 }] },
            { no: 36, id: 36, name: 'Question36', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },

            { no: 37, id: 37, name: 'Question37', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %>', choices: [{ no: 1, value: 4, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 7, largeGroup: 3 }] },
            { no: 38, id: 38, name: 'Question38', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %>', choices: [{ no: 1, value: 1, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 7, largeGroup: 3 }] },
            { no: 39, id: 39, name: 'Question39', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %>', choices: [{ no: 1, value: 1, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 7, largeGroup: 3 }] },
            { no: 40, id: 40, name: 'Question40', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %>', choices: [{ no: 1, value: 4, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 7, largeGroup: 3 }] },

            { no: 41, id: 41, name: 'Question41', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %>', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 42, id: 42, name: 'Question42', title: 'ทุกปัญหามีทางออกเสนอ', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 43, id: 43, name: 'Question43', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %>', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 44, id: 44, name: 'Question44', title: 'ฉันสนุกสนานทุกครั้งกับกิจกรรมในวันสุดสัปดาห์และวันพักผ่อน', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 45, id: 45, name: 'Question45', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %>', choices: [{ no: 1, value: 4, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 8, largeGroup: 3 }] },
            { no: 46, id: 46, name: 'Question46', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %>', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },

            { no: 47, id: 47, name: 'Question47', title: 'ฉันไม่รู้ว่าจะหาอะไรทำเมื่อรู้สึกเบื่อหน่าย', choices: [{ no: 1, value: 4, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 9, largeGroup: 3 }] },
            { no: 48, id: 48, name: 'Question48', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %>', choices: [{ no: 1, value: 1, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 9, largeGroup: 3 }] },
            { no: 49, id: 49, name: 'Question49', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %>', choices: [{ no: 1, value: 1, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 9, largeGroup: 3 }] },
            { no: 50, id: 50, name: 'Question50', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %>', choices: [{ no: 1, value: 1, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 9, largeGroup: 3 }] },
            { no: 51, id: 51, name: 'Question51', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %>', choices: [{ no: 1, value: 4, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 9, largeGroup: 3 }] },
            { no: 52, id: 52, name: 'Question52', title: 'ฉันมักทุกข์ร้อนกับเรื่องเล็กๆ น้อยๆ ที่เกิดขึ้นเสมอ', choices: [{ no: 1, value: 4, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 9, largeGroup: 3 }] },
        ];
        <% }
        else
        {%>
        var questions = [
            { no: 1, id: 1, name: 'Question1', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %>', choices: [{ no: 1, value: 1, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 1, largeGroup: 1 }] },
            { no: 2, id: 2, name: 'Question2', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %>', choices: [{ no: 1, value: 4, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 1, largeGroup: 1 }] },
            { no: 3, id: 3, name: 'Question3', title: 'เมื่อถูกขัดใจฉันมักรู้สึกหงุดหงิดจนควบคุมอารมณ์ไม่ได้', choices: [{ no: 1, value: 4, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 1, largeGroup: 1 }] },
            { no: 4, id: 4, name: 'Question4', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %>', choices: [{ no: 1, value: 1, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 1, largeGroup: 1 }] },
            { no: 5, id: 5, name: 'Question5', title: 'ฉันมักมีปฏิกิริยาโต้ตอบปัญหาเพียงเล็กน้อย', choices: [{ no: 1, value: 4, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 1, largeGroup: 1 }] },
            { no: 6, id: 6, name: 'Question6', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %>', choices: [{ no: 1, value: 1, smallGroup: 1, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 1, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 1, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 1, largeGroup: 1 }] },

            { no: 7, id: 7, name: 'Question7', title: 'ฉันสังเกตได้ เมื่อคนใกล้ชิดมีอารมณ์เปลี่ยนแปลง', choices: [{ no: 1, value: 1, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 2, largeGroup: 1 }] },
            { no: 8, id: 8, name: 'Question8', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %>', choices: [{ no: 1, value: 4, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 2, largeGroup: 1 }] },
            { no: 9, id: 9, name: 'Question9', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %>', choices: [{ no: 1, value: 4, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 2, largeGroup: 1 }] },
            { no: 10, id: 10, name: 'Question10', title: 'ฉันยอมรับได้ว่าผู้อื่นก็มีเหตุผลที่จะไม่พอใจในการกระทำของฉัน', choices: [{ no: 1, value: 1, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 2, largeGroup: 1 }] },
            { no: 11, id: 11, name: 'Question11', title: 'ฉันรู้สึกว่าผู้อื่นเรียกร้องความสนใจมากเกินไป', choices: [{ no: 1, value: 4, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 2, largeGroup: 1 }] },
            { no: 12, id: 12, name: 'Question12', title: 'แม้จะมีภาระที่ต้องทำก็ยินดีรับฟังความทุกข์ของผู้อื่นที่ต้องการความช่วยเหลือ', choices: [{ no: 1, value: 1, smallGroup: 2, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 2, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 2, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 2, largeGroup: 1 }] },

            { no: 13, id: 13, name: 'Question13', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %>', choices: [{ no: 1, value: 4, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 3, largeGroup: 1 }] },
            { no: 14, id: 14, name: 'Question14', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %>', choices: [{ no: 1, value: 1, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 3, largeGroup: 1 }] },
            { no: 15, id: 15, name: 'Question15', title: 'เมื่อทำผิดฉันสามารถกล่าวคำ "ขอโทษ" ผู้อื่นได้', choices: [{ no: 1, value: 1, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 3, largeGroup: 1 }] },
            { no: 16, id: 16, name: 'Question16', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %>', choices: [{ no: 1, value: 4, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 3, largeGroup: 1 }] },
            { no: 17, id: 17, name: 'Question17', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %>', choices: [{ no: 1, value: 1, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 4, smallGroup: 3, largeGroup: 1 }] },
            { no: 18, id: 18, name: 'Question18', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %>', choices: [{ no: 1, value: 4, smallGroup: 3, largeGroup: 1 }, { no: 2, value: 3, smallGroup: 3, largeGroup: 1 }, { no: 3, value: 2, smallGroup: 3, largeGroup: 1 }, { no: 4, value: 1, smallGroup: 3, largeGroup: 1 }] },

            { no: 19, id: 19, name: 'Question19', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %>', choices: [{ no: 1, value: 4, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 4, largeGroup: 2 }] },
            { no: 20, id: 20, name: 'Question20', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %>', choices: [{ no: 1, value: 1, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 4, largeGroup: 2 }] },
            { no: 21, id: 21, name: 'Question21', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %>', choices: [{ no: 1, value: 4, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 4, largeGroup: 2 }] },
            { no: 22, id: 22, name: 'Question22', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %>', choices: [{ no: 1, value: 1, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 4, largeGroup: 2 }] },
            { no: 23, id: 23, name: 'Question23', title: 'เมื่อต้องเผชิญกับอุปสรรคและความผิดหวัง ฉันจะไม่ยอมแพ้', choices: [{ no: 1, value: 1, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 4, largeGroup: 2 }] },
            { no: 24, id: 24, name: 'Question24', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %>', choices: [{ no: 1, value: 4, smallGroup: 4, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 4, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 4, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 4, largeGroup: 2 }] },

            { no: 25, id: 25, name: 'Question25', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %>', choices: [{ no: 1, value: 1, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 5, largeGroup: 2 }] },
            { no: 26, id: 26, name: 'Question26', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },
            { no: 27, id: 27, name: 'Question27', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },
            { no: 28, id: 28, name: 'Question28', title: 'เมื่อต้องทำอะไรหลายอย่างในเวลาเดียวกัน ฉันตัดสินใจได้ว่าจะทำอะไรก่อนหลัง', choices: [{ no: 1, value: 1, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 5, largeGroup: 2 }] },
            { no: 29, id: 29, name: 'Question29', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },
            { no: 30, id: 30, name: 'Question30', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %>', choices: [{ no: 1, value: 4, smallGroup: 5, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 5, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 5, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 5, largeGroup: 2 }] },

            { no: 31, id: 31, name: 'Question31', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },
            { no: 32, id: 32, name: 'Question32', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },
            { no: 33, id: 33, name: 'Question33', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %>', choices: [{ no: 1, value: 4, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 6, largeGroup: 2 }] },
            { no: 34, id: 34, name: 'Question34', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },
            { no: 35, id: 35, name: 'Question35', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %>', choices: [{ no: 1, value: 4, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 1, smallGroup: 6, largeGroup: 2 }] },
            { no: 36, id: 36, name: 'Question36', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %>', choices: [{ no: 1, value: 1, smallGroup: 6, largeGroup: 2 }, { no: 2, value: 2, smallGroup: 6, largeGroup: 2 }, { no: 3, value: 3, smallGroup: 6, largeGroup: 2 }, { no: 4, value: 4, smallGroup: 6, largeGroup: 2 }] },

            { no: 37, id: 37, name: 'Question37', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %>', choices: [{ no: 1, value: 4, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 7, largeGroup: 3 }] },
            { no: 38, id: 38, name: 'Question38', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %>', choices: [{ no: 1, value: 1, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 7, largeGroup: 3 }] },
            { no: 39, id: 39, name: 'Question39', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %>', choices: [{ no: 1, value: 1, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 7, largeGroup: 3 }] },
            { no: 40, id: 40, name: 'Question40', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %>', choices: [{ no: 1, value: 4, smallGroup: 7, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 7, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 7, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 7, largeGroup: 3 }] },

            { no: 41, id: 41, name: 'Question41', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %>', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 42, id: 42, name: 'Question42', title: 'ทุกปัญหามีทางออกเสนอ', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 43, id: 43, name: 'Question43', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %>', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 44, id: 44, name: 'Question44', title: 'ฉันสนุกสนานทุกครั้งกับกิจกรรมในวันสุดสัปดาห์และวันพักผ่อน', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },
            { no: 45, id: 45, name: 'Question45', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %>', choices: [{ no: 1, value: 4, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 8, largeGroup: 3 }] },
            { no: 46, id: 46, name: 'Question46', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %>', choices: [{ no: 1, value: 1, smallGroup: 8, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 8, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 8, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 8, largeGroup: 3 }] },

            { no: 47, id: 47, name: 'Question47', title: 'ฉันไม่รู้ว่าจะหาอะไรทำเมื่อรู้สึกเบื่อหน่าย', choices: [{ no: 1, value: 4, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 9, largeGroup: 3 }] },
            { no: 48, id: 48, name: 'Question48', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %>', choices: [{ no: 1, value: 1, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 9, largeGroup: 3 }] },
            { no: 49, id: 49, name: 'Question49', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %>', choices: [{ no: 1, value: 1, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 9, largeGroup: 3 }] },
            { no: 50, id: 50, name: 'Question50', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %>', choices: [{ no: 1, value: 1, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 4, smallGroup: 9, largeGroup: 3 }] },
            { no: 51, id: 51, name: 'Question51', title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %>', choices: [{ no: 1, value: 4, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 9, largeGroup: 3 }] },
            { no: 52, id: 52, name: 'Question52', title: 'ฉันมักทุกข์ร้อนกับเรื่องเล็กๆ น้อยๆ ที่เกิดขึ้นเสมอ', choices: [{ no: 1, value: 4, smallGroup: 9, largeGroup: 3 }, { no: 2, value: 3, smallGroup: 9, largeGroup: 3 }, { no: 3, value: 2, smallGroup: 9, largeGroup: 3 }, { no: 4, value: 1, smallGroup: 9, largeGroup: 3 }] },
        ];
                <% } %>

        var eqChooseData = [<%=EQChooseData%>];

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
                .data('choice-smallGroup', obj.smallGroup)
                .data('choice-largeGroup', obj.largeGroup)
                .text(choiceLabel[i]);

            // Init set have already save data
            if (eqChooseData.length > 0) {
                var objIndex = eqChooseData.findIndex((o => o.questionId == questionObj.id && o.choiceNo == obj.no));
                if (objIndex != -1 && !$('.answer-choice:eq(' + i + ')').hasClass('complete')) {
                    $('.answer-choice:eq(' + i + ')').addClass('complete')
                }
            }
        });
        $('.question-numbers ul li:nth-child(1)').addClass("active");

        // Init set have already save data
        if (eqChooseData.length > 0) {
            $('.question-numbers ul li').each(function (i) {
                var objIndex = eqChooseData.findIndex((obj => obj.questionId == questions[i].id));
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
            SaveEQChooseData($(this));
            moveQuestion(1);

            // Check all complete eq
            if (eqChooseData.length == questions.length) {
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
                    .data('choice-smallGroup', obj.smallGroup)
                    .data('choice-largeGroup', obj.largeGroup)
                    .text(choiceLabel[i])
                    .removeClass('complete');

                var objIndex = eqChooseData.findIndex((o => o.questionId == questionObj.id && o.choiceNo == obj.no));
                if (objIndex != -1 && !$('.answer-choice:eq(' + i + ')').hasClass('complete')) {
                    $('.answer-choice:eq(' + i + ')').addClass('complete')
                }
            });

            $('.question-numbers ul li:nth-child(' + currentNumber + ')').removeClass("active");
            if (!$('.question-numbers ul li:nth-child(' + currentNumber + ')').hasClass('complete')) {
                var objIndex = eqChooseData.findIndex((obj => obj.questionId == questions[currentNumber - 1].id));
                if (objIndex != -1) {
                    $('.question-numbers ul li:nth-child(' + currentNumber + ')').addClass("complete");
                }
            }

            $('.question-numbers ul li:nth-child(' + toNumber + ')').addClass("active");

            $('li.active')[0].scrollIntoView();
        }

        function SaveEQChooseData(answer) {
            var objIndex = eqChooseData.findIndex((obj => obj.questionId == answer.data('question-id')));
            if (objIndex == -1) {
                eqChooseData.push({ questionId: answer.data('question-id'), questionName: answer.data('question-name'), choiceNo: answer.data('choice-no'), choiceValue: answer.data('choice-value'), choiceSmallGroup: answer.data('choice-smallGroup'), choiceLargeGroup: answer.data('choice-largeGroup') });
            }
            else {
                eqChooseData[objIndex].questionId = answer.data('question-id');
                eqChooseData[objIndex].questionName = answer.data('question-name');
                eqChooseData[objIndex].choiceNo = answer.data('choice-no');
                eqChooseData[objIndex].choiceValue = answer.data('choice-value');
                eqChooseData[objIndex].choiceSmallGroup = answer.data('choice-smallGroup');
                eqChooseData[objIndex].choiceLargeGroup = answer.data('choice-largeGroup');
            }
        }

        function SaveEQDataToDatabase() {
            $.ajax({
                async: true,
                type: "POST",
                url: "EQForm.aspx/SaveEQData",
                data: "{schoolId: <%=Request.QueryString["schoolid"]%>, studentId: <%=Request.QueryString["sid"]%>, typeId: <%=Request.QueryString["type"]%>, eqDatas: " + JSON.stringify(eqChooseData) + "}",
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

        $(".save-draft-btn").click(function () {
            if (eqChooseData.length > 0) {
                // Check all complete eq
                var answer = [];
                $.each(questions, function (i, obj) {
                    var objIndex = eqChooseData.findIndex((o => o.questionId == obj.id));
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
                            SaveEQDataToDatabase();
                        }
                    });
                }
                else {
                    Swal.fire({
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101302") %>',
                        html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/>ประเมิน EQ',
                        reverseButtons: true,
                        confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                        cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                        showCancelButton: true
                    }).then(function (confirm) {
                        if (confirm.isConfirmed) {
                            SaveEQDataToDatabase();
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
                html: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133467") %><br/>ประเมิน EQ',
                reverseButtons: true,
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                showCancelButton: true
            }).then(function (confirm) {
                if (confirm.isConfirmed) {
                    SaveEQDataToDatabase();
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
