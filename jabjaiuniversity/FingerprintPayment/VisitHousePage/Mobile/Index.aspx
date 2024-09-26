<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/gh/alphardex/aqua.css/dist/aqua.min.css">
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

            .material-icons.content {
                vertical-align: middle;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <% 
            int no = 1;
            foreach (var s in studentModels)
            { %>
        <div class="user">
            <div class="w-50 d-flex align-items-center">
                <p class="number"><%= no %>.</p>
                <div class="d-flex align-items-center">
                    <img src="<%= string.IsNullOrEmpty(s.StudentPicture) ? "/Content/VisitHouse/assets/img/user.png" : s.StudentPicture %>" alt="user" style="width: 50px; height: 50px; border-radius: 50%;">
                    <div>
                        <p><%= s.StudentID %></p>
                        <p><%= s.Name %></p>
                        <p><%= s.Lastname %></p>
                    </div>
                </div>
            </div>
            <div class="home-lists w-50">
                <div class="home sdq" data-sid="<%= s.ID %>">
                    <span class="material-icons content" style="<%= (((s.SDQStudentRecorded > 0 && s.SDQStudentRecorded == s.SDQStudentAllQuestion) || (s.SDQStudentRecorded > 25 && s.SDQStudentQuestion26 == 1)) && ((s.SDQTeacherRecorded > 0 && s.SDQTeacherRecorded == s.SDQTeacherAllQuestion) || (s.SDQTeacherRecorded > 25 && s.SDQTeacherQuestion26 == 1)) && ((s.SDQParentsRecorded > 0 && s.SDQParentsRecorded == s.SDQParentsAllQuestion) || (s.SDQParentsRecorded > 25 && s.SDQParentsQuestion26 == 1)) ? "color: var(--success);": "") %>">house</span>
                    <small <%= (((s.SDQStudentRecorded > 0 && s.SDQStudentRecorded == s.SDQStudentAllQuestion) || (s.SDQStudentRecorded > 25 && s.SDQStudentQuestion26 == 1)) && ((s.SDQTeacherRecorded > 0 && s.SDQTeacherRecorded == s.SDQTeacherAllQuestion) || (s.SDQTeacherRecorded > 25 && s.SDQTeacherQuestion26 == 1)) && ((s.SDQParentsRecorded > 0 && s.SDQParentsRecorded == s.SDQParentsAllQuestion) || (s.SDQParentsRecorded > 25 && s.SDQParentsQuestion26 == 1)) ? "style=\"color: var(--success);\"" : "") %>>SDQ</small>
                    <div class="check">
                        <input type="checkbox" class="form-check-input" <%= (s.SDQStudentRecorded > 0 && s.SDQStudentRecorded == s.SDQStudentAllQuestion) || (s.SDQStudentRecorded > 25 && s.SDQStudentQuestion26 == 1) ? "checked" : "" %> />
                        <input type="checkbox" class="form-check-input" <%= (s.SDQTeacherRecorded > 0 && s.SDQTeacherRecorded == s.SDQTeacherAllQuestion) || (s.SDQTeacherRecorded > 25 && s.SDQTeacherQuestion26 == 1) ? "checked" : "" %> />
                        <input type="checkbox" class="form-check-input" <%= (s.SDQParentsRecorded > 0 && s.SDQParentsRecorded == s.SDQParentsAllQuestion) || (s.SDQParentsRecorded > 25 && s.SDQParentsQuestion26 == 1) ? "checked" : "" %> />
                    </div>
                </div>
                <div class="home eq" data-sid="<%= s.ID %>">
                    <div>
                        <span class="material-icons content" style="<%= ((s.EQStudentRecorded > 0 && s.EQStudentRecorded == s.EQStudentAllQuestion) ? "color: var(--success);": "") %>">house</span>
                        <small <%= ((s.EQStudentRecorded > 0 && s.EQStudentRecorded == s.EQStudentAllQuestion) ? "style=\"color: var(--success);\"" : "") %>>EQ</small>
                    </div>
                </div>
                <div class="home home-visit" data-sid="<%= s.ID %>">
                    <div>
                        <span class="material-icons content" style="<%= s.HomeVisitRecorded ? "color: var(--success);": "" %>">house</span>
                        <small <%= s.HomeVisitRecorded ? "style=\"color: var(--success);\"" : "" %>>เยี่ยมบ้าน</small>
                    </div>
                </div>
                <div class="home screening" data-sid="<%= s.ID %>">
                    <div>
                        <span class="material-icons content" style="<%= s.ScreeningStudentRecorded ? "color: var(--success);": "" %>">house</span>
                        <small <%= s.ScreeningStudentRecorded ? "style=\"color: var(--success);\"" : "" %>>คัดกรอง</small>
                    </div>
                </div>
            </div>
        </div>
        <% no++;
            }
        %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script>
        // sdq - strengths difficulties questionnaire

        $(".sdq").click(function () {
            location.href = "SDQIndex.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=" + $(this).data('sid');
        });

        $(".eq").click(function () {
            location.href = "EQForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&roomid=<%=Request.QueryString["roomid"]%>&sid=" + $(this).data('sid') + "&type=1&client=teacher";
        });

        $(".home-visit").click(function () {
            location.href = "VisitHouseForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&roomid=<%=Request.QueryString["roomid"]%>&sid=" + $(this).data('sid');
        });

        $(".screening").click(function () {
            location.href = "PersonalScreeningForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&roomid=<%=Request.QueryString["roomid"]%>&sid=" + $(this).data('sid') + "&client=teacher";
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
