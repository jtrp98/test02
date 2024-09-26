<%@ Page EnableEventValidation="true" Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="settingTimePeriod.aspx.cs" Inherits="FingerprintPayment.Setting.settingTimePeriod"  %>

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
        .righttext{
            text-align:right;
        }
        .lefttext{
            text-align:left;
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
        .centertext{
            text-align:center;
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
            $(function () {
                $("#beforeMid1").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#beforeMid2").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#duringMid1").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#duringMid2").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#afterMid1").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#afterMid2").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#final1").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#final2").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#extra1").datepicker({ dateFormat: 'dd/mm/yy' });
                $("#extra2").datepicker({ dateFormat: 'dd/mm/yy' });
            });

            var dateToday1 = new Date();
            var dates1 = $("#beforeMid1, #beforeMid2").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                numberOfMonths: 2,
                minDate: dateToday1,
                onSelect: function (selectedDate) {
                    var option = this.id == "beforeMid1" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    dates1.not(this).datepicker("option", option, date);
                }
            });

            var dateToday2 = new Date();
            var dates2 = $("#duringMid1, #duringMid2").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                numberOfMonths: 2,
                minDate: dateToday2,
                onSelect: function (selectedDate) {
                    var option = this.id == "duringMid1" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    dates2.not(this).datepicker("option", option, date);
                }
            });

            var dateToday3 = new Date();
            var dates3 = $("#afterMid1, #afterMid2").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                numberOfMonths: 2,
                minDate: dateToday3,
                onSelect: function (selectedDate) {
                    var option = this.id == "afterMid1" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    dates3.not(this).datepicker("option", option, date);
                }
            });

            var dateToday4 = new Date();
            var dates4 = $("#final1, #final2").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                numberOfMonths: 2,
                minDate: dateToday4,
                onSelect: function (selectedDate) {
                    var option = this.id == "final1" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    dates4.not(this).datepicker("option", option, date);
                }
            });

            var dateToday5 = new Date();
            var dates5 = $("#extra1, #extra2").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                numberOfMonths: 2,
                minDate: dateToday5,
                onSelect: function (selectedDate) {
                    var option = this.id == "extra1" ? "minDate" : "maxDate",
                        instance = $(this).data("datepicker"),
                        date = $.datepicker.parseDate(instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    dates5.not(this).datepicker("option", option, date);
                }
            });
        });

        

        $(document).ready(function () {

            var full = window.location.href;
            var half = full.split('?');
            if (half[1] != null)
            {
                var split = half[1].split('&');
                var term = split[1].split('=');
                var year = split[0].split('=');
                var pagedata = document.getElementsByClassName("pagedata");

                if (Number(term[1] != 0) && Number(year[1] != 0))
                    pagedata[0].classList.remove('hidden');
            }
         

            
        });

        $(document).ready(function () {
            //ddlyear();
            
            
            $('input[id*=btnSearch]').click(function () {
                var load = document.getElementsByClassName("load");
                
                load[0].classList.remove('hidden');              
                              
                var param3var = $('select[id*=DropDownList1] option:selected').val();               
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                if (param4var == undefined)
                    param4var = "";
                window.location.href = "settingTimePeriod.aspx?year=" + param3var + "&term=" + param4var;

                
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
        
         <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206160") %></h2>
        </div>
        <div class="modal-body">
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206156") %></label>
                    </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402003") %></label>
                    </div>
                <div class="col-xs-3">
                   <asp:TextBox ID="beforeMid1" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                    </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="beforeMid2" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                    </div>
            </div>
            
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106099") %></label>
                    </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402003") %></label>
                    </div>
                <div class="col-xs-3">
                   <asp:TextBox ID="duringMid1" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                    </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="duringMid2" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                    </div>
            </div>
          
            

            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206157") %></label>
                    </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402003") %></label>
                    </div>
                <div class="col-xs-3">
                   <asp:TextBox ID="afterMid1" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                    </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="afterMid2" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                    </div>
            </div>
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106100") %></label>
                    </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402003") %></label>
                    </div>
                <div class="col-xs-3">
                   <asp:TextBox ID="final1" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                    </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="final2" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                    </div>
            </div>
            <div class="col-xs-12" style="padding:5px;">
                <div class="col-xs-3">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206158") %></label>
                    </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M402003") %></label>
                    </div>
                <div class="col-xs-3">
                   <asp:TextBox ID="extra1" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
                </div>
                <div class="col-xs-1">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                    </div>
                <div class="col-xs-3">
                    <asp:TextBox ID="extra2" runat="server" ClientIdMode="static" CssClass="form-control" Width="100%"/>
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


        <div class="form-group row student">
            

            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList1"  runat="server" OnChange="ddlyear()" CssClass="ddl1 form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddl1 form-control">
                         <%-- <asp:ListItem Enabled="true" Text="1" Value="1" class="grey"></asp:ListItem>
                           <asp:ListItem Text="2" value="2"  class="grey"></asp:ListItem>--%>
                            </asp:DropDownList>
                </div>
            </div>
        </div>
        
        
        
       
        <div class="row">
            <div class="col-xs-12 button-section">
                <input type="button" id="btnSearch" class='btn btn-info search-btn' class="input--mid" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>
        <hr/>
        <div class="row mini--space__top pagedata hidden">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <div class="col-md-12 col-sm-12 centertext">
                <asp:Label ID="header" runat="server" class="centertext">
                    </asp:Label>
                
            </div> 
                    <div class="col-xs-2">
                        </div>
                    <div class="col-md-9 col-sm-9">
                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206156") %></label>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input centertext">
                    <asp:Label ID="mid1" runat="server" class="">
                    </asp:Label>
                </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-input">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>                   
                </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input lefttext">
                    <asp:Label ID="mid2" runat="server" class="">
                    </asp:Label>
                </div>
            </div> 
                     <div class="col-xs-2">
                        </div>
                    <div class="col-md-9 col-sm-9">
                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106099") %></label>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input centertext">
                    <asp:Label ID="mid3" runat="server" class="">
                    </asp:Label>
                </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-input">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>                   
                </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input lefttext">
                    <asp:Label ID="mid4" runat="server" class="">
                    </asp:Label>
                </div>
            </div> 
                    <div class="col-xs-2">
                        </div>
                    <div class="col-md-9 col-sm-9">
                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206157") %></label>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input centertext">
                    <asp:Label ID="mid5" runat="server" class="">
                    </asp:Label>
                </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-input">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>                   
                </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input lefttext">
                    <asp:Label ID="mid6" runat="server" class="">
                    </asp:Label>
                </div>
            </div> 
                     <div class="col-xs-2">
                        </div>
                    <div class="col-md-9 col-sm-9">
                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106100") %></label>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input centertext">
                    <asp:Label ID="fin1" runat="server" class="">
                    </asp:Label>
                </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-input">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>                   
                </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input lefttext">
                    <asp:Label ID="fin2" runat="server" class="">
                    </asp:Label>
                </div>
            </div> 
                    <div class="col-xs-2">
                        </div>
                    <div class="col-md-9 col-sm-9">
                <label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206158") %></label>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input centertext">
                    <asp:Label ID="ex1" runat="server" class="">
                    </asp:Label>
                </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 control-input">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>                   
                </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input lefttext">
                    <asp:Label ID="ex2" runat="server" class="">
                    </asp:Label>
                </div>
            </div> 
                </div>
            </div>
            <div class="row">
            <div class="col-xs-12 button-section">
                <div class="btn btn-success" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206159") %></div>
            </div>
        </div>
        </div>
         
         
    </div>
    
</asp:Content>
