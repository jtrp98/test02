<%@ Page Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="EQ_Report.aspx.cs" Inherits="FingerprintPayment.Qusetion.EQ_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>


    <div class="container-fluid full-card box-content">
        <div>
            <div>
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-center">
                    <h1><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132990") %> </strong></h1>
                    <br />
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div class=" row">
            <div class="col-lg-7">

                <%-------------------------------------<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132994") %>----------------------------------%>

                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th></th>
                            <th class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132991") %></strong></th>
                            <th class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132992") %></strong></th>
                            <th class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306015") %></strong></th>
                        </tr>
                    </thead>
                    <tbody>
                        <% var SumPoint_EQ1 = 0;
                            var index1 = 1;
                            foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ <= 3))
                            {
                                SumPoint_EQ1 += data.SumPoint_EQ ?? 0;%>
                        <tr>
                            <td class="text-center" style="width:50px;"><%=index1++ %></td>
                            <td class="text-left"><p><%=data.Group_EQ %></p></td>
                            <td class="text-center"><p><%=data.SumPoint_EQ %></p></td>
                            <td></td>
                        </tr>
                        <% } %>

                        <% foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ == 1))
                            { 
                                var SumPoint_String_EQ1 = "";
                                if (SumPoint_EQ1 <= 48)
                                {
                                    SumPoint_String_EQ1 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                                }
                                else if (SumPoint_EQ1 <= 48 && SumPoint_EQ1 >= 58)
                                {
                                    SumPoint_String_EQ1 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                                }
                                else
                                {
                                    SumPoint_String_EQ1 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                                }
                                %>
                        <tr>
                            <td></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132993") %> :</strong></td>
                            <td class="text-center"><strong><%= SumPoint_EQ1 %></strong></td>
                            <td class="text-center"><strong><%= SumPoint_String_EQ1 %></strong></td>
                        </tr>
                        <% } %>

        <%-------------------------------------<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132995") %>----------------------------------%>

                        <% var SumPoint_EQ2 = 0;
                            var index2 = 4;
                            foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ >= 4 && w.Question_GroupEQ <= 6))
                            {
                                SumPoint_EQ2 += data.SumPoint_EQ ?? 0;%>
                        <tr>
                            <td class="text-center"> <%=index2++ %> </td>
                            <td class="text-left"><p> <%=data.Group_EQ %></p></td>
                            <td class="text-center"><p><%=data.SumPoint_EQ %></p></td>
                            <td></td>
                        </tr>
                        <% } %>

                        <% foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ == 1))
                            { 
                                var SumPoint_String_EQ2 = "";
                                if (SumPoint_EQ2 <= 45)
                                {
                                    SumPoint_String_EQ2 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                                }
                                else if (SumPoint_EQ2 <= 44 && SumPoint_EQ2 >= 57)
                                {
                                    SumPoint_String_EQ2 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                                }
                                else
                                {
                                    SumPoint_String_EQ2 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                                }
                                %>
                        <tr>
                            <td></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132997") %> :</strong></td>
                            <td class="text-center"><strong><%=SumPoint_EQ2 %></strong></td>
                            <td class="text-center"><strong><%=SumPoint_String_EQ2 %></strong></td>
                        </tr>
                        <% } %>

        <%-------------------------------------<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132996") %>----------------------------------%>

                        <% var SumPoint_EQ3 = 0;
                            var index3 = 7;
                            foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ >= 7 && w.Question_GroupEQ <= 9))
                            { 
                                SumPoint_EQ3 += data.SumPoint_EQ ?? 0;%>
                        <tr>
                            <td class="text-center"> <%=index3++ %></td>
                            <td class="text-left"><p><%=data.Group_EQ %></p></td>
                            <td class="text-center"><p> <%=data.SumPoint_EQ %></p></td>
                            <td></td>
                        </tr>
                        <% } %>

                        <% foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ == 1))
                            {
                                var SumPoint_String_EQ3 = "";
                                if (SumPoint_EQ3 <= 40)
                                {
                                    SumPoint_String_EQ3 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                                }
                                else if (SumPoint_EQ3 <= 40 && SumPoint_EQ3 >= 55)
                                {
                                    SumPoint_String_EQ3 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                                }
                                else
                                {
                                    SumPoint_String_EQ3 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                                }
                                %>
                        <tr>
                            <td></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132982") %> :</strong></td>
                            <td class="text-center"><strong><%=SumPoint_EQ3 %></strong></td>
                            <td class="text-center"><strong><%=SumPoint_String_EQ3 %></strong></td>
                        </tr>
                        <% } %>

        <%-------------------------------------<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M302011") %>EQ----------------------------------%>
                        <% var ResultInt = 0;
                            foreach (var data in showDataStudentEQs.Where(w => w.Question_GroupEQ == 1))
                            {
                                ResultInt = SumPoint_EQ1 + SumPoint_EQ2 + SumPoint_EQ3;
                                var ResultString = "";
                                if (ResultInt < 140)
                                {
                                    ResultString = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132986") %>";
                                }
                                else if (ResultInt >= 140 && ResultInt <= 170)
                                {
                                    ResultString = "EQ อยู่ในเกณปกติ";
                                }
                                else
                                {
                                    ResultString = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132987") %>";
                                }
                                %>
                        <tr>
                            <td></td>
                            <td class="text-right"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132998") %> :</strong></td>
                            <td class="text-center"><strong><%=ResultInt %></strong></td>
                            <td class="text-center"><strong><%=ResultString %></strong></td>
                        </tr>

                        <% } %>
                    </tbody>
                </table>
            </div>

            <div class="col-lg-1">
            </div>

            <div class="col-lg-4">
                <table class="table table-bordered">
                    <tbody>
                        <tr>
                            <td class="text-center">
                                <asp:Image class="img-rounded" alt="Cinque Terre" ID="profileimage" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input class="form-control text-center" runat="server" id="sStudentID" readonly="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input class="form-control text-center" runat="server" id="studentName" readonly="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input class="form-control text-center" runat="server" id="studentClass" readonly="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class=" text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202006") %></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input class="form-control text-center" runat="server" id="professorOfclass" readonly="True" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="row full-card">
            <div class="col-lg-4">
                    
            </div>
            <div class="col-lg-4 text-center">
                <a href ="EQindex.aspx" class="btn btn-danger"><h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132989") %></h3></a>
            </div>
            <div class="col-lg-4 text-right">
                
            </div>
        </div>


    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
