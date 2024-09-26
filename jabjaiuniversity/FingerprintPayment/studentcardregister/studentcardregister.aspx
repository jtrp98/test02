<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="studentcardregister.aspx.cs" EnableEventValidation="false" Inherits="FingerprintPayment.studentCard.studentcardregister" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/TeacherStudentAutocomplete.ascx" TagPrefix="uc1" TagName="TeacherStudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link href="../Content/Material/assets/css/toggle.css" rel="stylesheet" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/css/fileinput.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer-fa/theme.min.css" rel="stylesheet" />
    <style>
        /* .text-mutedx {
            font-size: 20px;
        }*/

        /*    label {
            font-weight: normal;
            font-size: 26px;
        }*/

        .dropdown.bootstrap-select {
            padding: 0px;
        }

        table.dataTable td,
        table.dataTable th {
            border: 0px solid #fff;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .modal-body div.row > div.col-md-3 {
            padding-top: 6px;
        }

        .file-footer-buttons .kv-file-upload,
        .file-footer-buttons .kv-file-zoom {
            display: none !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-mutedx" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106001") %>           
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">

                        <div class=" row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlType" ClientIDMode="Static" runat="server" CssClass="selectpicker col-md-12" data-style="select-with-transition" Style="width: 100%" Width="100%">
                                    <asp:ListItem Selected="True" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>" Value="1" />
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %>" Value="2" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-7"></div>
                        </div>
                        <div class="row ">
                            <div class="col-xs-12 d-none">
                                <div class="col-xs-2 ">
                                    <label>ddl2</label>
                                </div>
                                <div class="col-xs-4">
                                    <asp:TextBox ID="txtddl2" runat="server" CssClass="d2" Width="50%"> </asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class=" row student">
                            <div class="col-md-1"></div>
                            <label class="col-md-1  col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="selectpicker " data-style="select-with-transition" data-width="100%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-md-3 ">
                                <asp:DropDownList ID="ddlsublevel2" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="ddl2 selectpicker " data-style="select-with-transition" data-width="100%">
                                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>
                        </div>

                        <div class="row ">
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-md-3">
                                <uc1:TeacherStudentAutocomplete runat="server" ID="TeacherStudentAutocomplete" />
                            </div>
                            <div class="col-md-1"></div>
                            <label class="col-md-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106003") %></label>
                            <div class="col-md-3 ">
                                <asp:TextBox ID="txtNFC" runat="server" ClientIDMode="Static" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106004") %>" Width="100%" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <br />
                                <button type="button" id="btnSearch" class="btn btn-info ">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>

                                <button type="button" id="btnModalImport" class="btn btn-success " data-toggle="modal" data-target="#myModalImport">
                                    <span class="btn-label">
                                        <i class="material-icons">download</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106005") %>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">
                    <div class="card-header card-header-warning card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102145") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="wrapper-table">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:LinkButton runat="Server" ID="btnUpdate" Text="Save" OnClick="btnUpdate_Click" CssClass="d-none" />
                                            <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="False"
                                                BorderWidth="0"
                                                OnRowDataBound="dgd_RowDataBound"
                                                OnDataBound="CustomersGridView_DataBound"
                                                CssClass="table-hover dataTable ">
                                                <%-- AllowPaging="true"
                                                PageSize="20"--%>
                                                <%-- OnPageIndexChanging="dgd_PageIndexChanging"
                                                PagerSettings-Mode="NumericFirstLast"
                                                PagerStyle-CssClass="dataTables_paginate paging_simple_numbers"--%>

                                                <%--<RowStyle BorderWidth="0" />--%>

                                                <%-- <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />--%>
                                                <%--<PagerStyle ForeColor="#337AB7" BorderColor="#337AB7"
                                                    BackColor="#337AB7" />--%>
                                                <%--  <PagerTemplate>
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
                                                </PagerTemplate>--%>
                                                <Columns>
                                                    <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <HeaderStyle Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sIdentification" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %>" HeaderStyle-CssClass="text-center">
                                                        <HeaderStyle Width="25%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106006") %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <div class="slot-card1" data-sid="<%#Eval("sId") %>">
                                                                <%# GetToggleButton(Eval("Card")+"",Eval("StatusCard")+"",Eval("sId")+"","1") %>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106007") %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <div class="slot-card2" data-sid="<%#Eval("sId") %>">
                                                                <%# GetToggleButton(Eval("Card")+"",Eval("StatusCard")+"",Eval("sId")+"","2") %>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106008") %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <div class="slot-card3" data-sid="<%#Eval("sId") %>">
                                                                <%# GetToggleButton(Eval("Card")+"",Eval("StatusCard")+"",Eval("sId")+"","3") %>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div class="btn btn-success" style="" data-toggle="modal" onclick="OpenStudentCardDialog('<%# Eval("sIdentification") %>','<%# ((string)Eval("sName")).Replace("'", "\\'") %>','<%# Eval("sId") %>',''
                                          ,'<%# (Eval("Card") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("Card") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("Card") + "").Split(',').ElementAtOrDefault(2) %>'

                                          ,'<%# (Eval("EnCard") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("EnCard") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("EnCard") + "").Split(',').ElementAtOrDefault(2) %>'

                                          ,'<%# (Eval("CardRe") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("CardRe") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("CardRe") + "").Split(',').ElementAtOrDefault(2) %>'
                                                                                                
                                          ,'<%# (Eval("EnCardRe") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("EnCardRe") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("EnCardRe") + "").Split(',').ElementAtOrDefault(2) %>'

                                          ,'<%# (Eval("UpdateBy") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("UpdateBy") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("UpdateBy") + "").Split(',').ElementAtOrDefault(2) %>'

                                          ,'<%# (Eval("UpdateDate") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("UpdateDate") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("UpdateDate") + "").Split(',').ElementAtOrDefault(2) %>'
                                          
                                          ,'<%# (Eval("FreeText") + "").Split(',').ElementAtOrDefault(0) %>'
                                          ,'<%# (Eval("FreeText") + "").Split(',').ElementAtOrDefault(1) %>'
                                          ,'<%# (Eval("FreeText") + "").Split(',').ElementAtOrDefault(2) %>'
                                          )"
                                                                data-target="#myModal">
                                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %>
                                                            </div>
                                                            <%-- <div class="btn btn-warning" style="" data-toggle="modal" onclick="UpdateStudentCard('<%# Eval("sstudentid") %>','<%# Eval("sName") %>','<%# Eval("stdId") %>','<%# Eval("NFC") %>')" data-target="#myModal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203036") %></div>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%--     <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" CssClass="headerCell" />
                                                <RowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" CssClass="itemCell" />
                                                <SelectedRowStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" />--%>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106009") %></h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12" style="">
                            <div id="panel-error" class="col-md-12 text-center alert alert-danger" style="display: none">
                                <%-- <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="has-errorxx" Text="" runat="server" />--%>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></span>
                                </div>
                                <div class="col-md-9">
                                    <asp:HiddenField runat="server" ID="hdnStdId" Value="10" ClientIDMode="Static" />
                                    <asp:Label ID="lblStudentCode" ClientIDMode="Static" BorderColor="#337AB7" Text="" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                        </div>
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></span>
                                </div>
                                <div class="col-md-9">
                                    <asp:Label ID="lblStudentSurName" ClientIDMode="Static" BorderColor="#337AB7" Text="" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106010") %></span>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106013") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <asp:TextBox ID="txtStudentCardNumber1" runat="server" placeholder="Student Card Number" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;" onclick="$('#txtStudentCardNumber1').val('').prop('disabled', false);"><i class="fa fa-remove text-danger"></i></a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106015") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtEncard1" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106014") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtNFCReverse1" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106016") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtEnCardReverse1" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="spanRemark1" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style="">Note</span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <asp:TextBox ID="txtFreeText1" runat="server" placeholder="Note" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106011") %></span>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106013") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <asp:TextBox ID="txtStudentCardNumber2" runat="server" placeholder="Student Card Number" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;" onclick="$('#txtStudentCardNumber2').val('').prop('disabled', false);"><i class="fa fa-remove text-danger"></i></a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106015") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtEncard2" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106014") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtNFCReverse2" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106016") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtEnCardReverse2" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="spanRemark2" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style="">Note</span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <asp:TextBox ID="txtFreeText2" runat="server" placeholder="Note" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106012") %></span>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106013") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <asp:TextBox ID="txtStudentCardNumber3" runat="server" placeholder="Student Card Number" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;" onclick="$('#txtStudentCardNumber3').val('').prop('disabled', false);"><i class="fa fa-remove text-danger"></i></a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106015") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtEncard3" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">

                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106014") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtNFCReverse3" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106016") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="txtEnCardReverse3" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                    <label></label>
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %></span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <span id="spanRemark3" class="form-control" style="color: #aaa">&nbsp;</span>
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-12" style="">
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <span class="text-mutedx " style="">Note</span>
                                </div>
                                <div class="col-md-6" style="display: table;">
                                    <asp:TextBox ID="txtFreeText3" runat="server" placeholder="Note" ClientIDMode="static" CssClass="form-control " Width="100%" MaxLength="20" />
                                    <a style="width: 41px; cursor: pointer; display: table-cell; vertical-align: middle; padding-left: 5px;">&nbsp;</a>
                                </div>
                            </div>
                        </div>

                        <div class="d-none" style="font-size: 30%">hidden</div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAddStudentCard" class="btn btn-success" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" OnClientClick="return ValidateStudentCard()" UseSubmitBehavior="false" Style="" />
                        <button type="button" class="btn btn-danger" id="btnUpdateStudentCard" runat="server" data-dismiss="modal" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModalImport" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106005") %> <span id="importType"></span></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <input type="file" id="fileImport" name="fileImport" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" data-msg-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106018") %>" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mt-3">

                                <div id="alertErrorImport" class="alert alert-danger" role="alert" style="display: none">
                                </div>

                                <div id="alertWarningImport" class="alert alert-warning" role="alert" style="display: none">
                                </div>

                                <div id="alerDupImport" class="alert alert-warning" role="alert" style="display: none">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer justify-content-center">

                        <button type="button" id="btnImport" class="btn btn-success " onclick="onImport()">
                            <span class="btn-label">
                                <i class="material-icons">download</i>
                            </span>
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206392") %>
                        </button>

                    </div>
                </div>
            </div>
        </div>
        <%--  <div class="full-card box-content userlist-container d-none">
            <div id="loading" class="hidden load"></div>
        </div>--%>
    </form>
    <%--   <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js" type="text/javascript"></script>--%>

    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/js/fileinput.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.0.8/themes/explorer-fa/theme.min.js"></script>

    <script type="text/javascript" language="javascript">

        function pageLoad() {
            //$('.card-status-toggle').bootstrapToggle();
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(onEachRequest);
        prm.add_endRequest(endRequest);

        function onEachRequest(sender, args) {
            ////$("body").mLoading('show');
            $("body").mLoading('show');
        }
        function endRequest(sender, args) {
            //Do all what you want to do in jQuery ready function
            $("body").mLoading('hide');
        }

        var availableValueplane = [];
        var $fileImport;
        $(document).ready(function () {

            $.fn.inputFilter = function (inputFilter) {
                return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                    if (inputFilter(this.value)) {
                        this.oldValue = this.value;
                        this.oldSelectionStart = this.selectionStart;
                        this.oldSelectionEnd = this.selectionEnd;
                    } else if (this.hasOwnProperty("oldValue")) {
                        this.value = this.oldValue;
                        this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                    } else {
                        this.value = "";
                    }
                });
            };

            $fileImport = $('#fileImport').fileinput({
                uploadUrl: "#",
                //append: true,
                //purifyHtml: true,
                //uploadAsync: true,
                maxFileCount: 1,
                maxFileSize: 5120,
                msgSizeTooLarge: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %> "{name}"(<b>{size}</b>) <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133338") %> <b>{maxSize}</b>. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132756") %>!',
                theme: 'explorer-fa',
                /* showCaption: false,*/
                showRemove: false,
                showUpload: false,
                dropZoneEnabled: false,
                //showBrowse: false,
                //dropZoneEnabled: true,
                //overwriteInitial: false,
                //initialPreviewAsData: true,
                allowedFileExtensions: ['xls', 'xlsx'],
            });

            $('#ddlType').on('change', function (e) {
                if ($(this).val() == "2") {
                    $('.row.student').hide();
                    //$("#ctl00_MainContent_tags2").show();
                    //$("#ctl00_MainContent_tags").hide();
                }
                else {
                    $('.row.student').show();
                    //$("#ctl00_MainContent_tags").show();
                    //$("#ctl00_MainContent_tags2").hide();
                }

            });

            $('#btnSearch').click(function () {

                if ($('#ddlType').val() == "1") {

                    var type = $('#ddlType option:selected').val();
                    var nfc = $('#txtNFC').val();
                    var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                    var param2var = $('select[id*=ddlsublevel2] option:selected').val();
                    if (param2var == undefined)
                        param2var = "";
                    window.location.href = "studentcardregister.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&name=" + TSAC.GetUserName() + "&type=" + type + '&nfc=' + nfc;
                }
                else {

                    var type = $('#ddlType option:selected').val();
                    var nfc = $('#txtNFC').val();
                    var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                    var param2var = $('select[id*=ddlsublevel2] option:selected').val();
                    if (param2var == undefined)
                        param2var = "";
                    window.location.href = "studentcardregister.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&name=" + TSAC.GetUserName() + "&type=" + type + '&nfc=' + nfc;
                }
                //$("body").mLoading('hide');
            });

            $("#myModal").on('shown.bs.modal', function () {
                //$(this).find('#txtStudentCardNumber1').focus();
                //$(this).find('#txtStudentCardNumber2').focus();
                //$(this).find('#txtStudentCardNumber3').focus();
                if ($("input[id$=txtStudentCardNumber1]").val() == "") {
                    $("input[id$=txtStudentCardNumber1]").focus();
                }
                else if ($("input[id$=txtStudentCardNumber2]").val() == "") {
                    $("input[id$=txtStudentCardNumber2]").focus();
                }
                else if ($("input[id$=txtStudentCardNumber3]").val() == "") {
                    $("input[id$=txtStudentCardNumber3]").focus();
                }

                //$('#ctl00_MainContent_btnAddStudentCard').prop('disabled', true);
            });

            $('.wrapper-table').on('change', '.switch-button', function () {
                var $this = $(this);
                $("body").mLoading();

                PageMethods.SwitchCardStatus($this.data('sid'), $this.data('index'), $this.prop('checked'),
                    function (response) {
                        Swal.fire({
                            type: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %>',
                        });
                        $("body").mLoading('hide');
                    },
                    function (response) {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                        });
                        $("body").mLoading('hide');
                    });
            });

            $('#ddlType').trigger('change');

            var wto;
            $("input[id*=txtStudentCardNumber]").on('keyup', function () {

                var $this = $(this);
                var _val = $this.val();
                var _old = $this.data('old') + '';

                clearTimeout(wto);
                wto = setTimeout(function () {


                    if (_val == "" || _val == _old) {
                        $('#ctl00_MainContent_btnAddStudentCard').prop('disabled', false);
                        $("#panel-error").text('');
                        $("#panel-error").hide();
                    }
                    else if (_val != _old) {
                        $("body").mLoading();
                        PageMethods.IsCardValid(_val,
                            function (response) {
                                if (response.isvalid) {
                                    $('#ctl00_MainContent_btnAddStudentCard').prop('disabled', false);
                                    $("#panel-error").text('');
                                    $("#panel-error").hide();
                                }
                                else {
                                    $('#ctl00_MainContent_btnAddStudentCard').prop('disabled', true);
                                    $("#panel-error").text('หมายเลขบัตรซ้ำกับ ' + response.info + ' ' + (response.type == 'backup' ? '(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603086") %>)' : ''));
                                    $("#panel-error").show();
                                    $this.focus();
                                }
                                $("body").mLoading('hide');
                            },
                            function (response) {
                                $("body").mLoading('hide');
                            });
                    }
                    else {

                        //$("#panel-error").text('');
                        //$("#panel-error").hide();

                    }

                }, 800);


            });

            $("#txtStudentCardNumber1, #txtStudentCardNumber2, #txtStudentCardNumber3").inputFilter(function (value) {
                return /^\d*$/.test(value); // Allow digits only, using a RegExp
            });

            $("#myModalImport").on('shown.bs.modal', function () {
                if ($('#ddlType').val() == '1') {
                    $('#importType').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>');
                }
                else {
                    $('#importType').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %>');
                }
            });
        });

        function onImport() {

            var d = new FormData();

            d.append('type', ($('#ddlType').val() == "1" ? "student" : "employee"));
            $.each($fileImport.fileinput('getFileList'), function (i, file) {
                d.append('attFile1', file);
            });

            $.ajax({
                type: "post",
                url: "ImportNFC.ashx",
                data: d,
                contentType: false, // Important
                processData: false, // Important
                success: function (result) {

                    if (result.notFound) {
                        $('#alertWarningImport').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133339") %> : " +result.notFound);
                        $('#alertWarningImport').show();
                    }

                    if (result.error) {
                        $('#alertErrorImport').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106020") %> : " +result.error);
                        $('#alertErrorImport').show();
                    }

                    if (result.duplicate) {
                        $('#alerDupImport').html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133289") %> : " + result.duplicate);
                        $('#alerDupImport').show();
                    }

                    Swal.fire({
                        type: 'success',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106019") %>',
                        showConfirmButton: false,
                        timer: 1500,
                        //willClose: () => {
                        //    window.location.reload();
                        //}
                    }).then((result) => {

                    });
                },
                error: function (result) {
                    Swal.fire({
                        type: 'error',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                        //ext: response.msg,
                    })
                },
            });
            return false;

        }

        function ddlclass() {
            ////$("body").mLoading();
            var ddl2 = document.getElementsByClassName("ddl2");

            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }

            setTimeout(function () {
                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + document.getElementById('<%=ddlsublevel.ClientID%>').value, function (Result) {
                    var opt = document.createElement("option");
                    // Assign text and value to Option object
                    opt.text = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>";
                    opt.value = "";
                    // Add an Option object to Drop Down List Box
                    document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");

                        // Assign text and value to Option object
                        opt.text = Result[index].name;
                        opt.value = Result[index].value;
                        if (getParameterByName("idlv2") == Result[index].value) {
                            opt.selected = "selected";
                        }
                        // Add an Option object to Drop Down List Box
                        document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);
                    });
                    $('#<%=ddlsublevel2.ClientID%>').selectpicker('refresh');
                });

            }, 300);
            ////$("body").mLoading('hide');
        }

        function OpenStudentCardDialog(studentId, studentSurName, stdId, nFC, card1, card2, card3
            , encard1, encard2, encard3
            , cardRe1, cardRe2, cardRe3
            , encardRe1, encardRe2, encardRe3
            , updateBy1, updateBy2, updateBy3
            , updateDate1, updateDate2, updateDate3
            , freetext1, freetext2, freetext3
        ) {
            try {
                //$("body").mLoading();
                $('#lblStudentCode').text(studentId);
                $('#lblStudentSurName').text(studentSurName);
                $('#hdnStdId').val(stdId);
                $("input[id$=txtStudentCardNumber1]").val(card1);
                $("input[id$=txtStudentCardNumber1]").attr('data-old', card1);
                $("input[id$=txtStudentCardNumber1]").prop('disabled', !!card1);
                $("input[id$=txtStudentCardNumber2]").val(card2);
                $("input[id$=txtStudentCardNumber2]").attr('data-old', card2);
                $("input[id$=txtStudentCardNumber2]").prop('disabled', !!card2);
                $("input[id$=txtStudentCardNumber3]").val(card3);
                $("input[id$=txtStudentCardNumber3]").attr('data-old', card3);
                $("input[id$=txtStudentCardNumber3]").prop('disabled', !!card3);
                $("span#txtEncard1").text(encard1 + ' ');
                $("span#txtEncard2").text(encard2 + ' ');
                $("span#txtEncard3").text(encard3 + ' ');

                $("span#txtNFCReverse1").text(cardRe1 + ' ');
                $("span#txtNFCReverse2").text(cardRe2 + ' ');
                $("span#txtNFCReverse3").text(cardRe3 + ' ');

                $("span#txtEnCardReverse1").text(encardRe1 + ' ');
                $("span#txtEnCardReverse2").text(encardRe2 + ' ');
                $("span#txtEnCardReverse3").text(encardRe3 + ' ');

                $("span#spanRemark1").text(updateDate1 + ' ' + updateBy1);
                $("span#spanRemark2").text(updateDate2 + ' ' + updateBy2);
                $("span#spanRemark3").text(updateDate3 + ' ' + updateBy3);

                $("input#txtFreeText1").val(freetext1);
                $("input#txtFreeText2").val(freetext2);
                $("input#txtFreeText3").val(freetext3);

                $("#panel-error").text('');
                $("#panel-error").css('display', 'none');
                //$("body").mLoading('hide');
                return false;
            }
            catch (err) {
                console.log(err);
                return false;
            }
            return false;
        }

        function ValidateStudentCard() {
            //$("body").mLoading();
            $("#panel-error").text('');
            if ($("input[id$=txtStudentCardNumber1]").val() != "" && (!$.isNumeric($("input[id$=txtStudentCardNumber1]").val()))) {
                $("input[id$=txtStudentCardNumber1]").focus();
                $("#panel-error").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133297") %>');
                $("#panel-error").css('display', '');
                //$("body").mLoading('hide');
                return false;
            }
            else if ($("input[id$=txtStudentCardNumber2]").val() != "" && (!$.isNumeric($("input[id$=txtStudentCardNumber2]").val()))) {
                $("input[id$=txtStudentCardNumber2]").focus();
                $("#panel-error").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133297") %>');
                $("#panel-error").css('display', '');
                //$("body").mLoading('hide');
                return false;
            }
            else if ($("input[id$=txtStudentCardNumber3]").val() != "" && (!$.isNumeric($("input[id$=txtStudentCardNumber3]").val()))) {
                $("input[id$=txtStudentCardNumber3]").focus();
                $("#panel-error").text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133297") %>');
                $("#panel-error").css('display', '');
                //$("body").mLoading('hide');
                return false;
            }
            else {
                $("#panel-error").text('');
                $("#panel-error").css('display', 'none');
                $('#myModal').modal('toggle');
                $("body").mLoading();
                PageMethods.UpdateStudentCard($('#hdnStdId').val()
                    , $("input[id$=txtStudentCardNumber1]").val()
                    , $("input[id$=txtStudentCardNumber2]").val()
                    , $("input[id$=txtStudentCardNumber3]").val()
                    , $("input#txtFreeText1").val()
                    , $("input#txtFreeText2").val()
                    , $("input#txtFreeText3").val()
                    //, $("input[id$=txtStudentCardNumber]").val(),
                    , function (response) {
                        if (response) {

                            Swal.fire({
                                type: 'success',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106017") %>',
                            }).then(function () {
                                __doPostBack('ctl00$MainContent$btnUpdate', '');
                            });


                        }
                        else {

                            Swal.fire({
                                type: 'error',
                                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132153") %>',
                            });
                        }

                        $("body").mLoading('hide');
                    },
                    function (response) {
                        console.log(response);
                        //$("body").mLoading('hide');

                    });
            }
            return false;

        }

        function start() {
            //$("body").mLoading();
            var availableTags = [];
            $.get("/app_logic/studentlist.ashx", function (Result) {
                $.each(Result, function (index) {
                    availableTags.push(Result[index].fullName);
                });
            });

            //$("#ctl00_MainContent_tags").autocomplete({
            //    source: availableTags
            //});

            var availableTags2 = [];
            $.get("/app_logic/emplist.ashx", function (Result) {
                $.each(Result, function (index) {
                    availableTags2.push(Result[index].fullName);
                });
            });

            //$("#ctl00_MainContent_tags2").autocomplete({
            //    source: availableTags2
            //});

            ddlclass();
            //$("body").mLoading('hide');
        }
        window.onload = start;

        function getParameterByName(name) {
            if (name !== "" && name !== null && name != undefined) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            } else {
                var arr = location.href.split("/");
                return arr[arr.length - 1];
            }
        }
    </script>



    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>
</asp:Content>
