<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FingerprintPayment.TuitionFee.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var student_list = [];
        var row_index;
        $(function () {
            $('#ctl00_MainContent_ddlSubLV').change(function () {
                funtionListSubLV2("ctl00_MainContent_ddlSubLV", "ddlSubLV2");
            });

            $("#submit").click(function () {
                row_index = 0;
                update_contact(student_list[row_index++]);
                //PageMethods.getContactId(parseInt($("#ddlSubLV2").val()),
                //    function (response) {
                //        console.log(response)
                //    },
                //    function (response) {
                //        console.log(response)
                //    })
            });



            $("#ddlSubLV2").change(function () {
                $("#table_student tbody").html("");
                PageMethods.getStudent(parseInt($("#ddlSubLV2").val()),
                    function (response) {
                        //console.log(response)
                        student_list = $.parseJSON(response);
                        $.each(student_list, function (index, data) {
                            $("#table_student tbody").append("<tr student_id='" + data.student_id + "'><td>" + (index + 1) + "<td>" + data.student_name + "<td>" + data.contact_id)
                        });
                    },
                    function (response) {
                        console.log(response)
                    })
            });

        })

        function update_contact(student_data) {
            console.log(student_data)
            var row_data = $("tr[student_id=" + student_data.student_id + "] td");
            $(row_data[2]).html("send data");
            PageMethods.getStudent_ContactId(student_data.student_id,
                function (response) {
                    $(row_data[2]).html(response);
                    if (row_index != student_list.length) {
                        update_contact(student_list[row_index++])
                    }
                    console.log(response)
                },
                function (response) {
                    console.log(response)
                });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="full-card box-content userlist-container">
        <div class="form-group row student">
            <div class="col-md-6 col-xs-12">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <asp:DropDownList ID="ddlSubLV" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103078") %>" Value="-1" class="grey hidden"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-class">
                <label class="col-xs-12 col-md-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-xs-12 col-md-8 control-input">
                    <select id="ddlSubLV2" class="form-control input--short">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                    </select>
                    <%--   <asp:DropDownList ID="ddlSubLV2" runat="server" class="input--short" CssClass="form-control">
                        <asp:ListItem Value="" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"></asp:ListItem>
                    </asp:DropDownList>--%>
                </div>
            </div>
        </div>
        <div class="form-group row student">
            <div class="col-md-4 col-xs-12">
            </div>
            <div class="col-md-4 col-xs-12">
                <div id="submit" class="btn btn-success">Send Contacts</div>
            </div>
            <div class="col-md-4 col-xs-12">
            </div>
        </div>
        <div class="form-group row student">
            <div class="col-md-12 col-xs-12">
                <script id="menu_template" type="x-tmpl-mustache">
                </script>
                <table id="table_student" class="table table-hover">
                    <thead>
                        <tr class="default">
                            <td style="width: 10%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603026") %> </td>
                            <td style="width: 30%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></td>
                            <td>Contact ID</td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
