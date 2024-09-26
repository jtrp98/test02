<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="OnlineManage.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline.OnlineManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link href="//fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">

    <link href="css/StyleSheet1.css?v=<%= DateTime.Now.ToString("ddMMyyyy")%>" rel="stylesheet" />


    <style>


        .page-online .ddl-create-item {
            /* font-size: 22px;*/
        }

            .page-online .ddl-create-item.disabled {
                color: lightgrey;
            }



        .page-online .group-level {
            color: #fff;
            background-color: #f0ad4e;
            border-color: #eea236;
            display: inline-block;
            padding: 6px 12px;
        }

        .page-online .breadcrumb {
            padding: 8px 0px;
            background-color: #ffffff;
            margin-bottom: 0
        }

        .page-online .bread-item.active {
            font-size: 20px;
            margin-top: 30px;
            margin-left: 5px;
        }

        .page-online .breadcrumb > li + li:before {
            padding: 0 5px;
            color: #ccc;
            content: " ";
        }

        .page-online .topic-item.active {
            color: red;
        }

        .page-online .topic-item {
            font-size: 18px;
        }

        .chosen-container-multi .chosen-choices .search-choice .search-choice-close {
            background-size: 440%; /*WTF this*/
        }

        .-custom-fix {
            display: block !important;
            width: 0 !important;
            height: 0 !important;
            border: 1px solid #fff;
            position: relative;
            top: 75px;
            /*left: 80px;*/
            margin: 0 auto;
        }

        /* .swal2-popup {
            font-size: 1.8rem !important;
        }*/

        /* .swal2-confirm, .swal2-cancel {
            font-size: 2rem !important;
        }*/

        #ddlShare_chosen ul.chosen-results > li[data-value=''] {
            pointer-events: none !important;
        }

        #ddlShare_chosen ul.chosen-results.empty-chosen {
            display: block !important;
        }

        #ddlShare_chosen ul.chosen-results > li[data-value=''] > input {
            display: none !important;
        }

        #ddlShare_chosen ul.chosen-choices .search-choice span {
            line-height: 1.2;
        }
