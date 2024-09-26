<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="eventsettings-detail.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.eventsettings_detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
        .leftText {
            text-align: left;
        }
        .centertext {
            text-align: center;
        }
        .rightText {
            text-align: right;
        }
    </style>
    <div class="full-card text-center planeadd-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hfdsClassID" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <div class="row" id="row-name">
                    <div class="col-xs-12">
                        <div class="col-xs-4 rightText">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132556") %></label>
                        </div>
                        <div class="col-xs-5 leftText" >
                            <asp:Label ID="txt" runat="server"/>
                        </div>
                    </div>
                </div>
                
                
                <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        ondatabound="CustomersGridView_DataBound"   ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table"  >
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <pagerstyle forecolor="#337AB7" BorderColor="#337AB7"
                          backcolor="#337AB7"/>

                        <pagertemplate>

                          <table width="100%"  class="tab">                    
                            <tr>  
                                <td style="width:25%">

                                <asp:label id="Label1" BorderColor="#337AB7"
                                  forecolor="white"
                                  text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:" 
                                  runat="server"/>
                                <asp:dropdownlist id="PageDropDownList2"
                                  autopostback="true"
                                   
                                  onselectedindexchanged="PageDropDownList_SelectedIndexChanged2" 
                                  runat="server"/>

                              </td>                       
                              <td style="width:45%">
                                  <asp:LinkButton ID="backbutton" runat="server" 
                                    CssClass="imjusttext" OnClick="backbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                </asp:LinkButton>                                
                                <asp:dropdownlist id="PageDropDownList"
                                  autopostback="true"
                                  onselectedindexchanged="PageDropDownList_SelectedIndexChanged" 
                                  runat="server"/>
                                  <asp:LinkButton ID="nextbutton" runat="server" 
                                    CssClass="imjusttext" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                </asp:LinkButton> 

                              </td>   

                              <td style="width:70%; text-align:right">

                                <asp:label id="CurrentPageLabel"
                                  forecolor="white"
                                  runat="server"/>

                              </td>

                            </tr>                    
                          </table>

                        </pagertemplate> 

                        <Columns>                            
                            <asp:BoundField DataField="number" HeaderStyle-Width="100px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="room" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="50%"></HeaderStyle>
                            </asp:BoundField>
                            
                                                        
                               
                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="sEmp" HeaderText="sEmp" Visible="False" />
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />
                        
                        <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:GridView>
                </div>
            </div>
        </div>

                <div class="row text-center planadd-row">
                    <div class="col-xs-12 button-segment">                        
                        <asp:Button CssClass="btn btn-danger global-btn" ID="btnCancle" runat="server"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
                    </div>
                </div>
                <div class="row--space">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox runat="server" ID="txtListtime" Style="display: none;" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>

