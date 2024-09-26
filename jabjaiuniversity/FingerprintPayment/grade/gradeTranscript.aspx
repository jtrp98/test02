<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="gradeTranscript.aspx.cs" Inherits="FingerprintPayment.grade.gradeTranscript"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <style>
        label {
            font-weight: normal;
            font-size: 26px;
        }
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
        #loading {
    display: block;
    position: fixed;
    top: 0;
    left: 0;
    z-index: 100;
    width: 100vw;
    height: 100vh;
    background-color: rgba(192, 192, 192, 0.5);
    background-image: url("https://i.imgur.com/CgViPo0.gif");
    background-repeat: no-repeat;
    background-position: center;
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

        var availableValueplane = [];

        $(document).ready(function () {
            $('.js-example-basic-multiple2').select2({
                allowClear: true,
                placeholder: ''
            });
        });

        function comboclick() {
            //var data = $('.js-example-basic-multiple2').select2('data');
            //data.prop('selectedIndex', 1).change();
            //$('#fieldId').select2("val", $('#fieldId option:eq(1)').val());

            //var ddllist = document.getElementsByClassName("select2-results__option");
            //for (var x = 0; x < ddllist.length; x++)
                //ddllist[x].classList.add('hidden');
            
        }

        $(document).ready(function () {

            
            $('input[id*=btnSearch]').click(function () {
                var load = document.getElementsByClassName("load");
                load[0].classList.remove('hidden');
                
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlsublevel2] option:selected').val();                
                var param3var = $('select[id*=ddlName] option:selected').val();
                if (param2var == undefined)
                    param2var = "";                
                window.location.href = "gradeTranscript.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&name=" + param3var;

                
            });

            
        });

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206412") %></h3>',
                backdrop: true
            });
        }

      
        

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");
            var load = document.getElementsByClassName("load");
                load[0].classList.remove('hidden');
            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }

            setTimeout(function () {
                    
                
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

                ddlrooms();

                 load[0].classList.add('hidden');
            });
            }, 300);

        }

        function ddlrooms() {
            console.log(ddlrooms);
            var ddl2 = document.getElementsByClassName("ddl2");
            var ddlName = document.getElementsByClassName("ddlName");
            var load = document.getElementsByClassName("load");
            load[0].classList.remove('hidden');
            //for (i = -1; i <= ddlName.length; i++) {
            //    ddlName[0].remove(0);
            //}
           
            $("#ctl00_MainContent_ddlName").empty();

            var opt = document.createElement("option");
           
            // Assign text and value to Option object
            opt.text = "";
            opt.value = "";

            opt.selected = "selected";

            // Add an Option object to Drop Down List Box
            document.getElementById('<%=ddlName.ClientID%>').options.add(opt);
            setTimeout(function () {
                var nTermSubLevel2 = 0;
                if (ddl2[1].options[ddl2[1].selectedIndex] != undefined) {
                    nTermSubLevel2 = parseInt(ddl2[1].options[ddl2[1].selectedIndex].value);
                }
                console.log(ddl2[0].options[ddl2[0].selectedIndex].value);
                var nTSubLevel = 0;
                if (ddl2[0].options[ddl2[0].selectedIndex] != undefined && ddl2[0].options[ddl2[0].selectedIndex].value != "") {
                    nTSubLevel = parseInt(ddl2[0].options[ddl2[0].selectedIndex].value);
                }
                PageMethods.GetStudentNamesByClassRoom(nTSubLevel, nTermSubLevel2, function (Result) {

                        $.each(Result, function (index) {

                            // Create an Option object       
                            var opt = document.createElement("option");
                            var name = Result[index].sName;
                            // Assign text and value to Option object
                            opt.text = name;
                            opt.value = name;

                            if (getUrlParameter("name") != "" && getUrlParameter("name") == name) {
                                opt.selected = "selected";
                            }

                            // Add an Option object to Drop Down List Box
                            document.getElementById('<%=ddlName.ClientID%>').options.add(opt);


                        });
                        //document.getElementById('<%=ddlName.ClientID%>').value = "";
                        load[0].classList.add('hidden');
                    });

                
            }, 300);

        }


        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }

        function start() {
           
            
            var d2 = document.getElementsByClassName("d2");
            
            
            ddlclass();

            var full = window.location.href;
            var half = full.split('?');

            if (half[1] != null) {
                setTimeout(function () {
                    document.getElementById('<%=ddlsublevel2.ClientID%>').value = d2[0].value;
                    if (getUrlParameter("name") != "") {
                        document.getElementById('<%=ddlName.ClientID%>').value = getUrlParameter("name");
                    }
                    
                }, 900);
            }
            
        }
        window.onload = start;
    </script>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content userlist-container">
        <div id="loading"class="hidden load"></div>
        <asp:HiddenField ID="hdfsid" runat="server" />
        
        <div class="form-group row student">
            <div class="col-xs-12 hidden">
                
                <div class="col-xs-2 righttext">                            
                    <label>ddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="txtddl2" runat="server" CssClass="d2"  width="50%"> </asp:Textbox>                           
                </div>
            </div>

          
        </div>
        
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="ddl2"  width="100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel2"  runat="server" onchange="ddlrooms()" CssClass="ddl2" Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="form-group row student">
        <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input" onclick="comboclick()">
                    <asp:DropDownList ID="ddlName" runat="server" class="form-control"  width="100%" CssClass="ddlName js-example-basic-multiple2" name="classchoice2[]">                    
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
                            <asp:BoundField DataField="number" HeaderStyle-CssClass="centertext" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sstudentid"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sName"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>">
                                <HeaderStyle Width="25%"></HeaderStyle>
                            </asp:BoundField>
                                   
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                  
                                    <a target="_blank" href="/grade/gradeTranscriptiframe.aspx?&id=<%# Eval("sID")%>" class="btn btn-warning" 
                                > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206314") %> </a> 
                                                                                                                                         
                                </ItemTemplate>
                                <HeaderStyle Width="5%"></HeaderStyle>
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