/*
        .dropdown .dropdown-menu{
            top: 0 !important; 
            left: 0 !important; 
         will-change: top, left !important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card ">

        <div class="col-md-12">

            <form id="from1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>

                <div class="full-card box-content smssetting-container page-online">
                    <div class="row">
                        <%--<div class="col-md-2"></div>--%>
                        <div class="col-md-8">
                            <div>
                                <h3><span class="material-icons" style="color: #f19f3a; font-size: 30px;">home_work</span>&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206345") %></h3>
                            </div>
                        </div>
                        <div class="col-md-4 pt-2">
                         
                            <a href="OnlineReportSummary.aspx?<%= Request.QueryString %>" class="btn btn-success  pull-right"><span>
                                <span class="material-icons">receipt</span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %>&nbsp;&nbsp;</span>
                            </a>
                            <div class="dropdown pull-right">
                                <button class="btn btn-success  dropdown-toggle topic-new" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                    <span class="material-icons">add</span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02111") %>&nbsp;&nbsp;<span class="caret"></span>
                                </button>

                                <% if (true) /*(Permission == JabjaiMainClass.PermissionType.Modify)*/
                                    { %>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenu1" style="width: 200px;">
                                    <li>
                                        <a href="#" class="ddl-create-item" data-toggle="modal" data-target="#myModal"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02259") %></span></a>

                                    </li>
                                    <li role="separator" class="divider"></li>
                                    <% if (_lstTopic.Count > 0)
                                        { %>
                                    <li><a href="OnlineWorkAdd.aspx?<%= Request.QueryString %>" class="ddl-create-item"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206351") %></span></a></li>
                                    <li><a href="OnlineLearnAdd.aspx?<%= Request.QueryString %>" class="ddl-create-item"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206352") %></span></a></li>
                                    <% }
                                        else
                                        { %>
                                    <li><a href="#" class="ddl-create-item disabled"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132068") %></span></a></li>
                                    <li><a href="#" class="ddl-create-item disabled"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206352") %></span></a></li>
                                    <% } %>
                                </ul>
                                <% }
                                    else
                                    { %>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenu1" style="width: 200px;">
                                    <li>
                                        <a href="#" class="ddl-create-item" onclick="NoPermissionPopup()"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02259") %></span></a>

                                    </li>
                                    <li role="separator" class="divider"></li>
                                    <% if (_lstTopic.Count > 0)
                                        { %>
                                    <li><a href="#" onclick="NoPermissionPopup()" class="ddl-create-item"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206351") %></span></a></li>
                                    <li><a href="#" onclick="NoPermissionPopup()" class="ddl-create-item"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206352") %></span></a></li>
                                    <% }
                                        else
                                        { %>
                                    <li><a href="#" class="ddl-create-item disabled"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132068") %></span></a></li>
                                    <li><a href="#" class="ddl-create-item disabled"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206352") %></span></a></li>
                                    <% } %>
                                </ul>
                                <% } %>

                              
                            </div>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-4 col-xs-4">
                            <br />
                            <%-- <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>--%>
                            <% if (_lstTopic.Count > 0)
                                { %>
                            <h4><a href="#" class="topic-top"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206346") %></a></h4>
                            <ul>
                                <% foreach (var topic in _lstTopic)
                                    { %>
                                <li>
                                    <a href="#" id="topic<%= topic.OnlineId %>" data-id="<%= topic.OnlineId %>" class="topic-item">
                                        <%= topic.TitleName.Trim() %>
                                    </a>
                                    <a href="#" class="topic-edit " data-toggle="modal" data-target="#myModal" style="font-size: 20px; margin-left: 6px; color: #5cb85c;" data-id="<%= topic.OnlineId %>">
                                       <%-- <i class="glyphicon glyphicon-pencil" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>"></i>--%>
                                        <span class="material-icons" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>" data-toggle="tooltip"  data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">edit</span>
                                    </a>
                             
                                </li>
                                <% } %>
                            </ul>

                            <% } %>
                        </div>
                        <div class="col-lg-9 col-md-8 col-xs-8">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-none">
                                        <asp:HiddenField ID="hfDelWork" runat="server" />
                                        <asp:HiddenField ID="hfDelLearn" runat="server" />
                                        <asp:Button ID="ButtonDelWork" runat="server" Text="" OnClick="ButtonDelWork_Click" />
                                        <asp:Button ID="ButtonDelLearn" runat="server" Text="" OnClick="ButtonDelLearn_Click" />
                                    </div>

                        <% foreach (var group in _lstWork.GroupBy(o => new { o.OnlineId , o.Group }))
                            { %>
                        <div id="group<%= group.Key.OnlineId %>" class="div-group">
                            <ul class="breadcrumb" style="">
                                <li class="bread-item">
                                    <h3 class="group-level"><%= group.Key.Group %></h3>
                                </li>
                                <li class="bread-item active"><%= _level.SubLevel %></li>
                                <li class="bread-item active"><%= _plan.sPlaneName%></li>
                                <li class="bread-item active"><%= _plan.courseCode %></li>
                            </ul>

                                        <table class="table-hover dataTable" style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;">
                                            <thead>
                                                <tr class="headerCell">
                                                    <th class="center" style="width: 8%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                    <th class="center" style="width: 20%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M403005") %></th>
                                                    <th class="center" style="width: 15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th>
                                                    <th class="center" style="width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206347") %></th>
                                                    <th class="center" style="width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206348") %></th>
                                                    <th class="center" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206349") %></th>
                                                    <th class="center" style="width: 12%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206350") %></th>
                                                    <th class="center" style="width: 10%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>


                                                <% int index = 1; %>
                                                <% foreach (var work in group.ToList())
                                                    { %>
                                                <tr class="itemCell">
                                                    <td class="center"><%= index++ %></td>
                                                    <td class="center"><%= work.Title%></td>
                                                    <td class="center">
                                                        <% if (work.Type == 1)
                                                            { %>
                                         <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206377") %>
                                        <% }
                                            else
                                            {  %>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206352") %>
                                        <% } %>
                                                    </td>
                                                    <td class="center"><%= work.Recieve %></td>
                                                    <td class="center"><%= work.Send %></td>
                                                    <td class="center"><%= work.SendDate.HasValue 
                                                           ? work.SendDate.Value.ToString("dd MMM yyyy" , new System.Globalization.CultureInfo("th-TH"))
                                                           : "-"%></td>
                                                    <td class="center"><%= work.DisplayDate.HasValue 
                                                           ? work.DisplayDate.Value.ToString("dd MMM yyyy HH:mm <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131027") %>" , new System.Globalization.CultureInfo("th-TH"))
                                                           : "-"%></td>
                                                    <td class="center">
                                                        <% if (work.Type == 1)
                                                            { %>


                                                        <div class="dropdown">
                                                            <button class="btn btn-success dropdown-toggle" id="dropdownMenu<%= work.Id %>" type="button" data-toggle="dropdown"  data-flip="false" aria-haspopup="true" aria-expanded="false"  data-offsetx="10,20">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404028") %>&nbsp;<span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu dropdown-menu-rightx" data-boundary="dropdownMenu<%= work.Id %>" x-placementx="top-left" style=""  data-flipx="false"  data-offsetx="10,20">
                                                                <li><a href="OnlineWorkEdit.aspx?id=<%= work.Id %>" class="" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106168") %>
                                                                </a></li>
                                                                <li><a href="OnlineReport.aspx?id=<%= work.Id %>" class="" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405041") %>
                                                                </a></li>
                                                                <li><a href="<%=   ConfigurationManager.AppSettings["ClassOnlineStudentLink"]+ "" %>/Preview/Work?id=<%= work.Id %>&sid=<%= work.SchooldID %>" class="" style="color: black;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107028") %>
                                                                </a></li>
                                                                <% if (work.Send == "0") { %>
                                                                    <% if (true)/*(Permission == JabjaiMainClass.PermissionType.Modify)*/
                                                                        { %>
                                                                            <li><a href="#" class="" onclick="return myConfirm1(this);" data-id="<%= work.Id %>" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01827") %>
                                                                            </a></li>
                                                                        <% } else { %>
                                                                            <li><a href="#" class="" onclick="NoPermissionPopup()" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01827") %>
                                                                            </a></li>
                                                                    <% } %>
                                                                <% } %>
                                                            </ul>
                                                        </div>
                                                        <% }
                                                            else if (work.Type == 2)
                                                            { %>
                                                        <div class="dropdown">
                                                            <button class="btn btn-success dropdown-toggle" id="dropdownMenu<%= work.Id %>" type="button" data-toggle="dropdown" data-flip="false" aria-haspopup="true" aria-expanded="false"  data-offsetx="10,20">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404028") %>&nbsp;<span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu dropdown-menu-rightx" data-boundary="dropdownMenu<%= work.Id %>"  x-placementx="top-left" data-flipx="false"  data-offsetx="10,20">
                                                                <li><a href="OnlineLearnEdit.aspx?id=<%= work.Id %>" class="" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106168") %>
                                                                </a></li>
                                                                <li><a href="<%=   ConfigurationManager.AppSettings["ClassOnlineStudentLink"]+ "" %>/Preview/Learning?id=<%= work.Id %>&sid=<%= work.SchooldID %>" class="" style="color: black;" target="_blank"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107028") %>
                                                                </a></li>

                                                                <% if (true)/* (Permission == JabjaiMainClass.PermissionType.Modify)*/ { %>
                                                                   <li><a href="#" class="" onclick="return myConfirm2(this);" data-id="<%= work.Id %>" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01827") %>
                                                                </a></li>
                                                                <% } else { %>
                                                                    <li><a href="#" class="" onclick="NoPermissionPopup()" style="color: black;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01827") %>
                                                                </a></li>
                                                                 <% } %>
                                                            </ul>
                                                        </div>
                                                        <% } %>
                                                    </td>
                                                </tr>

                                                <% } %>
                                            </tbody>
                                        </table>
                                    </div>
                                    <% } %>
                                </div>
                            </div>
                            <%--<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206345") %></h2>--%>
                            <%--  <ul class="breadcrumb" style="background-color: #ffffff;">
                        <li class="bread-item"><%= _plan.courseCode %></li>
                        <li class="bread-item active"><%= _plan.sPlaneName %></li>
                    </ul>--%>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-center">

                                <a class="btn btn-default search-btn" href="OnlineMain.aspx"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>

                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnTest" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <div id="dropstylehere1">
                                </div>
                                <div class="d-none">
                                    <asp:TextBox ID="txtSelectedRoom" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtOnlineID" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnTest" runat="server" Text="Button" OnClick="btnTest_Click" />
                                </div>
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132066") %></h3>
                                    </div>
                                    <div class="modal-body">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02259") %></h4>
                                                    <asp:TextBox runat="server" ID="txtTitleGroup" CssClass="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02259") %>"  />
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group chosen-wrap-here">
                                                    <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></h4>
                                                    <asp:ListBox ID="ddlLevel" ClientIDMode="Static" runat="server" SelectionMode="Multiple"
                                                        DataTextField="Text"
                                                               data-style="select-with-transition" data-live-search="true"
                                                        DataValueField="Value" CssClass="chosen-selectx -custom-fixx selectpicker1 -w100 "></asp:ListBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group chosen-wrap-here">
                                                    <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206353") %></h4>
                                                    <asp:ListBox ID="ddlShare" ClientIDMode="Static" runat="server" SelectionMode="Multiple"
                                                        DataTextField="Text"
                                                               data-style="select-with-transition" data-live-search="true"
                                                        DataValueField="Value" CssClass="chosen-selectx -custom-fixx selectpicker1 -w100 "></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" runat="server" ID="btnDel" CssClass="btn btn-danger search-btn" OnClick="btnDel_Click" />
                                        <asp:Button Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" runat="server" ID="btnSaveTitle" CssClass="btn btn-success search-btn" OnClick="btnSaveTitle_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div id="myModal2" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h3 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <div class="text-center">
                                            <img src="../images/exclamation-256.jpg" style="width: 200px; height: 200px;">
                                            <br />
                                        </div>
                                        <h1 class="text-center">
                                            <br />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132067") %> </h1>
                                    </div>

                                </div>
                            </div>

                        </div>

                    </div>
                </div>

            </form>

        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <!-- Modal -->

