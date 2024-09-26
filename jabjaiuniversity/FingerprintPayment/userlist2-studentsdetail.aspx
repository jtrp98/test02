<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
	CodeBehind="userlist2-studentsdetail.aspx.cs" Inherits="FingerprintPayment.userlist2_studentsdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	
	
	

	<div class="full-card planelist-container">
		<%--        <a href="plans-term.aspx" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131220") %></a> <a href="periodslist.aspx"
			class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131226") %></a>--%>
		
	   <div id="Div1" class="full-card box-content row--space holiday-table-container">
		<ul id="myTab" class="nav nav-tabs nav-tabs-title">
			<li class="hidden"><a href="#all" style="color: black;" data-toggle="all"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %> </a></li>
			<li class="active"><a href="#bad" style="color: black;" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302004") %></a></li>
			<li><a href="#good" style="color: black;" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %></a></li>
			<li><a href="#auto" style="color: black;" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302005") %></a></li>
		</ul>
		<div class="tab-content">
			<div class="tab-pane fade in active" id="bad" style="background: white;">
				<asp:DataGrid ID="dgd2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
					GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
					Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
					<AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
						Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
					<Columns>
						<asp:BoundColumn DataField="number" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>">
								<HeaderStyle Width="10%"></HeaderStyle>
							</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Type" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Score" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302006") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="BehaviorName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131227") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="header-tb-color">
						<ItemTemplate>
							<%-- <p style="margin-left:0px;"> <asp:LinkButton  CssClass="btn btn-primary width200-px glyphicon glyphicon-list" ID="LinkButton2" runat="server" CommandName="Edit">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
				   </p>--%>
							<p style="margin-left: 0px;">
								<%--   <asp:LinkButton CssClass="btn btn-info width100-px glyphicon glyphicon-pencil"
									ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("sPlaneID") %>'>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
								<a href="behavior-activities-edit.aspx?id=<%# Eval("BehaviorId") %>" class="btn btn-info minor-button" data-toggle="modal"
									data-target="#modalpopupdata"><span class="glyphicon glyphicon-pencil global-btn"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>
								<asp:LinkButton CssClass="btn btn-danger minor-button" ID="btnDel"
									runat="server" CommandName="Del" CommandArgument='<%# Eval("BehaviorId") %>'><span class="glyphicon glyphicon-trash"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></asp:LinkButton>
							</p>
						</ItemTemplate>
						<HeaderTemplate>
							<a href="behavior-activites-add.aspx" class="btn btn-success" data-toggle="modal"
								data-target="#modalpopupdata"><span class="glyphicon glyphicon-plus"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
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
			<div class="tab-pane fade" id="good" style="background: white;">
				<asp:DataGrid ID="dgd3" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
					GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
					Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
					<AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
						Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
					<Columns>
						<asp:BoundColumn DataField="number" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>">
								<HeaderStyle Width="10%"></HeaderStyle>
							</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Type" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Score" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302006") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="BehaviorName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131227") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="header-tb-color">
						<ItemTemplate>
							<%-- <p style="margin-left:0px;"> <asp:LinkButton  CssClass="btn btn-primary width200-px glyphicon glyphicon-list" ID="LinkButton2" runat="server" CommandName="Edit">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
				   </p>--%>
							<p style="margin-left: 0px;">
								<%--   <asp:LinkButton CssClass="btn btn-info width100-px glyphicon glyphicon-pencil"
									ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("sPlaneID") %>'>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
								<a href="behavior-activities-edit.aspx?id=<%# Eval("BehaviorId") %>" class="btn btn-info minor-button" data-toggle="modal"
									data-target="#modalpopupdata"><span class="glyphicon glyphicon-pencil global-btn"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>
								<asp:LinkButton CssClass="btn btn-danger minor-button" ID="btnDel"
									runat="server" CommandName="Del" CommandArgument='<%# Eval("BehaviorId") %>'><span class="glyphicon glyphicon-trash"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></asp:LinkButton>
							</p>
						</ItemTemplate>
						<HeaderTemplate>
							<a href="behavior-activites-add.aspx" class="btn btn-success" data-toggle="modal"
								data-target="#modalpopupdata"><span class="glyphicon glyphicon-plus"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
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
			<div class="tab-pane fade" id="auto" style="background: white;">
				<asp:DataGrid ID="dgd4" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
					GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
					Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
					<AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
						Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
					<Columns>
						<asp:BoundColumn DataField="number" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>">
								<HeaderStyle Width="10%"></HeaderStyle>
							</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Type" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Score" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302006") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="BehaviorName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131227") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="header-tb-color">
						<ItemTemplate>
							<%-- <p style="margin-left:0px;"> <asp:LinkButton  CssClass="btn btn-primary width200-px glyphicon glyphicon-list" ID="LinkButton2" runat="server" CommandName="Edit">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
				   </p>--%>
							<p style="margin-left: 0px;">
								<%--   <asp:LinkButton CssClass="btn btn-info width100-px glyphicon glyphicon-pencil"
									ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("sPlaneID") %>'>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
								<a href="behavior-activities-edit.aspx?id=<%# Eval("BehaviorId") %>" class="btn btn-info minor-button" style="visibility: hidden" data-toggle="modal"
									data-target="#modalpopupdata"><span class="glyphicon glyphicon-pencil global-btn"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>
								<asp:LinkButton CssClass="btn btn-danger minor-button" style="visibility: hidden" ID="btnDel"
									runat="server" CommandName="Del" CommandArgument='<%# Eval("BehaviorId") %>'><span class="glyphicon glyphicon-trash"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></asp:LinkButton>
							</p>
						</ItemTemplate>
						<HeaderTemplate>
							<a href="behavior-activites-add.aspx" class="btn btn-success" style="visibility: hidden" data-toggle="modal"
								data-target="#modalpopupdata"><span class="glyphicon glyphicon-plus"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
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
			<div class="tab-pane fade hidden" id="all" style="background: white;">
				<asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
					GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
					Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
					<AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
						Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
					<Columns>
						<asp:BoundColumn DataField="number" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %>">
								<HeaderStyle Width="10%"></HeaderStyle>
							</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Type" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Score" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302006") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="BehaviorName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131227") %>">
						<HeaderStyle></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="header-tb-color">
						<ItemTemplate>
							<%-- <p style="margin-left:0px;"> <asp:LinkButton  CssClass="btn btn-primary width200-px glyphicon glyphicon-list" ID="LinkButton2" runat="server" CommandName="Edit">&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131228") %></asp:LinkButton>
				   </p>--%>
							<p style="margin-left: 0px;">
								<%--   <asp:LinkButton CssClass="btn btn-info width100-px glyphicon glyphicon-pencil"
									ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("sPlaneID") %>'>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></asp:LinkButton>--%>
								<a href="behavior-activities-edit.aspx?id=<%# Eval("BehaviorId") %>" class="btn btn-info minor-button" data-toggle="modal"
									data-target="#modalpopupdata"><span class="glyphicon glyphicon-pencil global-btn"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></a>
								<asp:LinkButton CssClass="btn btn-danger minor-button" ID="btnDel"
									runat="server" CommandName="Del" CommandArgument='<%# Eval("BehaviorId") %>'><span class="glyphicon glyphicon-trash"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %></asp:LinkButton>
							</p>
						</ItemTemplate>
						<HeaderTemplate>
							<a href="behavior-activites-add.aspx" class="btn btn-success" data-toggle="modal"
								data-target="#modalpopupdata"><span class="glyphicon glyphicon-plus"></span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601032") %></a>
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
		   </div>
		</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
