<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="grade-quiz-detail.aspx.cs" Inherits="FingerprintPayment.grade_quiz_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card planelist-container">
        
        <style>
            .widthfull {
                width: 100%;
             }
             .centertext {
                 text-align: center;
             }
            .txt:hover {
                text-decoration: underline;
            }
            .righttext {
            position: relative;
            text-align: right;
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
            
                 
                
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        </div>
                        <div class="col-md-1 col-sm-1">
                            <asp:Label ID="Year"                                                                                     
                                           runat="server">                                    
                                </asp:Label>                            
                        </div>
                       <div class="col-md-2 col-sm-2 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        </div>
                        <div class="col-md-3 col-sm-3">
                            <asp:Label ID="Term"                                                                                     
                                           runat="server">                                    
                                </asp:Label>                            
                        </div>
                    </div>
                    
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                         
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        </div>
                        <div class="col-md-5 col-sm-5">
                            <asp:Label ID="classroom"                                                                                     
                                           runat="server">                                    
                                </asp:Label>
                            
                        </div>
                       
                    </div>
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
                        </div>
                        <div class="col-md-5 col-sm-5">
                                    <asp:Label ID="plan"                                                                                    
                                           runat="server">                                    
                                </asp:Label>                           
                        </div>
                       
                    </div>
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132199") %></label>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <asp:DropDownList ID="ddlTestNumber" runat="server" CssClass="width100 form-control" >
                                                            
                                                        </asp:DropDownList>                          
                        </div>
                       <div class="col-md-2 col-sm-2 ">                            
                            <asp:Button CssClass="btn btn-primary global-btn" ID="Button1" runat="server" Style="height: 40px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132201") %>" />
                        </div>
                        
                    </div>
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204046") %></label>
                        </div>
                        <div class="col-md-5 col-sm-5">
                                    <asp:Label ID="maxScore"                                                                                    
                                           runat="server">                                    
                                </asp:Label>                           
                        </div>
                       
                    </div>
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204032") %></label>
                        </div>
                        <div class="col-md-5 col-sm-5">
                                    <asp:Label ID="examDate"                                                                                    
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
                Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table cool-table-4"> 
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />               
                <Columns>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" >
                        <HeaderStyle Width="10%"></HeaderStyle>
                    </asp:BoundColumn>                                        
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                        <HeaderStyle Width="20%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sLastName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>" >
                        <HeaderStyle Width="20%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sIdentification" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" >
                        <HeaderStyle Width="14%"></HeaderStyle>
                    </asp:BoundColumn>  
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="score" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203075") %>" ItemStyle-CssClass="centertext" >
                        <HeaderStyle Width="11%"></HeaderStyle>
                    </asp:BoundColumn>  
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="rescore" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132202") %>" ItemStyle-CssClass="centertext">
                        <HeaderStyle Width="11%"></HeaderStyle>
                    </asp:BoundColumn>  
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="note" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>" >
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
                        <asp:Button CssClass="btn btn-danger global-btn" ID="btnedit" runat="server" Style="width: 30%;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132200") %>" />
                        <asp:Button CssClass="btn btn-warning global-btn" ID="btnCancle" runat="server" Style="width: 10%;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