<%--    <script src="js/chosen.jquery.js?v=<%= DateTime.Now.ToString("ddMMyyyy")%>"></script>--%>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@9"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.1/jquery.validate.min.js" type="text/javascript"></script>
    <script src="js/func.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>

    <script>    

        Sys.Application.add_init(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_initializeRequest(function (sender, args) {

                //alert('add_initializeRequest');

                if ($("#aspnetForm").valid() == false) {
                    args.set_cancel(true);
                }
            });

            prm.add_pageLoaded(function (sender, args) {
                //alert('pageLoad');
                if (!isMobileAndTabletCheck())
                    //$('.chosen-select').chosen();
                    $('.selectpicker1').selectpicker();
                else {
                    //$(".chosen-select").removeClass('-custom-fix');
                }


                //if ($('#ddlShare option').length == 0) {
                //}

                //$("#ddlShare_chosen li.active-result").on(function () {
                //    return false;
                //});

                var $ddlShare = $('#ddlShare');
                if ($ddlShare.data('isempty') == "1") {
                    $("#ddlShare_chosen ul.chosen-results").addClass('empty-chosen');
                }

                if (sender._postBackSettings != null && sender._postBackSettings.panelsToUpdate == '<%= UpdatePanel1.UniqueID %>') {
                    var s = $('#<%= txtSelectedRoom.ClientID%>').val();
                    //$('#ddlLevel').chosen({ no_results_text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206354") %>" }); 

                    if (s != "") {
                        var arr = s.split('|');
                        if (arr.length > 0) {
                            // var $s = $('#ddlLevel').chosen();
                           // $('#<%= ddlLevel.ClientID%>').removeAttr('required');
                            $('#<%=btnDel.ClientID %>').hide();
                            var _styletag = "<style>";
                            for (var i = 0; i < arr.length; i++) {
                                var $item = $('#ddlLevel option[value=' + arr[i] + ']');
                                var r = $item.text();

                                var isFound = $('#ddlLevel_chosen  ul.chosen-choices .search-choice span:contains(' + r + ')').length > 0;

                                if (isFound) {
                                    _styletag += "#ddlLevel_chosen .result-selected[data-value='" + arr[i] + "'] {display:none !important;}"
                                    $item.attr("disabled", true);
                                    $('#ddlLevel_chosen ul.chosen-choices .search-choice span:contains(' + r + ')').parent().find('a').remove();

                                    //$('#ddlLevel_chosen ul.chosen-choices .search-choice span:contains(' + r + ')').parent().parent().parent()
                                    //    .find('.chosen-drop')
                                    //    .find('.result-selected:contains(' + r + ')').parent().remove();
                                }
                            }

                            _styletag += "</style>";
                            $('#dropstylehere1').append(_styletag);
                            //$('#ddlLevel').chosen();
                            //$s.trigger("chosen:updated");//.trigger("chosen:updated");
                            //$('#ddlLevel_chosen .chosen-results').remove('.result-selected');
                            //$('#ddlLevel_chosen .chosen-results .result-selected').each(function () {
                            //    $(this).remove();
                            //});
                        }
                        else {
                            $('#<%=btnDel.ClientID %>').show();
                        }
                    }
                    else {
                        $('#<%=btnDel.ClientID %>').show();
                    }

                    $('#myModal').modal('show');
                }
            });

        });



        //var prm = Sys.WebForms.PageRequestManager.getInstance();


        //prm.add_endRequest(function (sender, args) {

        //    alert('add_endRequest');

        //    if ($("#aspnetForm").valid() == false) {
        //        args.set_cancel(true);
        //    }
        //});

        //prm.add_beginRequest(function (sender, args) {
        //    // Put your code here
        //    //alert('add_beginRequest');
        //});

        //prm.add_pageLoaded(function (sender, args) {

        //});

        //function endRequestHandler(sender, args) {
        //    alert('endRequest');

        //    if ($("#aspnetForm").valid() == false) {
        //        args.set_cancel(true);
        //    }
        //}

      <%--  function pageLoad(sender, args) {
            alert('pageLoad');
            $('.chosen-select').chosen();

            if (sender._postBackSettings != null && sender._postBackSettings.panelsToUpdate == '<%= UpdatePanel1.UniqueID %>') {
                var s = $('#<%= txtSelectedRoom.ClientID%>').val();

                if (s != "") {
                    var arr = s.split('|');
                    if (arr.length > 0) {
                        $('#<%=btnDel.ClientID %>').hide();
                        for (var i = 0; i < arr.length; i++) {
                            var r = $('#<%= ddlLevel.ClientID%> option[value=' + i + ']').text();

                            var isFound = $('ul.chosen-choices .search-choice span:contains(' + r + ')').length > 0;

                            if (isFound) {
                                $('ul.chosen-choices .search-choice span:contains(' + r + ')').parent().find('a').remove();
                            }
                        }
                    }
                    else {
                        $('#<%=btnDel.ClientID %>').show();
                    }
                }
                else {
                    $('#<%=btnDel.ClientID %>').show();
                }

                $('#myModal').modal('show');
            }

        }--%>
        //function pageLoad() {
        //    alert('pageLoad');
        //    //fix in update panel

        //    //$('#aspnetForm').on('submit', function (e) {
        //    //   // alert("Valid: " + $('#aspnetForm').valid());

        //    //    if ($('#aspnetForm').valid() == false) {

        //    //        e.preventDefault();
        //    //        e.stopPropagation();
        //    //        return false;
        //    //    }
        //    //});



        //    //$('#myModal').modal('show');

        //}

        function myConfirm1(t) {
            let $t = $(t);

            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>',
                //text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                //cancelButtonColor: '#d33',
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
            }).then((result) => {
                if (result.value) {
                    $("#<%= hfDelWork.ClientID %>").val($t.data('id'));
                    $("#<%= ButtonDelWork.ClientID %>").trigger('click');
                    return true;
                }
            });
           <%-- var r = confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %> ?");
            if (r == true) {
                $("#<%= hfDelWork.ClientID %>").val($t.data('id'));
                $("#<%= ButtonDelWork.ClientID %>").trigger('click');
                //return true;
            }--%>

            return false;
        }

        function myConfirm2(t) {
            let $t = $(t);
            <%--var r = confirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %> ?");
            if (r == true) {
                $("#<%= hfDelLearn.ClientID %>").val($t.data('id'));
                $("#<%= ButtonDelLearn.ClientID %>").trigger('click');
                //return true;
            }--%>

            Swal.fire({
                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102158") %>',
                //text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                //cancelButtonColor: '#d33',
                confirmButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>',
                cancelButtonText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
            }).then((result) => {
                if (result.value) {
                    $("#<%= hfDelLearn.ClientID %>").val($t.data('id'));
                    $("#<%= ButtonDelLearn.ClientID %>").trigger('click');
                    return true;
                }
            });

            return false;
        }

        $(function () {


            $('.topic-top').on('click', function (e) {
                $('.topic-item').removeClass('active');
                $('.div-group').show();
            });

            $('.topic-item').on('click', function (e) {
                let _id = $(this).data('id');
                $('.div-group').hide();
                $('#group' + _id).show();
                $('.topic-item').removeClass('active');
                $(this).addClass('active');
            });

            $('.topic-edit').on('click', function (e) {
                let $t = $(this);
                let id = $t.data('id');

                $('#<%= txtOnlineID.ClientID %>').val(id);
                $('#<%= ddlLevel.ClientID %>').find("option").attr("selected", false);
                $('#<%= ddlLevel.ClientID %>').trigger("chosen:updated");

                $('#<%= btnTest.ClientID %>').trigger('click');
                $('#<%= btnDel.ClientID %>').show();

                return false;//disable default modal

                // __doPostBack("<%=UpdatePanel1.UniqueID %>", "");

              <%--  $('#<%= ddlLevel.ClientID%>').attr('required', 'true');
                $('#<%= txtTitleGroup.ClientID%>').attr('required', 'true');//.removeAttr('required');--%>
            });

            $('.topic-new').on('click', function (e) {

                $('#<%= txtSelectedRoom.ClientID%>').val('');
                $('#<%= txtTitleGroup.ClientID %>').val('');
                $('#<%= txtOnlineID.ClientID %>').val('');
                $('#<%= ddlLevel.ClientID %>').find("option").attr("selected", false);
                $('#<%= ddlLevel.ClientID %>').trigger("chosen:updated");

                $('#<%= btnDel.ClientID %>').hide();
            });

            $('.topic-rem').on('click', function (e) {

            });

            $('#myModal').on('hidden.bs.modal', function () {
                $('#<%= ddlLevel.ClientID%>').removeAttr('required');
                $('#<%= txtTitleGroup.ClientID%>').removeAttr('required');
            });

            $('#myModal').on('shown.bs.modal', function () {

               // $('#<%= ddlLevel.ClientID%>').attr('required', 'true');
                $('#<%= txtTitleGroup.ClientID%>').attr('required', 'true');//.removeAttr('required');           

                var s = $('#<%= txtSelectedRoom.ClientID%>').val();
                //$('#ddlLevel').chosen({ no_results_text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206354") %>" }); 

                if (s == "") {
                    $('#<%= ddlLevel.ClientID%>').attr('required', 'true');
                }
                else {
                    $('#<%= ddlLevel.ClientID%>').removeAttr('required');
                }
            });



            //$('[data-toggle=modal]').on('click', function (e) {
            //    var $target = $($(this).data('target'));
            //    $target.data('triggered', true);
            //    setTimeout(function () {
            //        if ($target.data('triggered')) {
            //            $target.modal('show')
            //                .data('triggered', false); // prevents multiple clicks from reopening
            //        };
            //    }, 1000); // milliseconds
            //    return false;
            //});
        });
    </script>
</asp:Content>
