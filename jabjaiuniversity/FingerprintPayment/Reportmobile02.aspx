<%@ Page Title="" Language="C#" MasterPageFile="~/Mmobile.Master" AutoEventWireup="true" CodeBehind="Reportmobile02.aspx.cs" Inherits="FingerprintPayment.Reportmobile02" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container" >
    <form id="from1" runat="server" class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group col-sm-6">
                    <label class="col-sm-3 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="year" runat="server" class="form-control">
                            <asp:ListItem Text="2557" Value="2557" Selected=True />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-sm-6">
                    <label class="col-sm-3 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="semister" runat="server" class="form-control">
                            <asp:ListItem Text="1" Value="1" Selected=True />
                            <asp:ListItem Text="2" Value="2" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group col-sm-6">
                    <label class="col-sm-3 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :</label>
                    <div class="col-sm-9">
                    <div class="input-group"><asp:TextBox ID="ListBox2" runat="server" class="form-control" /><div class="input-group-addon"><i class="fa fa-calendar"></i></div></div></div>
                </div>
                <div class="form-group col-sm-6">
                    <label class="col-sm-3 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> :</label>
                    <div class="col-sm-9">
                    <div class="input-group"><asp:TextBox ID="ListBox3" runat="server" class="form-control" /><div class="input-group-addon"><i class="fa fa-calendar"></i></div></div></div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group col-sm-6">
                    <label class="col-sm-3 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %> :</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                            <asp:ListItem Text="1" Value="1" Selected=True />
                            <asp:ListItem Text="2" Value="2" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-sm-6">
                    <label class="col-sm-3 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="status" runat="server" class="form-control">
                            <asp:ListItem Text="1" Value="1" Selected=True />
                            <asp:ListItem Text="2" Value="2" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <button class="btn btn-primary col-sm-12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
            </div>
           
            <div class="col-sm-12">
             <br />
                <fieldset>
                    <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406001") %></legend>
                    <div class="form-group col-sm-4">
                        <p class="text-center text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %><br /><span class="text-large">10</span></p>
                    </div>
                    <div class="form-group col-sm-4">
                        <p class="text-center text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %><br /><span class="text-large">2</span></p>
                    </div>
                    <div class="form-group col-sm-4">
                        <p class="text-center text-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %><br /><span class="text-large">1</span></p>
                    </div>
                </fieldset>
            </div>

            <div class="col-sm-12 border-bottom">
                 <br />
                <fieldset>
                    <legend><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></legend>

                    <script type="text/javascript">
                        function toggleDetail(id) {
                            $(id).toggleClass("open");
                        }
                    </script>

                    <div class="col-sm-3 col-md-12">        
                        <p class="control-label col-md-2 text-sm text-md-right" onclick="toggleDetail('#031258')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131129") %></span></p>         
                        <div class="detail-box" id="031258">
                            <button type="button" class="close hidden-md" onclick="toggleDetail('#031258')"><span aria-hidden="true">×</span></button>
                            <div class="content">
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></span></p> 
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>    
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                            </div>
                        </div> 
                    </div>

                    <div class="col-sm-3 col-md-12"> 

                        <p class="control-label col-md-2 text-sm text-md-right" onclick="toggleDetail('#041258')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131130") %></span></p>         
                        <div class="detail-box" id="041258">
                            <button type="button" class="close hidden-md" onclick="toggleDetail('#041258')"><span aria-hidden="true">×</span></button>
                            <div class="content">
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></span></p> 
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>    
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm" text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                            </div>
                        </div> 
                        </div>


                        <div class="col-sm-3 col-md-12"> 

                         <p class="control-label col-md-2 text-sm text-md-right" onclick="toggleDetail('#051258')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131131") %></span></p>         
                        <div class="detail-box" id="051258">
                            <button type="button" class="close hidden-md" onclick="toggleDetail('#051258')"><span aria-hidden="true">×</span></button>
                            <div class="content">
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></span></p> 
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>    
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                            </div>
                        </div> 
                        </div>


                        <div class="col-sm-3 col-md-12"> 

                        <p class="control-label col-md-2 text-sm text-md-right" onclick="toggleDetail('#061258')"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131132") %></span></p>         
                        <div class="detail-box" id="061258">
                            <button type="button" class="close hidden-md" onclick="toggleDetail('#061258')"><span aria-hidden="true">×</span></button>
                            <div class="content">
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201011") %></span></p> 
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>    
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                                <p class="control-label col-md-2 text-sm"></p>
                                <p class="control-label col-md-2 text-sm text-md-right text-ellipsis" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %></span></p> 
                                 <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-md">08:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-warning"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></span></p>
                                <p class="control-label col-sm-6 col-md-1 text-sm text-sm-right text-md-right text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-md">16:09</span></p>
                                <p class="control-label col-sm-6 col-md-2 text-sm text-sm-left text-md-left text-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %><br /><span class="text-md"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></span></p>
                                <p class="control-label col-md-2 text-sm text-sm-right text-ellipsis" data-toggle="tooltip" title="-"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-md">-</span></p>
                            </div>
                        </div> 
                    </div>

                    <!-- Old design -->

                     <!--<div class="col-sm-12">              
                        <p class="control-label col-sm-4 col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-large"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131133") %></span></p>      
                        <p class="control-label col-sm-4 col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br /><span class="text-large">2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210021") %> 58</span></p>       
                        <p class="control-label col-sm-4 col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-large"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105016") %></span></p>
                        <p class="control-label col-sm-4 col-sm-push-8 col-md-3 col-md-push-none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-large"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105016") %></span></p>
                        <p class="control-label col-sm-4 col-md-6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %><br /><span class="text-large"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></span></p>
                    </div>
          
                    
                    <div class="col-sm-12">            
                        <p class="control-label col-sm-4 col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %><br /><span class="text-large"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201017") %></span></p>       
                        <p class="control-label col-sm-4 col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %><br /><span class="text-large"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131129") %></span></p>          
                        <p class="control-label col-sm-4 col-md-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803013") %><br /><span class="text-large">ไม่ได้แสกน</span></p>
                        <p class="control-label col-sm-4 col-sm-push-8 col-md-3 col-md-push-none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803016") %><br /><span class="text-large">ไม่ได้แสกน</span></p>
                    </div>-->

                </fieldset>
            </div>
           
        </div>
    </form>
</div>
</asp:Content>
