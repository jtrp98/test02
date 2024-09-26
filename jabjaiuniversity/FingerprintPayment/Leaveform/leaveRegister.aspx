<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="leaveRegister.aspx.cs" Inherits="FingerprintPayment.leaveRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .contentBox {
            margin: 0 auto;
            width: 120%;
        }

        .contentBox .column70 {
            float: left;
            margin: 0;
            width: 55%;
        }

        .contentBox .column50 {
            float: left;
            margin: 0;
            width: 45%;
        }
        .oneline {
   
    white-space: nowrap;
}
         .noborder
{
   border-style:none;
   text-decoration:none;
    text-shadow:none !important;
    box-shadow: inset 0px 0px 0px 0px red;
}
        .circle-cropper {
  
  background-repeat: no-repeat;
    background-position: 50%;
    border-radius: 50%;
    width: 100px;
    height: 100px;
}
        html,body
{
    width: 100%;   
    margin: 0px;
    padding: 0px;
    overflow-x: hidden; 
}
        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }
        .contentBox .column30 {
            float: left;
            margin: 0;
            width: 45%;
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
            color: yellow;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }
        .listItem {
            color: blue;
            background-color: White;
        }
        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
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
    <script type="text/javascript">
    function Radio_Click8() {
        var radio1 = document.getElementById("<%=RadioOther.ClientID %>");
	    var textBox = document.getElementById("<%=txtOther.ClientID %>");
        document.getElementById('<%= txtOther.ClientID %>').value = "";
        textBox.disabled = !radio1.checked;
        textBox.focus();
    }
        </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTEmployees" ServicePath="AutoCompleteService.asmx"
        EnableCaching="true" FirstRowSelected="true" CompletionListCssClass="completionList"
        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content ">        
        <div class="form-group row ">
            <div class="col-sm-12 text-center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102245") %></label>
            </div>
            <div class="col-md-12 col-sm-12 hid">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        hidden</label>
                </div>               
            </div>
            <div class="form-group row ">                
				<div class="col-md-12 col-sm-12">
				    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">						
					</div>
					<label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
						สถานศึกษา</label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
						<asp:Label ID="labelSchool" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
					</div>
				</div>                            
			</div>
            <div class="form-group row">                
				<div class="col-md-12 col-sm-12">
				    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">						
					</div>
					<label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label righttext noborder">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
						<asp:Label ID="labelDate" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
					</div>
				</div>                            
			</div>
            <div class="form-group row ">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132332") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
						<asp:TextBox ID="txtHeader" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="form-group row ">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext ">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105071") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 ">
						<asp:Label ID="labelName" runat="server" CssClass='form-control noborder' class="input--mid"></asp:Label>
					</div>
				</div>                            
			</div>
            
            
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105076") %></label>
					<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 control-input">
									<asp:RadioButton ID="RadioSick" runat="server"   Text = "&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206449") %>" GroupName = "Radio8" onclick = "Radio_Click8()" />
                                    <asp:RadioButton ID="RadioBusiness" runat="server"   Text = "&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132333") %>" GroupName = "Radio8" onclick = "Radio_Click8()" />
                                    <asp:RadioButton ID="RadioSon" runat="server"   Text = "&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132334") %>" GroupName = "Radio8" onclick = "Radio_Click8()" />
                                    
									</div>
				</div>                            
			</div>
            <div class="form-group row student ">
							<div class="col-md-12 col-sm-12">
								<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
									</label>
								<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
									<asp:RadioButton ID="RadioOther" runat="server" Text = "&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133523") %>" GroupName = "Radio8" onclick = "Radio_Click8()" />        								
									</div>                                
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-input">
									<asp:TextBox ID="txtOther" runat="server" CssClass='form-control' class="input--mid" Enabled = "false"></asp:TextBox>
								</div>

							</div>                            
						</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105073") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
						<asp:TextBox ID="txtReason" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105075") %></label>
					<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
								<asp:DropDownList ID="startDate" runat="server" CssClass="width100 form-control" >
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
								<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
								<asp:DropDownList ID="startMonth" runat="server" CssClass="width100 form-control">
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
								<asp:DropDownList ID="startYear" runat="server" CssClass="width100 form-control">
									<asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>								
								</asp:DropDownList>								
								</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105040") %></label>
					<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
								<asp:DropDownList ID="endDate" runat="server" CssClass="width100 form-control" >
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
								<div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-input">
								<asp:DropDownList ID="endMonth" runat="server" CssClass="width100 form-control">
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
								<asp:DropDownList ID="endYear" runat="server" CssClass="width100 form-control">
									<asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102202") %>" Value="-1" class="grey hidden"></asp:ListItem>								
								</asp:DropDownList>								
								</div>
				</div>                            
			</div>
            <div class="col-md-12 col-sm-12 hid">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        hidden</label>
                </div>               
            </div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102252") %></label>					
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
						<asp:TextBox ID="txtHomenumber" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
						<asp:TextBox ID="txtRoad" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105081") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
						<asp:TextBox ID="txtTumbon" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
						<asp:TextBox ID="txtAumpher" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
								<asp:DropDownList ID="Ddlprovince" runat="server" CssClass="width100 form-control">
								    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>" Value="-1" class="grey hidden"></asp:ListItem>									
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133002") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133002") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133003") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133003") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133004") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133004") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133005") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133005") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133006") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133006") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133007") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133007") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133008") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133008") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133010") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133010") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133011") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133011") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133012") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133012") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133009") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133009") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133014") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133014") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133013") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133013") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133015") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133015") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133016") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133016") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133017") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133017") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133020") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133020") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133022") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133022") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133024") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133024") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133027") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133027") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133029") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133029") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133032") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133032") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ะเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132942") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ะเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132942") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133040") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133040") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133045") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133045") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133061") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133061") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133059") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133059") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133064") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133064") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133069") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133069") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133070") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133070") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133076") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133076") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133072") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133072") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133073") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133073") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133074") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133074") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133075") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133075") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %>"></asp:ListItem>
									<asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>" Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>"></asp:ListItem>
									

								</asp:DropDownList>								

								</div>
				</div>                            
			</div>
            <div class="form-group row">
				<div class="col-md-12 col-sm-12">
					<label class="col-lg-3 col-md-3 col-sm-3 col-xs-3 control-label righttext">
						<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %></label>
					<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
						<asp:TextBox ID="txtPhone" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
					</div>
				</div>                            
			</div>
            <div class="col-md-12 col-sm-12 hid">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>
                        hidden</label>
                </div>               
            </div>
            <div class="row form-group">
            <div class="col-sm-12 text-center">
                <asp:Button ID="Button1" class="btn btn-primary global-btn"  Style="width: 30%;"
                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132335") %>"  />
                
            </div>
        </div>
        </div>
        
    </div>
    
</asp:Content>
