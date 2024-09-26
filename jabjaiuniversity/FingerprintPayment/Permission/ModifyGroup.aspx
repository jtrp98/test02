<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="ModifyGroup.aspx.cs" Inherits="FingerprintPayment.Permission.ModifyGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="asset/css.css" rel="stylesheet" />
    <link href="../Content/Material/assets/css/toggle.css" rel="stylesheet" />
    <style>
        #menu, #user {
            padding-top: 15px;
        }

        #tblSelectedUser .checklist {
            visibility: hidden;
        }

        .expand-collape {
            cursor: pointer;
        }

        .table .level2 td, .table .level3 td {
            display: none;
            /* transition: all .5s ease-in-out;*/
        }

        .table .level2 .menu-name {
            padding-left: 30px;
        }

        .table .level3 .menu-name {
            padding-left: 60px;
        }

        .table tr, .table td {
            /*  transition: all 0.1s ease;*/
        }

            .table td span, .table th span {
                display: block;
            }

        #menu .table thead th {
            background: #4FC6f6b3;
            color: black;
            border-color: #FFF !important;
            border-bottom: 10px solid #fff !important;
        }

        #menu .table tbody td {
            color: #2b2b2b;
            /*  background: #f9f3f3;*/
            border-width: 0px;
        }

        #menu .table tbody .level1 td {
            background: #8fDDFDb3;
            border-width: 5px !important;
            border-color: #FFF !important;
        }

        #menu .table tbody .level2 td {
            background: #C0EAFBb3;
        }

        #menu .table tbody .level3 td {
            background: #E5f7feb3;
        }

        #permission-list .nav {
            padding: 0;
        }

            #permission-list .nav .nav-item {
                margin: 0px;
            }

            #permission-list .nav .nav-link {
                background-color: #fff;
                color: black;
                font-size: 18px;
                border-radius: 0;
                border-top: 1px solid #07b1c6;
                border-bottom: 1px solid #07b1c6;
            }

                #permission-list .nav .nav-link.active {
                    background-color: #07b1c6;
                    color: #fff;
                }

        #user .col-left {
            background: #f6f6f6;
        }

        #user #tblAllUser td {
            border-color: transparent !important;
        }

        .advancepermision{
            pointer-events: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">groups</i>
                        </div>
                        <span style="font-size: 18px;" class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803023") %></span>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <span class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803024") %></span>
                                    <div class="col-md-8">
                                        <div class="form-group ">
                                            <asp:Label ID="lblGroupName" runat="server" CssClass="form-control"></asp:Label>
                                            <asp:TextBox ID="txtGroupName" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div id="permission-list" class="row">
                            <div class="col-md-12">
                                <ul class="nav nav-pills nav-fill">
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#menu" data-toggle="tab" role="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132759") %>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#user" data-toggle="tab" role="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132760") %>
                                        </a>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="menu">
                                        <div class="row justify-content-center">
                                            <div class="col-md-12">
                                                <table class="table website">
                                                    <thead class=" text-primary">
                                                        <tr>
                                                            <th style="width: 50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803027") %>
                                                            </th>
                                                            <th style="width: 15%" class="text-center">
                                                                <span class="text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803044") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="0" data-id="0" <%= ListMenu.Where(o => o.Type == "W").All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>
                                                            <%-- <th style="width: 15%" class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131039") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="0" data-id="0" <%= ListMenu.Where(o => o.Type == "W").All( o => o.Role == 1) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>--%>
                                                            <th style="width: 15%" class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="3" data-lvl="0" data-id="0" <%= ListMenu.Where(o => o.Type == "W").All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>
                                                            <th style="width: 5%" class="text-center"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% foreach (var lvl1 in ListMenu.Where(o => o.Type == "W").GroupBy(o => new { o.GroupID, o.GroupMenu }))
                                                            { %>

                                                        <tr class="level1 parent__0 id__<%=lvl1.Key.GroupID %>">
                                                            <td class="menu-name"><%=lvl1.Key.GroupMenu %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803044") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>" data-groupid="0" <%= lvl1.All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <%-- <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131039") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>" data-groupid="0" <%= lvl1.All( o => o.Role == 1) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>--%>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="3" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>" data-groupid="0" <%= lvl1.All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="expand-collape material-icons" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>">expand_more</span>
                                                            </td>
                                                        </tr>

                                                        <% foreach (var lvl2 in lvl1.GroupBy(o => new { o.SegmentID, o.SegmentName }))
                                                            { %>

                                                        <tr class="level2 parent__<%=lvl1.Key.GroupID %> id__<%=lvl2.Key.SegmentID %>">
                                                            <td class="menu-name"><%=lvl2.Key.SegmentName %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803044") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="2" data-id="<%=lvl2.Key.SegmentID %>" data-groupid="<%=lvl1.Key.GroupID %>" <%= lvl2.All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <%--<td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131039") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="2" data-id="<%=lvl2.Key.SegmentID %>" data-groupid="<%=lvl1.Key.GroupID %>" <%= lvl2.All( o => o.Role == 1) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>--%>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %>
                                                                </span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="3" data-lvl="2" data-id="<%=lvl2.Key.SegmentID %>" data-groupid="<%=lvl1.Key.GroupID %>" <%= lvl2.All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="expand-collape material-icons" data-lvl="2" data-id="<%=lvl2.Key.SegmentID %>">expand_more</span>
                                                            </td>
                                                        </tr>

                                                        <% foreach (var lvl3 in lvl2.GroupBy(o => new { o.MenuId, o.MenuName }))
                                                            { %>

                                                        <tr class="level3 parent__<%=lvl2.Key.SegmentID %> id__<%=lvl3.Key.MenuId %>">
                                                            <td class="menu-name"><%=lvl3.Key.MenuName %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803044") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="3" data-groupid="<%=lvl2.Key.SegmentID %>" data-id="<%=lvl3.Key.MenuId %>" <%= lvl3.All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <%-- <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131039") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="3" data-groupid="<%=lvl2.Key.SegmentID %>" data-id="<%=lvl3.Key.MenuId %>" <%= lvl3.All( o => o.Role == 1) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>--%>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="3" data-lvl="3" data-groupid="<%=lvl2.Key.SegmentID %>" data-id="<%=lvl3.Key.MenuId %>" <%= lvl3.All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center"></td>
                                                        </tr>

                                                        <% } %>

                                                        <% } %>

                                                        <% } %>



                                                        <% foreach (var lvlgl1 in ListMenuGroupless.Where(o => o.Type == "W").GroupBy(o => new { o.MenuId, o.MenuName }))
                                                            { %>

                                                        <tr class="level1 parent__0 id__<%=lvlgl1.Key.MenuId %>">
                                                            <td class="menu-name"><%=lvlgl1.Key.MenuName %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803044") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="3" data-id="<%=lvlgl1.Key.MenuId %>" data-groupid="0" <%= lvlgl1.All(o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>

                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="3" data-lvl="3" data-id="<%=lvlgl1.Key.MenuId %>" data-groupid="0" <%= lvlgl1.All(o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <%--  <span class="expand-collape material-icons" data-lvl="3" data-id="<%=lvlgl1.Key.MenuId %>">expand_more</span>--%>
                                                            </td>
                                                        </tr>
                                                        <% } %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="row justify-content-center">
                                            <div class="col-md-12">
                                                <table class="table mobileapp">
                                                    <thead class=" text-primary">
                                                        <tr>
                                                            <th style="width: 50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803034") %>
                                                            </th>
                                                            <th style="width: 15%" class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803042") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="0" data-id="0" <%= ListMenu.Where(o => o.Type == "M").All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>
                                                            <th style="width: 15%" class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="0" data-id="0" <%= ListMenu.Where(o => o.Type == "M").All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>
                                                            <th style="width: 5%" class="text-center"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% foreach (var lvl1 in ListMenu.Where(o => o.Type == "M").OrderBy(o => o.OrderNo1).GroupBy(o => new { o.GroupID, o.GroupMenu }))
                                                            { %>

                                                        <tr class="level1 parent__0 id__<%=lvl1.Key.GroupID %>">
                                                            <td class="menu-name"><%=lvl1.Key.GroupMenu %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803042") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>" data-groupid="0" <%= lvl1.All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>" data-groupid="0" <%= lvl1.All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <span class="expand-collape material-icons" data-lvl="1" data-id="<%=lvl1.Key.GroupID %>">expand_more</span>
                                                            </td>
                                                        </tr>

                                                        <% foreach (var lvl2 in lvl1.OrderBy(o => o.OrderNo2).GroupBy(o => new { o.MenuId, o.MenuName }))
                                                            { %>

                                                        <tr class="level2 parent__<%=lvl1.Key.GroupID %> id__<%=lvl2.Key.MenuId %>">
                                                            <td class="menu-name"><%=lvl2.Key.MenuName %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803042") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="2" data-groupid="<%=lvl1.Key.GroupID %>" data-id="<%=lvl2.Key.MenuId %>" <%= lvl2.All( o => o.Role == 0) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="2" data-id="<%=lvl2.Key.MenuId %>" data-groupid="<%=lvl1.Key.GroupID %>" <%= lvl2.All( o => o.Role == 2) ? "checked" : "" %> hidden>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center"></td>
                                                        </tr>

                                                        <%--  <% foreach (var lvl3 in lvl2.GroupBy(o => new { o.MenuId, o.MenuName }))
                                                            { %>

                                                        <tr class="level3 parent__<%=lvl2.Key.SegmentID %> id__<%=lvl3.Key.MenuId %>">
                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=lvl3.Key.MenuName %>
                                                            </td>
                                                            <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803044") %>
                                                            </td>
                                                            <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131039") %>
                                                            </td>
                                                            <td class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %>
                                                            </td>
                                                            <td class="text-center"></td>
                                                        </tr>--%>

                                                        <%-- <% } %>--%>

                                                        <% } %>

                                                        <% } %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="row justify-content-center">
                                            <div class="col-md-12">
                                                <table class="table advancepermision">
                                                    <thead class=" text-primary">
                                                        <tr>
                                                            <th style="width: 50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803040") %>
                                                            </th>
                                                            <th style="width: 15%" class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803042") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="0" data-id="0" hidden="" <%= IsAdmin ? "checked" : "" %>>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>
                                                            <th style="width: 15%" class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="0" data-id="0" hidden="" <%= !IsAdmin ? "checked" : "" %>>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </th>
                                                            <th style="width: 5%" class="text-center"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>


                                                        <tr class="level1 parent__0 id__1">
                                                            <td class="menu-name"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803041") %>
                                                            </td>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803042") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="1" data-lvl="1" data-id="1" data-groupid="0" hidden=""  <%= IsAdmin ? "checked" : "" %>>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803043") %></span>
                                                                <label class="el-switch el-switch">
                                                                    <input type="checkbox" class="switch-button" data-col="2" data-lvl="1" data-id="1" data-groupid="0" hidden="" <%= !IsAdmin ? "checked" : "" %>>
                                                                    <span class="el-switch-style"></span>
                                                                </label>
                                                            </td>
                                                            <td class="text-center">
                                                              <%--  <span class="expand-collape material-icons" data-lvl="1" data-id="1">expand_more</span>--%>
                                                            </td>
                                                        </tr>
                                                        
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane " id="user">
                                        <div class="row justify-content-center" style="padding-left: 15px; padding-right: 15px;">
                                            <div class="col-md-6 text-center col-left" style="padding-right: 0; border-bottom: 1px solid #777; height: 35px; padding-top: 5px;">
                                                <span style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803045") %>
                                                </span>
                                            </div>
                                            <div class="col-md-6 text-center" style="padding-left: 0; border-bottom: 1px solid #777; height: 35px; padding-top: 5px;">
                                                <span style="font-size: 18px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803046") %>
                                                </span>
                                            </div>
                                        </div>
                                        <%--  <div class="row justify-content-center">
                                            <div class="col-md-6 ">
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>--%>
                                        <div class="row justify-content-center" style="padding-left: 15px; padding-right: 15px;">
                                            <div class="col-md-6 col-left" style="padding-right: 0;">
                                                <table id="tblAllUser" class="table ">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 10%"></th>
                                                            <th style="width: 30%"></th>
                                                            <th style="width: 60%" colspan="2">
                                                                <input type="text" id="searchUser" class="form-control" value="" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803045") %>" /></th>
                                                        </tr>
                                                        <tr>
                                                            <th style="width: 10%">
                                                                <input type="checkbox" onclick="checkUserAll(this.checked)" /></th>
                                                            <th style="width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %></th>
                                                            <th style="width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                                            <th style="width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% foreach (var u in ListAllUser)
                                                            { %>
                                                        <tr class="row__<%=u.UserID %>">
                                                            <td class="checklist">
                                                                <%if (u.IsSelectable)
                                                                    { %>
                                                                <input type="checkbox" class="checkUser" value="<%=u.UserID %>" />
                                                                <% } %>
                                                            </td>
                                                            <td><%= u.Code %></td>
                                                            <td><%= u.FullName %></td>
                                                            <td><%= u.Remark %></td>
                                                        </tr>
                                                        <% } %>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="col-md-6" style="padding-left: 0;">
                                                <table id="tblSelectedUser" class="table">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 10%"></th>
                                                            <th style="width: 40%"></th>
                                                            <th style="width: 50%">
                                                                <input type="text" class="form-control" style="visibility: hidden;" />
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <th class="checklist" style="width: 10%"></th>
                                                            <th style="width: 40%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %></th>
                                                            <th style="width: 50%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% foreach (var u in ListSelectedUser)
                                                            { %>
                                                        <tr class="row__<%=u.UserID %>">
                                                            <td class="checklist">
                                                                <input type="checkbox" class="checkUser" value="<%=u.UserID %>" />
                                                            </td>
                                                            <td><%= u.Code %></td>
                                                            <td><%= u.FullName %></td>
                                                        </tr>
                                                        <% } %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <button id="btnSave" class="btn btn-success" type="button" onclick="onSaveData()">
                                    <span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                </button>

                                <button class="btn btn-danger" type="button" onclick="onCancle()">
                                    <span class="material-icons">close</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="asset/js.js"></script>

    <script>   
        function checkUserAll(t) {
            //check all
            //$('#tblAllUser .checkUser').prop('checked', t);

            if (t) {
                $("#tblAllUser .checkUser:not(:checked):visible").each(function (i) {
                    $(this).trigger('click');
                });
            }
            else {
                $("#tblAllUser .checkUser:checked:visible").each(function (i) {
                    $(this).trigger('click');
                });
                //$("#tblSelectedUser tbody").html('')
            }
            //append to selected
            //appendCheckedUser($('#tblAllUser .checkUser:checked'))
        }
        //var searchIDs = $("#find-table input:checkbox:checked").map(function () {
        //    return $(this).val();
        //}).get(); // <----

        function onCancle() {
            window.location = "Main.aspx";
        }

        function onSaveData() {

            var w1 = $('.website .switch-button[data-lvl=3][data-col=1]:checked').map(function () {
                return { MenuId: $(this).data('id'), Type: 'W', Role: '0' }
            }).get();

            var w2 = $('.website .switch-button[data-lvl=3][data-col=2]:checked').map(function () {
                return { MenuId: $(this).data('id'), Type: 'W', Role: '1' }
            }).get();

            var w3 = $('.website .switch-button[data-lvl=3][data-col=3]:checked').map(function () {
                return { MenuId: $(this).data('id'), Type: 'W', Role: '2' }
            }).get();

            var m1 = $('.mobileapp .switch-button[data-lvl=2][data-col=1]:checked').map(function () {
                return { MenuId: $(this).data('id'), Type: 'M', Role: '0' }
            }).get();

            var m2 = $('.mobileapp .switch-button[data-lvl=2][data-col=2]:checked').map(function () {
                return { MenuId: $(this).data('id'), Type: 'M', Role: '2' }
            }).get();

            var menu = w1.concat(w2).concat(w3).concat(m1).concat(m2);

            var user = $('#tblSelectedUser .checkUser').map(function () {
                return $(this).val();
            }).get();

            //console.log(menu);
            //console.log(user);
            var obj = {
                GroupID: '<%= GroupID %>',
                GroupName: $('#txtGroupName').val() + '',
                SelectedMenu: menu,
                SelectedUser: user,
            };

            $('#btnSave').html('<span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>...').prop('disabled', true);

            PageMethods.AddOrModifyGroup(obj
                , function (response) {
                    $('#btnSave').html('<span class="material-icons">save</span>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>').prop('disabled', false);
                    if (response.status) {
                        Swal.fire({
                            icon: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                            showConfirmButton: false,
                            timer: 2000,
                            willClose: () => {
                                window.location = "Main.aspx";
                            }
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                            text: response.msg,
                        })
                    }
                },
                function (response) {
                    Swal.fire({
                        icon: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                        text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132756") %>',
                    })
                }
            );

            return false;
        }

        function setUser() {
            $('#tblSelectedUser .checkUser').each(function (i) {
                var v = $(this).val();
                $('#tblAllUser .checkUser[value=' + v + ']').prop('checked', true);
            });
        }

        $(function () {

            setUser();

            $("#tblAllUser").on('change', '.checkUser', function () {
                if (this.checked) {
                    $('#tblSelectedUser tbody').append('<tr class="row__' + this.value + '">' + $(this).parents("tr").html() + '</tr>');
                }
                else {
                    $('#tblSelectedUser tbody .row__' + this.value).remove();
                }
            });

            $("#searchUser").on("keyup", function () {
                var value = $(this).val();

                $("#tblAllUser tbody tr").each(function (index) {
                    //if (index !== 0)
                    {

                        $row = $(this);

                        var txt = $row.text().replace(/\s/g, '');;

                        if (txt.indexOf(value) == -1) {
                            $row.hide();
                        }
                        else {
                            $row.show();
                        }
                    }
                });
            });

            $('.expand-collape').on('click', function () {
                var $this = $(this);
                var txt = $this.text();
                var id = $this.data('id');
                var lvl = $this.data('lvl');

                switch (txt) {
                    case 'expand_more':
                        $(this).parents('.table').find('.level' + (lvl + 1) + '.parent__' + id + ' td ').show();
                        $(this).text('expand_less');
                        break;

                    case 'expand_less':
                        $(this).parents('.table').find('.level' + (lvl + 1) + '.parent__' + id + ' td ').hide();
                        $(this).parents('.table').find('.level' + (lvl + 1) + '.parent__' + id + ' ').find('.expand-collape:contains(expand_less)').trigger('click');
                        $(this).text('expand_more');
                        break;

                    default:
                }
                //if (txt == 'expand_more') {

                //} else {

                //}
            });

            /*$('.table input[type=checkbox]').on('jsclick', function (e ) {

                var $this = $(this);
                var lvl = $this.data('lvl');
                var col = $this.data('col');
                var id = $this.data('id');
                var groupid = $this.data('groupid');

                if ($this.is(':checked')) {
                    $('.table .parent__' + id + '.level' + (lvl + 1) + ' input[type=checkbox][data-col=' + col + ']:not(:checked)').prop('checked', true);
                    $('.table .parent__' + id + '.level' + (lvl + 1) + ' input[type=checkbox][data-col=' + col + ']:not(:checked)').trigger('jsclick');
                }
                else {
                    $('.table .parent__' + id + '.level' + (lvl + 1) + ' input[type=checkbox][data-col=' + col + ']:checked').prop('checked', false);
                    $('.table .parent__' + id + '.level' + (lvl + 1) + ' input[type=checkbox][data-col=' + col + ']:checked').trigger('jsclick');
                }
                //console.log(id);
            });*/

            $('.website input.switch-button').on('click', function (e) {
                var $this = $(this);
                var lvl = $this.data('lvl');
                var col = $this.data('col');
                var id = $this.data('id');
                var groupid = $this.data('groupid');

                if ($this.is(':checked')) {
                    $('.website input.switch-button[data-id=' + id + '][data-lvl=' + lvl + '][data-col!=' + col + ']:checked').trigger('click');
                    $('.website .parent__' + id + '.level' + (lvl + 1) + ' input.switch-button[data-col=' + col + ']:not(:checked)').trigger('click');
                }
                else {
                    $('.website .parent__' + id + '.level' + (lvl + 1) + ' input.switch-button[data-col=' + col + ']:checked').trigger('click');
                }

                //if (e.originalEvent.isTrusted)
                {

                    do {
                        var $f = $('.website .parent__' + groupid + '.level' + (lvl) + ' input.switch-button[data-col=' + col + '][data-groupid=' + groupid + ']');
                        var _selector = '.website input.switch-button[data-col=' + col + '][data-lvl=' + (lvl - 1) + '][data-id=' + groupid + '] ';
                        var $selector = $(_selector);
                        if ($f.length == $f.filter(':checked').length) {
                            $selector.prop('checked', true);
                        }
                        else {
                            $selector.prop('checked', false);
                        }
                        groupid = $selector.data('groupid');
                        lvl--;
                    } while (lvl > 0);
                }

            });

            $('.mobileapp input.switch-button').on('click', function (e) {
                var $this = $(this);
                var lvl = $this.data('lvl');
                var col = $this.data('col');
                var id = $this.data('id');
                var groupid = $this.data('groupid');

                if ($this.is(':checked')) {
                    $('.mobileapp input.switch-button[data-id=' + id + '][data-lvl=' + lvl + '][data-col!=' + col + ']:checked').trigger('click');
                    $('.mobileapp .parent__' + id + '.level' + (lvl + 1) + ' input.switch-button[data-col=' + col + ']:not(:checked)').trigger('click');
                }
                else {
                    $('.mobileapp .parent__' + id + '.level' + (lvl + 1) + ' input.switch-button[data-col=' + col + ']:checked').trigger('click');
                }

                //if (e.originalEvent.isTrusted)
                {

                    do {
                        var $f = $('.mobileapp .parent__' + groupid + '.level' + (lvl) + ' input.switch-button[data-col=' + col + '][data-groupid=' + groupid + ']');
                        var _selector = '.mobileapp input.switch-button[data-col=' + col + '][data-lvl=' + (lvl - 1) + '][data-id=' + groupid + '] ';
                        var $selector = $(_selector);
                        if ($f.length == $f.filter(':checked').length) {
                            $selector.prop('checked', true);
                        }
                        else {
                            $selector.prop('checked', false);
                        }
                        groupid = $selector.data('groupid');
                        lvl--;
                    } while (lvl > 0);
                }

            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
