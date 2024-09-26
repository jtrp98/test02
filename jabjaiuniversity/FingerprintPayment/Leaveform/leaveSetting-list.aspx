<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="leaveSetting-list.aspx.cs" Inherits="FingerprintPayment.leaveSetting_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }
        .cover {
    color: yellow;
    text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        .listItem {
            color: blue;
            background-color: White;
        }
        .hid {
             visibility: hidden;
         }
        .width10 {
            margin: 0 auto;
            width: 10%;
        }
        .itemHighlighted {
            background-color: #ffc0c0;
        }
        .centertext {
            text-align: center;
        }
        label {
            font-weight: normal;
            font-size: 26px;
        }
        .gvbutton  {
     font-size: 25px;
     
        }
        .nounder a:hover{
    text-decoration: none;
}
        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }
        .boxhead a {
    color: #FFFFFF;
    text-decoration: none;
}
        a.imjusttext{ color: #ffffff; text-decoration: none; }
a.imjusttext:hover { color: aquamarine; }
        .centerText {
            text-align: center;
        }
        .btn-red {
  background: red; /* use your color here */
}
        

        .nowrap {
            max-width:100%;
            white-space:nowrap;
        }
        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }
        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
        .tab {border-collapse:collapse;margin-left: 6px; margin-right: 6px; border-bottom:3px solid #337AB7; border-left:3px solid #337AB7;border-right: 3px solid #337AB7; border-top:3px solid #337AB7;box-shadow: inset 0 1px 0 #337AB7;}

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
    <div class="full-card box-content">
        <div class="form-group row ">
                <div class="col-md-12 col-sm-12">
                    <label class="col-lg-12 col-md-12 col-sm-12 col-xs-12 control-label centertext ">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132337") %></label>
                </div>
            </div>
        
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        ondatabound="CustomersGridView_DataBound" 
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
                                <HeaderStyle Width="7%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sName" HeaderStyle-Width="200px" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" >
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sLast" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="">
                                <ItemTemplate>                                    
                                   <a href="/Leaveform/leaveSetting-add.aspx?id=<%# Eval("sEmp") %>" class=" btn-sm btn-info gvbutton "  
                                > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>สิทธิ</a>                                                                     
                                </ItemTemplate>
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:TemplateField>    
                            
                                                                                 
                            
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
        <div class="form-group row ">
                <div class="col-md-12 col-sm-12">
                    <label class="col-lg-12 col-md-12 col-sm-12 col-xs-12 control-label centertext hid">
                        hidden</label>
                                     
                </div>
            </div>
        <div class="row form-group">
            <div class="col-sm-12 pull-right">
                <asp:Button ID="Button1" class="btn btn-primary global-btn pull-right"  
                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>"  />
                
            </div>
        </div>
    </div>
</asp:Content>
