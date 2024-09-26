<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GradeCheckList.aspx.cs" Inherits="FingerprintPayment.grade.GradeCheckList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
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

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
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
       
        .gvbutton  {
     font-size: 25px;
     
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
   
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    <script type="text/javascript" language="javascript">

        

        $(document).ready(function () {

            
            $('input[id*=btnSearch]').click(function () {                
                
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlsublevel2] option:selected').val();                
                var param3var = $('select[id*=DropDownList1] option:selected').val();
                if (param2var == undefined)
                    param2var = "";
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                if (param4var == undefined)
                    param4var = "";
                window.location.href = "GradeCheckList.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var;

                
            });

            
        });

      
        function ddlyear() {
            var ddl1 = document.getElementsByClassName("ddl1");
            var select = document.getElementById('DD1');

            for (i = -1; i <= 5; i++) {
                ddl1[1].remove(0);
            }

            $.get("/App_Logic/ddlterm.ashx?year=" + ddl1[0].options[ddl1[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
        var opt = document.createElement("option");        

        // Assign text and value to Option object
        opt.text = Result[index].value;
        opt.value = Result[index].value;

        // Add an Option object to Drop Down List Box
        document.getElementById('<%=DropDownList2.ClientID%>').options.add(opt);
                                      

                });
            });

            
        }

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");            
            
            for (i = -1; i <= 20; i++) {
                ddl2[1].remove(0);
            }

            $("#<%=ddlsublevel2.ClientID%> option").remove();

            $.get("/App_Logic/ddlclassroom.ashx?idlv=" + ddl2[0].options[ddl2[0].selectedIndex].value, function (Result) {
                $.each(Result, function (index) {

                    // Create an Option object       
                var opt = document.createElement("option");        

                // Assign text and value to Option object
                opt.text = Result[index].name;
                opt.value = Result[index].value;

                if (getUrlParameter("idlv2") != "" && getUrlParameter("idlv2") == Result[index].value) {
                    opt.selected = "selected";
                }  

                // Add an Option object to Drop Down List Box
                document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                                      

                });
            });

            
        }

        
        function start() {
            
            var orange = document.getElementsByClassName("orangebutton");
            var green = document.getElementsByClassName("greenbutton");
            var greybutton = document.getElementsByClassName("greybutton");
            var havedata = document.getElementsByClassName("havedata");
            
            var d2 = document.getElementsByClassName("d2");
            ddlyear();
            ddlclass();

            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');
            var idlv = split[0];
            var idlv2 = split[1].split('=');
            var year = split[2];
            var term = split[3].split('=');
            
            var d1 = document.getElementsByClassName("d1");
                        

            setTimeout(function () {
            document.getElementById('<%=DropDownList2.ClientID%>').value = d1[0].value;
            document.getElementById('<%=ddlsublevel2.ClientID%>').value = d2[0].value;
            }, 900);
        }
        window.onload = start;
    </script>
   
    <div class="full-card box-content userlist-container">
       
        <div class="form-group row student">
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>ddl1</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="txtddl1" runat="server" CssClass="d1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="txtddl2" runat="server" CssClass="d2"  width="50%"> </asp:Textbox>                           
                </div>
            </div>

            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList1" onchange="ddlyear()" runat="server" CssClass="ddl1 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl1 form-control">
                          
                            </asp:DropDownList>
                </div>
            </div>
        </div>
       
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="ddl2 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel2"  runat="server" CssClass="ddl2 form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
       
        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
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
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">  
                        <HeaderStyle Width="5%"></HeaderStyle>                                             
                        <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding:0px;">
                                        <input type="text" class="" style="padding:0px; text-align:center; width:100%; font-size:90%; border:0px; background-color:white;" value="<%# Eval("number") %>">                                          
                                            <input type="text" class="havedata hidden" style="padding:0px; text-align:center; width:100%; font-size:90%; border:0px; background-color:white;" value="<%# Eval("havedata") %>">
                                                           </div></div>       
                                </div>
                                </ItemTemplate>
                    </asp:TemplateField> 
                            <asp:BoundField DataField="code"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="35%"></HeaderStyle>
                            </asp:BoundField>                            
                            <asp:BoundField DataField="planName"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>" >
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>                            
                             <asp:BoundField DataField="planName"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132214") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                  <a href="/grade/Webform2.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>" class="btn btn-success greenbutton" 
                                ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405041") %></a> 
                                    <a href="/grade/BP5detail.aspx?year=<%# Eval("year")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&term=<%# Eval("term")%>&id=<%# Eval("planId")%>" class="btn btn-warning orangebutton" 
                                ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132213") %></a>                                                                                                                                          
                                </ItemTemplate>
                                <HeaderStyle Width="25%"></HeaderStyle>
                            </asp:TemplateField>  
                             
                              
                                                                                  
                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>
                            
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
