<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="settingTeacherExtraTime.aspx.cs" Inherits="FingerprintPayment.Setting.settingTeacherExtraTime" %>
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
        .width100{
            height:42px;
            font-size:150%;
            width:80%;
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
        .righttext{
            text-align:right;
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
        .modalsize{
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
        startup();
            $('.js-example-basic-multiple1').select2({
                allowClear: true,
                placeholder: ''
            });
        });
      
        $(document.body).on("change", ".js-example-basic-multiple1", function () {
            var regis = document.getElementsByClassName("regis2");
            
        });

        function editModal(id,name,planname,date) {
            var deleteName = document.getElementsByClassName("deleteName");
            var deletePlanname = document.getElementsByClassName("deletePlanname");
            var deleteDate = document.getElementsByClassName("deleteDate");
            var deletePlanID = document.getElementsByClassName("deletePlanID");
            var startup = document.getElementsByClassName("startup");
            
            deleteName[0].value = name;
            deletePlanname[0].value = planname;
            deleteDate[0].value = date;
            deletePlanID[0].value = id;
        }
        
        function startup() {
            var statustxt = document.getElementsByClassName("statustxt");
            var statusmode = document.getElementsByClassName("statusmode");
            

            for(var x =0;x<statustxt.length;x++)
            {
                if(statustxt[x].textContent == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206172") %>")
                    statusmode[x].classList.add('hidden');
            }
        }

        function ddluserlist() {
            var ddluser = document.getElementsByClassName("ddluser");
            var ddlselect = document.getElementsByClassName("ddlselect");

            ddlselect[0].value = ddluser[0].value;

            if ($("#<%=planlist.ClientID%>") != undefined)
                $("#<%=planlist.ClientID%>").empty();
            
            $.get("/api/Plan/GetSubjectsByTeacherAndRoom?nYear=" + $("#ctl00_MainContent_ddlyear option:selected").val() + "&nTerm=" + $("#ctl00_MainContent_ddlterm option:selected").val() + "&nTSublevel=" + $("#ctl00_MainContent_room1 option:selected").val() + "&nTermSubLevel2=" + $("#ctl00_MainContent_room2 option:selected").val() + "&sEmpId=" + $("#ctl00_MainContent_userlist option:selected").val() , function (Result) {
                console.log(Result);
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].CourseCode + " " + Result[index].CourseName;
                    opt.value = Result[index].SPlaneId;

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=planlist.ClientID%>').options.add(opt);


                });
            });
        }

        function ddlyear(control) {
           
            //var select = document.getElementById('DD1');
            document.getElementById('<%=ddlterm.ClientID%>')
            console.log("ddlyear" + $(control));
            if ($("#<%=ddlterm.ClientID%>") != undefined)
                $("#<%=ddlterm.ClientID%>").empty();
            
            $.get("/api/common/TermByYear?numberYear=" + $("#ctl00_MainContent_ddlyear option:selected").text(), function (Result) {
                console.log(Result);
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].sTerm;
                    opt.value = Result[index].nTerm;

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlterm.ClientID%>').options.add(opt);


                });
            });


        }

        function ddlclass(control) {
            var ddl2 = document.getElementsByClassName("ddl2");
            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }
            if (ddl2[0].options[ddl2[0].selectedIndex].value != "") {
                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + ddl2[0].options[ddl2[0].selectedIndex].value, function (Result) {

                    // Create an Empty Option object       
                    /*var opt = document.createElement("option");*/

                    // Assign text and value to Option object
                    //opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %>";
                    //opt.value = "";

                    // Add an Option object to Drop Down List Box
                    console.log(Result);

                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");

                        // Assign text and value to Option object
                        opt.text = Result[index].name;
                        opt.value = Result[index].value;

                        

                        // Add an Option object to Drop Down List Box
                        document.getElementById('<%=room2.ClientID%>').options.add(opt);


                    });

                    //Fetch Teachers from Plan on Room Selection
                    $.get("/api/Plan/GetTeachersFromPlan?nYear=" + $("#ctl00_MainContent_ddlyear option:selected").val() + "&nTerm=" + $("#ctl00_MainContent_ddlterm option:selected").val() + "&nTSublevel=" + $("#ctl00_MainContent_room1 option:selected").val() + "&nTermSubLevel2=" + $("#ctl00_MainContent_room2 option:selected").val(), function (Result) {
                        console.log(Result);
                        if ($("#<%=userlist.ClientID%>") != undefined)
                            $("#<%=userlist.ClientID%>").empty();
                        $.each(Result, function (index) {
                            console.log(Result[index]);
                            // Create an Option object       
                            var opt = document.createElement("option");

                            // Assign text and value to Option object
                            opt.text = Result[index].TeacherFullName;
                            opt.value = Result[index].SEmp;

                            // Add an Option object to Drop Down List Box
                            document.getElementById('<%=userlist.ClientID%>').options.add(opt);


                        });

                        ddluserlist();
                    });

                }).fail(function (jqXHR, textStatus, errorThrown) {

                    if (errorThrown == "Request Timeout") {

                        bootbox.confirm({
                        title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                        message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132218") %></h>',
                        buttons: {
                       
                            confirm: {
                                label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                            }
                        },
                        callback: function (result) {
                                window.location.href = window.location.protocol + "//" + window.location.host + "/Default.aspx";
                            }
                        });
                    }
                
                });
            }
            else {

                // // Create an Empty Option object       
                //var opt = document.createElement("option");

                //// Assign text and value to Option object
                //opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %>";
                //opt.value = "";

                //// Add an Option object to Drop Down List Box
                //document.getElementById('#ctl00_MainContent_room2').options.add(opt);

            }
        }

        function ddlRoomChange(control) {
            var ddl2 = document.getElementsByClassName("ddl2");
            //Fetch Teachers from Plan on Room Selection
            $.get("/api/Plan/GetTeachersFromPlan?nYear=" + $("#ctl00_MainContent_ddlyear option:selected").val() + "&nTerm=" + $("#ctl00_MainContent_ddlterm option:selected").val() + "&nTSublevel=" + $("#ctl00_MainContent_room1 option:selected").val() + "&nTermSubLevel2=" + $("#ctl00_MainContent_room2 option:selected").val(), function (Result) {
                console.log(Result);
                if ($("#<%=userlist.ClientID%>") != undefined)
                    $("#<%=userlist.ClientID%>").empty();
                $.each(Result, function (index) {

                    // Create an Option object       
                    var opt = document.createElement("option");

                    // Assign text and value to Option object
                    opt.text = Result[index].TeacherFullName;
                    opt.value = Result[index].SEmp;

                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=userlist.ClientID%>').options.add(opt);

                });

                ddluserlist();
            });
        }

     function UpdateSettingExtraTime() {
           

            var settingExtraTimeRequest = {
                nTerm: $("#ctl00_MainContent_ddlterm").val(),
                nTermSubLevel2: $("#ctl00_MainContent_room2").val(),
                sPlaneID: $("#ctl00_MainContent_planlist").val(),
                sEMP:  $("#ctl00_MainContent_userlist").val(),
            };

          $.ajax({
        ContentType: "application/json; charset=utf-8",
        url: "/api/AssessmentScore/UpdateSettingExtraTime",
        type: "Post",
        data: settingExtraTimeRequest,
        success: function (response) {
           window.location = window.location;
        },
        error: function (error) {
            ShowPageError();
        }
    });
       
     };
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
                 <div class="col-xs-12 regis1" style="padding:5px;">
             <%--   <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>--%>
                                        <div class="column70">
                                            
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="ddlyear"  runat="server" onchange="ddlyear(this)"  width="80%" CssClass="width100 form-control ddlyear">                        
                                                        </asp:DropDownList>                                                    </div>
                                                </div>
                                            </div> 

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                    <asp:DropDownList ID="ddlterm" runat="server"  width="80%" CssClass="width100 form-control">                                
                                                       <%-- <asp:ListItem Enabled="true" Text="1" Value="1" class="grey"></asp:ListItem>
                                                        <asp:ListItem Text="2" value="2"  class="grey"></asp:ListItem>--%>
                                                    </asp:DropDownList>                                                    </div>
                                                </div>
                                            </div>  
                                            
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                        <asp:DropDownList ID="room1" runat="server" CssClass="width100 form-control ddl2" onchange="ddlclass(this)">
                                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>" Value="-1" class="hidden"></asp:ListItem>
                                                        </asp:DropDownList>                                                   </div>
                                                </div>
                                            </div> 

                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                    <asp:DropDownList ID="room2" runat="server" onchange="ddlRoomChange(this)"   width="80%" CssClass="width100 form-control ddl2"> 
                                                    </asp:DropDownList>                                                    </div>
                                                </div>
                                            </div>                                          
                                            
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206166") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                      <asp:DropDownList ID="userlist" runat="server" CssClass="width100 form-control ddluser" onchange="ddluserlist()">
                                                          <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206166") %>" Value="-1" class="hidden"></asp:ListItem>
                                                        </asp:DropDownList>                                                    </div>
                                                </div>
                                            </div> 

                                            <div class="col-xs-12 hidden">
                                                <asp:Textbox ID="Textbox1" runat="server" CssClass="ddlselect" width="80%"> </asp:Textbox>
                                                <asp:Textbox ID="Textbox2" runat="server" CssClass="" width="80%"> </asp:Textbox>
                                                </div>
                                                         
                                            <div class="form-group row student">
                                                <div class="col-md-12 col-sm-12">
                                                    <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206051") %></label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">
                                                      <asp:DropDownList ID="planlist" runat="server"  width="80%" CssClass="width100 form-control">                        
        
                                                      </asp:DropDownList> 
                                                        
                                                    </div>
                                                </div>
                                            </div>                                 
                                            
                                                                                                                              
                                        </div>
                                   <%-- </ContentTemplate>
                                    <Triggers>                                        
                                        <asp:AsyncPostBackTrigger ControlID="room1" EventName="SelectedIndexChanged" />                                     
                                        <asp:AsyncPostBackTrigger ControlID="userlist" EventName="SelectedIndexChanged" />   
                                        <asp:PostBackTrigger ControlID="btnSave" />
                                    </Triggers>
                </asp:UpdatePanel>--%>

            </div>
                
                
            </div>
           
           <div class="hid" style="font-size:30%">hidden</div>
        </div>
        <div class="modal-footer">
           <%-- <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" OnClientClick="" Style="width: 100px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />--%>
           
            <button type="button" class="btn btn-primary global-btn" style="width:100px;" onclick="UpdateSettingExtraTime()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
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
          <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206173") %></h2>
        </div>
        <div class="modal-body">
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3 righttext">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101295") %></label>
                    </div>
                <div class="col-xs-9">
                   <asp:Textbox ID="deleteName" enabled="false" runat="server" CssClass="deleteName modalsize" width="80%"> </asp:Textbox>
                </div>
            </div>

            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3 righttext">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
                    </div>
                <div class="col-xs-9">
                   <asp:Textbox ID="deletePlan" enabled="false" runat="server" CssClass="deletePlanname modalsize" width="80%"> </asp:Textbox>
                </div>
            </div>

            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3 righttext">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                    </div>
                <div class="col-xs-9">
                   <asp:Textbox ID="deleteDate" enabled="false" runat="server" CssClass="deleteDate modalsize" width="80%"> </asp:Textbox>
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
            
           <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206168") %> <br>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00004") %>)</label>
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
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>                                                                            
            <asp:BoundField DataField="name"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>" ItemStyle-CssClass="lefttext modalname" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>   
                            <asp:BoundField DataField="planName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206170") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="25%"></HeaderStyle>
                            </asp:BoundField>                                                                            
            <asp:BoundField DataField="room"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %>" ItemStyle-CssClass="centertext modalname" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>       
                            <asp:BoundField DataField="Date"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" ItemStyle-CssClass="centertext modalname" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>                   
                                  <asp:BoundField DataField="status"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>" ItemStyle-CssClass="centertext statustxt" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="12%"></HeaderStyle>
                            </asp:BoundField>  
                                   
  
                            <asp:TemplateField HeaderText="">
                               <ItemTemplate>                                    
                                  <div class="col-md-12 col-sm-12">                                                                                              
                                    <div class="col-lg-4 col-md-4 col-sm-4 nounder statusmode">
                                        <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206161") %>">
                                                       <div   class="glyphicon glyphicon-remove"onclick="editModal('<%#Eval("dataID")%>','<%#Eval("name")%>','<%#Eval("planName")%>','<%#Eval("Date")%>')" style=" font-size: 70%; cursor:pointer; color:red; font-size:70%" data-toggle="modal" data-target="#modalDelete"> 
                                                     </div>     </div>    
                                    </div>
                                </div>                                                                                                     
                                </ItemTemplate>
                                <HeaderTemplate>
                            <div class="btn btn-success" style="margin-right:10px;" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206163") %></div>
                        </HeaderTemplate>
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
