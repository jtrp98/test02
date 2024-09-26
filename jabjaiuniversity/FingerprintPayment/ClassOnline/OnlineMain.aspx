<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="OnlineMain.aspx.cs"
    Inherits="FingerprintPayment.ClassOnline.OnlineMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <%--    <link href="/bootstrap/bootstrap-chosen/bootstrap-chosen.css" rel="stylesheet" />--%>
    <%--    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />--%>
    <link href="css/StyleSheet1.css?v=1" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <style>
        .group-level {
            color: #fff;
            background-color: #f0ad4e;
            border-color: #eea236;
            display: inline-block;
            padding: 6px 12px;
        }

        .-w100 {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206344") %>         
            </p>
        </div>
    </div>

    <form id="from1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
        <%--  <iframe width="560" height="315"  src="https://www.youtube.com/embed/7TF00hJI78Y" frameborder="0" allowfullscreen></iframe>--%>

        <% if (ListPlan.Count == 0)
            { %>
        <% return;
            } %>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlTerm" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
            <ContentTemplate>

                <div class="row">
                    <div class="col-md-12">

                        <div class="card ">
                            <div class="card-header card-header-info card-header-icon">
                                <div class="card-icon">
                                    <i class="material-icons">search</i>
                                </div>
                                <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                            </div>
                            <div class="card-body ">

                                <div class="row  ml-md-5">

                                    <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlYear" runat="server"
                                            DataTextField="Text"
                                            DataValueField="Value" CssClass="chosen-selectx selectpicker1 -x" data-width="100%" data-style="select-with-transition" data-live-search="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-1 col-form-label text-left "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlTerm" runat="server"
                                            DataTextField="Text"
                                            DataValueField="Value" CssClass="chosen-selectx selectpicker1 -x"  data-width="100%" data-style="select-with-transition" data-live-search="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1"></div>
                                    <label class="col-md-1 col-form-label text-left "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlLevel" runat="server"
                                            DataTextField="Text"
                                            DataValueField="Value" CssClass="chosen-selectx selectpicker1 -x"  data-width="100%" data-style="select-with-transition" data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>

                                <div class="row ">
                                    <div class="col-md-12">
                                        <div class="form-group text-center">
                                            <asp:LinkButton Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" runat="server" ID="btnSearch" CssClass="btn btn-info search-btn" OnClick="btnSearch_Click">
                                                 <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="card ">

                            <div class="card-header card-header-warning card-header-icon">
                                <div class="card-icon">
                                    <i class="material-icons">list</i>
                                </div>
                                <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                            </div>
                            <div class="card-body ">
                                <div class="row mini--space__top">
                                    <div class="col-md-12">
                                        <div class="wrapper-tablex">
                                            <% foreach (var level in _listDataPlay.OrderBy(o => o.nTSubLevel).GroupBy(o => new { o.SubLevel }))
                                                { %>

                                            <h4 class="group-level"><%= level.Key.SubLevel %></h4>
                                            <table class="table-hover dataTable" style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;">
                                                <thead>
                                                    <tr class="headerCell">
                                                        <th class="text-center" style="width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                                                        <th class="text-center" style="width: 35%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></th>
                                                        <th class="text-center" style="width: 35%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></th>
                                                        <th class="text-center"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <% int index = 1; %>
                                                    <% foreach (var room in level.ToList().GroupBy(i => new { i.nTerm, i.sPlaneID, i.nTSubLevel }))
                                                        { %>
                                                    <tr class="itemCell">
                                                        <td class="text-center"><%= index++ %></td>
                                                        <td class="text-center"><%= room.Max( i => i.courseCode) %></td>
                                                        <td class="center"><%= room.Max( i=> i.sPlaneName )%></td>
                                                        <td class="text-center">
                                                            <a href="OnlineManage.aspx?term=<%= room.Key.nTerm.Trim() %>&plan=<%= room.Key.sPlaneID %>&level=<%= room.Key.nTSubLevel %>" class="btn  btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404028") %></a>
                                                        </td>
                                                    </tr>
                                                    <% } %>
                                                </tbody>
                                            </table>
                                            <% } %>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="myModal2" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="text-center">
                                    <img src="../images/exclamation-256.jpg" style="width: 200px; height: 200px;">
                                    <br />
                                </div>
                                <h2 class="text-center">
                                    <br />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132065") %>
                                <br />
                                </h2>
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">


    <%--  <script src="js/chosen.jquery.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>--%>
    <%--  <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>--%>
    <script src="js/func.js?v=<%= DateTime.Now.ToString("ddMMyyyyHHmm")%>"></script>

    <script>
        <% if (ListPlan.Count == 0)
        { %>
        $("#myModal2").modal("show");
        <% } %>

    </script>

    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(function (sender, args) {
            if (!isMobileAndTabletCheck()) {
                $('.selectpicker1').selectpicker();
            }
            //$('.chosen-select').chosen();
        });

        prm.add_initializeRequest(function (sender, args) {
             $("body").mLoading('show');
        });


        prm.add_endRequest(function (sender, args) {
            //Do all what you want to do in jQuery ready function
             $("body").mLoading('hide');
        });



        //$(function () {
        //    $('.chosen-select').chosen();
        //})
    </script>
</asp:Content>


