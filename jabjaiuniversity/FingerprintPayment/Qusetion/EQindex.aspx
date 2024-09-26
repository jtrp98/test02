<%@ Page Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="EQindex.aspx.cs" Inherits="FingerprintPayment.Qusetion.EQindex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <!-------------- Modal ----------------->
    <div class="modal fade" id="ModelScoringSDQ" tabindex="-1" role="dialog" aria-labelledby="exampleModalScrollableTitle" aria-hidden="true">


        <div class="modal-dialog modal-dialog-scrollable modal-lg" role="document">
            <div class="modal-content ">
                <div class="modal-header">
                    <h1 class="text-center">
                        <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132999") %>
                            <br />
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133000") %> </strong></h1>
                </div>
                <div class="modal-body">
                    <div class="container-fluid full-card">
                        <div class="row">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center">
                                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132974") %></strong></h2>
                                        </th>
                                        <th class="text-center">
                                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %></strong></h2>
                                        </th>
                                        <th class="text-center">
                                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %></strong></h2>
                                        </th>
                                        <th class="text-center">
                                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %></strong></h2>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133001") %>
                                            <br />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133002") %>
                                        </th>
                                        <td class="text-center">
                                            <br />
                                            <strong>0 - 16</strong></td>
                                        <td class="text-center">
                                            <br />
                                            <strong>17 - 18</strong></td>
                                        <td class="text-center">
                                            <br />
                                            <strong>19 - 20</strong></td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr>
                                        <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133003") %></th>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr>
                                        <th>1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203056") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤติก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>มแต่ละ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306028") %></th>
                                        <td class="text-center"><strong>0 - 5</strong></td>
                                        <td class="text-center"><strong>6</strong></td>
                                        <td class="text-center"><strong>7 - 10</strong></td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr>
                                        <th>2.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803037") %>เกเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>/ความ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ฤติ</th>
                                        <td class="text-center"><strong>0 - 4</strong></td>
                                        <td class="text-center"><strong>5</strong></td>
                                        <td class="text-center"><strong>6 - 10</strong></td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr>
                                        <th>3.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803037") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>อยู่นิ่ง/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02108") %>สั้น</th>
                                        <td class="text-center"><strong>0 - 5</strong></td>
                                        <td class="text-center"><strong>6</strong></td>
                                        <td class="text-center"><strong>7 -10</strong></td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr>
                                        <th>4.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803037") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304053") %></th>
                                        <td class="text-center"><strong>0 - 3</strong></td>
                                        <td class="text-center"><strong>4 </strong></td>
                                        <td class="text-center"><strong>5 - 10</strong></td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr>
                                        <th>5.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803037") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306028") %>สัม<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ันธ์ภา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ทาง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02147") %>
                                            <br />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133009") %>
                                        </th>
                                        <td class="text-center"><strong>4 - 10
                                            <br />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133010") %></strong></td>
                                        <td class="text-center" colspan="2"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133011") %>
                                            <br />
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01385") %></strong></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="text-center " data-dismiss="modal">
                    <div class="btn btn-success text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></div>
                </div>
                <br />
            </div>
        </div>
    </div>


    <div class="full-card box-content userlist-container">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:DropDownList CssClass="form-control"  id="DropDownYears" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' Style="width: 100%;" />
                </div>
            </div>           
        </div>

        <%--<div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:DropDownList CssClass="form-control" id="DropDownClass" runat="server" ></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">  
                    <select class="form-control" id="DropDownRoom"></select>
                </div>
            </div>
        </div>

        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label>55555</label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    
                </div>
            </div>          
            <div class="col-md-6 col-sm-12">
                <div class="col-lg-4 col-md-5 col-sm-3 adjust-col-padding col-space">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                </div>
                <div class="col-lg-8 col-md-7 col-sm-8">
                    <asp:DropDownList CssClass="form-control"  id="DropDownTerm" runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>--%>

        <div class="row form-group">
            <div class="col-sm-12 text-center">
                <asp:Button runat="server" type="button" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" class="btn btn-primary global-btn" ID="btnsearch" />
            </div>
        </div>


        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">

                    <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" ShowFooter="False" GridLines="None" AllowPaging="True" Font-Bold="False"
                        Font-Italic="False" Font-Overline="False" OnDataBound="CustomersGridView_DataBound" Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">

                        <AlternatingRowStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />

                        <PagerStyle ForeColor="#337AB7" BorderColor="#337AB7" BackColor="#337AB7" />

                        <PagerTemplate>

                            <table width="100%" class="tab">

                                <tr>
                                    <td style="width: 25%">
                                        <asp:Label ID="Label1" BorderColor="#337AB7" ForeColor="white" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:" runat="server" />
                                        <asp:DropDownList ID="PageDropDownList2" AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged2" runat="server" />
                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="backbutton" runat="server" CssClass="imjusttext" OnClick="backbutton_Click"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %> </asp:LinkButton>
                                        <asp:DropDownList ID="PageDropDownList" AutoPostBack="true"
                                            OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" runat="server" />
                                        <asp:LinkButton ID="nextbutton" runat="server" CssClass="imjusttext" OnClick="nextbutton_Click"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %> </asp:LinkButton>
                                    </td>
                                    <td style="width: 70%; text-align: right">
                                        <asp:Label ID="CurrentPageLabel" ForeColor="white" runat="server" />
                                    </td>
                                </tr>

                            </table>

                        </PagerTemplate>

                        <Columns>
                            <asp:BoundField DataField="Autonumber" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>">
                                <HeaderStyle Width="10%" CssClass="center" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="studentName" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                                <HeaderStyle Width="15%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="studentLastname" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="15%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderStyle-Width="20%" DataField="studentId" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104022") %>">
                                <HeaderStyle Width="11%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderStyle-Width="200px" DataField="studentStatus" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %>">
                                <HeaderStyle Width="11%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderStyle-Width="200px" DataField="studentClass" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %>">
                                <HeaderStyle Width="11%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderStyle-Width="200px" DataField="professorOfclass" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202006") %>">
                                <HeaderStyle Width="11%" CssClass="centerText"></HeaderStyle>
                            </asp:BoundField>

                            <asp:BoundField HeaderStyle-Width="200px" ItemStyle-CssClass="STDcheckID hidden" DataField="STDcheckID" HeaderText="test">
                                <HeaderStyle Width="0.1%" CssClass="centerText hidden"></HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField>
                                <ItemTemplate>

                                    <a class="btn btn-primary eq_question hidden" href="EQ_Question.aspx?id=<%# Eval("sID") %>" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133012") %>" style="width: 150px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133013") %></a>

                                    <a class="btn btn-danger eq_report hidden" href="EQ_Report.aspx?id=<%# Eval("sID") %>" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133014") %>"
                                        style="width: 150px"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133014") %></a>

                                </ItemTemplate>
                                <HeaderTemplate>
                                    <center>
                                        <%--<div class="btn btn-success" data-toggle="modal" data-target="#ModelScoringSDQ"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133015") %></div>--%>
                                    </center>
                                </HeaderTemplate>
                                <ItemStyle CssClass="center" />
                                <HeaderStyle></HeaderStyle>

                            </asp:TemplateField>
                            <asp:BoundField DataFormatString="sFinger" HeaderText="sFinger" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="sEmp" HeaderText="sEmp" Visible="False" />
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

    <script type="text/javascript">    

        $(document).ready(function () {
            var STDcheckID = document.getElementsByClassName("STDcheckID");
            var eq_question = document.getElementsByClassName("eq_question");
            var eq_report = document.getElementsByClassName("eq_report");

            for (var x = 0; x < STDcheckID.length; x++) {

                if (STDcheckID[x].textContent == "0") {
                    eq_question[x].classList.remove('hidden');
                } else {
                    eq_report[x].classList.remove('hidden');
                }
            }    
        });


    </script>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
