<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="SchoolReportTeacherDescribe.aspx.cs" Inherits="FingerprintPayment.grade.SchoolReportTeacherDescribe" %>
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

            .select2-selection__arrow b {
                border-color: black transparent transparent transparent !important;
            }

        [class^='select2'] {
            border-radius: 1px !important;
            border-top-color: #abadb3 !important;
            border-left-color: #dbdfe6 !important;
            border-right-color: #dbdfe6 !important;
            border-bottom-color: #dbdfe6 !important;
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

        .statusOnline {
            background-color: palegreen;
            color: black;
        }

        .statusOffline {
            background-color: hotpink;
            color: black;
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

        .statusbox3 {
            width: 100px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
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

        .width10 {
            margin: 0 auto;
            width: 10%;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        .smol {
            font-size: 80%;
        }

        .nounder a:hover {
            text-decoration: none;
        }

        .shadowblack {
            text-decoration: none;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .boxhead a {
            color: #FFFFFF;
            text-decoration: none;
        }

        .attendancebox {
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        a.imjusttext {
            color: #ffffff;
            text-decoration: none;
        }

            a.imjusttext:hover {
                color: aquamarine;
            }

        .btn-red {
            background: red; /* use your color here */
        }


        .nowrap {
            max-width: 100%;
            white-space: nowrap;
        }

        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }

        .tab {
            border-collapse: collapse;
            margin-left: 6px;
            margin-right: 6px;
            border-bottom: 3px solid #337AB7;
            border-left: 3px solid #337AB7;
            border-right: 3px solid #337AB7;
            border-top: 3px solid #337AB7;
            box-shadow: inset 0 1px 0 #337AB7;
        }
    </style>
   

    <script type="text/javascript" language="javascript">


       

        function editModal(number,word) {
            var editName = document.getElementsByClassName("editName");
            var editPlanID = document.getElementsByClassName("editPlanID");
           


            var deleteName = document.getElementsByClassName("deleteName");
            var deletePlanID = document.getElementsByClassName("deletePlanID");
            
            editName[0].value = word;
            editPlanID[0].value = number;
            deleteName[0].value = word;
            deletePlanID[0].value = number;
          
        }

     


    </script>

    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />


        <div class="form-group row student">

            <div class="col-xs-12 centerText">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206176") %></label>
                </div>
            

        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206175") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206094") %></label>
                            </div>
                            <div class="col-xs-9">
                                <asp:TextBox ID="savetxt" runat="server" Width="100%" TextMode="MultiLine" Rows="3"> </asp:TextBox>
                            </div>
                        </div>

                       

                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206177") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206094") %></label>
                            </div>
                            <div class="col-xs-9">
                                <asp:TextBox ID="edittxt" runat="server" CssClass="editName" Width="100%"> </asp:TextBox>
                            </div>
                        </div>

                        
                        <div class="col-xs-12 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>planID</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="Textbox3" runat="server" CssClass="editPlanID"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="editBtn" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalDelete" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206178") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="col-xs-12" style="padding: 5px;">
                            <div class="col-xs-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206094") %></label>
                            </div>
                            <div class="col-xs-9">
                                <asp:TextBox ID="deleteName" Enabled="false" runat="server" CssClass="deleteName" Width="100%"> </asp:TextBox>
                            </div>
                        </div>
                        

                        <div class="col-xs-12 hidden" style="padding: 5px;">
                            <div class="col-xs-5">
                                <label>planID</label>
                            </div>
                            <div class="col-xs-7">
                                <asp:TextBox ID="deleteID" runat="server" CssClass="deletePlanID"> </asp:TextBox>
                            </div>
                        </div>


                        <div class="hid" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-danger global-btn" ID="deleteBtn" runat="server" Style="width: 100px;"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>" />
                        <button type="button" class="btn btn-default" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        OnDataBound="CustomersGridView_DataBound" ShowHeaderWhenEmpty="true"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                            BackColor="#337AB7" />

                        <PagerTemplate>

                            <table width="100%" class="tab">
                                <tr>
                                    <td style="width: 25%">

                                        <asp:Label ID="Label1" BorderColor="#337AB7"
                                            ForeColor="white"
                                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:"
                                            runat="server" />
                                        <asp:DropDownList ID="PageDropDownList2"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2"
                                            runat="server" />

                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="backbutton" runat="server"
                                            CssClass="imjusttext" OnClick="backbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>
                                        </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged"
                                            runat="server" />
                                        <asp:LinkButton ID="nextbutton" runat="server"
                                            CssClass="imjusttext" OnClick="nextbutton_Click">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                        </asp:LinkButton>

                                    </td>

                                    <td style="width: 70%; text-align: right">

                                        <asp:Label ID="CurrentPageLabel"
                                            ForeColor="white"
                                            runat="server" />

                                    </td>

                                </tr>
                            </table>

                        </PagerTemplate>

                        <Columns>
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>

                            
                            <asp:BoundField DataField="describe" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206094") %>" ItemStyle-CssClass="lefttext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="60%"></HeaderStyle>
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="id" HeaderText="id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:BoundField>
                            
                            <asp:TemplateField ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext" HeaderText="">
                                <HeaderStyle Width="1%"></HeaderStyle>
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 " style="padding: 0px;">
                                        <input type="text" class="classID" style="width: 1%; font-size: 1%; border: 0px; background-color: white;" disabled="disabled" value="<%# Eval("id") %>">
                                    </div>
                                    </div>       
                                </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12">
                                        
                                        <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %>">
                                                <div class="glyphicon glyphicon-edit" onclick="editModal('<%# Eval("id") %>','<%# Eval("describe") %>')" style="font-size: 70%; cursor: pointer;" data-toggle="modal" data-target="#myModal2">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 nounder">
                                            <div title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>">
                                                <div class="glyphicon glyphicon-remove" onclick="editModal('<%# Eval("id") %>','<%# Eval("describe") %>')" style="font-size: 70%; cursor: pointer; color: red; font-size: 70%" data-toggle="modal" data-target="#modalDelete">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="btn btn-success" style="margin-left: 25px;" data-toggle="modal" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206175") %></div>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
