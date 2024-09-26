<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="behavior-details.aspx.cs" Inherits="FingerprintPayment.behavior_details" %>

<%@ Register TagPrefix="ajax" Namespace="System.Reflection.Emit" Assembly="mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card planelist-container">
        <%--        <a href="plans-term.aspx" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131220") %></a> <a href="periodslist.aspx"
            class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131226") %></a>--%>

        <div style="background: white;">

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <div class="row planadd-row">
                <br />
                <div class="col-xs-12">
                    <div class="col-xs-3">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                    </div>
                    <div class="col-xs-5">
                        <asp:Label ID="studentName2"
                            runat="server">                                    
                        </asp:Label>
                    </div>

                </div>
                <div class="col-xs-12">
                    <div class="col-xs-3">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></label>
                    </div>
                    <div class="col-xs-5">
                        <asp:Label ID="studentId2"
                            runat="server">                                    
                        </asp:Label>

                    </div>

                </div>
            </div>
            <br>
            <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table cool-table-4">
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />

                <Columns>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>">
                        <HeaderStyle></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="DayAdd" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01953") %>">
                        <HeaderStyle></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="BehaviorName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131227") %>">
                        <HeaderStyle></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>">
                        <ItemTemplate>
                            <%# Eval("StudentYear") + " / " + Eval("StudentTerm")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>">
                        <ItemTemplate>
                            <%# Eval("Type") + " " + Eval("Score")%>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="ScoreTotal" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405006") %>">
                        <HeaderStyle></HeaderStyle>
                    </asp:BoundColumn>
                    <%--<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Note" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>">--%>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="dCancle" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>">
                        <HeaderStyle></HeaderStyle>
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderStyle-CssClass="header-tb-color">
                        <ItemTemplate>
                            <%-- <p style="margin-left:0px;"> <asp:LinkButton  CssClass="btn btn-primary width200-px glyphicon glyphicon-list" ID="LinkButton2" runat="server" CommandName="Edit">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
                   </p>--%>
                            <p style="margin-left: 0px;">
                                <%--   <asp:LinkButton CssClass="btn btn-info width100-px glyphicon glyphicon-pencil"
                                    ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("sPlaneID") %>'>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
                                <a href="behavior-activities-edit.aspx?id=<%# Eval("BehaviorId") %>" class="btn btn-info hidden minor-button" data-toggle="modal"
                                    data-target="#modalpopupdata"><span class="glyphicon glyphicon-pencil global-btn"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>
                                <asp:LinkButton CssClass="btn btn-danger minor-button hidden" ID="btnDel"
                                    runat="server" CommandName="Del" CommandArgument='<%# Eval("BehaviorId") %>'><span class="glyphicon glyphicon-trash hidden"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></asp:LinkButton>
                            </p>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <a href="behavior-activites-add.aspx" class="btn btn-success hidden" data-toggle="modal"
                                data-target="#modalpopupdata"><span class="glyphicon glyphicon-plus hidden"></span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
                        </HeaderTemplate>
                    </asp:TemplateColumn>

                </Columns>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" CssClass="itemCell" />
                <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="pagerCell" />
                <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
