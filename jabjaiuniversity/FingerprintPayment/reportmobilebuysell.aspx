<%@ Page Title="" Language="C#" MasterPageFile="~/Mmobile.Master" AutoEventWireup="true"
    CodeBehind="reportmobilebuysell.aspx.cs" Inherits="FingerprintPayment.reportmobilebuysell1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <form id="from1" runat="server" class="form-horizontal">
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-sm-5 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                <div class="col-sm-7">
                    <div class="input-group">
                        <asp:TextBox ID="ListBox2" runat="server" class="form-control" /><div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-sm-5 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                <div class="col-sm-7">
                    <div class="input-group">
                        <asp:TextBox ID="ListBox3" runat="server" class="form-control" /><div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-sm-5 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603091") %> :</label>
                <div class="col-sm-7">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                        <asp:ListItem Text="1" Value="1" Selected="True" />
                        <asp:ListItem Text="2" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-sm-5 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601044") %> :</label>
                <div class="col-sm-7">
                    <asp:TextBox ID="TextBox2" runat="server" class="form-control" /></div>
            </div>
        </div>
        <div class="row">
            <input type="submit" class="btn btn-primary col-xs-4 col-xs-push-4" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
        </div>
        <div class="border-bottom">
            <div class="row">
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br />
                    <span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131132") %></span></p>
                <p class="control-label hidden-sm col-md-1 text-sm text-md-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %><br />
                    <span class="text-md">10:05</span></p>
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left text-md-ellipsis"
                    data-toggle="tooltip" title="Lorem Ipsum Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603011") %><br />
                    <span class="text-md">Lorem Ipsum Lorem Ipsum</span></p>
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left text-md-ellipsis"
                    data-toggle="tooltip" title="Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01131") %><br />
                    <span class="text-md">Lorem Ipsum</span></p>
                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-ellipsis" data-toggle="tooltip"
                    title="Lorem Ipsum Lorem Ipsum Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %><br />
                    <span class="text-md">Lorem Ipsum Lorem Ipsum Lorem Ipsum</span></p>
                <p class="control-label col-xs-6 col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %><br />
                    <span class="text-md">1</span></p>
                <p class="control-label hidden-xs col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601080") %><br />
                    <span class="text-md">200</span></p>
                <p class="control-label col-xs-6 col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131102") %><br />
                    <span class="text-md">200</span></p>
            </div>
            <div class="row">
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br />
                    <span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131132") %></span></p>
                <p class="control-label hidden-sm col-md-1 text-sm text-md-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %><br />
                    <span class="text-md">11:05</span></p>
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left text-md-ellipsis"
                    data-toggle="tooltip" title="Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603011") %><br />
                    <span class="text-md">Lorem Ipsum</span></p>
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left text-md-ellipsis"
                    data-toggle="tooltip" title="Lorem Ipsum Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01131") %><br />
                    <span class="text-md">Lorem Ipsum Lorem Ipsum</span></p>
                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-ellipsis" data-toggle="tooltip"
                    title="Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %><br />
                    <span class="text-md">Lorem Ipsum</span></p>
                <p class="control-label col-xs-6 col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %><br />
                    <span class="text-md">1</span></p>
                <p class="control-label hidden-xs col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601080") %><br />
                    <span class="text-md">200</span></p>
                <p class="control-label col-xs-6 col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131102") %><br />
                    <span class="text-md">200</span></p>
            </div>
            <div class="row">
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br />
                    <span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131132") %></span></p>
                <p class="control-label hidden-sm col-md-1 text-sm text-md-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %><br />
                    <span class="text-md">11:05</span></p>
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left text-md-ellipsis"
                    data-toggle="tooltip" title="Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603011") %><br />
                    <span class="text-md">Lorem Ipsum</span></p>
                <p class="control-label hidden-sm col-md-2 text-sm text-md-left text-md-ellipsis"
                    data-toggle="tooltip" title="Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01131") %><br />
                    <span class="text-md">Lorem Ipsum</span></p>
                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-ellipsis" data-toggle="tooltip"
                    title="Lorem Ipsum">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502020") %><br />
                    <span class="text-md">Lorem Ipsum</span></p>
                <p class="control-label col-xs-6 col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %><br />
                    <span class="text-md">2</span></p>
                <p class="control-label hidden-xs col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601080") %><br />
                    <span class="text-md">200</span></p>
                <p class="control-label col-xs-6 col-sm-2 col-md-1 text-sm text-sm-left">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131102") %><br />
                    <span class="text-md">400</span></p>
            </div>
        </div>
        </form>
    </div>
</asp:Content>
