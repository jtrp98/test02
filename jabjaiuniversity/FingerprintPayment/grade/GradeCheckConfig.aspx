<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GradeCheckConfig.aspx.cs" Inherits="FingerprintPayment.grade.GradeCheckConfig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
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
        
        .tab {border-collapse:collapse;margin-left: 6px; margin-right: 6px; border-bottom:3px solid #337AB7; border-left:3px solid #337AB7;border-right: 3px solid #337AB7; border-top:3px solid #337AB7;box-shadow: inset 0 1px 0 #337AB7;}

    </style>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];

        

        $(document).ready(function () {

            


            $('input[id*=btnSearch]').click(function () {
                
                var param2var = $('#ctl00_MainContent_courseddl option:selected').val();

                

                window.location.href = "gradecheckconfig.aspx?id=" + param2var ;
            });

            
        });
        
        function nextpage(id) {
            
            bootbox.confirm({
                title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132211") %></h>',
                buttons: {
                    cancel: {
                        label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                    },
                    confirm: {
                        label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701009") %>'
                    }
                },
                callback: function (result) {
                    if (result == true)
                        window.location = ("GradeCheckConfig.aspx?del=" + id);
                }
            });
                
           

        }
    </script>

     <script type="text/javascript" language="javascript">

         
         
         

    </script>
   
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
        
        
        <div class="form-group row student">              
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="courseddl" runat="server" class="form-control">                    
                        </asp:DropDownList>
                </div>
            </div>          
        </div>


        
       
        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>

        <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202014") %></h2>
        </div>
        <div class="modal-body">
            
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-5">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132209") %></label>
                    </div>
                <div class="col-xs-7">
                   <asp:DropDownList ID="modalTeacher" runat="server" width="90%" CssClass="ddl1" AutoPostBack="false" onchange="exampleName();" >                                
                   </asp:DropDownList>
                </div>
            </div>

            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-5">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132212") %></label>
                    </div>
                <div class="col-xs-7">
                   <asp:DropDownList ID="modalcourse" runat="server" width="90%" AutoPostBack="false"  >                                
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
        
         <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table"  >
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <pagerstyle forecolor="#337AB7" BorderColor="#337AB7"
                          backcolor="#337AB7"/>

                       

                        <Columns>                            
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext counter">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" HeaderStyle-CssClass="centertext">
                                <ItemTemplate>
                                    <asp:Label  ID="Label13" runat="server" Width="100%"  CssClass="lefttext smol" Text='<%# Eval("name") %>'></asp:Label>
                                    
                                </ItemTemplate>                                                                
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="35%"></HeaderStyle>
                            </asp:TemplateField>
                                                                                    
                            <asp:BoundField DataField="coursename" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132212") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext counter">
                                <HeaderStyle Width="40%"></HeaderStyle>
                            </asp:BoundField>
                            
                            <asp:TemplateField ItemStyle-CssClass="nopad">
                                <ItemTemplate>
                                    <i class="glyphicon glyphicon-remove" onclick="nextpage(<%# Eval("gradecheckId") %>)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" style=" font-size: 70%; cursor:pointer; color:red; font-size:70%;margin-left:35%;"></i>&nbsp;
                                    
                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle Width="15%" CssClass="nopad"></ItemStyle>
                                <HeaderTemplate>
                            <div class="btn btn-success" style="" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132210") %></div>
                        </HeaderTemplate>
                                <ItemStyle  />
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

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>

