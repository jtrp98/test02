<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="leaveSetting-checker.aspx.cs" Inherits="FingerprintPayment.leaveSetting_checker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
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
        .centertext {
            text-align: center;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTEmployees" ServicePath="AutoCompleteService.asmx"
        EnableCaching="true" FirstRowSelected="true" CompletionListCssClass="completionList"
        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content employeeslist-container">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-md-12 center">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301045") %></label>
            </div>
        </div>
        <div class="row form-group">
                <div class="col-sm-12 text-center hid">
                    <label>hidden</label>
                    
                </div>
            </div>
        <div class="row form-group">
            <div class="col-md-12 col-sm-12">
                <div class="col-lg-7 col-md-7 col-sm-7 righttext" >
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301048") %></label>
                </div> 
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <asp:DropDownList ID="ddlAmount" runat="server" CssClass="width100 form-control">
									<asp:ListItem Enabled="true" Text="1" Value="1" ></asp:ListItem>
									<asp:ListItem Text="2" Value="2"></asp:ListItem>
									<asp:ListItem Text="3" Value="3"></asp:ListItem>																	
								</asp:DropDownList>	
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1">
                    <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></label>
                </div>
            </div>
        </div>
        <div class="row form-group">
                <div class="col-sm-12 text-center">
                    <asp:Button ID="SubmitButton" class="btn btn-success global-btn"  Style="width: 20%;"
                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>"  />
                    <asp:Button ID="BackButton" class="btn btn-primary global-btn "  Style="width: 20%;"
                runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>"  />
                </div>
            </div>
        
    </div>
    
</asp:Content>


