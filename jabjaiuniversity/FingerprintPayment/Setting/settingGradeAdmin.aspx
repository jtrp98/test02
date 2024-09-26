<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="settingGradeAdmin.aspx.cs" Inherits="FingerprintPayment.Setting.settingGradeAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        .select2-selection__rendered {
    line-height: 41px !important;
}
.select2-container .select2-selection--single {
    height: 41px !important;
}
.select2-selection__arrow {
    height: 41px !important;    
}
.select2-selection__arrow b{
    border-color:black transparent transparent transparent !important; 
}
[class^='select2'] {
  border-radius: 1px !important;
  border-top-color:#abadb3 !important;
  border-left-color:#dbdfe6 !important;
  border-right-color:#dbdfe6 !important;
  border-bottom-color:#dbdfe6 !important;
}
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }
        .cover {
    
    text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        
        .listItem {
            color: blue;
            background-color: White;
        }
        label {
            font-weight: normal;
            font-size: 26px;
        }
        .statusOnline{
            background-color:palegreen;
            color:black;
        }
        .statusOffline{
            background-color:hotpink;
            color:black;
        }
        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }
        .centertext {
            text-align: center;
        }
        .centerText {
            text-align: center;
        }
        .statusbox3{
    width:100px;  padding:0px;
                background:rgba(0,0,0,0);
                border:none;                       
                text-align:center;
}
        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
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
    
    text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        .listItem {
            color: blue;
            background-color: White;
        }
        .hid {
             visibility: hidden;
         }
        .hid2 {
             visibility: hidden;
             display: none;
         }
        
        .width10 {
            margin: 0 auto;
            width: 10%;
        }
        
        .itemHighlighted {
            background-color: #ffc0c0;
        }
       
        .smol  {
     font-size: 80%;
     
        }
        .nounder a:hover{
    text-decoration: none;
        

}
        .shadowblack{
    text-decoration: none;
        text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        
        .boxhead a {
    color: #FFFFFF;
    text-decoration: none;
}
        .attendancebox{
            font-size:70%;
    padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
               
}
        a.imjusttext{ color: #ffffff; text-decoration: none; }
a.imjusttext:hover { color: aquamarine; }
        
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
        .deleteName{
            height:38px;
            font-size:170%;
        }
        .select2-selection__rendered{
            font-size:170%;
        }
        .tab {border-collapse:collapse;margin-left: 6px; margin-right: 6px; border-bottom:3px solid #337AB7; border-left:3px solid #337AB7;border-right: 3px solid #337AB7; border-top:3px solid #337AB7;box-shadow: inset 0 1px 0 #337AB7;}

    </style>
    
    <script type="text/javascript" language="javascript">

        $.fn.modal.Constructor.prototype.enforceFocus = function () { };

        $(document).ready(function () {
            $('.js-example-basic-multiple1').select2({
                allowClear: true,
                placeholder: ''
            });
        });
      
       
        function editModal(number) {
            var modalname = document.getElementsByClassName("modalname");
            var modalid = document.getElementsByClassName("modalid");
            


            var deleteName = document.getElementsByClassName("deleteName");
            var deletePlanID = document.getElementsByClassName("deletePlanID");
            
            deleteName[0].value = modalname[number - 1].textContent;
            deletePlanID[0].value = modalid[number - 1].textContent;
            //$('#ctl00_MainContent_teacher3').select2("val", $('#ctl00_MainContent_teacher3 option:eq(5)').val());

        }
        
        
    </script>

    <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206165") %></h2>
        </div>
        <div class="modal-body">
        
            <div class="col-xs-12 regis1" style="padding:5px;">
                <div class="col-xs-5">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206166") %></label>
                    </div>
                <div class="col-xs-7">
                   <asp:DropDownList ID="teacherRegis1" runat="server" CssClass="js-example-basic-multiple1" name="classchoice1[]" AutoPostBack="false"  width="80%" >                        
        
                   </asp:DropDownList>
                </div>
            </div>
           
           <div class="hid" style="font-size:30%">hidden</div>
        </div>
        <div class="modal-footer">
            <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
          <button type="button" class="btn btn-default" data-dismiss="modal" style="width:100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
        </div>
      </div>
    </div>
  </div>
      

        <div class="modal fade" id="modalDelete" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206164") %></h2>
        </div>
        <div class="modal-body">
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-5">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101295") %></label>
                    </div>
                <div class="col-xs-7">
                   <asp:Textbox ID="deleteName" enabled="false" runat="server" CssClass="deleteName" width="80%"> </asp:Textbox>
                </div>
            </div>
             
            
          <div class="col-xs-12 hidden" style="padding:5px;">
                <div class="col-xs-5">
                    <label>planID</label>
                    </div>
                <div class="col-xs-7">
                   <asp:Textbox ID="deleteID" runat="server" CssClass="deletePlanID" > </asp:Textbox>
                </div>
            </div>
            
            
           <div class="hid" style="font-size:30%">hidden</div>
        </div>
        <div class="modal-footer">
            <asp:Button CssClass="btn btn-danger global-btn" ID="deleteBtn" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" />
          <button type="button" class="btn btn-default" data-dismiss="modal" style="width:100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
        </div>
      </div>
    </div>
  </div>

    <div class="full-card box-content userlist-container">
       
        <div class="form-group row student centertext">
            
           <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206162") %></label>
        </div>


        

        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        ondatabound="CustomersGridView_DataBound"  ShowHeaderWhenEmpty="true"
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
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                                               
                             
            <asp:BoundField DataField="name"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>" ItemStyle-CssClass="lefttext modalname" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="70%"></HeaderStyle>
                            </asp:BoundField>                           
                                  
                            <asp:BoundField DataField="sEMP"  HeaderText="" ItemStyle-CssClass="centertext modalid hid" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>          
  
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                  <div class="col-md-12 col-sm-12">  
                                      
                                    <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                        <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                       <div   class="glyphicon glyphicon-remove" onclick="editModal(<%# Eval("number") %>)" style=" font-size: 70%; cursor:pointer; color:red; font-size:70%" data-toggle="modal" data-target="#modalDelete"> 
                                                     </div>     </div>    
                                    </div>
                                </div>                                                                                                     
                                </ItemTemplate>
                                <HeaderTemplate>
                            <div class="btn btn-success" style="margin-left:25px;" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206163") %></div>
                        </HeaderTemplate>
                                <HeaderStyle Width="15%"></HeaderStyle>
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

        <div class="col-xs-12 hid">
            <p>hidden</p>
            </div>
        <div class="form-group row student centertext">
        <asp:Button CssClass="btn btn-danger global-btn" ID="Button1" runat="server" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>" />
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
