<%@ Page Title="" Language="C#" MasterPageFile="~/Material.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" EnableEventValidation="false" Inherits="FingerprintPayment.Exam.Manage" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="CPH_HEADER" runat="server">
    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203002") %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH_CSS" runat="server">
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.22/css/jquery.dataTables.css">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CPH_JS" runat="server">
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <%--   <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.22/js/dataTables.bootstrap.min.js"></script>--%>

    <script>
        function onSelectedYear() {
            $.get("<%=Page.ResolveUrl("~/api/exam/GetTermByYear")%>?year=" + $("#ddlYear").val(), function (data) {

                $("#ddlTerm").empty();

                $("<option></option>", {
                    value: "",
                    text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"
                }).appendTo("#ddlTerm");

                $.each(data, function (index, item) {

                    $("<option></option>", {
                        value: item.value,
                        text: item.text
                    }).appendTo("#ddlTerm");
                });

                $("#ddlTerm").selectpicker("refresh");
            });
        }

        function onSelectedCourse() {

            $.get("<%=Page.ResolveUrl("~/api/exam/GetSubjectByCoruseType")%>?type=" + $("#ddlCourse").val(), function (data) {

                $("#ddlSubject").empty();

                $("<option></option>", {
                    value: "",
                    text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"
                }).appendTo("#ddlSubject");

                $.each(data, function (index, item) {

                    $("<option></option>", {
                        value: item.value,
                        text: item.text
                    }).appendTo("#ddlSubject");
                });

                $("#ddlSubject").selectpicker("refresh");
            });

        }

        function onSelectedLevel1() {
            $.get("/App_Logic/ddlclassroom.ashx?idlv=" + $("#ddlLevel1").val(), function (data) {

                $("#ddlLevel2").empty();

                $("<option></option>", {
                    value: "",
                    text: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"
                }).appendTo("#ddlLevel2");

                $.each(data, function (index, item) {

                    $("<option></option>", {
                        value: item.value,
                        text: item.name
                    }).appendTo("#ddlLevel2");
                });

                $("#ddlLevel2").selectpicker("refresh");
            });
        }


        $(function () {

            $('#lst-data').DataTable({
                //data: dataSet,
                searching: false,
                paging: false,
                info: false,
                columns: [
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %>" },
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %>" },
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %>" },
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %>" },
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203011") %>" },
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203012") %>" },
                    { title: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203013") %>" },
                    { title: "" }
                ]
            });

            onSelectedYear();
        });
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

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
                    <div class="row">
                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <asp:DropDownList ID="ddlYear" ClientIDMode="Static" onchange="onSelectedYear()" runat="server" data-style="select-with-transition" CssClass="selectpicker" data-size="7" DataValueField="Value" DataTextField="Text">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <select id="ddlTerm" class="selectpicker" data-style="select-with-transition" data-size="7">
                                            <option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                        </select>
                                      <%--  <asp:DropDownList ID="ddlTerm" ClientIDMode="Static" runat="server" data-style="select-with-transition" CssClass="selectpicker" data-size="7" DataValueField="Value" DataTextField="Text">
                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                        </asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121095") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <asp:DropDownList ID="ddlCourse" ClientIDMode="Static" onchange="onSelectedCourse()" runat="server" data-style="select-with-transition" CssClass="selectpicker" data-size="7" DataValueField="Value" DataTextField="Text">
                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <select id="ddlSubject" class="selectpicker" data-style="select-with-transition" data-size="7">
                                            <option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <asp:DropDownList ID="ddlLevel1" ClientIDMode="Static" onchange="onSelectedLevel1()" runat="server" data-style="select-with-transition" CssClass="selectpicker" data-size="7" DataValueField="Value" DataTextField="Text">
                                            <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value="" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <select id="ddlLevel2" class="selectpicker" data-style="select-with-transition" data-size="7">
                                            <option><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203013") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <input type="text" class="form-control" placeholder="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <label class="col-md-4 col-form-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203117") %></label>
                                <div class="col-md-8">
                                    <div class="form-group has-default bmd-form-group">
                                        <select id="ddlExamType" class="selectpicker" data-style="select-with-transition" data-size="7">
                                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106099") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106100") %></option>
                                            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203005") %></option>
                                            <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203006") %></option>
                                            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00970") %></option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4"></div>

                        <div class="col-md-12 text-center">
                            <br />
                            <button type="submit" class="btn btn-fill btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="card ">
                <div class="card-header card-header-success card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">text_snippet</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132127") %></h4>
                </div>
                <div class="card-body ">
                    <div class="row">
                        <div class="col-md-12">
                            <table id="lst-data" class="display" width="100%"></table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

