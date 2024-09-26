<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="BP5attendance.aspx.cs" Inherits="FingerprintPayment.grade.BP5attendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card planelist-container" style="width:380%">
        
        <style>
            .centertext {
                text-align: center;
            }
            .hid {
                visibility:hidden;
            }
            .righttext2 {
                background-color: #337AB7;
                position: relative;
                text-align: right;
                color: white
            } 
            .righttext3 {
                background-color: #337AB7;
                position: relative;                
            }         
            .hid2 { 
            }
            .haha td {
    padding: 0px 0px 0px 0px;
}
            .hid4 { 
                white-space: nowrap;
                background-color: #337AB7;
                background :  #337AB7;
                position:absolute;
                visibility:hidden;
            }
            .hid3 { 
                white-space: nowrap;
                background-color: #337AB7;
                background :  #337AB7;
                color:#337AB7;
                visibility:hidden;
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
                <div class="col-xs-6">
                <div class="col-xs-1 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206446") %></label>
                </div>
                <div class="col-xs-5">
                    
                            
                </div>
                       </div>
            </div>
            <div class="col-xs-12">
                <div class="col-xs-6">
                <div class="col-xs-1 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></label>
                </div>
                <div class="col-xs-1">
                    <asp:Label ID="planid"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>
                       <div class="col-xs-1">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
                </div>
                <div class="col-xs-5">
                    <asp:Label ID="planname"                                                                                     
                               runat="server">                                    
                    </asp:Label>                            
                </div>
                    </div>
            </div>
            
            <div class="col-xs-12">
                <div class="col-xs-6">
                <div class="col-xs-1 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                </div>
                <div class="col-xs-1">
                    <asp:Label ID="Year"                                                                                     
                               runat="server">                                    
                    </asp:Label>                            
                </div>
               <div class="col-xs-1">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                </div>
                <div class="col-xs-1">
                    <asp:Label ID="Term"                                                                                     
                               runat="server">                                    
                    </asp:Label>                            
                </div>
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
            <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        OnRowCreated="grvMergeHeader_RowCreated"
                          Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table cool-table-4 haha haha-td"> 
                             
                <Columns>
                    <asp:BoundField HeaderStyle-CssClass="header-tb-color" DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" >
                        <HeaderStyle Width="3%"></HeaderStyle>
                    </asp:BoundField>    
                    <asp:BoundField ItemStyle-CssClass="hid"   DataField="sID" HeaderText="" >
                        <HeaderStyle Width="0.6%"></HeaderStyle>
                    </asp:BoundField>      
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301008") %>"> 
                        <HeaderStyle Width="15%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:Label  ID="txtGrade5" runat="server" Width="200px" Text='<%# Eval("sName") %>'></asp:Label>
                        </ItemTemplate><ItemStyle Width="200px" /> 
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle> 
                        <ItemTemplate>
                                    <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>
                                </ItemTemplate><ItemStyle Width="10px" />                                          
                        
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
<div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
<div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
<div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
<div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week1_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week1_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                                        
                                </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week2_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week2_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week3_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week3_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                        <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                         <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                                                    <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div> 
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week4_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week4_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week5_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week5_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week6_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week6_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week7_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week7_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week8_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week8_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week9_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week9_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week10_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week10_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week11_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week11_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>                                                        
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week12_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week12_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week13_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week13_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week14_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week14_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week15_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week15_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week16_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week16_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week17_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week17_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week18_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week18_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week19_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week19_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="จ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_1").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_1").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_1").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="อ"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_2").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_2").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_2").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_3").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_3").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_3").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_4").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_4").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_4").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ศ">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_5").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_5").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_5").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>                   
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="ส"> 
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_6").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_6").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_6").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                                                     <div class="col-md-12 col-sm-12" style="width:10px">  
                                                       <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_7").ToString()=="0" ? "":"hidden" %>" > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>
                                                     </div> 
                                                      <div style=" font-size: 70%; width:10px" class="fa fa-check <%# Eval("week20_7").ToString()=="1" ? "":"hidden" %>" > 
                                                     </div> 
                                                        <div style=" font-size: 70%; width:10px" class=" <%# Eval("week20_7").ToString()=="2" ? "":"hidden" %>" > 
                                                     </div>                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132168") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:Label  ID="totalcome" runat="server" Width="40px" Text='<%# Eval("totalcome") %>'></asp:Label>
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132169") %>">
                        <HeaderStyle Width="0.6%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:Label  ID="totalskip" runat="server" Width="40px" Text='<%# Eval("totalskip") %>'></asp:Label>
                        </ItemTemplate><ItemStyle Width="10px" />
                    </asp:TemplateField>

                    
                   <asp:BoundField ItemStyle-CssClass="hid"   DataField="sID" HeaderText="">
                        <HeaderStyle Width="0.6%"></HeaderStyle>
                    </asp:BoundField> 
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
                
                
            </asp:GridView>
            <div class="row text-center planadd-row">
                <br/>
                <div class="col-xs-3 button-segment">
                    <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server" Style="width: 200px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>" />
                    <asp:Button CssClass="btn btn-success global-btn" ID="Button1" runat="server" Style="width: 150px;"
                                Text="Export to Excel" OnClick = "ExportToExcel"/>
                    <asp:Button CssClass="btn btn-warning global-btn" ID="btnCancle" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
