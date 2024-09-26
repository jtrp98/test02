<%@ Page Title="" Language="C#" MasterPageFile="~/Material3.Master" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Mobile.Index2" %>

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

        .answer-choice.eq-assistant:hover span {
            color: var(--green);
        }

        .material-icons.content {
            font-size: 48px;
        }

        .material-icons.list-number {
            margin-left: -38px;
            margin-top: 5px;
            font-size: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content contents">
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
        <div class="SDQ-content">
            <a class="sdq-student" href="SDQSection.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=1&client=student">
                <div class="answer-choice eq-assistant">
                    <span class="material-icons content">content_paste</span>
                    <span class="material-icons list-number">format_list_numbered</span>
                    <div class="ml-3 text-left">
                        <p>แบบประเมิน SDQ - นักเรียน</p>
                        <p class="student-sdq-record-date" style="<%=(string.IsNullOrEmpty(StudentSDQRecordDate)? "display: none;": "")%>"><%=StudentSDQRecordDate%></p>
                    </div>
                </div>
            </a>
            <a class="sdq-parents" href="SDQSection.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=3&client=student">
                <div class="answer-choice eq-assistant">
                    <span class="material-icons content">content_paste</span>
                    <span class="material-icons list-number">format_list_numbered</span>
                    <div class="ml-3 text-left">
                        <p>แบบประเมิน SDQ - ผู้ปกครอง</p>
                        <p class="parents-sdq-record-date" style="<%=(string.IsNullOrEmpty(ParentsSDQRecordDate)? "display: none;": "")%>"><%=ParentsSDQRecordDate%></p>
                    </div>
                </div>
            </a>
            <a class="homevisit" href="VisitHouseForm2.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>">
                <div class="answer-choice eq-assistant">
                    <span class="material-icons content">content_paste</span>
                    <span class="material-icons list-number">format_list_numbered</span>
                    <div class="ml-3 text-left">
                        <p>แบบประเมิน - เยี่ยมบ้าน</p>
                        <p class="homevisit-record-date" style="<%=(string.IsNullOrEmpty(HomeVisitRecordDate)? "display: none;": "")%>"><%=HomeVisitRecordDate%></p>
                    </div>
                </div>
            </a>
            <a class="eq" href="EQForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&type=1&client=student">
                <div class="answer-choice eq-assistant">
                    <span class="material-icons content">content_paste</span>
                    <span class="material-icons list-number">format_list_numbered</span>
                    <div class="ml-3 text-left">
                        <p>แบบประเมิน - EQ</p>
                        <p class="student-eq-record-date" style="<%=(string.IsNullOrEmpty(StudentEQRecordDate)? "display: none;": "")%>"><%=StudentEQRecordDate%></p>
                    </div>
                </div>
            </a>
            <%--<a class="screening" href="PersonalScreeningForm.aspx?schoolid=<%=Request.QueryString["schoolid"]%>&sid=<%=Request.QueryString["sid"]%>&client=student">
                <div class="answer-choice eq-assistant">
                    <span class="material-icons content">content_paste</span>
                    <span class="material-icons list-number">format_list_numbered</span>
                    <div class="ml-3 text-left">
                        <p>แบบประเมิน - คัดกรอง</p>
                        <p class="student-screening-record-date" style="<%=(string.IsNullOrEmpty(StudentScreeningRecordDate)? "display: none;": "")%>"><%=StudentScreeningRecordDate%></p>
                    </div>
                </div>
            </a>--%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script>
        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
