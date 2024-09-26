<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="grade-quiz.aspx.cs" Inherits="FingerprintPayment.grade_quiz" EnableViewState="true" %>

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
    </style>
    <script type="text/javascript" language="javascript">

        var availableValueplane = [];
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            $('#ctl00_MainContent_ddlsublevel').val(getUrlParameter("idlv"));

            funtionListSubLV3("ctl00_MainContent_ddlsublevel", "ddlSubLV2", getUrlParameter("idlv2"));
            availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");

            $('input[id*=txtSearch]').val(getUrlParameter("sname"));

            $('#ctl00_MainContent_ddlsublevel').change(function () {
                funtionListSubLV3("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });


            $('input[id*=btnSearch]').click(function () {
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlSubLV2] option:selected').val();
                var param3var = $('select[id*=DropDownList1] option:selected').val();
                var param4var = $('select[id*=DropDownList2] option:selected').val();
                window.location.href = "grade-quiz.aspx?idlv=" + param1var + "&idlv2=" + param2var + "&year=" + param3var + "&term=" + param4var;
            });

            $('input[id*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });
        });
        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=0", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }
        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }
        function funtionListSubLV3(controlsublevel, sontrolsublevel2) {
            var SubLVID = $('#' + controlsublevel + ' option:selected').val();
            var sSubLV = $('#' + controlsublevel + ' option:selected').text();
            $("#" + sontrolsublevel2 + " option").remove();
            $('select[id*=' + sontrolsublevel2 + ']')
                            .append($("<option></option>")
                            .attr("value", "")
                            .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>"));
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=' + sontrolsublevel2 + ']')
                            .append($("<option></option>")
                            .attr("value", msg[index].nTermSubLevel2)
                            .text(sSubLV + " / " + msg[index].nTSubLevel2));
                    });

                }
            });
        }

        function funtionListSubLV3(controlsublevel, controlsublevel2, setvalues) {
            var SubLVID = $('#' + controlsublevel + ' option:selected').val();
            var sSubLV = $('#' + controlsublevel + ' option:selected').text();
            $("#" + controlsublevel2 + " option").remove();
            $('select[id*=' + controlsublevel2 + ']')
                            .append($("<option></option>")
                            .attr("value", "")
                            .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106126") %>"));
            var request = $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {

                    $.each(msg, function (index) {
                        $('select[id*=' + controlsublevel2 + ']')
                            .append($("<option></option>")
                            .attr("value", msg[index].nTermSubLevel2)
                            .text(sSubLV + " / " + msg[index].nTSubLevel2));
                    });

                }
            });
            request.done(function () {
                $('#' + controlsublevel2).val(setvalues);
            })
        }
    </script>
    <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
        CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
        ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
        FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
        CompletionListItemCssClass="listItem">
    </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content userlist-container">
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="DropDownList2" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group row student">
            <div class="col-md-6 col-sm-12">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-lg-4 col-md-5 col-sm-4 col-xs-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-lg-8 col-md-7 col-sm-8 col-xs-8 control-input">
                    <select id="ddlSubLV2" class="form-control">
                    </select>
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
                            <asp:BoundField DataField="number" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="8%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sPlanID"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %>">
                                <HeaderStyle Width="9%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="sPlanName"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %>">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="totaltest" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132206") %>" ItemStyle-CssClass="centertext" HeaderStyle-CssClass="centertext">
                                <HeaderStyle Width="10%"></HeaderStyle>
                            </asp:BoundField>                            
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                  <a href="/grade/grade-quiz-register.aspx?plan=<%# Eval("planid")%>&year=<%# Eval("year")%>&term=<%# Eval("term")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&test=<%# Eval("totaltest")%>" class="btn btn-success" 
                                > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132208") %></a>                                                                                                      
                                </ItemTemplate>
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:TemplateField>  

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>                                    
                                  <a href="/grade/grade-quiz-detail.aspx?plan=<%# Eval("planid")%>&year=<%# Eval("year")%>&term=<%# Eval("term")%>&idlv=<%# Eval("idlv")%>&idlv2=<%# Eval("idlv2")%>&test=1" class="btn btn-info"> 
                                 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132207") %></a>                                                                                                                 
                                </ItemTemplate>
                                <HeaderStyle Width="10%"></HeaderStyle>
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
