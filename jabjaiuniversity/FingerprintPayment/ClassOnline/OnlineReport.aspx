<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="OnlineReport.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline.OnlineReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <%--<link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>
    <%-- <script src="//cdn.jsdelivr.net/npm/js-cookie@rc/dist/js.cookie.min.js"></script>--%>
    <link href="css/StyleSheet1.css?v=<%= DateTime.Now.ToString("ddMMyyyy")%>" rel="stylesheet" />


    <style>
        .page-online .ddl-create-item {
            font-size: 22px;
        }

            .page-online .ddl-create-item.disabled {
                color: lightgrey;
            }

        .page-online .bread-item {
            font-size: 20px;
        }

        .page-online .group-level {
            color: #fff;
            background-color: #f0ad4e;
            border-color: #eea236;
            display: inline-block;
            padding: 14px 12px;
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
            padding: 10px 14px;
            margin-bottom: 5px;
            margin-right: 8px;
            border-radius: 0px;
            font-size: 16px;
        }

            .div-group .nav > li > a:hover,
            .div-group .nav > li > a:active,
            .div-group .nav > li > a:focus {
                background-color: #ff9600;
            }

        .nav-pills > li.active a {
            background-color: #ff9600;
            color: #000 !important;
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
            top: -45px;
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
                                <div class="col-md-8 col-xs-12">
                                    <ul class="breadcrumb pull-left" style="">
                                        <li class="bread-item">
                                            <span class="group-level"><%= _class.TitleName %></span>
                                        </li>
                                        <li class="bread-item active"><%= _level.SubLevel %></li>
                                        <li class="bread-item active"><%= _plan.sPlaneName%></li>
                                        <li class="bread-item active"><%= _plan.courseCode %></li>
                                    </ul>
                                </div>
                                <div class="col-md-4 col-xs-12">
                                    <div class="pull-right" style="widthx: 200px;">
                                        <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true" Width="150px" CssClass="selectpicker1" data-style="select-with-transition" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02049") %>"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01456") %>"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405018") %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="display: none;">
                                    <asp:Button ID="btnRefreshTable" runat="server" Text="Button" OnClick="btnRefreshTable_Click" />
                                </div>
                                <div id="alltable-wrapper" class=" col-md-12" style="">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlFilter" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnRefreshTable" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div id="" class="div-group">
                                                <div class="row tab-header">
                                                    <div class="col-md-8 p-0">
                                                        <ul class="nav nav-pills" style="padding: 0; margin-top: 10px;">
                                                            <% 
                                                                int i = 1;
                                                                var _filter = ddlFilter.SelectedValue;

                                                                var q = _reportList.AsQueryable();
                                                                if (_filter != "")
                                                                {
                                                                    switch (_filter)
                                                                    {
                                                                        case "1":
                                                                            q = q.Where(o => o.IsSend == true);
                                                                            break;
                                                                        case "2":
                                                                            q = q.Where(o => o.IsSend != true);
                                                                            break;
                                                                        case "3":
                                                                            q = q.Where(o => o.IsLate == true || (o.SendDate == null && DateTime.Now > _homework.dStart));
                                                                            break;
                                                                        default:
                                                                            break;
                                                                    }
                                                                }

                                                                foreach (var g in q.GroupBy(o => new { o.Level, o.LevelID }).OrderBy(o => o.Key.Level))
                                                                { %>
                                                            <li class="<%= (i == 1) ? "active" : "" %>">
                                                                <a class="link-pill" data-toggle="pill" href="#<%= "tab"+i %>" data-text="<%= g.Key.LevelID %>"><%= g.Key.Level %>
                                                                </a>
                                                            </li>
                                                            <%  i++;
                                                                } %>
                                                        </ul>
                                                    </div>
                                                    <div class="col-md-4"></div>
                                                </div>

                                                <div class="tab-content">
                                                    <% 
                                                        i = 1;
                                                        foreach (var g in q.GroupBy(o => o.Level))
                                                        { %>

                                                    <div id="<%= "tab"+i %>" class="tab-pane <%= (i == 1) ? "active" : "" %>">

                                                        <div class="row" style="height: 80px;">
                                                            <div class="col-lg-10 col-md-8 col-xs-12">
                                                            </div>
                                                            <div class="col-lg-2 col-md-4 col-xs-12 pull-right p-0">
                                                                <table class="table" style="">
                                                                    <thead>
                                                                        <tr class="headerCell" style="background-color: #f0ad4e; height: 50px;">
                                                                            <th class="text-center" style="width: 50%">ส่งแล้ว</th>
                                                                            <th class="text-center" style="width: 50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01456") %></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="itemCell">
                                                                            <td class="text-center"><%= _reportList.Count( o => o.Level == g.Key && o.IsSend == true) %></td>
                                                                            <td class="text-center"><%=  _reportList.Count( o => o.Level == g.Key && o.IsSend != true) %></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>

                                                        <table id="tableroom" class="table-hover dataTable" style="margin-top:24px !important">
                                                            <thead>
                                                                <tr class="headerCell">
                                                                    <th class="text-center" style="width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                                    <th class="text-center" style="width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                                                    <th class="text-center" style="width: 15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                                                                    <th class="text-center" style="width: 20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %></th>
                                                                    <th class="text-center" style="width: 15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %></th>
                                                                    <th class="text-center" style="width: 15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132069") %></th>
                                                                    <th class="text-center" style="width: 15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></th>
                                                                    <th class="text-center" style=""></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>


                                                                <% int index = 1; %>
                                                                <% foreach (var r in g.ToList())
                                                                    { %>
                                                                <tr class="itemCell id-<%= r.UserId%> <%= r.IsLate == true || (r.SendDate == null && DateTime.Now > _homework.dStart) ? "tr-late" : ""%>">
                                                                    <td class="text-center"><%= index++ %></td>
                                                                    <td class="text-center"><%= r.Level%></td>
                                                                    <td class="text-center"><%= r.Code%></td>
                                                                    <td class="left"><%= r.FullName%></td>
                                                                    <td class="text-center"><%= r.IsRead == true ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401060") %>" : "ยังไม่อ่าน" %></td>
                                                                    <td class="text-center"><%= r.IsSend == true ? "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02049") %><br/>" + (r.SendDate.HasValue ? r.SendDate.Value.ToString("dd/MM/yyyy HH:mm <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>",new System.Globalization.CultureInfo("th-TH")) : "-") : "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01456") %>" %></td>
                                                                    <td class="text-center"><%= r.Score  %></td>
                                                                    <td class="text-center">
                                                                        <%-- <a href="OnlineReportEdit.aspx?wid=<%= r.WorkId%>&uid=<%= r.UserId%>" data-homeid="<%= r.WorkId%>" data-uid="<%= r.UserId %>" class="btn  btn-success vpopup"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405041") %></a>--%>
                                                                        <div class="dropdown">
                                                                            <button class="btn btn-success dropdown-toggle" type="button" data-toggle="dropdown">
                                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101022") %>&nbsp;<span class="caret"></span>
                                                                            </button>
                                                                            <ul class="dropdown-menu">
                                                                                <li><a href="OnlineReportEdit.aspx?wid=<%= r.WorkId%>&uid=<%= r.UserId%>" data-homeid="<%= r.WorkId%>" data-uid="<%= r.UserId %>" class=" vpopup" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405041") %></a></li>
                                                                                <li><a href="#tableroom" class="link-status" data-name="<%= r.FullName %>" data-code="<%= r.Code %>" data-uid="<%= r.UserId %>" data-homeid="<%= r.WorkId%>" style="" data-togglex="modal" data-targetx="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01047") %></a></li>
                                                                            </ul>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <% } %>
                                                            </tbody>
                                                        </table>

                                                    </div>

                                                    <%  i++;
                                                        } %>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-center">
                                <a class="btn btn-default search-btn" href="OnlineManage.aspx?term=<%=(_class.TermId+"").Trim() %>&plan=<%=_class.PlanId%>&level=<%=_class.LevelId %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title" id="modaltitle" style="display: block !important;"></h3>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateStatus" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="modal-body" style="">
                                        <div class="d-none">
                                            <asp:TextBox ID="txtHID" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtUID" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnUpdateStatus" runat="server" Text="Button" OnClick="btnUpdateStatus_Click" />
                                        </div>
                                        <!-- Modal content-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02078") %></p>
                                                    <asp:DropDownList ID="ddlStatus" runat="server"
                                                        DataTextField="Text"
                                                        DataValueField="Value"
                                                        CssClass="form-controlx selectpicker1 -custom-fixx chosen-selectx -w100"
                                                         data-style="select-with-transition">
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>" Value="" />
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02041") %>" Value="1" />
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132070") %>" Value="2" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02086") %></p>
                                                    <asp:DropDownList ID="ddlTime" runat="server"
                                                        DataTextField="Text"
                                                        DataValueField="Value"
                                                        CssClass="form-controlx selectpicker1 -custom-fixx chosen-selectx -w100"
                                                         data-style="select-with-transition">
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206188") %>" Value="" />
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>" Value="1" />
                                                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405018") %>" Value="2" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <p>
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %> (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132248") %>
                                            <asp:Literal ID="LiteralMax" runat="server"></asp:Literal>)
                                                    </p>
                                                    <asp:TextBox ID="txtScore" runat="server" TextMode="Number" CssClass="form-control -w100" Style="" required="required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer">

                                        <% if (true) /*(Permission == JabjaiMainClass.PermissionType.Modify)*/
                                            { %>
                                            <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" runat="server" ID="btnSaveTitle" CssClass="btn btn-success search-btn" OnClick="btnSaveTitle_Click" />
                                        <% }
                                        else
                                        { %>
                                            <button class="btn btn-success " type="button" onclick="NoPermissionPopup()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                                        <% } %>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
       <script src="//cdn.jsdelivr.net/npm/sweetalert2@9"></script>
   <%-- <script src="js/chosen.jquery.js?v=<%= DateTime.Now.ToString("ddMMyyyy")%>"></script>--%>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/js-cookie/2.2.1/js.cookie.min.js"></script>
    <script src="js/func.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>
    <!-- Modal -->

    <script>    

        //fix in update panel
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(function (sender, args) {
            if ($("#aspnetForm").valid() == false) {
                args.set_cancel(true);
            }
        });

      <%--  prm.add_endRequest(function (sender, args) {
            if (sender._postBackSettings != null && sender._postBackSettings.panelsToUpdate == '<%= UpdatePanel2.UniqueID %>') {
                //$('#myModal .modal-body').show();
                $('#myModal').modal('show');
            }
        });--%>

        prm.add_pageLoaded(function (sender, args) {
            if (!isMobileAndTabletCheck())
                //$('.chosen-select').chosen();
                $('.selectpicker1').selectpicker();
            else {
                //$(".chosen-select").removeClass('-custom-fix');
            }

            var room = Cookies.get('online_report_room') || '';
            var status = Cookies.get('online_report_status') || '';

            var activeroom = $('.nav-pills li.active a').data('text');
            var activestatus = $('#ctl00_MainContent_ddlFilter').val() + '';

            if (status != '' && status != activestatus) {
                $('#ctl00_MainContent_ddlFilter').val(status);
                $('#ctl00_MainContent_ddlFilter').trigger('change');
            }

            if (room != '' && room != activeroom) {
                $('.nav-pills li.active').removeClass('active');
                $('a.link-pill[data-text="' + room + '"]').trigger('click');//parent().addClass('active');
            }
            else if (room != '') {
                // $('a.link-pill[data-text="' + room + '"]').trigger('click');
            }
            //$('#alltable-wrapper').show();
          <%--  if (args._panelsUpdated.length > 0 && args._panelsUpdated[0] == 'div#<%= UpdatePanel2.ClientID %>') {
                $('#myModal .modal-body').show();
            }--%>
        });

        function showModal() {
            $('#myModal').modal('show');
        }

        function reloadAfterSave(uid) {
            //close modal
            $('#myModal').modal('hide');

            //trigger ddlFilter
            //$("#<%= ddlFilter.ClientID %>").trigger('change');
            $("#<%= btnRefreshTable.ClientID %>").trigger('click');
            //__doPostBack('<%= ddlFilter.UniqueID %>', '');
        }

        $(function () {

            $('#alltable-wrapper').on('click', '.link-status', function (e) {
                e.preventDefault();

                let $t = $(this);
                let hid = $t.data('homeid');
                let uid = $t.data('uid');

                var title = $t.data('code') + ' - ' + $t.data('name');
                //alert(title);
                $("#modaltitle").text(title);
                $('#<%= txtHID.ClientID %>').val(hid);
                $('#<%= txtUID.ClientID %>').val(uid);
                $('#<%= ddlStatus.ClientID %>').find("option").attr("selected", false);
                $('#<%= ddlStatus.ClientID %>').trigger("chosen:updated");

                $('#<%= ddlTime.ClientID %>').find("option").attr("selected", false);
                $('#<%= ddlTime.ClientID %>').trigger("chosen:updated");

                $('#<%= btnUpdateStatus.ClientID %>').trigger('click');
                // __doPostBack("<%=UpdatePanel1.UniqueID %>", "");

                //return false;
            });

            $('#ctl00_MainContent_ddlFilter').on('change', function (e) {
                Cookies.set('online_report_status', $(this).val());
            });

            $('#alltable-wrapper').on('click', '.link-pill', function (e) {
                Cookies.set('online_report_room', $(this).data('text'));
                e.preventDefault();
            });

            $('#myModal').on('hidden.bs.modal', function () {
                $('#<%= ddlStatus.ClientID%>').removeAttr('required');;
                $('#<%= ddlTime.ClientID%>').removeAttr('required');
            });

            //$('#myModal').on('show.bs.modal', function () {
            //    $('#myModal .modal-body').hide();
            //});

            $('#myModal').on('shown.bs.modal', function () {

                $('#<%= ddlStatus.ClientID%>').attr('required', 'true');
                $('#<%= ddlTime.ClientID%>').attr('required', 'true');//.removeAttr('required');
            });

        });
    </script>

</asp:Content>
