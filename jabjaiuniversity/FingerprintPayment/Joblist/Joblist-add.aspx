<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true" CodeBehind="Joblist-add.aspx.cs" Inherits="FingerprintPayment.Joblist_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
        .leftText {
            text-align: left;
        }
        .rightText {
            text-align: right;
        }
    </style>
    <div class="full-card text-center planeadd-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hfdsClassID" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <div class="row" id="row-name">
                    <div class="col-xs-12">
                        <div class="col-xs-5 rightText">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                        </div>
                        <div class="col-xs-5 leftText" >
                            <asp:TextBox ID="txt" runat="server"/>
                        </div>
                    </div>
                </div>

                <div class="row" id="row-name">
                    <div class="col-xs-12">
                        <div class="col-xs-5 rightText">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %></label>
                        </div>
                        <div class="col-xs-5 leftText" >
                            <asp:RadioButtonList ID="UserType" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %>&nbsp;" Selected="True" Value="1" />
                                                                <asp:ListItem Text="&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %>" Value="2" />
                                                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                          
                <div class="row text-center planadd-row">
                    <div class="col-xs-12 button-segment">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
                        <asp:Button CssClass="btn btn-danger global-btn" ID="btnCancle" runat="server"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                    </div>
                </div>
                <div class="row--space">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox runat="server" ID="txtListtime" Style="display: none;" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>

