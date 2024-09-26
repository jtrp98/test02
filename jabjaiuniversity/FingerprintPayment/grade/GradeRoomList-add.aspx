<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GradeRoomList-add.aspx.cs" Inherits="FingerprintPayment.grade.GradeRoomList_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card planelist-container">
        
        <style>
            .centertext {
            text-align: center;
        }
            .hid {
            visibility: hidden;
        }
            .righttext {
            position: relative;
            text-align: right;
        }
             .txt:hover {
                 text-decoration: underline;
             }
        </style>
        <script type="text/javascript" language="javascript">
            var i = 1;
            $('#image').click(function() {    
            });
            function myFunction() {
                document.getElementById(i).classList.remove('hidden');
                i = i + 1;
            }
        </script>
        <%--        <a href="plans-term.aspx" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131220") %></a> <a href="periodslist.aspx"
            class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131226") %></a>--%>
       <div class="full-card box-content ">
           
                
                    <div class="col-xs-12">
                        <div class="col-xs-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></label>
                        </div>
                        <div class="col-xs-5">
                                    <asp:Label ID="planid"                                                                                    
                                           runat="server">                                    
                                </asp:Label>                           
                        </div>
                       
                    </div>
                <div class="col-xs-12">
                        <div class="col-xs-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
                        </div>
                        <div class="col-xs-5">
                            <asp:Label ID="planname"                                                                                     
                                           runat="server">                                    
                                </asp:Label>
                            
                        </div>
                       
                    </div>
                <div class="col-xs-12">
                        <div class="col-xs-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        </div>
                        <div class="col-xs-2">
                            <asp:Label ID="Year"                                                                                     
                                           runat="server">                                    
                                </asp:Label>                            
                        </div>
                       <div class="col-xs-1">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        </div>
                        <div class="col-xs-3">
                            <asp:Label ID="Term"                                                                                     
                                           runat="server">                                    
                                </asp:Label>                            
                        </div>
                    </div>
           <div class="col-xs-12">
                        <div class="col-xs-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204046") %></label>
                        </div>
                        <div class="col-xs-2">
                                    <asp:TextBox  ID="maxScore" runat="server" Width="100%"></asp:TextBox>                          
                        </div>
                       <div class="col-xs-3">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></label>
                        </div>
                    </div>
               <div class="col-xs-12 hid">
                        <div class="col-xs-3 righttext">                            
                            <label>hidden</label>
                        </div>
                        
                       
                    </div>
               
           
           </div>
             
        
        <div class="full-card box-content ">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" AllowCustomPaging="False"
                GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table cool-table-4"> 
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />               
                <Columns>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" >
                        <HeaderStyle Width="10%"></HeaderStyle>
                    </asp:BoundColumn>    
                    <asp:BoundColumn ItemStyle-CssClass="hid" DataField="sID" HeaderText="" >
                        <HeaderStyle Width="1%"></HeaderStyle>
                    </asp:BoundColumn>                
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %>">
                        <HeaderStyle Width="35%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>"> 
                        <HeaderStyle Width="10%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade" runat="server" Width="50px" Text='<%# Eval("grade") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203075") %>"> 
                        <HeaderStyle Width="11%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtScore" runat="server" Width="50px" Text='<%# Eval("score") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132202") %>">  
                        <HeaderStyle Width="11%"></HeaderStyle>                                             
                        <ItemTemplate>
                            <asp:TextBox  ID="txtReScore" runat="server" Width="50px" Text='<%# Eval("reGrade") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>">   
                        <HeaderStyle Width="12%"></HeaderStyle>                                   
                        <ItemTemplate>
                            <asp:TextBox  ID="txtNote" runat="server" Width="120px" Text='<%# Eval("note") %>'></asp:TextBox>
                        </ItemTemplate>
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
            <div class="row text-center planadd-row">
                <br/>
                    <div class="col-xs-12 button-segment">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server" Style="width: 30%;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>" />
                        <asp:Button CssClass="btn btn-warning global-btn" ID="btnCancle" runat="server" Style="width: 10%;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>

