<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="FingerprintPayment.AdminMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <style>
        .card .card-header-purple .card-icon {
            box-shadow: 0 4px 20px 0px rgb(0 0 0 / 14%), 0 7px 10px -5px rgb(166 118 183 / 40%);
        }

        .card .card-header-purple .card-icon {
            background: linear-gradient(60deg, var(--purple), var(--purple));
        }

        .card-student-all-amount .card-header .card-amount:not([class*="text-"]), .card-employee-all-amount .card-header .card-amount:not([class*="text-"]), .card-credit-bureau .card-header .card-credit:not([class*="text-"]) {
            color: darkgray;
        }

        .card-student-all-amount .card-header.card-header-icon, .card-employee-all-amount .card-header.card-header-icon, .card-credit-bureau .card-header.card-header-icon {
            text-align: right;
        }

        .card-student-all-amount .card-header + .card-body + .card-footer, .card-employee-all-amount .card-header + .card-body + .card-footer, .card-credit-bureau .card-header + .card-body + .card-footer {
            border-top: 1px solid #eee;
            margin-top: 20px;
        }

        .card .card-body + .card-footer .stats .material-icons, .card .card-footer .stats .material-icons {
            top: 0px;
        }

        .system-info {
            position: relative;
            height: 738px;
            /*overflow-y: auto;*/
        }

        .system-overview h4, .all-student-statistics h4, .statistical-scans-in-out h4, table.behavior-score th, table.behavior-score td {
            font-size: .875rem !important;
        }

        .system-info-item .detail {
            font-size: .875rem !important;
        }

        .system-info .logo {
            width: 48px;
            height: 48px;
        }

        .fc-toolbar .fc-state-active, .fc-toolbar .ui-state-active {
            z-index: 1;
        }

        .refresh-chart {
            width: 41px;
            height: 41px;
        }

            .refresh-chart .material-icons {
                font-size: 30px;
            }

        .dot-green {
            height: 25px;
            width: 25px;
            background-color: var(--green);
            border-radius: 50%;
            display: inline-block;
        }

        .dot-cyan {
            height: 25px;
            width: 25px;
            background-color: var(--cyan);
            border-radius: 50%;
            display: inline-block;
        }

        .dot-orange {
            height: 25px;
            width: 25px;
            background-color: var(--orange);
            border-radius: 50%;
            display: inline-block;
        }

        .dot-pink {
            height: 25px;
            width: 25px;
            background-color: var(--pink);
            border-radius: 50%;
            display: inline-block;
        }

        .dot-purple {
            height: 25px;
            width: 25px;
            background-color: var(--purple);
            border-radius: 50%;
            display: inline-block;
        }

        .dot-red {
            height: 25px;
            width: 25px;
            background-color: var(--red);
            border-radius: 50%;
            display: inline-block;
        }

        .dot-gray {
            height: 25px;
            width: 25px;
            background-color: var(--gray);
            border-radius: 50%;
            display: inline-block;
        }

        .ct-chart-pie .ct-label {
            font-size: 1.5em;
            fill: #555 !important;
        }

        .ct-chart-line .ct-label.ct-horizontal {
            transform-origin: 100% 0;
            transform: translate(0%) rotate(25deg);
            margin-top: 13px;
            font-size: 0.9em;
        }

        .ct-chart .ct-series-a .ct-slice-pie, .ct-chart .ct-series-a .ct-slice-donut-solid, .ct-chart .ct-series-a .ct-area {
            fill: var(--green);
        }

        .ct-chart .ct-series-a .ct-point, .ct-chart .ct-series-a .ct-line, .ct-chart .ct-series-a .ct-bar, .ct-chart .ct-series-a .ct-slice-donut, .ct-chart .ct-series-a .ct-slice-pie, .ct-chart .ct-series-a .ct-slice-donut-solid, .ct-chart .ct-series-a .ct-area {
            stroke: var(--green);
        }

        .ct-chart .ct-series-b .ct-slice-pie, .ct-chart .ct-series-b .ct-slice-donut-solid, .ct-chart .ct-series-b .ct-area {
            fill: var(--cyan);
        }

        .ct-chart .ct-series-b .ct-point, .ct-chart .ct-series-b .ct-line, .ct-chart .ct-series-b .ct-bar, .ct-chart .ct-series-b .ct-slice-donut, .ct-chart .ct-series-b .ct-slice-pie, .ct-chart .ct-series-b .ct-slice-donut-solid, .ct-chart .ct-series-b .ct-area {
            stroke: var(--cyan);
        }

        .ct-chart .ct-series-c .ct-slice-pie, .ct-chart .ct-series-c .ct-slice-donut-solid, .ct-chart .ct-series-c .ct-area {
            fill: var(--orange);
        }

        .ct-chart .ct-series-c .ct-point, .ct-chart .ct-series-c .ct-line, .ct-chart .ct-series-c .ct-bar, .ct-chart .ct-series-c .ct-slice-donut, .ct-chart .ct-series-c .ct-slice-pie, .ct-chart .ct-series-c .ct-slice-donut-solid, .ct-chart .ct-series-c .ct-area {
            stroke: var(--orange);
        }

        .ct-chart .ct-series-d .ct-slice-pie, .ct-chart .ct-series-d .ct-slice-donut-solid, .ct-chart .ct-series-d .ct-area {
            fill: var(--pink);
        }

        .ct-chart .ct-series-d .ct-point, .ct-chart .ct-series-d .ct-line, .ct-chart .ct-series-d .ct-bar, .ct-chart .ct-series-d .ct-slice-donut, .ct-chart .ct-series-d .ct-slice-pie, .ct-chart .ct-series-d .ct-slice-donut-solid, .ct-chart .ct-series-d .ct-area {
            stroke: var(--pink);
        }

        .ct-chart .ct-series-e .ct-slice-pie, .ct-chart .ct-series-e .ct-slice-donut-solid, .ct-chart .ct-series-e .ct-area {
            fill: var(--purple);
        }

        .ct-chart .ct-series-e .ct-point, .ct-chart .ct-series-e .ct-line, .ct-chart .ct-series-e .ct-bar, .ct-chart .ct-series-e .ct-slice-donut, .ct-chart .ct-series-e .ct-slice-pie, .ct-chart .ct-series-e .ct-slice-donut-solid, .ct-chart .ct-series-e .ct-area {
            stroke: var(--purple);
        }

        .ct-chart .ct-series-f .ct-slice-pie, .ct-chart .ct-series-f .ct-slice-donut-solid, .ct-chart .ct-series-f .ct-area {
            fill: var(--red);
        }

        .ct-chart .ct-series-f .ct-point, .ct-chart .ct-series-f .ct-line, .ct-chart .ct-series-f .ct-bar, .ct-chart .ct-series-f .ct-slice-donut, .ct-chart .ct-series-f .ct-slice-pie, .ct-chart .ct-series-f .ct-slice-donut-solid, .ct-chart .ct-series-f .ct-area {
            stroke: var(--red);
        }

        .ct-chart .ct-series-g .ct-slice-pie, .ct-chart .ct-series-g .ct-slice-donut-solid, .ct-chart .ct-series-g .ct-area {
            fill: var(--gray);
        }

        .ct-chart .ct-series-g .ct-point, .ct-chart .ct-series-g .ct-line, .ct-chart .ct-series-g .ct-bar, .ct-chart .ct-series-g .ct-slice-donut, .ct-chart .ct-series-g .ct-slice-pie, .ct-chart .ct-series-g .ct-slice-donut-solid, .ct-chart .ct-series-g .ct-area {
            stroke: var(--gray);
        }

        #chartEmployee, #chartStudent {
            height: 370px;
        }

        .blur {
            filter: blur(3px);
            -webkit-filter: blur(3px);
        }

        @media (max-width: 500px) {
            .fc-scroller.fc-day-grid-container {
                overflow: hidden !important;
                height: 100% !important;
            }
        }

        @media only screen and (max-width: 576px) {
            .system-info .logo {
                width: 36px;
                height: 36px;
            }

            .m-sm-1 {
                margin: 0.25rem !important;
            }

            .mt-sm-1 {
                margin-top: 0.25rem !important;
            }

            .mt-n2 {
                margin-top: -0.50rem !important;
            }

            #chartEmployee, #chartStudent {
                height: 270px;
            }

            #chartStudent {
                margin-top: 1.5rem !important;
            }

            .ct-chart-pie .ct-label {
                font-size: .75em;
                fill: #555 !important;
            }

            .refresh {
                display: none;
            }

            .w-sm-70 {
                width: 70% !important;
            }
        }

        @media (max-width: 991px) {
            .system-info, #fullCalendar .fc-month-view .fc-scroller {
                overflow: auto !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        decimal? MaxScoreBehavior = 100;
        int StudentAmount = 0;
        int StudentStatus0 = 0, StudentStatus1 = 0, StudentStatus2 = 0, StudentStatus3 = 0;
        int StudentStatus4 = 0, StudentStatus5 = 0, StudentStatus6 = 0;
        if (AdminMainData != null && AdminMainData.MaxScoreBehavior.HasValue)
        {
            MaxScoreBehavior = AdminMainData.MaxScoreBehavior.Value;
        }

        if (AdminMainData != null)
        {
            if (AdminMainData.StudentStatusData != null)
            {
                StudentAmount = AdminMainData.StudentStatusData.StudentAmount ?? 0;
                StudentStatus0 = AdminMainData.StudentStatusData.StudentStatus0 ?? 0;
                StudentStatus1 = AdminMainData.StudentStatusData.StudentStatus1 ?? 0;
                StudentStatus2 = AdminMainData.StudentStatusData.StudentStatus2 ?? 0;
                StudentStatus3 = AdminMainData.StudentStatusData.StudentStatus3 ?? 0;
                StudentStatus4 = AdminMainData.StudentStatusData.StudentStatus4 ?? 0;
                StudentStatus5 = AdminMainData.StudentStatusData.StudentStatus5 ?? 0;
                StudentStatus6 = AdminMainData.StudentStatusData.StudentStatus6 ?? 0;
            }
        }
    %>

    <div class="row">
        <div class="col-md-12">
            <h3 class="text-warning fw-bold"><%=AdminMainData.SchoolName%>
            </h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="card card-student-all-amount">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">school</i>
                    </div>
                    <h4 class="card-title text-muted"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></h4>
                    <h3 class="my-0 py-0 card-amount"><%=StudentAmount.ToString("#,0")%></h3>
                </div>
                <div class="card-body d-none">
                    <!-- search content here -->
                </div>
                <!-- end content-->
                <div class="card-footer">
                    <div class="stats">
                        <i class="material-icons">date_range</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <%=AdminMainData.AcademicYear%>
                    </div>
                </div>
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-4 -->
        <div class="col-md-6">
            <div class="card card-employee-all-amount">
                <div class="card-header card-header-purple card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">business_center</i>
                    </div>
                    <h4 class="card-title text-muted"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00451") %></h4>
                    <h3 class="my-0 py-0 card-amount"><%=AdminMainData.EmployeeAmount.ToString("#,0")%></h3>
                </div>
                <div class="card-body d-none">
                    <!-- search content here -->
                </div>
                <!-- end content-->
                <div class="card-footer">
                    <div class="stats">
                        <i class="material-icons">date_range</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <%=AdminMainData.AcademicYear%>
                    </div>
                </div>
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-4 -->
        <div class="col-md-4 d-none">
            <div class="card card-credit-bureau">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">equalizer</i>
                    </div>
                    <h4 class="card-title text-muted"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131026") %></h4>
                    <h3 class="my-0 py-0 card-credit"><%=AdminMainData.SchoolCreditBureau%></h3>
                </div>
                <div class="card-body d-none">
                    <!-- search content here -->
                </div>
                <!-- end content-->
                <div class="card-footer">
                    <div class="stats">
                        <i class="material-icons">date_range</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> <%=AdminMainData.AcademicYear%>
                    </div>
                </div>
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-4 -->
    </div>
    <!-- end row 1 -->

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">view_compact</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01309") %></h4>
                </div>
                <div class="card-body pb-4 system-overview">
                    <!-- search content here -->
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02128") %></h4>
                            <h4 class="w-25">: <%=AdminMainData.SummaryTime%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803039") %></h4>
                            <h4 class="w-25">: <%=AdminMainData.ReportTime%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %></h4>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01070") %></h4>
                            <h4 class="w-25">: <%=AdminMainData.AcademicYear%></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01301") %></h4>
                            <h4 class="w-25 text-nowrap">: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %> <%=AdminMainData.Semester%></h4>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01990") %></h4>
                            <h4 class="w-25 text-nowrap">: <%=AdminMainData.SemesterStartDate == null ? "-" : AdminMainData.SemesterStartDate.Value.ToString("d MMM yyyy", new System.Globalization.CultureInfo("th-TH"))%></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01994") %></h4>
                            <h4 class="w-25 text-nowrap">: <%=AdminMainData.SemesterEndDate == null ? "-" : AdminMainData.SemesterEndDate.Value.ToString("d MMM yyyy", new System.Globalization.CultureInfo("th-TH"))%></h4>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01998") %></h4>
                            <h4 class="w-25">: <%=AdminMainData.HolidayAmount%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00313") %></h4>

                            <h4 class="w-25 text-nowrap">: <%=MaxScoreBehavior.Value.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></h4>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row 2 -->

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">analytics</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02096") %></h4>
                </div>
                <div class="card-body pb-4 all-student-statistics">
                    <!-- search content here -->
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %></h4>
                            <h4 class="w-25">: <%=StudentStatus0.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01849") %></h4>
                            <h4 class="w-25">: <%=StudentStatus3.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %></h4>
                            <h4 class="w-25">: <%=StudentStatus1.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %></h4>
                            <h4 class="w-25">: <%=StudentStatus5.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0 mr-md-5">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %></h4>
                            <h4 class="w-25">: <%=StudentStatus2.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                        </div>
                        <div class="col-12 col-md-4 d-flex ml-5 ml-md-0">
                            <h4 class="w-50"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01166") %></h4>
                            <h4 class="w-25">: <%=StudentStatus6.ToString("#,0")%> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row 3 -->

    <div class="row">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header card-header-purple card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">calendar_today</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00990") %></h4>
                </div>
                <div class="card-body pb-4">
                    <!-- search content here -->
                    <div id="fullCalendar"></div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-8 -->
        <div class="col-md-4">
            <div class="card">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">notifications</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00231") %></h4>
                </div>
                <div class="card-body pr-1">
                    <!-- search content here -->
                    <div class="system-info pr-3">
                        <% if (AdminMainData.Broadcasts != null && AdminMainData.Broadcasts.Count > 0)
                            {
                                foreach (var m in AdminMainData.Broadcasts)
                                {
                        %>
                        <div class="system-info-item" data-id="<%=m.NewsID%>">
                            <div class="row">
                                <div class="col-12">
                                    <div class="d-flex float-start w-80 w-sm-70 position-absolute">
                                        <img class="logo" src="images/School Bright logo only.png" alt="">
                                        <h4 class="fw-bold text-truncate m-2 m-sm-1 p-1"><%=m.Title%></h4>
                                    </div>
                                    <h6 class="float-end mt-2 mt-sm-1 pt-2"><%=m.Created.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("th-TH"))%></h6>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="detail word-wrap mt-n2">
                                        <%=m.Detail%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <%
                                }
                            }
                            else
                            {
                        %>
                        <center><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01377") %></center>
                        <%
                            }
                        %>
                    </div>
                    <div class="mt-3 text-center">
                        <a href="UpdateLog.aspx" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00648") %></a>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-4 -->
    </div>
    <!-- end row 4 -->

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-danger card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">pie_chart</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02094") %>
                        <a id="aScanInOutChartRefresh" href="#" class="refresh-chart btn btn-danger float-end rounded p-0 pt-1"><span class="material-icons m-0">refresh</span></a><p class="h4 mt-3 mr-2 float-end refresh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00021") %></p>
                    </h4>
                </div>
                <div class="card-body pb-4 statistical-scans-in-out">
                    <!-- search content here -->
                    <div class="row scan-in-out-area-chart-refresh">
                        <div class="col-12 col-md-6">
                            <div id="chartEmployee" class="ct-chart"></div>
                            <div class="text-center">
                                <h4 class="fw-bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %></h4>
                            </div>
                        </div>
                        <div class="col-12 col-md-6">
                            <div id="chartStudent" class="ct-chart"></div>
                            <div class="text-center">
                                <h4 class="fw-bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></h4>
                            </div>
                        </div>
                    </div>
                    <div class="row d-none">
                        <div class="col-6 text-center">
                            <h4 class="fw-bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %></h4>
                        </div>
                        <div class="col-6 text-center">
                            <h4 class="fw-bold"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row d-none">
                        <div class="col-12">
                            <h4>LEGEND</h4>
                        </div>
                    </div>
                    <div class="row row-cols-auto">
                        <div class="col-12 col-md d-flex">
                            <span class="dot-green"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></h4>
                        </div>
                        <div class="col-12 col-md d-flex">
                            <span class="dot-cyan"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></h4>
                        </div>
                        <div class="col-12 col-md d-flex">
                            <span class="dot-orange"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></h4>
                        </div>
                        <div class="col-12 col-md d-flex">
                            <span class="dot-pink"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></h4>
                        </div>
                        <div class="col-12 col-md d-flex">
                            <span class="dot-purple"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></h4>
                        </div>
                        <div class="col-12 col-md d-flex">
                            <span class="dot-red"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></h4>
                        </div>
                        <div class="col-12 col-md d-flex">
                            <span class="dot-gray"></span>
                            <h4 class="ml-1 mt-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01365") %></h4>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row 5 -->

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-rose card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">request_quote</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02095") %>
                        <a id="aFinancialStatisticsChartRefresh" href="#" class="refresh-chart btn btn-rose float-end rounded p-0 pt-1"><span class="material-icons m-0">refresh</span></a><p class="h4 mt-3 mr-2 float-end refresh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00021") %></p>
                    </h4>

                </div>
                <div class="card-body financial-statistics-area-chart-refresh">
                    <!-- search content here -->
                    <div id="chartFinancialStatistics" class="ct-chart"></div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row 6 -->

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">group</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701017") %>
                        <a id="aBehaviorScoreChartRefresh" href="#" class="refresh-chart btn btn-success float-end rounded p-0 pt-1"><span class="material-icons m-0">refresh</span></a><p class="h4 mt-3 mr-2 float-end refresh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00021") %></p>
                    </h4>
                </div>
                <div class="card-body">
                    <!-- search content here -->
                    <div class="table-responsive behavior-score-area-chart-refresh">
                        <table class="table fs-5 behavior-score">
                            <thead class=" text-primary">
                                <tr>
                                    <th class="col-1 text-center text-dark fs-5"></th>
                                    <th class="col-4 text-left text-dark fs-5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>
                                    <th class="col-4 text-center text-dark fs-5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                    <th class="col-3 text-center text-dark fs-5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></th>
                                </tr>
                            </thead>
                            <%--<tbody>
                                <% foreach (var r in AdminMainData.BehaviorScoreRankings)
                                    {
                                %>
                                <tr>
                                    <td class="text-center"></td>
                                    <td class="text-left"><%=r.Name %></td>
                                    <td class="text-center"><%=r.Classroom %></td>
                                    <td class="text-center"><%=r.Score %></td>
                                </tr>
                                <%
                                    }
                                %>
                            </tbody>--%>
                            <tbody>
                                <tr>
                                    <td colspan="4" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00031") %></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row 7 -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <!-- Full Calendar Plugin, full documentation here: https://github.com/fullcalendar/fullcalendar -->
    <script src="/Content/Material/assets/js/plugins/fullcalendar.min.js"></script>

    <!-- Chartist JS -->
    <script src="/Content/Material/assets/js/plugins/chartist.min.js"></script>

    <script>  


        function OnError(xhr, errorType, exception) {
            var responseText;
            try {
                responseText = jQuery.parseJSON(xhr.responseText);
                var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;
                console.log(errorMessage);
            } catch (e) {
                responseText = xhr.responseText;
                console.log(responseText);
            }
        }

        $(document).ready(function () {

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00990") %>
            initFullCalendar(<%=AdminMainData.EventJsonData%>);

            <%--// <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02094") %>
            $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school01teacher", "",
                function (obj) { })
                .done(function (items) {

                    var employeeAmount = items[0].employessnumber == 0 ? 1 : items[0].employessnumber;
                    var data = { labels: [items[0].status_0 + '%', '0%', items[0].status_1 + items[0].status_4 + '%', items[0].status_3 + '%', '0%', items[0].status_2 + '%', '0%'], series: [items[0].status_0, 0, items[0].status_1, items[0].status_4, items[0].status_3, items[0].status_2, 0] };

                    for (var i = 0; i < data.series.length; i++) {
                        data.series[i] = ((data.series[i] / employeeAmount) * 100).toFixed(2);
                        data.labels[i] = data.series[i] < 1 ? ' ' : data.series[i] + '%';
                    }

                    initPieChart('#chartEmployee', data);
                });

            initPieChart('#chartStudent', JSON.parse('<%=AdminMainData.StudentDayScanJsonData%>'));--%>

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02094") %> (mockup data)
            initPieChart('#chartEmployee', JSON.parse('{"labels":[" ", " ", " ", " ", " ", " ", "0%"], "series":[0, 0, 0, 0, 0, 0, 100]}'));
            initPieChart('#chartStudent', JSON.parse('{"labels":[" "," "," "," "," "," ","0%"],"series":[0, 0, 0, 0, 0, 0, 100]}'));


            <%--// <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02095") %>
            $.ajax({
                url: "/App_Logic/dataReportGeneric.ashx?mode=GetReportChart01",
                dataType: 'JSON',
                type: 'POST',
                data: { get_values: true },
                success: function (result) {

                    var maxAxisY = 1000;
                    var data = { labels: [], series: [[], []] };
                    $.each(result, function (i, item) {

                        data.labels.push(new Date(item.thedate).toLocaleDateString('th'));
                        data.series[0].push(item.nSumSell);
                        data.series[1].push(item.nSumAdd);

                        if (maxAxisY < item.nSumSell) maxAxisY = item.nSumSell;
                        if (maxAxisY < item.nSumAdd) maxAxisY = item.nSumAdd;
                    });

                    initLineChart('#chartFinancialStatistics', data, maxAxisY + (maxAxisY * 0.02));
                }
            });--%>

            // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02095") %> (mockup data)
            initLineChart('#chartFinancialStatistics', JSON.parse('{"labels":["1/11/2565","2/11/2565","3/11/2565","4/11/2565","5/11/2565","6/11/2565","7/11/2565","8/11/2565","9/11/2565","10/11/2565","11/11/2565","12/11/2565","13/11/2565","14/11/2565","15/11/2565","16/11/2565","17/11/2565","18/11/2565","19/11/2565","20/11/2565","21/11/2565","22/11/2565","23/11/2565","24/11/2565","25/11/2565","26/11/2565","27/11/2565","28/11/2565","29/11/2565","30/11/2565"],"series":[[],[],[],[],[],[],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]]}'), 1530);


            $("#aScanInOutChartRefresh").click(function () {
                // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02094") %>
                $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school01teacher", "",
                    function (obj) { })
                    .done(function (items) {

                        var employeeAmount = items[0].employessnumber == 0 ? 1 : items[0].employessnumber;
                        var data = { labels: [items[0].status_0 + '%', '0%', items[0].status_1 + items[0].status_4 + '%', items[0].status_3 + '%', '0%', items[0].status_2 + '%', '0%'], series: [items[0].status_0, 0, items[0].status_1, items[0].status_4, items[0].status_3, items[0].status_2, 0] };

                        for (var i = 0; i < data.series.length; i++) {
                            data.series[i] = ((data.series[i] / employeeAmount) * 100).toFixed(2);
                            data.labels[i] = data.series[i] < 1 ? ' ' : data.series[i] + '%';
                        }

                        initPieChart('#chartEmployee', data);
                    });

                $.ajax({
                    async: true,
                    type: "POST",
                    url: 'AdminMain.aspx/LoadStudentDayScanData',
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var r = JSON.parse(result.d);
                        if (r.success) {
                            initPieChart('#chartStudent', JSON.parse(r.data));
                        }
                    },
                    error: OnError
                });

                return false;
            });

            $("#aFinancialStatisticsChartRefresh").click(function () {
                // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02095") %>
                $.ajax({
                    url: "/App_Logic/dataReportGeneric.ashx?mode=GetReportChart01",
                    dataType: 'JSON',
                    type: 'POST',
                    data: { get_values: true },
                    success: function (result) {

                        var maxAxisY = 1000;
                        var data = { labels: [], series: [[], []] };
                        $.each(result, function (i, item) {

                            data.labels.push(new Date(item.thedate).toLocaleDateString('th'));
                            data.series[0].push(item.nSumSell);
                            data.series[1].push(item.nSumAdd);

                            if (maxAxisY < item.nSumSell) maxAxisY = item.nSumSell;
                            if (maxAxisY < item.nSumAdd) maxAxisY = item.nSumAdd;
                        });

                        initLineChart('#chartFinancialStatistics', data, maxAxisY + (maxAxisY * 0.02));
                    }
                });

                return false;
            });

            $("#aBehaviorScoreChartRefresh").click(function () {
                // <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701017") %>
                $.ajax({
                    async: true,
                    type: "POST",
                    url: 'AdminMain.aspx/LoadStudentBehaviorScoreData',
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var r = JSON.parse(result.d);
                        if (r.success) {
                            $('table.behavior-score tbody').html('');
                            $.each(r.data, function (i, obj) {
                                $('table.behavior-score tbody:last-child').append(`<tr><td class="text-center"></td><td class="text-left">` + obj.name + `</td><td class="text-center">` + obj.classroom + `</td><td class="text-center">` + obj.score + `</td></tr>`);
                            });
                        }
                    },
                    error: OnError
                });

                return false;
            });


            // $('.system-info').perfectScrollbar();
            if ($(window).width() > 991) {
                $('.system-info').perfectScrollbar();
            }


        });

        function initFullCalendar(events) {
            $calendar = $('#fullCalendar');

            $calendar.fullCalendar({
                viewRender: function (view, element) {
                    // We make sure that we activate the perfect scrollbar when the view isn't on Month
                    if (view.name != 'month') {
                        // $(element).find('.fc-scroller').perfectScrollbar();
                        if ($(window).width() > 991) {
                            $(element).find('.fc-scroller').perfectScrollbar();
                        }
                    }
                },
                header: {
                    left: 'title',
                    center: 'month,agendaWeek,agendaDay',
                    right: 'prev,next,today'
                },
                defaultDate: new Date(),
                selectable: true,
                selectHelper: true,
                views: {
                    month: { // name of view
                        titleFormat: 'MMMM YYYY'
                        // other view-specific options here
                    },
                    week: {
                        titleFormat: " MMMM D YYYY"
                    },
                    day: {
                        titleFormat: 'D MMM, YYYY'
                    }
                },

                editable: false,
                eventLimit: true, // allow "more" link when too many events

                // color classes: [ event-blue | event-azure | event-green | event-orange | event-red ]
                events: events,
                eventClick: function (event, jsEvent, view) {
                    console.log(event, jsEvent, view);
                    swal({
                        title: '<h4 class="fw-bold">' + event.title + '</h4>',
                        type: 'info',
                        html: `<p class="text-left"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131025") %>: </strong>` + event.startText + `</p>
                               <p class="text-left"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132130") %>: </strong>`+ event.endText + `</p>
                               <p class="text-left"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132131") %>: </strong>`+ event.holidayType + `</p>`,
                        confirmButtonText: '<i class="fa fa-times"></i> Close'
                    });
                },
                displayEventTime: false
            });
        }

        function initPieChart(obj, data) {

            var options = {
                chartPadding: 40,
                labelOffset: 100,
                labelDirection: 'explode'
            };

            var responsiveOptions = [
                ['screen and (max-width: 576px)', {
                    chartPadding: 20,
                    labelOffset: 70,
                    labelDirection: 'explode'
                }]
            ];

            Chartist.Pie(obj, data, options, responsiveOptions);
        }

        function initLineChart(obj, data, maxAxisY) {

            options = {
                lineSmooth: Chartist.Interpolation.cardinal({
                    tension: 10
                }),
                axisY: {
                    showGrid: true,
                    offset: 40
                },
                axisX: {
                    showGrid: true,
                },
                low: 0,
                high: maxAxisY,
                chartPadding: 20,
                showPoint: true,
                height: '420px'
            };

            /* Now we can specify multiple responsive settings that will override the base settings based on order and if the media queries match. In this example we are changing the visibility of dots and lines as well as use different label interpolations for space reasons. */
            //var responsiveOptions = [
            //    ['screen and (min-width: 641px) and (max-width: 1024px)', {
            //        showPoint: false,
            //        axisX: {
            //            labelInterpolationFnc: function (value) {
            //                return 'Week ' + value;
            //            }
            //        }
            //    }],
            //    ['screen and (max-width: 640px)', {
            //        showLine: false,
            //        axisX: {
            //            labelInterpolationFnc: function (value) {
            //                return 'W' + value;
            //            }
            //        }
            //    }]
            //];
            var responsiveOptions = [];

            var lineChart = new Chartist.Line(obj, data, options, responsiveOptions);
            startAnimationForLineChart(lineChart);
        }

        var seq = 0;
        function startAnimationForLineChart(chart) {

            chart.on('draw', function (data) {
                if (data.type === 'line' || data.type === 'area') {
                    data.element.animate({
                        d: {
                            begin: 600,
                            dur: 700,
                            from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                            to: data.path.clone().stringify(),
                            easing: Chartist.Svg.Easing.easeOutQuint
                        }
                    });
                } else if (data.type === 'point') {
                    seq++;
                    data.element.animate({
                        opacity: {
                            begin: seq * delays,
                            dur: durations,
                            from: 0,
                            to: 1,
                            easing: 'ease'
                        }
                    });
                }
            });

            seq = 0;
        }


    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
    <div class="modal fade modal-font-size-default" role="dialog" id="modal-invoice-setting" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content modal-lg">
                <div class="modal-header modal-lg">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h4>
                </div>
                <div class="modal-body modal-lg word-wrap" id="modal-content">
                </div>
                <div class="modal-footer text-center">
                    <button id="modalClose" type="button" class="btn btn-default" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            CheckInvoiceRemind();
        });

        function CheckInvoiceRemind() {
            $.ajax({
                async: true,
                type: "POST",
                url: 'AdminMain.aspx/CheckInvoiceRemind',
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    result = JSON.parse(result.d);
                    if (result.success == true) {
                        $("#modal-content").html(result.message);
                        $("#modal-invoice-setting").modal('show');
                    }
                },
                error: OnError
            });
        }
    </script>
</asp:Content>
