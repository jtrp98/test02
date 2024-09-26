<%@ Page Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="SDQ_Report.aspx.cs" Inherits="FingerprintPayment.Qusetion.SDQ_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <div class="container-fluid full-card box-content">
        <div>
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4 text-center">
                    <h1><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132990") %> </strong></h1>
                    <br />
                </div>
                <div class="col-sm-4">
                </div>
            </div>
        </div>

        <div class=" row">
            <div class="col-lg-7">
                <table class="table table-bordered">

                    <tbody>
                        <% var _Sum4ItemInt = 0;
                            foreach (var data in showDataStudentandFontQuestions.OrderBy(o => o.Question_Group).Where(w => w.Question_Group <= 4))
                            {
                                _Sum4ItemInt += data.SumPoint ?? 0;
                                %>
                        <tr>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133119") %> <%=data.Question_Group %> </strong></td>
                            <td class="text-center"><strong><%=data.SumPoint %> </strong></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %> </strong></td>
                            <td class="text-center"><strong><%=data.SumString %> </strong></td>
                        </tr>
                        <% } %>

                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>

                        <% foreach (var data in showDataStudentandFontQuestions.Where(w => w.Question_Group == 1))
                            {
                                var _Sum4ItemString = "";
                                if (_Sum4ItemInt <= 15)
                                {
                                    _Sum4ItemString = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                                }
                                else if (_Sum4ItemInt <= 16 && _Sum4ItemInt >= 17)
                                {
                                    _Sum4ItemString = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                                }
                                else
                                {
                                    _Sum4ItemString = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133120") %>";
                                }
                                %>
                        <tr>
                            <td class="text-right"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133121") %></strong></td>
                            <td class="text-center"><strong><%= _Sum4ItemInt %></strong></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %></strong></td>
                            <td class="text-center"><strong><%= _Sum4ItemString %></strong></td>
                        </tr>
                        <% } %>


                        <% foreach (var data in showDataStudentandFontQuestions.OrderBy(o => o.Question_Group).Where(w => w.Question_Group == 5 ))
                            { %>
                        <tr>
                            <td class="text-right"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133122") %> </strong></td>
                            <td class="text-center"><strong><%= data.SumPoint %> </strong></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %> </strong></td>
                            <td class="text-center"><strong><%=data.SumString %> </strong></td>
                        </tr>
                        <% } %>

                        <% foreach (var data in showDataStudentandFontQuestions.OrderBy(o => o.Question_Group).Where(w => w.Question_Group == 6 ))
                            { %>
                        <tr>
                            <td class="text-right"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133123") %> </strong></td>
                            <td class="text-center"><strong><%= data.SumPoint %> </strong></td>
                            <td class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %> </strong></td>
                            <td class="text-center"><strong><%=data.SumString %> </strong></td>
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
                                <asp:Image class="img-rounded" alt="Cinque Terre" id="profileimage" runat="server"/>
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

        <div>
            <div class ="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-4 text-center">
                    <a href="SDQIndex.aspx" class="btn btn-danger"><h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132989") %></h3></a>
                </div>
                <div class="col-sm-4">
                </div>
            </div>
        </div>


    </div>

    <script>

    </script>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
