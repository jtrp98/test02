<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="studentCardMain.aspx.cs" Inherits="FingerprintPayment.studentCard.studentCardMain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks %>" />
    <style>
        .dropdown.bootstrap-select {
            padding-left: 0px;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th,
        table.dataTable thead tr td {
            border-bottom: 1px solid #000;
        }
      
    </style>
           <% if(!IsReportBuilder){%>
            <style>
                .reportbuilder {display:none !important;}
                .td-reportbuilder {width:1% !important;}
            </style>
       <% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="aspnetForm" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

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

                        <div class="form-group row student">
                            <div class="col-md-12 d-none">
                                <div class="col-md-2 righttext">
                                    <span>ddl2</span>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtddl2" runat="server" CssClass="d2" Width="50%"> </asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row student">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlyear" runat="server" CssClass="selectpicker col-md-12 --req-append-last" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>" data-msg="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01581") %>" required></asp:DropDownList>
                            </div>
                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="semister" runat="server" CssClass="selectpicker col-md-12 --req-append-last" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>" data-msg="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01580") %>" required></asp:DropDownList>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class="row student">

                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlsublevel" onchange="ddlclass()" runat="server" CssClass="selectpicker col-md-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlsublevel2" runat="server" CssClass="ddl2 selectpicker col-md-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>

                        <div class=" row student">

                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                            <div class="col-sm-3">
                                <uc1:StudentAutocomplete runat="server" ID="StudentAutocomplete" />
                            </div>

                            <div class="col-sm-1"></div>
                            <label class="col-sm-1 col-form-label text-left"></label>
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-2"></div>


                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <input type="button" id="btSearch" class="btn btn-info search-btn " value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
                            </div>
                        </div>

                        <div class="row d-none">

                            <div class="col-md-7 button-section">

                                <input type="button" id="btnSearch" class="btn btn-info search-btn " value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
                            </div>

                            <div class="col-md-5 button-section" id="printok" runat="server">
                                <input type="button" id="btnPrint2" onclick="nextpage(99999, 0, 0)" style="margin-left: 5%; width: 150px;" class='btn btn-warning search-btn button2 pull-right' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133336") %>" />
                            </div>
                            <div class="col-md-5 button-section" id="Div1" runat="server">
                                <input type="button" id="btnPrint3" onclick="bootbox4()" style="margin-left: 5%; width: 150px;" class='btn btn-warning search-btn button2 pull-right' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133336") %>" />
                            </div>
                            <div class="col-md-5 button-section" id="printerror" runat="server">
                                <input type="button" id="error1" onclick="bootbox2()" style="margin-left: 5%; width: 150px;" class='btn btn-warning search-btn button2 pull-right' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133336") %>" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card ">

                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">list</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104020") %></h4>
                    </div>
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-md-12">

                                <table id="studentTable" class="table-hover dataTable" style="width:100%"></table>

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
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106025") %></h4>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; height: 600px;">


                        <div class="boxPrintDate row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106029") %></label>
                            </div>
                            <div class="col-md-7">
                                <div class="form-groupx col-md-12" style="padding-left: 1px;">
                                    <asp:TextBox ID="txtPrintDate" runat="server" onchange="" ClientIDMode="static" CssClass="form-control datepicker linkfrom " Style="padding-left: 10px;" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1; right: 15px;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="boxExpTextDate row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106030") %></label>
                            </div>
                            <div class="col-md-7">
                                <div class="form-groupx col-md-12" style="padding-left: 1px;">
                                    <asp:TextBox ID="startDay" runat="server" onchange="" ClientIDMode="static" CssClass="form-control datepicker linkfrom " Style="padding-left: 10px;" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1; right: 15px;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>


                        <div class="row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106026") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="optionCourse" runat="server" CssClass="width100x  selectpicker col-md-12 typevalue" onchange="savetype()" data-style="select-with-transition" data-size="10">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106028") %>"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106058") %>"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106077") %>"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106078") %>"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106079") %>"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106080") %>"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106081") %>"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106082") %>"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106083") %>"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106084") %>"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106085") %>"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106086") %>"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106087") %>"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106088") %>"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106089") %>"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106090") %>"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxPatternContent d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106064") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="PatternContent" runat="server" CssClass="selectpicker col-md-12" onchange="fcPatternContent()" data-style="select-with-transition">
                                    <asp:ListItem Value="3" Selected="True"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106065") %></asp:ListItem>
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106066") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106067") %></asp:ListItem>
                                    <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106068") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxShowName d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106031") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="optionShowName" runat="server" CssClass="selectpicker col-md-12" onchange="showName()" data-style="select-with-transition">
                                    <asp:ListItem Value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106032") %></asp:ListItem>
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106033") %></asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106034") %></asp:ListItem>
                                    <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106035") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxExpDate d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102089") %></label>
                            </div>
                            <div class="col-md-7">
                                <select id="optionExpDate" onchange="changePropValue('ShowExpireDate',this.value)" class="selectpicker col-md-12" data-style="select-with-transition">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></option>
                                </select>
                            </div>
                        </div>

                        <div class="boxNickName d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="optionNickName" runat="server" CssClass="selectpicker col-md-12" onchange="showNickName()" data-style="select-with-transition">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxColorNickName d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label" id="txtColorNickname"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106063") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="optionColorNickName" runat="server" CssClass="selectpicker col-md-12" onchange="changeColor()" data-style="select-with-transition">
                                    <asp:ListItem Value="Black" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106039") %>" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="White" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106040") %>"></asp:ListItem>
                                    <asp:ListItem Value="Blue" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106041") %>"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxBrithday d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106059") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="optionboxBrithday" runat="server" CssClass="selectpicker col-md-12" onchange="fcshowBrithday()" data-style="select-with-transition">
                                    <asp:ListItem Value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106032") %></asp:ListItem>
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106060") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106061") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxNumberPhone d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="optionnumberPhone" runat="server" CssClass="selectpicker col-md-12" onchange="shownumberPhone()" data-style="select-with-transition">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxQR d-none row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106042") %></label>
                            </div>
                            <div class="col-md-7">
                                <select id="optionBarQue" class="selectpicker col-md-12" data-style="select-with-transition">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                </select>
                            </div>
                        </div>

                        <div class="boxEducation row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106044") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList runat="server" ID="optionClass" ClientIDMode="Static" class="selectpicker col-md-12" data-style="select-with-transition" onchange="changePropValue('EducationType',this.value)">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106045") %></asp:ListItem>
                                    <asp:ListItem Value="1" Selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106046") %></asp:ListItem>
                                    <asp:ListItem Value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106047") %></asp:ListItem>
                                    <asp:ListItem Value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxPID row" style="padding: 0 4px; display: none">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></label>
                            </div>
                            <div class="col-md-7">
                                <select id="optionPID" class="selectpicker col-md-12" data-style="select-with-transition" onchange="changePropValue('PIDDisplay' , this.value)">
                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></option>
                                </select>
                            </div>
                        </div>

                        <div class="boxPID row " style="padding: 5px; display: none">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106048") %></label><br />
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106050") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlPIDPosition" ClientIDMode="Static" runat="server" CssClass=" selectpicker " data-width="100%" data-size="7" data-style="select-with-transition" onchange="changePropValue('PIDPosition' , this.value)">
                                    <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                    <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                    <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                    <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                    <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                    <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                    <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                    <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                    <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>


                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>

                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                    <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                    <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                    <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                    <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                    <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                    <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                    <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                    <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                    <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                    <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                    <asp:ListItem Value="61" Text="61"></asp:ListItem>
                                    <asp:ListItem Value="62" Text="62"></asp:ListItem>
                                    <asp:ListItem Value="63" Text="63"></asp:ListItem>
                                    <asp:ListItem Value="64" Text="64"></asp:ListItem>
                                    <asp:ListItem Value="65" Text="65"></asp:ListItem>
                                    <asp:ListItem Value="66" Text="66"></asp:ListItem>
                                    <asp:ListItem Value="67" Text="67"></asp:ListItem>
                                    <asp:ListItem Value="68" Text="68"></asp:ListItem>
                                    <asp:ListItem Value="69" Text="69"></asp:ListItem>
                                    <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="boxExpDate2 row" style="padding: 0 4px; display: none">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106061") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="selectpicker col-md-12" onchange="changePropValue('ExpDate2Display' , this.value)" data-style="select-with-transition">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="boxReleaseDate2  row" style="padding: 0 4px; display: none">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106062") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="selectpicker col-md-12" onchange="changePropValue('ReleaseDate2Display' , this.value)" data-style="select-with-transition">
                                    <asp:ListItem Value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106036") %></asp:ListItem>
                                    <asp:ListItem Value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="boxcard d-none">
                            <div class="row" style="padding: 0 4px;">
                                <div class="col-md-5">
                                    <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106051") %></label>
                                </div>
                                <div class="col-md-7">
                                    <iframe scrolling="no" src="uploadiframe.aspx" style="border: 0px none; height: 200px; width: 300px;"></iframe>
                                </div>
                            </div> 
                        </div>

                        <div class="boxcard d-none">
                            <div class="row" style="padding: 0 4px;">
                                <div class="col-md-5">
                                    <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106069") %></label>
                                    <p>
                                        <input type="checkbox" runat="server" id="chkJourneyType1" clientidmode="Static" onclick="changePropValue('JourneyType1',+$('#chkJourneyType1').is(':checked'))" />
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106070") %></label>
                                    </p>
                                </div>
                                <div class="col-md-7">
                                    <iframe scrolling="no" src="uploadiframe-1.aspx" style="border: 0px none; height: 200px; width: 300px;"></iframe>
                                </div>
                            </div>

                            <div class="row" style="padding: 0 4px;">
                                <div class="col-md-5">
                                    <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106071") %></label>
                                    <p>
                                        <input type="checkbox" runat="server" id="chkJourneyType2" clientidmode="Static" value="2" onclick="changePropValue('JourneyType2',+$('#chkJourneyType2').is(':checked'))" />
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106072") %></label>
                                    </p>
                                </div>
                                <div class="col-md-7">
                                    <iframe scrolling="no" src="uploadiframe-2.aspx" style="border: 0px none; height: 200px; width: 300px;"></iframe>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-12 pad0 moredetail d-none">

                            <div class="col-md-12 pad0 BProfile d-none">
                                <div class="row" style="padding: 0 4px;">
                                    <div class="col-md-5 pad0">
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106054") %></label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="PicProfileUpDown" runat="server" CssClass="width100x selectpicker  col-md-12 positionvalue" onchange="saveposition(0)" data-style="select-with-transition">
                                            <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                            <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                            <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                            <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                            <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                            <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                            <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="col-form-label">pixel</span>
                                    </div>
                                </div>

                                <div class="row" style="padding: 0 4px;">
                                    <div class="col-md-5 pad0">
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106055") %></label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="PicProfileLeftRight" runat="server" CssClass="width100x selectpicker col-md-12 positionvalue" onchange="saveposition(1)" data-style="select-with-transition">
                                            <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                            <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                            <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                            <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                            <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                            <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                            <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="col-form-label">pixel</span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 pad0 Barcode d-none">
                                <div class="row" style="padding: 0 4px;">
                                    <div class="col-md-5 pad0">
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106056") %></label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="BarcodeUpDown" runat="server" CssClass="width100x selectpicker col-md-12 positionvalue" onchange="saveposition(2)" data-style="select-with-transition">
                                            <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                            <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                            <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                            <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                            <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                            <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                            <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="col-form-label">pixel</span>
                                    </div>
                                </div>

                                <div class="row" style="padding: 0 4px;">
                                    <div class="col-md-5 pad0">
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106057") %></label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="BarcodeRightLeft" runat="server" CssClass="width100x selectpicker col-md-12 positionvalue" onchange="saveposition(3)" data-style="select-with-transition">
                                            <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                            <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                            <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                            <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                            <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                            <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                            <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="col-form-label">pixel</span>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 pad0 BContent d-none">
                                <div class="row" style="padding: 0 4px;">
                                    <div class="col-md-5 pad0">
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106073") %></label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ContentAlignmentTopBottom" runat="server" CssClass="width100x selectpicker col-md-12 positionvalue" onchange="saveposition(4)" data-style="select-with-transition">
                                            <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                            <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                            <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                            <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                            <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                            <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                            <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="col-form-label">pixel</span>
                                    </div>
                                </div>

                                <div class="row" style="padding: 0 4px;">
                                    <div class="col-md-5 pad0">
                                        <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106074") %></label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ContentAlignmentLeftRight" runat="server" CssClass="width100x selectpicker col-md-12 positionvalue" onchange="saveposition(5)" data-style="select-with-transition">
                                            <asp:ListItem Value="-10" Text="-10"></asp:ListItem>
                                            <asp:ListItem Value="-9" Text="-9"></asp:ListItem>
                                            <asp:ListItem Value="-8" Text="-8"></asp:ListItem>
                                            <asp:ListItem Value="-7" Text="-7"></asp:ListItem>
                                            <asp:ListItem Value="-6" Text="-6"></asp:ListItem>
                                            <asp:ListItem Value="-5" Text="-5"></asp:ListItem>
                                            <asp:ListItem Value="-4" Text="-4"></asp:ListItem>
                                            <asp:ListItem Value="-3" Text="-3"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="-2"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="-1"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106049") %>" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <span class="col-form-label">pixel</span>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <%--     <div class="hid" style="font-size: 30%">hidden</div>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" onclick="modaldetail()" style="width: 155px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106053") %></button>
                        &nbsp;
                        <button type="button" id="btnSubmit" class="btn btn-success" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106116") %></h4>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; height: 600px;">


                        <div class="row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106026") %></label>
                            </div>
                            <div class="col-md-7">
                                <asp:DropDownList ID="ddlCardForm2" runat="server" CssClass="width100x  selectpicker col-md-12 typevalue" data-style="select-with-transition" data-size="10"
                                    onchange="changePropValue('NewCardType',this.value)">
                                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106027") %>" Selected="True"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class=" row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106029") %></label>
                            </div>
                            <div class="col-md-7">
                                <div class="form-groupx col-md-12" style="padding-left: 1px;">
                                    <asp:TextBox ID="txtPrintDate2" runat="server" onchange="" ClientIDMode="static" CssClass="form-control datepicker linkfrom " Style="padding-left: 10px;" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1; right: 15px;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class=" row" style="padding: 0 4px;">
                            <div class="col-md-5">
                                <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106030") %></label>
                            </div>
                            <div class="col-md-7">
                                <div class="form-groupx col-md-12" style="padding-left: 1px;">
                                    <asp:TextBox ID="startDay2" runat="server" onchange="" ClientIDMode="static" CssClass="form-control datepicker linkfrom " Style="padding-left: 10px;" />
                                    <span class="form-control-feedback" style="color: #000; opacity: 1; right: 15px;">
                                        <i class="material-icons">event</i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class=" ">
                            <div class="row" style="padding: 0 4px;">
                                <div class="col-md-5">
                                    <label class="col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106051") %></label>
                                </div>
                                <div class="col-md-7">
                                    <iframe scrolling="no" src="uploadiframe.aspx" style="border: 0px none; height: 200px; width: 300px;"></iframe>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSubmit2" class="btn btn-success" data-dismiss="modal" style="width: 100px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </div>
            </div>
        </div>

        <%--  <div class="full-card box-content userlist-container">

            <div id="loading" class="d-none load"></div>


            


            <div class="row border-bottom">
                <br />

            </div>


        </div>--%>
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script src="Script/studentCard.js?v=<%=DateTime.Now.Ticks %>" type="text/javascript"></script>

    <script type="text/javascript">

        var StudentCard = new Student_Card();
        var availableValueplane = [];
        var resultData = "";

        if (jQuery.validator) {

            jQuery.extend(jQuery.validator.messages, {
                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",

            });

            $("#aspnetForm").validate({  // initialize the plugin
                errorPlacement: function (error, element) {
                    var _class = element.attr('class');

                    if (_class.includes('--req-append-last')) {
                        error.insertAfter(element.parent());
                    }
                    else
                        error.insertAfter(element);
                }

            });
        }

        $(document).ready(function () {

            $('.datepicker').datetimepicker({
                format: 'DD/MM/YYYY-BE',
                locale: 'th',
                debug: false,
                //defaultDate: "<%=EXP_DATE %>",
                //autoclose: true,
                //autoclose: true,
                //showOn: "button",
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });
            //    .on('dp.change', function (e) {
            //    var TexTDate = $('#startDay').val();

            //    if (TexTDate == '') {
            //        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133333") %>");
            //    }
            //    else {
            //        $.ajax({
            //            url: "/studentCard/studentCardMain.ashx?date=" + TexTDate,
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //        });
            //    }
            //})

            $('#startDay,#startDay2').on('dp.change', function (e) {
                var TexTDate = $(this).val();

                //if (TexTDate == '') {
                //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133333") %>");
                //}
                //else {

                //}
                $.ajax({
                    url: "/studentCard/studentCardMain.ashx?date=" + TexTDate,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
            })

            $('#txtPrintDate,#txtPrintDate2').on('dp.change', function (e) {
                var TexTDate = $(this).val();

                //if (TexTDate == '') {
                //    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133333") %>");
                //}
                //else {

                //}
                changePropValue('CardPrintDate', TexTDate);
            });

            //for (var i = 1; i <= 70; i = i+1) {
            //    var listItem = $("<option></option>").val(i).html(i);
            //    $("#ddlPIDPosition").append(listItem);
            //}
            //$("#ddlPIDPosition").selectpicker("refresh");
            /* $("#startDay").datepicker();*/

            //$('.js-example-basic-multiple2').select2({
            //    allowClear: true,
            //    placeholder: ''
            //});

            //$('input[id*=btnSearch]').click(function () {
            //    var load = document.getElementsByClassName("load");
            //    var inputname = STAC.GetStudentName();// document.getElementById("iptStudentName").value;

            //    //load[0].classList.remove('d-none');

            //    var param1var = $('select[id*=ddlsublevel] option:selected').val();
            //    var param2var = $('select[id*=ddlsublevel2] option:selected').val();
            //    if (param2var == undefined)
            //        param2var = "";

            //    window.location.href = "studentCardMain.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&name=" + inputname;
            //});


            //$("#iptStudentName").autocomplete({
            //    source: function (request, response) {
            //        var param = { keyword: $('#iptStudentName').val() };

            //        $.ajax({
            //            url: "studentCardMain.aspx/GetStudentName",
            //            data: JSON.stringify(param),
            //            dataType: "json",
            //            type: "POST",
            //            contentType: "application/json; charset=utf-8",
            //            dataFilter: function (data) { return data; },
            //            success: function (data) {
            //                response($.map(data.d, function (item) {
            //                    return {
            //                        value: item
            //                    }
            //                }))
            //            },
            //            error: function (XMLHttpRequest, textStatus, errorThrown) {
            //                console.log(textStatus);
            //            }
            //        });
            //    },
            //    error: function (event, ui) {

            //    },
            //    minLength: 1
            //});


            $('select[id*=ddlyear]').change(function () {
                getListTrem();
            });


            $('input[id*=btSearch]').click(function () {
                //var load = document.getElementsByClassName("load");
                //load[0].classList.remove('d-none');


                if ($('#aspnetForm').valid() == false) {
                    //e.preventDefault();
                    //e.stopPropagation();
                    return false;
                }

                var ddlyear = $('select[id*=ddlyear]').val();
                var semister = $('select[id*=semister]').val();
                var ddlsublevel = $('select[id*=ddlsublevel]').val();
                var ddlsublevel2 = $('select[id*=ddlsublevel2]').val();
                var studentName = SAC.GetStudentName();// $('#iptStudentName').val();

                if (ddlyear != "") {
                    var data = {
                        "year": ddlyear,
                        "term": semister,
                        "level": ddlsublevel,
                        "className": ddlsublevel2,
                        "fullname": studentName
                    };
                    console.log(data);

                    $("body").mLoading();
                    PageMethods.List_DataStudent(data, function (e) {
                        resultData = e.Studentlists;
                        console.log(e.Studentlists);
                        StudentCard.List_DataStudent = e;
                        StudentCard.RenderHtml_ListDataStudent("studentTable", false);
                        $("body").mLoading('hide');
                    });
                }
                else {
                    //alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %>");
                    //load[0].classList.add('d-none');
                }

            });

            $("#myModal").on('shown.bs.modal', function () {
                savetype();
            });

        });

        function getUrlParameter(sParam) {
            var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : sParameterName[1];
                }
            }
        }

        function SentStudentData(checkID) {
            var enddate = document.getElementsByClassName("enddate");

            /*var subleve1 = $('select[id*=ddlsublevel]').val();*/
            /*var subleve2 = $('select[id*=ddlsublevel2]').val();*/
            /*var term = $('select[id*=semister]').val();*/
            var wordName = $('select[id*=optionShowName]').val();
            var typeCard = $('select[id*=optionCourse]').val();
            var typeBarQue = $('select[id*=optionBarQue]').val();
            var typeClass = $('select[id*=optionClass]').val();

            var typeExpDate = $('select[id*=optionExpDate]').val();
            var typeNickName = $('select[id*=optionNickName]').val();

            var correctData = 0;
            for (var i = 0; i < resultData.length; i++) {
                var arrDataSID = resultData[i].sid;
                if (checkID == arrDataSID) {
                    correctData = i;
                }
            }

            var subleve1 = resultData[correctData].nTSubLevel;
            var subleve2 = resultData[correctData].nTermSubLevel2;
            var term = resultData[correctData].nTerm;
            var sID = resultData[correctData].sid;

            var queryString = "&idlv=" + subleve1 + "&idlv2=" + subleve2 + "&term=" + term + "&id=" + sID + "&wE=" + wordName + "&tC=" + typeCard + "&tBQ=" + typeBarQue + "&tCL=" + typeClass + "&tEXP=" + typeExpDate + "&tNik=" + typeNickName;

            window.open('/studentCard/studentCardPrintiframe.aspx?' + queryString);
            //if (enddate[0].value == "") {
            //    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133334") %>');
            //} else {
            //    var queryString = "&idlv=" + subleve1 + "&idlv2=" + subleve2 + "&term=" + term + "&id=" + sID + "&wE=" + wordName + "&tC=" + typeCard + "&tBQ=" + typeBarQue + "&tCL=" + typeClass + "&tEXP=" + typeExpDate + "&tNik=" + typeNickName;

            //    window.open('/studentCard/studentCardPrintiframe.aspx?' + queryString);
            //}
        }

        function SentStudentData2(checkID) {
            var enddate = document.getElementsByClassName("enddate");

            /*var subleve1 = $('select[id*=ddlsublevel]').val();*/
            /*var subleve2 = $('select[id*=ddlsublevel2]').val();*/
            /*var term = $('select[id*=semister]').val();*/
            var wordName = $('select[id*=optionShowName]').val();
            var typeCard = $('select[id*=optionCourse]').val();
            var typeBarQue = $('select[id*=optionBarQue]').val();
            var typeClass = $('select[id*=optionClass]').val();

            var typeExpDate = $('select[id*=optionExpDate]').val();
            var typeNickName = $('select[id*=optionNickName]').val();

            var correctData = 0;
            for (var i = 0; i < resultData.length; i++) {
                var arrDataSID = resultData[i].sid;
                if (checkID == arrDataSID) {
                    correctData = i;
                }
            }

            var subleve1 = resultData[correctData].nTSubLevel;
            var subleve2 = resultData[correctData].nTermSubLevel2;
            var term = resultData[correctData].nTerm;
            var sID = resultData[correctData].sid;

            var queryString = "&idlv=" + subleve1 + "&idlv2=" + subleve2 + "&term=" + term + "&id=" + sID + "&wE=" + wordName + "&tC=" + typeCard + "&tBQ=" + typeBarQue + "&tCL=" + typeClass + "&tEXP=" + typeExpDate + "&tNik=" + typeNickName;

            window.open('/studentCard/studentCardPrintiframe2.aspx?' + queryString);
            //if (enddate[0].value == "") {
            //    alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133334") %>');
            //} else {
            //    var queryString = "&idlv=" + subleve1 + "&idlv2=" + subleve2 + "&term=" + term + "&id=" + sID + "&wE=" + wordName + "&tC=" + typeCard + "&tBQ=" + typeBarQue + "&tCL=" + typeClass + "&tEXP=" + typeExpDate + "&tNik=" + typeNickName;

            //    window.open('/studentCard/studentCardPrintiframe.aspx?' + queryString);
            //}
        }

        function nextpage() {
            //var subleve1 = $('select[id*=ddlsublevel]').val();
            //var subleve2 = $('select[id*=ddlsublevel2]').val();
            //var term = $('select[id*=semister]').val();
            var wordName = $('select[id*=optionShowName]').val();
            var typeCard = $('select[id*=optionCourse]').val();
            var typeBarQue = $('select[id*=optionBarQue]').val();
            var typeClass = $('select[id*=optionClass]').val();
            var typeExpDate = $('select[id*=optionExpDate]').val();
            var typeNickName = $('select[id*=optionNickName]').val();

            var subleve1 = resultData[0].nTSubLevel;
            var subleve2 = resultData[0].nTermSubLevel2;
            var term = resultData[0].nTerm;

            var queryString = "idlv=" + subleve1 + "&idlv2=" + subleve2 + "&wE=" + wordName + "&tC=" + typeCard + "&tBQ=" + typeBarQue + "&tCL=" + typeClass + "&tEXP=" + typeExpDate + "&tNik=" + typeNickName + "&term=" + term;
            window.open('/studentcard/studentCardPrintAll.aspx?' + queryString);

        }

        function PrintAll2() {
            //var subleve1 = $('select[id*=ddlsublevel]').val();
            //var subleve2 = $('select[id*=ddlsublevel2]').val();
            //var term = $('select[id*=semister]').val();
            var wordName = $('select[id*=optionShowName]').val();
            var typeCard = $('select[id*=optionCourse]').val();
            var typeBarQue = $('select[id*=optionBarQue]').val();
            var typeClass = $('select[id*=optionClass]').val();
            var typeExpDate = $('select[id*=optionExpDate]').val();
            var typeNickName = $('select[id*=optionNickName]').val();

            var subleve1 = resultData[0].nTSubLevel;
            var subleve2 = resultData[0].nTermSubLevel2;
            var term = resultData[0].nTerm;

            var queryString = "idlv=" + subleve1 + "&idlv2=" + subleve2 + "&wE=" + wordName + "&tC=" + typeCard + "&tBQ=" + typeBarQue + "&tCL=" + typeClass + "&tEXP=" + typeExpDate + "&tNik=" + typeNickName + "&term=" + term;
            window.open('/studentcard/studentCardPrintAll2.aspx?' + queryString);

        }

        //function copytext() {
        //    var enddate = document.getElementsByClassName("enddate");

        //    var NowYear = new Date();
        //    var Y = NowYear.getFullYear();
        //    var LimitYear = parseInt(Y) + 100;

        //    var TexTDate = $('#startDay').val();
        //    var ArrayDate = TexTDate.split('/');
        //    var ArrayYear = ArrayDate[2];
        //    var Year = parseInt(ArrayYear);

        //    if (Year < LimitYear) {
        //        $.ajax({
        //            url: "/studentCard/studentCardMain.ashx?date=" + enddate[0].value,
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //        });
        //    } else {
        //        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133333") %>");
        //    }

        //}

        function saveposition(index) {
            var value = $('select.positionvalue');//document.getElementsByClassName("positionvalue");
            var txtposition = Number(index);
            var position = "";

            if (index == 0)
                position = "PicProfileUpDown";
            else if (index == 1)
                position = "PicProfileLeftRight";
            else if (index == 2)
                position = "BarcodeQRTopBottom";
            else if (index == 3)
                position = "BarcodeQRLeftRight";
            else if (index == 4)
                position = "ContentAlignmentTopBottom";
            else if (index == 5)
                position = "ContentAlignmentLeftRight";
            //alert("/studentCard/studentCardEditPosition.ashx?position=" + position + "&value=" + value[txtposition].value);
            //console.log("/studentCard/studentCardEditPosition.ashx?position=" + position + "&value=" + value[txtposition].value);
            $.ajax({
                url: "/studentCard/studentCardEditPosition.ashx?position=" + position + "&value=" + value[txtposition].value,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }

        function reload() {
            location.reload();
        }

        function modaldetail() {
            var moredetail = document.getElementsByClassName("moredetail");
            if (moredetail[0].classList.contains('d-none') == true) {
                moredetail[0].classList.remove('d-none');
                //moredetail[1].classlist.remove('d-none');
                //moredetail[2].classlist.remove('d-none');
                //moredetail[3].classlist.remove('d-none');
            }
            else {
                moredetail[0].classList.add('d-none');
                //moredetail[1].classList.add('d-none');
                //moredetail[2].classList.add('d-none');
                //moredetail[3].classList.add('d-none');
            }
        }

        function savetype() {
            var typeCard = $('select[id*=optionCourse]').val();

            var yBarcode = document.getElementsByClassName("Barcode");
            var yBContent = document.getElementsByClassName("BContent");
            var yBProfile = document.getElementsByClassName("BProfile");
            var boxShowName = document.getElementsByClassName("boxShowName");
            var boxQR = document.getElementsByClassName("boxQR");
            var boxcard = document.getElementsByClassName("boxcard");
            var boxExpDate = document.getElementsByClassName("boxExpDate");
            var boxNickName = document.getElementsByClassName("boxNickName");
            var boxColorNickName = document.getElementsByClassName("boxColorNickName");
            var boxPatternContent = document.getElementsByClassName("boxPatternContent");
            var txtColorNickname = document.getElementById("txtColorNickname");

            var boxBrithday = document.getElementsByClassName("boxBrithday");
            var boxNumberPhone = document.getElementsByClassName("boxNumberPhone");
            var boxEducation = $('.boxEducation');
            var boxPID = $('.boxPID');
            var boxExpTextDate = $('.boxExpTextDate');
            var boxPrintDate = $('.boxPrintDate');

            var boxExpDate2 = $('.boxExpDate2');
            var boxReleaseDate2 = $('.boxReleaseDate2');

            boxExpDate2.hide();
            boxReleaseDate2.hide();

            if (typeCard == 3 || typeCard == 14) {
                txtColorNickname.innerHTML = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106038") %>";

                yBProfile[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                yBContent[0].classList.add('d-none');
                boxShowName[0].classList.remove('d-none');
                boxQR[0].classList.remove('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.remove('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.remove('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.remove('d-none');
                boxNumberPhone[0].classList.remove('d-none');

                boxPID.show();
                boxReleaseDate2.show();
            }
            else if (typeCard == 4 || typeCard == 15) {
                txtColorNickname.innerHTML = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106063") %>";

                yBProfile[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                yBContent[0].classList.add('d-none');
                boxShowName[0].classList.remove('d-none');
                boxQR[0].classList.remove('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.remove('d-none');
                boxNickName[0].classList.remove('d-none');
                boxColorNickName[0].classList.remove('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();

                if (typeCard == 4) {
                    boxExpDate[0].classList.remove('d-none');
                    boxNickName[0].classList.remove('d-none');
                    boxColorNickName[0].classList.remove('d-none');
                    boxExpTextDate.show();
                }
                else if (typeCard == 15) {
                    boxExpDate[0].classList.add('d-none');
                    boxNickName[0].classList.add('d-none');
                    boxColorNickName[0].classList.add('d-none');
                    boxExpTextDate.hide();
                }

            }
            else if (typeCard == 5) {
                yBProfile[0].classList.remove('d-none');
                yBContent[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');

                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.remove('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.show();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 6) {
                yBProfile[0].classList.add('d-none');
                yBContent[0].classList.remove('d-none');
                yBarcode[0].classList.add('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 7) {
                yBProfile[0].classList.remove('d-none');
                yBContent[0].classList.remove('d-none');
                yBarcode[0].classList.add('d-none');
                boxShowName[0].classList.remove('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 8) {
                yBProfile[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                yBContent[0].classList.add('d-none');
                boxShowName[0].classList.remove('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();
                boxExpTextDate.hide()
            }
            else if (typeCard == 9) {
                yBProfile[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                yBContent[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.add('d-none');
                boxcard[1].classList.remove('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.remove('d-none');
                boxShowName[0].classList.remove('d-none');
                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 10) {
                yBProfile[0].classList.add('d-none');
                yBarcode[0].classList.add('d-none');
                yBContent[0].classList.remove('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 11) {
                yBProfile[0].classList.remove('d-none');
                yBarcode[0].classList.add('d-none');
                yBContent[0].classList.remove('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.show();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 12) {
                txtColorNickname.innerHTML = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133337") %>";

                yBProfile[0].classList.add('d-none');
                yBarcode[0].classList.add('d-none');
                yBContent[0].classList.remove('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.remove('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxPID.hide();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 13) {
                yBProfile[0].classList.remove('d-none');
                yBContent[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxEducation.hide();
                boxPID.hide();
                boxExpTextDate.hide()
                boxPrintDate.hide();
            }
            else if (typeCard == 16) {
                yBProfile[0].classList.remove('d-none');
                yBContent[0].classList.add('d-none');
                yBarcode[0].classList.remove('d-none');
                boxShowName[0].classList.add('d-none');
                boxQR[0].classList.add('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.add('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.add('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxEducation.hide();
                boxPID.hide();
                boxExpTextDate.show()
                boxPrintDate.show();

                boxExpDate2.show();
                boxReleaseDate2.show();
            }
            else {
                txtColorNickname.innerHTML = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106038") %>";

                yBProfile[0].classList.remove('d-none');
                yBarcode[0].classList.remove('d-none');
                yBContent[0].classList.add('d-none');
                boxShowName[0].classList.remove('d-none');
                boxQR[0].classList.remove('d-none');
                boxcard[0].classList.remove('d-none');
                boxcard[1].classList.add('d-none');
                boxExpDate[0].classList.remove('d-none');
                boxNickName[0].classList.add('d-none');
                boxColorNickName[0].classList.remove('d-none');
                boxPatternContent[0].classList.add('d-none');

                boxBrithday[0].classList.add('d-none');
                boxNumberPhone[0].classList.add('d-none');
                boxEducation.show();
                boxPID.show();
                boxExpTextDate.show()
                boxPrintDate.show();
            }
            var typevalue = $("#ctl00_MainContent_optionCourse").val();// document.getElementsByClassName("typevalue");
            //alert("/studentCard/studentCardEditType.ashx?type=" + typevalue[0].value);
            $.ajax({
                url: "/studentCard/studentCardEditType.ashx?type=" + typevalue,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133179") %></h3>',
                backdrop: true
            });
        }

        function bootbox3(url) {
            bootbox.confirm({
                title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133335") %></h>',
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                    }
                },
                callback: function (result) {
                    if (result == true) {
                        window.location.href = url;
                    }
                }
            });
        }

        function bootbox4(url) {
            bootbox.confirm({
                title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133335") %></h>',
                buttons: {
                    cancel: {
                        label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                    },
                    confirm: {
                        label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %>'
                    }
                },
                callback: function (result) {
                    if (result == true) {
                        nextpage(99999, 0, 0);
                    }
                }
            });
        }

        function ddlclass() {
            var ddl2 = document.getElementsByClassName("ddl2");
            //var load = document.getElementsByClassName("load");
            //load[0].classList.remove('d-none');

            for (i = -1; i <= 90; i++) {
                ddl2[1].remove(0);
            }

            setTimeout(function () {

                $.get("/App_Logic/ddlclassroom.ashx?idlv=" + document.getElementById('<%=ddlsublevel.ClientID%>').value, function (Result) {
                    $.each(Result, function (index) {

                        // Create an Option object       
                        var opt = document.createElement("option");
                        // Assign text and value to Option object
                        opt.text = Result[index].name;
                        opt.value = Result[index].value;

                        //console.log(getUrlParameter("idlv2"));

                        //if (getUrlParameter("idlv2") != "" && Result[index].value == getUrlParameter("idlv2")) {
                        //    opt.selected = true;
                        //}

                        document.getElementById('<%=ddlsublevel2.ClientID%>').options.add(opt);

                    });

                    $('#<%=ddlsublevel2.ClientID%>').selectpicker('refresh');
                    //load[0].classList.add('d-none');
                });
            }, 300);
        }

        function start() {
            var enddate = document.getElementsByClassName("enddate");
            var enddatecheckok = document.getElementsByClassName("enddatecheckok");
            //var enddatecheckerror = document.getElementsByClassName("enddatecheckerror");
            var availableTags = [];

            //$.get("/app_logic/studentlist.ashx", function (Result) {
            //    $.each(Result, function (index) {
            //        availableTags.push(Result[index].FirstandLast);
            //    });
            //});

            //$("#ctl00_MainContent_tags").autocomplete({
            //    source: availableTags
            //});

            var d2 = document.getElementsByClassName("d2");

            ddlclass();

            var full = window.location.href;
            var half = full.split('?');

            if (half[1] != null) {
                setTimeout(function () {
                    document.getElementById('<%=ddlsublevel2.ClientID%>').value = d2[0].value;
                }, 900);
            }
        }
        window.onload = start;


        function changeColor() {
            var optionColorNickName = document.getElementById("ctl00_MainContent_optionColorNickName");
            //console.log("/studentCard/studentCardEditColor.ashx?ColorValue=" + optionColorNickName.value);
            $.ajax({
                url: "/studentCard/studentCardEditColor.ashx?ColorValue=" + optionColorNickName.value
            });
        }


        function showName() {
            var value = $('select[id*=optionShowName]').val();
            $.ajax({
                url: "/studentCard/ashx/funcShowName.ashx?ShowName=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }


        function showNickName() {
            var value = $('select[id*=optionNickName]').val();
            //console.log("/studentCard/funcShowNickName.ashx?ShowNickName=" + value);
            $.ajax({
                url: "/studentCard/funcShowNickName.ashx?ShowNickName=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }


        function fcPatternContent() {
            var value = $('select[id*=PatternContent]').val();
            $.ajax({
                url: "/studentCard/ashx/PatternContent.ashx?PatternContent=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }


        function fcshowBrithday() {
            var value = $('select[id*=optionboxBrithday]').val();
            $.ajax({
                url: "/studentCard/ashx/funcShowBrithday.ashx?Brithday=" + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }


        function shownumberPhone() {
            var value = $('select[id*=optionnumberPhone]').val();
            if (value != undefined) {
                $.ajax({
                    url: "/studentCard/ashx/funcShowNumberPhone.ashx?NumberPhone=" + value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                });
            }
        }

        function updateCardProp() {
            var value = $('select[id*=optionnumberPhone]').val();
            if (value != undefined) {
                $.ajax({
                    url: "/studentCard/ashx/funcShowNumberPhone.ashx?NumberPhone=" + value,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                });
            }
        }

        function getListTrem() {
            var YearID = $('select[id*=ddlyear] option:selected').val();
            $("select[id*=semister] option").remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
                success: function (msg) {
                    $.each(msg, function (index) {
                        $('select[id*=semister]')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTerm)
                                .text(msg[index].sTerm));
                    });

                    $('select[id*=semister]').selectpicker('refresh');
                }
            });
        }

        function changePropValue(prop, value) {
            $.ajax({
                url: "/studentCard/ashx/studentCardProp.ashx?prop=" + prop + '&value=' + value,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        }
    </script>


</asp:Content>


