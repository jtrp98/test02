<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="grade-quiz-edit.aspx.cs" Inherits="FingerprintPayment.grade_quiz_edit" %>

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
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132203") %></label>
                        </div>
                        <div class="col-md-5 col-sm-5">
                            <asp:Label ID="testNumber"                                                                                     
                                           runat="server">                                    
                                </asp:Label>
                            
                        </div>
                       
                    </div>
                
                <div class="col-md-12 col-sm-12 form-group row">
                        <div class="col-md-3 col-sm-3 righttext">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132204") %></label>
                        </div>
                        <div class="col-md-2 col-sm-2">
                             <asp:TextBox  ID="maxscore" runat="server"  CssClass="widthfull"></asp:TextBox>                                    
                        </div>
                       <div class="col-md-1 col-sm-1">                            
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></label>
                        </div>
                        
                    </div>
                                                <div class="col-md-12 col-sm-12 form-group row">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204032") %></label>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" style="width: 13%;">
                                                        <asp:DropDownList ID="ddlDay" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                            <asp:ListItem Text="1" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input" >
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="width100 form-control">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107041") %>" Value="01"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107042") %>" Value="02"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107043") %>" Value="03"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %>" Value="04"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107045") %>" Value="05"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107046") %>" Value="06"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107047") %>" Value="07"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107048") %>" Value="08"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107049") %>" Value="09"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107050") %>" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %>" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107052") %>" Value="12"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
                                                        <asp:DropDownList ID="ddlAge" runat="server" CssClass="width100 form-control" >
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>
                                                        </asp:DropDownList>
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
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sLastName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                        <HeaderStyle Width="20%"></HeaderStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderStyle-CssClass="header-tb-color" DataField="sIdentification" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" >
                        <HeaderStyle Width="14%"></HeaderStyle>
                    </asp:BoundColumn>  
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203075") %>"> 
                        <HeaderStyle Width="11%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtScore" runat="server" Width="50px" Text='<%# Eval("score") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132202") %>">  
                        <HeaderStyle Width="11%"></HeaderStyle>                                             
                        <ItemTemplate>
                            <asp:TextBox  ID="txtReScore" runat="server" Width="50px" Text='<%# Eval("rescore") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>">   
                        <HeaderStyle Width="14%"></HeaderStyle>                                   
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
                        <asp:Button CssClass="btn btn-danger global-btn" ID="btnSave" runat="server" Style="width: 30%;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105042") %>" />
                        <asp:Button CssClass="btn btn-warning global-btn" ID="btnCancle" runat="server" Style="width: 10%;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
