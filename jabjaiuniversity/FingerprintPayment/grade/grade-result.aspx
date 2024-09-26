<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="grade-result.aspx.cs" Inherits="FingerprintPayment.grade_result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card planelist-container">
        
        <style>
            .txt:hover {
    text-decoration: underline;
}
            .righttext {
            position: relative;
            text-align: right;
        }
            .centertext {
                 text-align: center;
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
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                        </div>
                        <div class="col-xs-5">
                                    <asp:Label ID="studentName2"                                                                                    
                                           runat="server">                                    
                                </asp:Label>                           
                        </div>
                       
                    </div>
                <div class="col-xs-12">
                        <div class="col-xs-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %></label>
                        </div>
                        <div class="col-xs-5">
                            <asp:Label ID="studentId2"                                                                                     
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
               
           
           </div>
             
        
        <div class="full-card box-content ">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" AllowCustomPaging="False"
                GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" PageSize="50" CssClass="cool-table cool-table-4"> 
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />               
                <Columns>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sPlanID" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>" >
                        <HeaderStyle Width="15%"></HeaderStyle>
                    </asp:BoundColumn>                    
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sPlanName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>">
                        <HeaderStyle Width="39%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color centertext" DataField="Grade" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206067") %>" ItemStyle-CssClass="centertext">
                        <HeaderStyle Width="10%"></HeaderStyle>
                    </asp:BoundColumn>                                      
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Score" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203075") %>" ItemStyle-CssClass="centertext">
                        <HeaderStyle Width="11%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="ReGrade" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132202") %>" ItemStyle-CssClass="centertext">
                        <HeaderStyle Width="11%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="Note" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>">
                        <HeaderStyle Width="14%"></HeaderStyle>
                    </asp:BoundColumn>
                            
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
                        <asp:Button CssClass="btn btn-warning global-btn" ID="btnCancle" runat="server"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
