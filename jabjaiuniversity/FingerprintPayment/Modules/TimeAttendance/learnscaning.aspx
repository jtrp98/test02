<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="learnscaning.aspx.cs" 
Inherits="FingerprintPayment.Modules.TimeAttendance.learnscaning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script language="javascript">

        shortcut.add("Ctrl+N", function () {
            $('input[id*=btnSavepage1]').click();
        });

        function j_infosell(msg) {
         
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
                $("input[id*=txtsBarCode]").focus();
            });
        }

         
        $(document).ready(function () {
            fnOpenDevice();
        });


        function hideResult() {
            $("#scandiv").show();
            $("#resultScan").hide();
        }

        function hideScan(name, statusScan) {
            $("#scandiv").hide();
            $("#resultScan").show();
            $("#resultName").val(name);
            if (statusScan == "0") {
                $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132599") %>");
                $("#statusScan").css({ "color": "#00796B" });
            }
            else if (statusScan == "-1") {
                $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132548") %>");
                $("#statusScan").css({ "color": "#FB8C00" });
            }
            else if (statusScan == "2") {
                $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>");
                $("#statusScan").css({ "color": "#EF5350" });
            }
            else if (statusScan == "3") {
                $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132549") %>");
                $("#statusScan").css({ "color": "#000000" });
            }
            else if (statusScan == "4") {
                $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132550") %>");
                $("#statusScan").css({ "color": "#00796B" });
            }
            else {
                $("#statusScan").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131136") %>");
                $("#statusScan").css({ "color": "#EF5350" });
            }
            setTimeout(function () { hideResult(); }, 1800);
        }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
 <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
           <div class="detail-card box-content">
                <asp:Literal ID="ltrjavascript" runat="server" />

                <div id="scandiv">
                <div class="row" style="display: none;">
                    <div class="col-xs-4">
                        <label class="pull-right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
                    </div>
                    <div class="col-xs-8">
                        <asp:Button ID="btnRegister" runat="server" OnClientClick="fnCapture(); return false;"
                            class="btn btn-info" Text="ตรวจสอบรายนิ้วมือ(Ctrl+P)" />
                        <span id="div" style="clear: both; width: 100px;"></span>&nbsp;
                        <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" Text="1" />
                        <asp:TextBox ID="txtUserFinger" runat="server" Style="display: none;" Text="1" />
                        <asp:RequiredFieldValidator ID="revtxtCheckFinger" runat="server" Display="None"
                            SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %><br>"
                            ControlToValidate="txtCheckFinger" ValidationGroup="add" />
                        <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtCheckFinger" TargetControlID="revtxtCheckFinger"
                            HighlightCssClass="validatorcallouthighlight" Width="200px" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4">
                        <label class="pull-right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                    </div>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" class="input--mid" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4">
                        <label class="pull-right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
                    </div>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" class="input--mid" />
                    </div>
                </div>

                 <div class="row">
                    <div class="col-xs-4">
                        <label class="pull-right">
                            IP </label>
                    </div>
                    <div class="col-xs-8">
                        <asp:HiddenField ID="roomvalue" runat="server" />
                        <asp:Label ID="txtRoom" runat="server" ReadOnly="True" class="input--mid" />
                    </div>
                </div>

                  <div class="row">
                    <div class="col-xs-4">
                        <label class="pull-right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101314") %></label>
                    </div>
                    <div class="col-xs-8">
                        <asp:Label ID="txtRoomName" runat="server" ReadOnly="True" class="input--mid" />
                    </div>
                </div>

                <div class="row mini--space__top">
                    <div class="col-xs-12 center">
                        <asp:Button ID="btnSavepage1" runat="server" AccessKey="N" OnKeyPress="return fales;"
                            TabIndex="100" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>(Ctrl+N)" UseSubmitBehavior="False" class="btn btn-primary" />
                    </div>
                </div>
            </div>
              <div id="resultScan" style="display:none;">
               <div class="row">
                    <div class="col-xs-4">
                        <label class="pull-right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
                    </div>
                    <div class="col-xs-8">
                        <asp:TextBox ID="resultName" runat="server" MaxLength="512" ReadOnly="True" class="input--mid" ClientIDMode="Static" disabled />
                    </div>
                </div>
                <hr/>
                <div class="row">
                    <div class="col-xs-12" style="text-align:center">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303012") %></h1>
                    <h2><label  id="statusScan">-</label></h2>
                    </div>
                </div>

              </div>
            </div>
     
              </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
