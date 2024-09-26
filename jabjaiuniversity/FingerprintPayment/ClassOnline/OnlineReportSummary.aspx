<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="OnlineReportSummary.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline.OnlineReportSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">

    <link href="css/StyleSheet1.css?v=<%= DateTime.Now.ToString("ddMMyyyy")%>" rel="stylesheet" />
    <%--   <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>


    <style>
        .page-online .ddl-create-item {
            font-size: 22px;
        }

            .page-online .ddl-create-item.disabled {
                color: lightgrey;
            }

        .page-online .bread-item {
            font-size: 30px;
        }

        .page-online .group-level {
            color: #fff;
            background-color: #f0ad4e;
            border-color: #eea236;
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 10px;
        }

        .page-online .breadcrumb {
            padding: 0px 0px;
            background-color: #ffffff;
            margin-bottom: 0
        }

            .page-online .breadcrumb > li + li:before {
                padding: 0 5px;
                color: #ccc;
                content: " ";
            }

        .page-online .topic-item.active {
            color: red;
        }

        .tr-late {
            background-color: #ffcece;
        }

        .div-group .nav > li > a {
            background-color: #f0ad4e;
            padding: 5px 10px;
            margin-bottom: 5px;
            margin-top: 5px;
            margin-right: 8px;
            border-radius: 0px;
        }

            .div-group .nav > li > a:hover,
            .div-group .nav > li > a:active,
            .div-group .nav > li > a:focus {
                background-color: #ff9600;
            }

        .div-group .roomtab.active {
            background-color: #ff9600;
            color: #000;
            font-weight: bold;
        }
        /*.chosen-container {
            width: 200px !important;
        }*/


        .-custom-fix {
            display: block !important;
            width: 0 !important;
            height: 0 !important;
            border: 1px solid #fff;
            position: relative;
            top: 35px;
            /*left: 80px;*/
            margin: 0 auto;
        }

        .div-group .tab-content {
            position: relative;
            top: -75px;
            z-index: 1;
        }

        .div-group .tab-header {
            position: relative;
            z-index: 9;
        }




        @media (max-width: 800px) {
            .div-group .tab-content {
                position: relative;
                top: 0;
                z-index: 1;
            }
        }

        .cool-table .headerCell th {
            padding: 0 8px;
        }
    </style>


    <style>
        #floatingCirclesG {
            position: relative;
            width: 100px;
            height: 100px;
            -moz-transform: scale(0.6);
            -webkit-transform: scale(0.6);
            -ms-transform: scale(0.6);
            -o-transform: scale(0.6);
            transform: scale(0.6);
        }

        .f_circleG {
            position: absolute;
            background-color: #FFFFFF;
            height: 18px;
            width: 18px;
            -moz-border-radius: 9px;
            -moz-animation-name: f_fadeG;
            -moz-animation-duration: 1.04s;
            -moz-animation-iteration-count: infinite;
            -moz-animation-direction: normal;
            -webkit-border-radius: 9px;
            -webkit-animation-name: f_fadeG;
            -webkit-animation-duration: 1.04s;
            -webkit-animation-iteration-count: infinite;
            -webkit-animation-direction: normal;
            -ms-border-radius: 9px;
            -ms-animation-name: f_fadeG;
            -ms-animation-duration: 1.04s;
            -ms-animation-iteration-count: infinite;
            -ms-animation-direction: normal;
            -o-border-radius: 9px;
            -o-animation-name: f_fadeG;
            -o-animation-duration: 1.04s;
            -o-animation-iteration-count: infinite;
            -o-animation-direction: normal;
            border-radius: 9px;
            animation-name: f_fadeG;
            animation-duration: 1.04s;
            animation-iteration-count: infinite;
            animation-direction: normal;
        }

        #frotateG_01 {
            left: 0;
            top: 41px;
            -moz-animation-delay: 0.39s;
            -webkit-animation-delay: 0.39s;
            -ms-animation-delay: 0.39s;
            -o-animation-delay: 0.39s;
            animation-delay: 0.39s;
        }

        #frotateG_02 {
            left: 12px;
            top: 12px;
            -moz-animation-delay: 0.52s;
            -webkit-animation-delay: 0.52s;
            -ms-animation-delay: 0.52s;
            -o-animation-delay: 0.52s;
            animation-delay: 0.52s;
        }

        #frotateG_03 {
            left: 41px;
            top: 0;
            -moz-animation-delay: 0.65s;
            -webkit-animation-delay: 0.65s;
            -ms-animation-delay: 0.65s;
            -o-animation-delay: 0.65s;
            animation-delay: 0.65s;
        }

        #frotateG_04 {
            right: 12px;
            top: 12px;
            -moz-animation-delay: 0.78s;
            -webkit-animation-delay: 0.78s;
            -ms-animation-delay: 0.78s;
            -o-animation-delay: 0.78s;
            animation-delay: 0.78s;
        }

        #frotateG_05 {
            right: 0;
            top: 41px;
            -moz-animation-delay: 0.91s;
            -webkit-animation-delay: 0.91s;
            -ms-animation-delay: 0.91s;
            -o-animation-delay: 0.91s;
            animation-delay: 0.91s;
        }

        #frotateG_06 {
            right: 12px;
            bottom: 12px;
            -moz-animation-delay: 1.04s;
            -webkit-animation-delay: 1.04s;
            -ms-animation-delay: 1.04s;
            -o-animation-delay: 1.04s;
            animation-delay: 1.04s;
        }

        #frotateG_07 {
            left: 41px;
            bottom: 0;
            -moz-animation-delay: 1.17s;
            -webkit-animation-delay: 1.17s;
            -ms-animation-delay: 1.17s;
            -o-animation-delay: 1.17s;
            animation-delay: 1.17s;
        }

        #frotateG_08 {
            left: 12px;
            bottom: 12px;
            -moz-animation-delay: 1.3s;
            -webkit-animation-delay: 1.3s;
            -ms-animation-delay: 1.3s;
            -o-animation-delay: 1.3s;
            animation-delay: 1.3s;
        }

        @-moz-keyframes f_fadeG {
            0% {
                background-color: #000000
            }

            100% {
                background-color: #FFFFFF
            }
        }

        @-webkit-keyframes f_fadeG {
            0% {
                background-color: #000000
            }

            100% {
                background-color: #FFFFFF
            }
        }

        @-ms-keyframes f_fadeG {
            0% {
                background-color: #000000
            }

            100% {
                background-color: #FFFFFF
            }
        }

        @-o-keyframes f_fadeG {
            0% {
                background-color: #000000
            }

            100% {
                background-color: #FFFFFF
            }
        }

        @keyframes f_fadeG {
            0% {
                background-color: #000000
            }

            100% {
                background-color: #FFFFFF
            }
        }

        .page-online .bread-item.active {
            font-size: 20px;
            /*  margin-top: 30px;*/
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card ">

        <div class="col-md-12">

            <form id="from1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>

                <div class="full-card box-content smssetting-container  page-online">
                    <div class="row">
                        <div class="col-md-8 col-xs-12">
                            <div>
                                <h3><span class="material-icons" style="color: #f19f3a; font-size: 30px;">home_work</span>&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206345") %></h3>
                            </div>
                        </div>
                        <div class="col-md-7 col-xs-12 pull-right">
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12 ">
                                    <ul class="breadcrumb pull-left" style="">
                                        <li class="bread-item active"><%= _level.SubLevel %></li>
                                        <li class="bread-item active"><%= _plan.sPlaneName%></li>
                                        <li class="bread-item active"><%= _plan.courseCode %></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 div-group">
                                    <ul class="nav nav-pills" style="padding: 20px 4px;">
                                        <%  foreach (var r in _roomList.OrderBy(o => o.Room))
                                            { %>
                                        <li class=''>
                                            <a class="roomtab" data-toggle="pill" data-id="<%=r.Id %>" href="#<%= "tab"+r.Id %>"><%= r.Room %>
                                            </a>
                                        </li>
                                        <%  }%>
                                    </ul>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 ">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="floatingCirclesG" class="text-center" style="float: none; margin: 0 auto;">
                                                <div class="f_circleG" id="frotateG_01">
                                                </div>
                                                <div class="f_circleG" id="frotateG_02">
                                                </div>
                                                <div class="f_circleG" id="frotateG_03">
                                                </div>
                                                <div class="f_circleG" id="frotateG_04">
                                                </div>
                                                <div class="f_circleG" id="frotateG_05">
                                                </div>
                                                <div class="f_circleG" id="frotateG_06">
                                                </div>
                                                <div class="f_circleG" id="frotateG_07">
                                                </div>
                                                <div class="f_circleG" id="frotateG_08">
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" OnLoad="UpdatePanel1_Load">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnLoadRoom" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:LinkButton runat="Server" ID="btnLoadRoom" Text="Save" OnClick="btnLoadRoom_Click" CssClass="d-none" />

                                    <div class="row <%= _reportList.Count == 0 ? "d-none" : "" %>">
                                        <div class="col-md-12 " style="">

                                            <% 
                                                var groupCol = _reportList.GroupBy(o => new { o.HID, o.HomeWork }).OrderBy(o => o.Key.HID).ToList();
                                                var groupRow = _reportList.GroupBy(o => new { o.sID, o.StudentName, o.sStudentID }).ToList();
                                            %>
                                            <div class="table-responsivxe" style="overflow-x: auto;">
                                                <%--font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;--%>
                                                <table id="reportTable" class="table-hover dataTable " style="border-collapse: collapse; text-decoration: none; width: 100%;">
                                                    <thead>

                                                        <tr class="headerCell">
                                                            <th class="center" style="width: 5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                            <th class="center" style="width: 5%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></th>
                                                            <th class="center" style="width: 20%; min-width:220px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>


                                                            <%   foreach (var c in groupCol)
                                                                { %>
                                                            <th class='center' style="width: 150px;" data-id="<%= c.Key.HID %>">
                                                                <%= c.Key.HomeWork %>
                                                                <br />
                                                                (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204046") %> <%= c.Max( i => i.MaxScore) %>)
                                                            </th>
                                                            <%  } %>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr class="headerCell">
                                                            <th class="center" style="width: 5%"></th>
                                                            <th class="center" style="width: 5%"></th>
                                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132073") %></th>
                                                            <%   foreach (var c in groupCol)
                                                                { %>
                                                            <th class='center' data-id="<%= c.Key.HID %>">
                                                                <%= (c.Average( o => o.Score) ?? 0).ToString("#,0.##") %>
                                                            </th>
                                                            <%  } %>
                                                        </tr>

                                                        <%   
                                                            int index = 1;
                                                            foreach (var r in groupRow)
                                                            { %>
                                                        <tr class="itemCell">
                                                            <td class="center"><%= index++ %></td>
                                                            <td class="center"><%= r.Key.sStudentID %></td>
                                                            <td class="center"><%= r.Key.StudentName %></td>

                                                            <%   foreach (var c in groupCol)
                                                                {
                                                                    var _score = r.FirstOrDefault(o => o.HID == c.Key.HID);
                                                            %>
                                                            <td class='center' data-hid="<%= c.Key.HID %>" data-sid="<%= r.Key.sID %>">
                                                                <%= _score != null ? ""+ (_score.Score.HasValue ? _score.Score+ "" : "-" ) : "-" %>
                                                            </td>
                                                            <%  } %>
                                                        </tr>
                                                        <%  } %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-center">
                                <a class="btn btn-default search-btn" href="OnlineManage.aspx?<%= Request.QueryString %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--<script src="js/chosen.jquery.js?v=<%= DateTime.Now.ToString("ddMMyyyy")%>"></script>--%>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>
    <script src="js/func.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>

    <%--<!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnUpdateStatus" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="hidden">

                        <asp:TextBox ID="txtHID" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtUID" runat="server"></asp:TextBox>
                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Button" OnClick="btnUpdateStatus_Click" />
                    </div>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01047") %></h2>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02078") %></h3>
                                        <asp:DropDownList ID="ddlStatus" runat="server"
                                            DataTextField="Text"
                                            DataValueField="Value"
                                            CssClass="form-control -custom-fix chosen-select">
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>" Value="" />
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02041") %>" Value="1" />
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132070") %>" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02086") %></h3>
                                        <asp:DropDownList ID="ddlTime" runat="server"
                                            DataTextField="Text"
                                            DataValueField="Value"
                                            CssClass="form-control -custom-fix chosen-select">
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>" Value="" />
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="1" />
                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405018") %>" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" runat="server" ID="btnSaveTitle" CssClass="btn btn-success search-btn" OnClick="btnSaveTitle_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>--%>

    <script>    

        //fix in update panel
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(onEachRequest);

        function onEachRequest(sender, args) {
            //if ($("#aspnetForm").valid() == false) {
            //    args.set_cancel(true);
            //}
            $('#reportTable').hide();
        }

        function pageLoad() {
            //$('.chosen-select').chosen();


        }

        $(function () {

            $('.roomtab').on('click', function () {
                $('.roomtab').removeClass('active');
                $(this).addClass('active');
                __doPostBack('ctl00$MainContent$btnLoadRoom', $(this).data('id'));

                return false;
            });

            $('.roomtab:eq(0)').trigger('click');

        });
    </script>

</asp:Content>
